using Microsoft.Extensions.Configuration;
using PostalCode.API.Model;
using PostalCode.API.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PostalCode.API.Service.Mapper
{
    public class SearchResultMapper
    { 
        
        private readonly PostCodeAPIConfig _postCodeAPIConfig;
        public SearchResultMapper(PostCodeAPIConfig postCodeAPIConfig)
        {
            _postCodeAPIConfig = postCodeAPIConfig;
        }

        public SearchResult Map(PostcodeResponse postcodeResponse)
        {
            var searchResult = new SearchResult();
            searchResult.Country = postcodeResponse.Result.Country;
            searchResult.Region = postcodeResponse.Result.Region;
            searchResult.ParliamentaryConstituency = postcodeResponse.Result.Parliamentary_constituency;
            searchResult.AdminDistrict = postcodeResponse.Result.Admin_district;
            searchResult.Postcode = postcodeResponse.Result.Postcode; 

            double latitude;
            if (double.TryParse(_postCodeAPIConfig.LatitudeSouth, out double south) &&
                double.TryParse(_postCodeAPIConfig.LatitudeMidlands, out double midlands))
            {
                if (double.TryParse(postcodeResponse.Result.Latitude.ToString(), out latitude))
                {
                    switch (latitude)
                    {
                        case double l when l < south:
                            searchResult.Area = "South";
                            break;
                        case double l when l >= south && l< midlands:
                            searchResult.Area = "Midlands";
                            break;
                        default:
                            searchResult.Area = "North";
                            break;
                    }
                }
            }

            return searchResult;
        }
    }
}
