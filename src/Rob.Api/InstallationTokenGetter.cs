using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rob.Api;

public class InstallationTokenGetter : IInstallationToken
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public InstallationTokenGetter(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClient = httpClientFactory.CreateClient();
        _config = config;
    }

    public async Task<InstallationToken> GetInstallationTokenAsync(string jwt)
    {
        InstallationTokenRequestBody body = new()
        {
            Repository = "mithrandir",
            Permissions = new Dictionary<string, string>()
            {
                { "contents", "read" },
                { "metadata", "read" }
            }
        };

        string serializedBody = JsonSerializer.Serialize(body);

        HttpContent content = new StringContent(serializedBody, Encoding.UTF8, 
            new MediaTypeHeaderValue("application/json"));

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Rob.Api");

        _httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue(
            "application/vnd.github+json"));

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", jwt);

        Uri uri = new(
            $"https://api.github.com/app/installations/{_config["GitHub:InstallationId"]}/access_tokens",
            UriKind.Absolute);

        HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<InstallationToken>(responseContent);
    }
}
