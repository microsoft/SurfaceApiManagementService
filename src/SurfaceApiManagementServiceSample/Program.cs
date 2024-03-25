using Microsoft.Identity.Client;
using System.Net.Http.Headers;

// Surface API Management Service app id
const string SurfaceApiManagementServiceAppId = "76bd8628-ca60-441c-9d83-06503cbfd9c5";

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
request.RequestUri = new Uri("https://appx-sms-prod-apim.azure-api.net/api/external/warranty/enrollment");

// Send the request
using (var client = new HttpClient())
{
    var response = await client.SendAsync(request);
    response.EnsureSuccessStatusCode();
}
