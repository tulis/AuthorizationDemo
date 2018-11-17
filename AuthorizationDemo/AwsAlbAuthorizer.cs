using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationDemo
{
    public class AwsAlbAuthorizer : IAuthorizer
    {
        public AwsAlbAuthorizer()
        {
        }

        public async Task Authorize()
        {
            // Obtain a JWT to validate and put it in here
            const string testToken = @"eyJ0eXAiOiJKV1QiLCJraWQiOiI0YTAwM2NkYS01YmQ1LTQ2M2QtOTZhOC1iZDYwOGI5ZTgyZTUiLCJhbGciOiJFUzI1NiIsImlzcyI6Imh0dHBzOi8vY29nbml0by1pZHAuYXAtc291dGhlYXN0LTIuYW1hem9uYXdzLmNvbS9hcC1zb3V0aGVhc3QtMl93OUgyVmRQb2EiLCJjbGllbnQiOiJtYnNhZTVydHRyM2dmMzV0cjExazcwZ2doIiwic2lnbmVyIjoiYXJuOmF3czplbGFzdGljbG9hZGJhbGFuY2luZzphcC1zb3V0aGVhc3QtMjo3ODk0NjIyODkzMjQ6bG9hZGJhbGFuY2VyL2FwcC9hbGljZWJvYi1wcmVkaWN0b3IvNDBlYmNkM2NlMzUzOWMzMCIsImV4cCI6MTU0MjMzODkyMn0=.eyJzdWIiOiIyYmRmZTcyOC0yNmY5LTRlNDAtYmNmYy1jMjY1MTQ1NzYxOTYiLCJlbWFpbF92ZXJpZmllZCI6InRydWUiLCJiaXJ0aGRhdGUiOiIwMS8wMS8yMDAwIiwiZW1haWwiOiJ0c3VyeWF0aSthYnVzZXJwb29sLjAwMUBhc3NldGljLmNvbSIsInVzZXJuYW1lIjoiMmJkZmU3MjgtMjZmOS00ZTQwLWJjZmMtYzI2NTE0NTc2MTk2In0=.K1Uufn36oT7sh38ERVtG3w9a33rhbpTaL7vJpPytAuGrmYqe6zo90W8qnf9xXUwFGf_sLt5Lzvf0YeyLZPwp4g==";

            try
            {
                var keyId = this.GetKeyId(testToken);
                var ecdsa = await this.GetEcdsa(keyId);
                var validationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new ECDsaSecurityKey(ecdsa)
                };

                // Now validate the token. If the token is not valid for any reason, an exception will be thrown by the method
                var handler = new JwtSecurityTokenHandler();
                var user = handler.ValidateToken(testToken, validationParameters, out SecurityToken validatedToken);
                // The ValidateToken method above will return a ClaimsPrincipal. Get the user ID from the NameIdentifier claim
                // (The sub claim from the JWT will be translated to the NameIdentifier claim)
                Console.WriteLine($"Token is validated. User Id {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");
            }
            catch (Exception e)
            {
                //https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/7692d12e49a947f68a44cd3abc040d0c241376e6/src/System.IdentityModel.Tokens.Jwt/JwtSecurityTokenHandler.cs#L298

                Console.WriteLine($"Exception occurred: {e.Message}");
            }

            throw new NotImplementedException();
        }

        public string GetKeyId(string token)
        {
            var jwtHeaders = token.Split('.')[0];
            var decodedJwtHeaders = Encoding.UTF8.GetString(Convert.FromBase64String(jwtHeaders));
            dynamic decodedJsonJwtHeaders = JsonConvert.DeserializeObject<ExpandoObject>(decodedJwtHeaders);
            var keyId = decodedJsonJwtHeaders.kid;
            return keyId;
        }

        private async Task<ECDsa> GetEcdsa(string keyId)
        {
            var cngBlob = await this.GetCngBlob(keyId);
            var cngKey = CngKey.Import(cngBlob, CngKeyBlobFormat.EccPublicBlob);
            var ecDsaCng = new ECDsaCng(cngKey);
            ecDsaCng.HashAlgorithm = CngAlgorithm.ECDsaP256;
            return ecDsaCng;
        }

        //https://stackoverflow.com/questions/44502331/c-sharp-get-cngkey-object-from-public-key-in-text-file?noredirect=1
        //https://web.archive.org/web/20181117134911/https://stackoverflow.com/questions/44502331/c-sharp-get-cngkey-object-from-public-key-in-text-file?noredirect=1
        private static byte[] Secp256r1Prefix { get; } = Convert.FromBase64String("MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE");
        private static byte[] CngBlobPrefix { get; } = { 0x45, 0x43, 0x53, 0x31, 0x20, 0, 0, 0 };
        private async Task<byte[]> GetCngBlob(string keyId)
        {
            using (var httpClient = new HttpClient())
            {
                //The sample public key from AWS ELB is also provided in SampleEcdsaPublicKey.txt
                var base64PublicKey = (await httpClient
                    .GetStringAsync($@"https://public-keys.auth.elb.ap-southeast-2.amazonaws.com/{keyId}"))
                .Replace(oldValue: "-----BEGIN PUBLIC KEY-----", newValue: String.Empty)
                .Replace(oldValue: "-----END PUBLIC KEY-----", newValue: String.Empty);

                byte[] subjectPublicKeyInfo = Convert.FromBase64String(base64PublicKey);

                if (subjectPublicKeyInfo.Length != 91)
                {
                    throw new InvalidOperationException();
                }

                byte[] prefix = Secp256r1Prefix;

                if (!subjectPublicKeyInfo.Take(prefix.Length).SequenceEqual(prefix))
                {
                    throw new InvalidOperationException();
                }

                byte[] cngBlob = new byte[CngBlobPrefix.Length + 64];
                Buffer.BlockCopy(CngBlobPrefix, 0, cngBlob, 0, CngBlobPrefix.Length);

                Buffer.BlockCopy(
                    subjectPublicKeyInfo
                    , Secp256r1Prefix.Length
                    , cngBlob
                    , CngBlobPrefix.Length
                    , 64);

                return cngBlob;
            }
        }
    }
}