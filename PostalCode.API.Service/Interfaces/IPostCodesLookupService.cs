using PostalCode.API.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PostalCode.API.Service.Interfaces
{
    public interface IPostCodesLookupService
    {
        Task<PostcodeResult> LookupPostcode(string postcode);
        Task<IEnumerable<SearchResult>> AutocompletePostcode(string partialPostcode);
    }
}
