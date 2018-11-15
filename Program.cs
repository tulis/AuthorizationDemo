using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationDemo
{
    public class Program
    {

        //https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization
        public static async Task Main(string[] args)
        {
            const string auth0Domain = @"https://cognito-idp.ap-southeast-2.amazonaws.com/ap-southeast-2_liBGTVsZg"; // Your Auth0 domain
            const string auth0Audience = "54mt5ov8rl5i0c6nsneh5jjnio"; // Your API Identifier
            // Obtain a JWT to validate and put it in here
            const string testToken = @"eyJraWQiOiJiOXU2ZTVzeFUxOGFjUGlsejdwQmNKV1dxWXdESHM0OEsrWXN4QldLS3RZPSIsImFsZyI6IlJTMjU2In0.eyJhdF9oYXNoIjoiVkdUV05KSjdnUzNoS24zdzZKZFJuQSIsInN1YiI6IjVlNmNiMjJlLWU4OTMtNDU2Ni04YjQwLThmOGZjODdjODEwZSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJiaXJ0aGRhdGUiOiIwMVwvMDFcLzIwMDAiLCJpc3MiOiJodHRwczpcL1wvY29nbml0by1pZHAuYXAtc291dGhlYXN0LTIuYW1hem9uYXdzLmNvbVwvYXAtc291dGhlYXN0LTJfbGlCR1RWc1pnIiwiY29nbml0bzp1c2VybmFtZSI6IjVlNmNiMjJlLWU4OTMtNDU2Ni04YjQwLThmOGZjODdjODEwZSIsImF1ZCI6IjU0bXQ1b3Y4cmw1aTBjNm5zbmVoNWpqbmlvIiwiZXZlbnRfaWQiOiI2NTk1OGNiOC1lOGUzLTExZTgtYWY4OS03Nzg1ZDg3MTE2NGIiLCJ0b2tlbl91c2UiOiJpZCIsImF1dGhfdGltZSI6MTU0MjI5MjM5OSwiZXhwIjoxNTQyMjk1OTk5LCJpYXQiOjE1NDIyOTIzOTksImVtYWlsIjoiajIwMzQwMDFAbnd5dGcubmV0In0.TtRoe0KuNRXgXAHYr6YL1pj8BHthebInuQGI2v8R2rQeEVYYCHeb6XfZLY56ZWpkuY-qLQzmuexDn-i8qX_3M01j1iZJN1AY52xclOql9wVuZNelTjt8SaEPR2vn3bVclUDbyTckygk2J9_GPsIyef0sdV32C5TRlaTaREKsQKGDd58oQtm8VlHwXDpyxLw7FkNfMckO3HMGTByZ7Vwt9Jm2RptYAI7fGmg-s3lo9KH3wdZOl2e5jmzrL6J8mlh5_8m-wlYyLWuE5wRvlvkp5SfEG81eUS0V4mzGxVMulhxLWnRRtJTmioIpS5ZqKL5gtgDCn6PdjSrjCJ6bY1svbA&access_token=eyJraWQiOiJlcnA5SGh1QkxaME1hdXJIcWN2a0NHZk00dGlVMFNMNWtxT3ZIM2Rob3AwPSIsImFsZyI6IlJTMjU2In0.eyJzdWIiOiI1ZTZjYjIyZS1lODkzLTQ1NjYtOGI0MC04ZjhmYzg3YzgxMGUiLCJldmVudF9pZCI6IjY1OTU4Y2I4LWU4ZTMtMTFlOC1hZjg5LTc3ODVkODcxMTY0YiIsInRva2VuX3VzZSI6ImFjY2VzcyIsInNjb3BlIjoib3BlbmlkIiwiYXV0aF90aW1lIjoxNTQyMjkyMzk5LCJpc3MiOiJodHRwczpcL1wvY29nbml0by1pZHAuYXAtc291dGhlYXN0LTIuYW1hem9uYXdzLmNvbVwvYXAtc291dGhlYXN0LTJfbGlCR1RWc1pnIiwiZXhwIjoxNTQyMjk1OTk5LCJpYXQiOjE1NDIyOTIzOTksInZlcnNpb24iOjIsImp0aSI6Ijg4NDhkMmM5LTk3Y2YtNDFiMC04NGI4LTFkMTE1MjdjYjE0NSIsImNsaWVudF9pZCI6IjU0bXQ1b3Y4cmw1aTBjNm5zbmVoNWpqbmlvIiwidXNlcm5hbWUiOiI1ZTZjYjIyZS1lODkzLTQ1NjYtOGI0MC04ZjhmYzg3YzgxMGUifQ.chxl5maQNBNbFGS9xjBx2AqZjxD_NsHRQMOcymgAjC0ud9z2Jeo9etNfaFuPv6XdoqsCbVEytdwce8l04aRddeoee_7XcYEHPr8RgyxRIPfxd_7TEjKfE--o1b07AhbfEEJHkeQ9KdUB8Oc72Rwl5q6AYkHs7_fYJH2nYuJFRVW8HhYfN94bNg7_GrTkhJg7CGBDLj5xc1HfrbrBQkPy_eMfdg9e8UZ0mrg5xBlQtjwcb6e6zSHxnLcBfS__8etR9zdE8tDD6wPYxFzBhDmeyd9YdrUCUEDQ549X13XuYYPe-Su1PY5jz1i5m6NRyETN7fdGLiOlnykzDRJV6r5a0A";

            //const string auth0Domain = @"https://tulis.auth0.com/"; // Your Auth0 domain
            //const string auth0Audience = "https://tulis.auth0.com/api/v2/"; // Your API Identifier
            //// Obtain a JWT to validate and put it in here
            //const string testToken = @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ik5qQTVOMFV6T0RJME5EVkZNVGhET1RJek5VUkZOVFF4TkRRMk56VkJSVEUwUlRJek5qVTJPUSJ9.eyJpc3MiOiJodHRwczovL3R1bGlzLmF1dGgwLmNvbS8iLCJzdWIiOiJoc2VIT1Z5cmV5OXVVTUIxbHJUQjl0c0lEQldiQ3AzMkBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly90dWxpcy5hdXRoMC5jb20vYXBpL3YyLyIsImlhdCI6MTU0MjI5MDcwNiwiZXhwIjoxNTQyMzc3MTA2LCJhenAiOiJoc2VIT1Z5cmV5OXVVTUIxbHJUQjl0c0lEQldiQ3AzMiIsInNjb3BlIjoicmVhZDpjbGllbnRfZ3JhbnRzIGNyZWF0ZTpjbGllbnRfZ3JhbnRzIGRlbGV0ZTpjbGllbnRfZ3JhbnRzIHVwZGF0ZTpjbGllbnRfZ3JhbnRzIHJlYWQ6dXNlcnMgdXBkYXRlOnVzZXJzIGRlbGV0ZTp1c2VycyBjcmVhdGU6dXNlcnMgcmVhZDp1c2Vyc19hcHBfbWV0YWRhdGEgdXBkYXRlOnVzZXJzX2FwcF9tZXRhZGF0YSBkZWxldGU6dXNlcnNfYXBwX21ldGFkYXRhIGNyZWF0ZTp1c2Vyc19hcHBfbWV0YWRhdGEgY3JlYXRlOnVzZXJfdGlja2V0cyByZWFkOmNsaWVudHMgdXBkYXRlOmNsaWVudHMgZGVsZXRlOmNsaWVudHMgY3JlYXRlOmNsaWVudHMgcmVhZDpjbGllbnRfa2V5cyB1cGRhdGU6Y2xpZW50X2tleXMgZGVsZXRlOmNsaWVudF9rZXlzIGNyZWF0ZTpjbGllbnRfa2V5cyByZWFkOmNvbm5lY3Rpb25zIHVwZGF0ZTpjb25uZWN0aW9ucyBkZWxldGU6Y29ubmVjdGlvbnMgY3JlYXRlOmNvbm5lY3Rpb25zIHJlYWQ6cmVzb3VyY2Vfc2VydmVycyB1cGRhdGU6cmVzb3VyY2Vfc2VydmVycyBkZWxldGU6cmVzb3VyY2Vfc2VydmVycyBjcmVhdGU6cmVzb3VyY2Vfc2VydmVycyByZWFkOmRldmljZV9jcmVkZW50aWFscyB1cGRhdGU6ZGV2aWNlX2NyZWRlbnRpYWxzIGRlbGV0ZTpkZXZpY2VfY3JlZGVudGlhbHMgY3JlYXRlOmRldmljZV9jcmVkZW50aWFscyByZWFkOnJ1bGVzIHVwZGF0ZTpydWxlcyBkZWxldGU6cnVsZXMgY3JlYXRlOnJ1bGVzIHJlYWQ6cnVsZXNfY29uZmlncyB1cGRhdGU6cnVsZXNfY29uZmlncyBkZWxldGU6cnVsZXNfY29uZmlncyByZWFkOmVtYWlsX3Byb3ZpZGVyIHVwZGF0ZTplbWFpbF9wcm92aWRlciBkZWxldGU6ZW1haWxfcHJvdmlkZXIgY3JlYXRlOmVtYWlsX3Byb3ZpZGVyIGJsYWNrbGlzdDp0b2tlbnMgcmVhZDpzdGF0cyByZWFkOnRlbmFudF9zZXR0aW5ncyB1cGRhdGU6dGVuYW50X3NldHRpbmdzIHJlYWQ6bG9ncyByZWFkOnNoaWVsZHMgY3JlYXRlOnNoaWVsZHMgZGVsZXRlOnNoaWVsZHMgdXBkYXRlOnRyaWdnZXJzIHJlYWQ6dHJpZ2dlcnMgcmVhZDpncmFudHMgZGVsZXRlOmdyYW50cyByZWFkOmd1YXJkaWFuX2ZhY3RvcnMgdXBkYXRlOmd1YXJkaWFuX2ZhY3RvcnMgcmVhZDpndWFyZGlhbl9lbnJvbGxtZW50cyBkZWxldGU6Z3VhcmRpYW5fZW5yb2xsbWVudHMgY3JlYXRlOmd1YXJkaWFuX2Vucm9sbG1lbnRfdGlja2V0cyByZWFkOnVzZXJfaWRwX3Rva2VucyBjcmVhdGU6cGFzc3dvcmRzX2NoZWNraW5nX2pvYiBkZWxldGU6cGFzc3dvcmRzX2NoZWNraW5nX2pvYiByZWFkOmN1c3RvbV9kb21haW5zIGRlbGV0ZTpjdXN0b21fZG9tYWlucyBjcmVhdGU6Y3VzdG9tX2RvbWFpbnMgcmVhZDplbWFpbF90ZW1wbGF0ZXMgY3JlYXRlOmVtYWlsX3RlbXBsYXRlcyB1cGRhdGU6ZW1haWxfdGVtcGxhdGVzIHJlYWQ6bWZhX3BvbGljaWVzIHVwZGF0ZTptZmFfcG9saWNpZXMiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.sNVKU5snQdoBJHjFA9Fa65FZry8HHuYupXYSeEaRohgsKWHzhkhbjk3WiDOMvVdfpDqnZcdfuGEtVaTvyPYoMrUcnttAqVw4yP_zK3LxWR9iSdlmjtWcY-SIL0na5UcyyfmEegDd7LWksJqiQjp3O3z8p6z2kFuleQKheRZaLuVU78TEEioeXZkdwZ-OssOpqX_Uodtg7e7kHfCvNuiXdFT2FsCX8r7CxJQWLs7yF6yIj4Vt4DNCrY4uaWPxY4LluZXssSkpAgsoXFkm15Pfr4dPQrhLb1HkPEEO9NNc-8mICLYS0K4bKMg0QRXX21QE_u0gE5JvqPuTGXEXX31mxw";

            try
            {
                // Download the OIDC configuration which contains the JWKS
                // NB!!: Downloading this takes time, so do not do it very time you need to validate a token, Try and do it only once in the lifetime
                //     of your application!!
                IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    @"https://cognito-idp.ap-southeast-2.amazonaws.com/ap-southeast-2_liBGTVsZg/.well-known/openid-configuration"
                    //@"https://tulis.auth0.com/.well-known/openid-configuration"
                    , new OpenIdConnectConfigurationRetriever());

                OpenIdConnectConfiguration openIdConfig = await configurationManager
                    .GetConfigurationAsync(CancellationToken.None);

                // Configure the TokenValidationParameters. Assign the SigningKeys which were downloaded from Auth0.
                // Also set the Issuer and Audience(s) to validate
                TokenValidationParameters validationParameters =
                    new TokenValidationParameters
                    {
                        ValidIssuer = auth0Domain
                        , ValidAudiences = new[] { auth0Audience }
                        , IssuerSigningKeys = openIdConfig.SigningKeys
                    };

                // Now validate the token. If the token is not valid for any reason, an exception will be thrown by the method
                SecurityToken validatedToken;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                handler.MaximumTokenSizeInBytes = Int32.MaxValue;
                var user = handler.ValidateToken(testToken, validationParameters, out validatedToken);

                // The ValidateToken method above will return a ClaimsPrincipal. Get the user ID from the NameIdentifier claim
                // (The sub claim from the JWT will be translated to the NameIdentifier claim)
                Console.WriteLine($"Token is validated. User Id {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");
            }
            catch (Exception e)
            {
                //https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/7692d12e49a947f68a44cd3abc040d0c241376e6/src/System.IdentityModel.Tokens.Jwt/JwtSecurityTokenHandler.cs#L298
                Console.WriteLine($"Error occurred while validating token: {e.Message}");
                throw;
            }

            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}
