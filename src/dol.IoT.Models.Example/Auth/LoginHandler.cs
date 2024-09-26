using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using dol.IoT.Models.Public.Auth;

namespace dol.IoT.Models.Example.Auth;

public class LoginHandler : DelegatingHandler
{
    public required string Email { get; init; } = null!;
    public required string Password { get; init; } = null!;

    private static readonly HttpClient Client = new();

    // Relevant HTTP API endpoints in the "Auth" section of the Swagger page to authenticate our HTTP requests
    private const string LoginUri = "https://dol-iot-api-qa.azurewebsites.net/api/auth/login";
    private const string RefreshTokenUri = "https://dol-iot-api-qa.azurewebsites.net/api/auth/refresh";

    // In this example AccessTokenResponse is being tracked by this field, but consider saving it in a cache so that it can be reused / reloaded
    private static AccessTokenResponse? _accessTokenResponse;
    private static long _tokenExpiration;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpResponseMessage = await _updateToken();
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(_accessTokenResponse!.TokenType, _accessTokenResponse.AccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<HttpResponseMessage> _updateToken()
    {
        // check if our access token has been set or if it is still valid or if it needs to be updated
        if (_accessTokenResponse?.AccessToken is null)
        {
            // access token has not been set, so we need to log in to set our access token
            return await _login();
        }

        if (_tokenExpiration < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
        {
            // access token expires after 1 hour, so we can use refresh token to update out access token
            return await _refreshToken();
        }

        return new HttpResponseMessage(HttpStatusCode.OK);
    }

    private async Task<HttpResponseMessage> _login()
    {
        // login to get new / initial access token
        LoginRequest loginRequest = new() { Email = Email, Password = Password };
        var loginResponse = await Client.PostAsJsonAsync(LoginUri, loginRequest);
        if (loginResponse.IsSuccessStatusCode)
        {
            _accessTokenResponse = await loginResponse.Content.ReadFromJsonAsync<AccessTokenResponse>();
            _tokenExpiration = DateTimeOffset.Now.AddSeconds(_accessTokenResponse!.ExpiresIn).ToUnixTimeSeconds();
        }

        return loginResponse;
    }

    private static async Task<HttpResponseMessage> _refreshToken()
    {
        // access token lasts 1 hour, after that we can use our refresh token to get a new access token
        RefreshRequest refreshRequest = new() { RefreshToken = _accessTokenResponse!.RefreshToken };
        var refreshResponse = await Client.PostAsJsonAsync(RefreshTokenUri, refreshRequest);
        if (refreshResponse.IsSuccessStatusCode)
        {
            _accessTokenResponse = await refreshResponse.Content.ReadFromJsonAsync<AccessTokenResponse>();
            _tokenExpiration = DateTimeOffset.Now.AddSeconds(_accessTokenResponse!.ExpiresIn).ToUnixTimeSeconds();
        }

        return refreshResponse;
    }
}