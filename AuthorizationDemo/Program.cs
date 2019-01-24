using Microsoft.IdentityModel.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationDemo
{
    public static class Program
    {

        //https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization
        public static async Task Main(string[] args)
        {
            IdentityModelEventSource.ShowPII = true;

            //var authorizer = new Auth0Authorizer();
            var authorizer = new AwsAlbAuthorizer();
            //var authorizer = new EcdsaJwtAuthorizer();

            await authorizer.Authorize();

            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}
