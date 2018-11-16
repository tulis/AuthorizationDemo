<Query Kind="Program">
  <Reference>C:\src\assetic3-develop\lib\AutoMapper.5.1.1\lib\net45\AutoMapper.dll</Reference>
  <Reference>C:\src\assetic3-develop\lib\CsvHelper.2.13.5.0\lib\net40-client\CsvHelper.dll</Reference>
  <Reference Relative="..\..\.nuget\packages\Dapper\1.50.4\lib\net451\Dapper.dll">C:\Users\tsuryati\.nuget\packages\Dapper\1.50.4\lib\net451\Dapper.dll</Reference>
  <Reference>C:\src\assetic3-develop\lib\LinqKit.1.1.9.0\lib\net45\LinqKit.dll</Reference>
  <Reference>C:\src\assetic3-develop\lib\SqlKata.1.0.0-beta-476\lib\net451\QueryBuilder.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\SMDiagnostics.dll</Reference>
  <Reference>C:\src\assetic3-develop\lib\SqlKata.Execution.1.0.0-beta-476\lib\net451\SqlKata.Execution.dll</Reference>
  <Reference Relative="..\..\.nuget\packages\System.Collections.Immutable\1.3.1\lib\netstandard1.0\System.Collections.Immutable.dll">C:\Users\tsuryati\.nuget\packages\System.Collections.Immutable\1.3.1\lib\netstandard1.0\System.Collections.Immutable.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Internals.dll</Reference>
  <Reference Relative="..\..\.nuget\packages\System.Threading.Tasks.Extensions\4.3.0\lib\netstandard1.0\System.Threading.Tasks.Extensions.dll">C:\Users\tsuryati\.nuget\packages\System.Threading.Tasks.Extensions\4.3.0\lib\netstandard1.0\System.Threading.Tasks.Extensions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <NuGetReference>Microsoft.IdentityModel.Protocols.OpenIdConnect</NuGetReference>
  <NuGetReference>Microsoft.IdentityModel.Tokens</NuGetReference>
  <NuGetReference>Rock.Core.Newtonsoft</NuGetReference>
  <NuGetReference>System.IdentityModel.Tokens.Jwt</NuGetReference>
  <Namespace>AutoMapper</Namespace>
  <Namespace>AutoMapper.Configuration</Namespace>
  <Namespace>AutoMapper.Configuration.Conventions</Namespace>
  <Namespace>AutoMapper.Execution</Namespace>
  <Namespace>AutoMapper.Mappers</Namespace>
  <Namespace>AutoMapper.QueryableExtensions</Namespace>
  <Namespace>AutoMapper.QueryableExtensions.Impl</Namespace>
  <Namespace>CsvHelper</Namespace>
  <Namespace>CsvHelper.Configuration</Namespace>
  <Namespace>CsvHelper.TypeConversion</Namespace>
  <Namespace>Dapper</Namespace>
  <Namespace>LinqKit</Namespace>
  <Namespace>Microsoft.IdentityModel.JsonWebTokens</Namespace>
  <Namespace>Microsoft.IdentityModel.Logging</Namespace>
  <Namespace>Microsoft.IdentityModel.Protocols</Namespace>
  <Namespace>Microsoft.IdentityModel.Protocols.OpenIdConnect</Namespace>
  <Namespace>Microsoft.IdentityModel.Tokens</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>SqlKata</Namespace>
  <Namespace>SqlKata.Compilers</Namespace>
  <Namespace>SqlKata.Execution</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.IdentityModel.Claims</Namespace>
  <Namespace>System.IdentityModel.Tokens</Namespace>
  <Namespace>System.IdentityModel.Tokens.Jwt</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Configuration</Namespace>
  <Namespace>System.Runtime.Serialization.Json</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Xml</Namespace>
  <Namespace>System.Dynamic</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
</Query>

