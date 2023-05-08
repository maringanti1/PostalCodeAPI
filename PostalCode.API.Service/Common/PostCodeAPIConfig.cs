using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PostalCode.API.Service.Common
{ 
    public class PostCodeAPIConfig
    {
        public string BaseURL { get; set; }
        public string LookupEndpoint { get; set; }
        public string AutoCompleteEndpoint { get; set; }
        public string PostCodeAPILimit { get; set; }
        public string LatitudeSouth { get; set; }
        public string LatitudeMidlands { get; set; }


        public PostCodeAPIConfig(IConfiguration configuration)
        {
            BaseURL = configuration.GetSection("PostCodeAPIConfig:BaseURL").Value;
            LookupEndpoint = configuration.GetSection("PostCodeAPIConfig:LookupEndpoint").Value;
            AutoCompleteEndpoint = configuration.GetSection("PostCodeAPIConfig:AutoCompleteEndpoint").Value;
            PostCodeAPILimit = configuration.GetSection("PostCodeAPIConfig:PostCodeAPILimit").Value;
            LatitudeSouth = configuration.GetSection("PostCodeAPIConfig:LatitudeSouth").Value;
            LatitudeMidlands = configuration.GetSection("PostCodeAPIConfig:LatitudeMidlands").Value;
        }
    }

}
