using Duende.IdentityModel.Client;
using Microservice_Net9_.Web.Pages.Auth.SignUp;
using Microservice_Net9_.Web.Pages.Options;
using Microservice_Net9_.Web.Services;

namespace Microservice_Net9_.Web.Pages.Auth.SignIn
{
    public class SignInService(IdentityOption identityOption, HttpClient client, ILogger<SignUpService> logger)
    {

        public async Task<ServiceResult> SignInAsync(SignInViewModel signInViewModel)
        {

            var tokenResponse = await GetTokenResponseAsync(signInViewModel);

            if (tokenResponse.IsError)
            {
                return ServiceResult.Error(tokenResponse.Error!, tokenResponse.ErrorDescription!);
            }

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


    
