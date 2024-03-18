using Microsoft.Identity.Client;
using System.Net.Http.Headers;

// Surface API Management Service app id
const string SurfaceApiManagementServiceAppId = "05c8c62b-18c7-4dff-9f65-2812120f06f0";

// Your tenant and app information
string tenantId = "<your-tenant-id>";
string clientId = "<your-app-client-id>";
string clientSecret = "<your-app-client-secret>";
string subscriptionKey = "<your-subscription-key>";

// Request token
var app = ConfidentialClientApplicationBuilder.Create(clientId)
    .WithClientSecret(clientSecret)
    .WithAuthority($"https://login.microsoftonline.com/{tenantId}")
    .Build();
var authResult = await app.AcquireTokenForClient([$"{SurfaceApiManagementServiceAppId}/.default"]).ExecuteAsync();

// Create request message
HttpRequestMessage request = new HttpRequestMessage();
request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
request.Method = HttpMethod.Put;
request.RequestUri = new Uri("https://appx-sms-int-apim.azure-api.net/api/external/warranty/enrollment");

// Send the request
using (var client = new HttpClient())
{
    var response = await client.SendAsync(request);
    response.EnsureSuccessStatusCode();
}