async Task Main()
{   
    // Obtain a JWT to validate and put it in here
    const string testToken = @"eyJ0eXAiOiJKV1QiLCJraWQiOiI0YTAwM2NkYS01YmQ1LTQ2M2QtOTZhOC1iZDYwOGI5ZTgyZTUiLCJhbGciOiJFUzI1NiIsImlzcyI6Imh0dHBzOi8vY29nbml0by1pZHAuYXAtc291dGhlYXN0LTIuYW1hem9uYXdzLmNvbS9hcC1zb3V0aGVhc3QtMl93OUgyVmRQb2EiLCJjbGllbnQiOiJtYnNhZTVydHRyM2dmMzV0cjExazcwZ2doIiwic2lnbmVyIjoiYXJuOmF3czplbGFzdGljbG9hZGJhbGFuY2luZzphcC1zb3V0aGVhc3QtMjo3ODk0NjIyODkzMjQ6bG9hZGJhbGFuY2VyL2FwcC9hbGljZWJvYi1wcmVkaWN0b3IvNDBlYmNkM2NlMzUzOWMzMCIsImV4cCI6MTU0MjMzODkyMn0=.eyJzdWIiOiIyYmRmZTcyOC0yNmY5LTRlNDAtYmNmYy1jMjY1MTQ1NzYxOTYiLCJlbWFpbF92ZXJpZmllZCI6InRydWUiLCJiaXJ0aGRhdGUiOiIwMS8wMS8yMDAwIiwiZW1haWwiOiJ0c3VyeWF0aSthYnVzZXJwb29sLjAwMUBhc3NldGljLmNvbSIsInVzZXJuYW1lIjoiMmJkZmU3MjgtMjZmOS00ZTQwLWJjZmMtYzI2NTE0NTc2MTk2In0=.K1Uufn36oT7sh38ERVtG3w9a33rhbpTaL7vJpPytAuGrmYqe6zo90W8qnf9xXUwFGf_sLt5Lzvf0YeyLZPwp4g==";
    
    try
    {        
        var keyId = this.GetKeyId(testToken);
        var ecdsa = await this.GetEcdsa(keyId);
        
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new ECDsaSecurityKey(ecdsa)
        };

            // Now validate the token. If the token is not valid for any reason, an exception will be thrown by the method
        Microsoft.IdentityModel.Tokens.SecurityToken validatedToken;
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        
        var user = handler.ValidateToken(testToken, validationParameters, out validatedToken).Dump();

        // The ValidateToken method above will return a ClaimsPrincipal. Get the user ID from the NameIdentifier claim
        // (The sub claim from the JWT will be translated to the NameIdentifier claim)
        Console.WriteLine($"Token is validated. User Id {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");
    }
    catch (Exception e)
    {
        //https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/7692d12e49a947f68a44cd3abc040d0c241376e6/src/System.IdentityModel.Tokens.Jwt/JwtSecurityTokenHandler.cs#L298
        e.Dump();
    }
    
    "PRESS Enter to Continue...".Dump();
    Console.ReadLine();
}

// Define other methods and classes here
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
    var publicKey = await this.GetPublicKey(keyId);
    var cngKey = CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob);
    var ecDsaCng = new ECDsaCng(cngKey);
    ecDsaCng.HashAlgorithm = CngAlgorithm.ECDsaP256;
    return ecDsaCng;
}

private async Task<byte[]> GetPublicKey(string keyId)
{
    using(var httpClient = new HttpClient())
    {        
        var publicKey = await httpClient.GetStringAsync(
            $@"https://public-keys.auth.elb.ap-southeast-2.amazonaws.com/{keyId}");
            
        var x509Certificate = new X509Certificate($@"https://public-keys.auth.elb.ap-southeast-2.amazonaws.com/{keyId}");
        //return Convert.ToBase64String(x509Certificate.GetPublicKey());    
        return x509Certificate.GetPublicKey();
        //Possibly requires Convert.ToBase64String(publicKey);
    }
}