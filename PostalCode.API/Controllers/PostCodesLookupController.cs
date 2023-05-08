using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks; 
using System.Reflection.Emit;
using System.Net.Http.Json;
using PostalCode.API.Model;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using System.Text.Json.Nodes;
using PostalCode.API.Common;
using PostalCode.API.Service;
using PostcodeLookup.API.Interfaces;
using PostalCode.API.Service.Interfaces;

namespace PostcodeLookup.API.Controllers
{ 

    [EnableCors("AllowOrigin")]
    [ApiController]
    public class PostCodesLookupController : IPostcodesLookupController
    { 
        private readonly IPostCodesLookupService _service;

        public PostCodesLookupController(IPostCodesLookupService service)
        {
            _service = service;
        }

        [HttpGet("autocomplete/{partialPostcode}")]
        public async Task<ActionResult<IEnumerable<SearchResult>>> AutocompletePostcode(string partialPostcode)
        {
            try
            {
                var result = await _service.AutocompletePostcode(partialPostcode);
                return result.ToList();
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.StatusCode, ex.Message);
            }
        }
        [HttpGet("LookupPostcode/{postcode}")]
        public async Task<ActionResult<PostcodeResult>> LookupPostcodeAsync(string postcode)
        {
            try
            {
                var result = await _service.LookupPostcode(postcode);
                return result;
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.StatusCode, ex.Message); ;
            }
        } 


    }
}
