using Duende.IdentityModel.Client;
using Microservice_Net9_.Web.Pages.Auth.SignUp;
using Microservice_Net9_.Web.Pages.Options;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Microservice_Net9_.Web.Pages.Auth.SignIn
{
    public class SignInService(
        IHttpContextAccessor httpContextAccessor,
        TokenService tokenService,
        IdentityOption identityOption, 
        HttpClient client, 
        ILogger<SignUpService> logger)
    {

        public async Task<ServiceResult> AuthenticateAsync(SignInViewModel signInViewModel)
        {

            var tokenResponse = await GetTokenResponseAsync(signInViewModel);

            if (tokenResponse.IsError)
            {
                return ServiceResult.Error(tokenResponse.Error!, tokenResponse.ErrorDescription!);
            }

            var userClaims = tokenService.ExtractClaims(tokenResponse.AccessToken!);

            var authenticateProperties = tokenService.CreateAuthenticationProperties(tokenResponse);

            var cleamIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            var claimPrincipal = new ClaimsPrincipal(cleamIdentity);

            await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authenticateProperties);


            return ServiceResult.Success();
             
        }

        private async Task<TokenResponse> GetTokenResponseAsync(SignInViewModel signInViewModel)
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = new Duende.IdentityModel.Client.DiscoveryPolicy()
                {
                    RequireHttps = false
                }
            };

            client.BaseAddress = new Uri(identityOption.Address);

            var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }

            var passwordTokenRequest = new PasswordTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                Password = signInViewModel.Password,
                UserName = signInViewModel.Email!
            };

            TokenResponse tokenResponse = await client.RequestPasswordTokenAsync(passwordTokenRequest);




            return tokenResponse;

        }
    }
}


    
