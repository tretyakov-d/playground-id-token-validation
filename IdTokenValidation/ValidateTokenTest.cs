using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;
using Xunit;

namespace IdTokenValidation;

public class ValidateTokenExample
{
    // Obviously constant values must be replaced for test to pass
    
    const string Authority = "AUTHORITY";
    const string ClientId = "CLIENT-ID";

    const string IdToken = "ID-TOKEN";

    [Fact]
    public async Task Run()
    {
        DiscoveryDocumentResponse? doc = await new HttpClient().GetDiscoveryDocumentAsync(Authority);
        Assert.False(doc!.IsError);

        var validator = new JwtHandlerIdentityTokenValidator();
        var options = new OidcClientOptions
        {
            ClientId = ClientId,
            ProviderInformation = new ProviderInformation
            {
                IssuerName = doc.Issuer,
                KeySet = doc.KeySet
            }
        };

        IdentityTokenValidationResult? result = await validator.ValidateAsync(IdToken, options);

        Assert.False(result!.IsError);
    }
}