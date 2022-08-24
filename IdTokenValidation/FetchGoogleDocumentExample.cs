using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Xunit;

namespace IdTokenValidation;

public class FetchGoogleDocumentExample
{
    [Fact]
    public async Task FetchGoogleDiscoveryDocumentUsingWhitelist()
    {
        var opt = new DiscoveryDocumentRequest
        {
            Address = "https://accounts.google.com/",
            Policy =
            {
                AdditionalEndpointBaseAddresses =
                {
                    "https://oauth2.googleapis.com/",
                    "https://www.googleapis.com/",
                    "https://openidconnect.googleapis.com/"
                }
            }
        };

        DiscoveryDocumentResponse? doc = await new HttpClient().GetDiscoveryDocumentAsync(opt);
        Assert.False(doc!.IsError);
    }

    [Fact]
    public async Task FetchGoogleDiscoveryDocumentWithEndpointValidationDisabled()
    {
        var opt = new DiscoveryDocumentRequest
        {
            Address = "https://accounts.google.com/",
            Policy =
            {
                ValidateEndpoints = false
            }
        };
        var http = new HttpClient();

        DiscoveryDocumentResponse? doc = await http.GetDiscoveryDocumentAsync(opt);
        Assert.False(doc!.IsError);
    }
}