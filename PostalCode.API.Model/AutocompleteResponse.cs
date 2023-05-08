using System;
using System.Collections.Generic;

namespace PostalCode.API.Model
{

    public class AutocompleteResponse
    {
        public int Status { get; set; }
        public IEnumerable<string> Result { get; set; } 
    }
}
