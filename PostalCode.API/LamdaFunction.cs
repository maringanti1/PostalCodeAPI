using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PostalCode.API
{
    public class LamdaFunction:  Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        { 
        builder.UseContentRoot(Directory.GetCurrentDirectory()).UseStartup<Startup>().UseLambdaServer();
        }
    }
}
