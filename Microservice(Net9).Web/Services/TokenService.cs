using Duende.IdentityModel.Client;
using Microservice_Net9_.Web.Pages.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Microservice_Net9_.Web.Services
{
    public class TokenService(HttpClient httpClient, IdentityOption identityOption)
    {
        public List<Claim> ExtractClaims(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(accessToken);

            return jwtSecurityToken.Claims.ToList();
        }

        public AuthenticationProperties CreateAuthenticationProperties(TokenResponse tokenResponse)
        {
            var authenticationTokens = new List<AuthenticationToken>
            {
                new()
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = tokenResponse.AccessToken!
                },
                new()
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = tokenResponse.RefreshToken!
                },
                new()
                {
                    Name = OpenIdConnectParameterNames.ExpiresIn,
                    Value = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn).ToString("o")
                }
            };

            AuthenticationProperties authenticationProperties = new()
            {
                IsPersistent = true
            };

            authenticationProperties.StoreTokens(authenticationTokens);

            return authenticationProperties;

        }

        public async Task<TokenResponse> GetTokensByRefreshTokenAsync(string refreshToken)
        {
            var discoveryResponse = await GetDiscoveryDocumentResponseAsync();

            var refreshTokenRequest = new RefreshTokenRequest(){
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                Address = discoveryResponse.TokenEndpoint,
                RefreshToken = refreshToken  
            };

            var tokenResponse = await httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            return tokenResponse;

        }

        public async Task<TokenResponse> GetClientAccessTokenAsync()
        {
            var discoveryResponse = await GetDiscoveryDocumentResponseAsync();

            var clientTokenRequest = new ClientCredentialsTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret
            };

            TokenResponse tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(clientTokenRequest);

            return tokenResponse;

        }

        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentResponseAsync()
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = new Duende.IdentityModel.Client.DiscoveryPolicy()
                {
                    RequireHttps = false
                }
            };

            httpClient.BaseAddress = new Uri(identityOption.Address);

            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError)
            {
                throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");
            }

            return discoveryResponse;
        }
    }
}
