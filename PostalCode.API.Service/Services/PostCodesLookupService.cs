using PostalCode.API.Model;
using PostalCode.API.Service.Common;
using PostalCode.API.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PostalCode.API.Service.Mapper;

namespace PostalCode.API.Service
{
    public class PostCodesLookupService : IPostCodesLookupService
    {
        private readonly IPostCodesLookupRepository _repository; 
        private readonly SearchResultMapper _searchResultMapper;
        private readonly PostCodeAPIConfig _postCodeAPIConfig;
        public PostCodesLookupService(IPostCodesLookupRepository repository, 
            IConfiguration configuration, SearchResultMapper searchResultMapper,
            PostCodeAPIConfig postCodeAPIConfig)
        {
            _repository = repository; 
            _searchResultMapper = searchResultMapper;
            _postCodeAPIConfig = postCodeAPIConfig;
        }

        public async Task<PostcodeResult> LookupPostcode(string postcode)
        {
            var lookupEndpoint = string.Format(_postCodeAPIConfig.LookupEndpoint, postcode);
            var limit = _postCodeAPIConfig.PostCodeAPILimit;
            var response = await _repository.GetAsync($"{_postCodeAPIConfig.BaseURL}{lookupEndpoint}?limit={limit}");

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                // Deserialize into PostcodeResult using camelCase naming policy
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                PostcodeResponse postcodeResponse = JsonSerializer.Deserialize<PostcodeResponse>(jsonContent, options);
                return postcodeResponse.Result;
            }

            throw new CustomException((int)response.StatusCode, response.ReasonPhrase);
        }

        
        public async Task<IEnumerable<SearchResult>> AutocompletePostcode(string partialPostcode)
        {
            List<SearchResult> searchResults = new List<SearchResult>();
           
            var AutoCompleteEndpoint = string.Format(_postCodeAPIConfig.AutoCompleteEndpoint, partialPostcode);
            var limit = _postCodeAPIConfig.PostCodeAPILimit; 
            var response = await _repository.GetAsync($"{_postCodeAPIConfig.BaseURL}{AutoCompleteEndpoint}?limit={limit}"); 
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                // Deserialize into PostcodeResult using camelCase naming policy
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                AutocompleteResponse autocompleteResponse = JsonSerializer.Deserialize<AutocompleteResponse>(result, options);
                if (autocompleteResponse.Result != null)
                {
                    foreach (var postcode in autocompleteResponse.Result)
                    {
                        var lookupEndpoint = string.Format(_postCodeAPIConfig.LookupEndpoint, postcode);
                        var data = await _repository.GetAsync($"{_postCodeAPIConfig.BaseURL}{lookupEndpoint}?limit={limit}");                        
                        if (data.IsSuccessStatusCode)
                        {
                            var jsonContent = await data.Content.ReadAsStringAsync();
                            PostcodeResponse postcodeResponse = JsonSerializer.Deserialize<PostcodeResponse>(jsonContent, options);
                            searchResults.Add(_searchResultMapper.Map(postcodeResponse));
                        }
                    }
                }
                return searchResults;
            }
            throw new CustomException((int)response.StatusCode, response.ReasonPhrase);
        }
    }

}
