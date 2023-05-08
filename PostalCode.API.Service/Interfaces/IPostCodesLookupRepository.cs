using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PostalCode.API.Service.Interfaces
{
    public interface IPostCodesLookupRepository
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}
