using System;

namespace PostalCode.API.Model
{
    
    public class SearchResult
    {
        public string Area { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string AdminDistrict { get; set; }
        public string Postcode { get; set; }
        public string ParliamentaryConstituency { get; set; }
    }
}
