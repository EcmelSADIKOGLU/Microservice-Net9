
using Duende.IdentityModel.Client;
using Microservice_Net9_.Web.Options;
using Microservice_Net9_.Web.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Microservice_Net9_.Web.Pages.Auth.SignUp
{
    public record KeycloakErrorResponse(string ErrorMessage);
    public class SignUpService(IdentityOption identityOption, HttpClient client, ILogger<SignUpService> logger)
    {
        private async Task<string> GetClientCredentialTokenAsAdminAsync()
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = new Duende.IdentityModel.Client.DiscoveryPolicy()
                {
                    RequireHttps = false
                }
            };

            // var client = httpClientFactory.CreateClient("IdentityClient");
            client.BaseAddress = new Uri(identityOption.Address);

            var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }

            var clientTokenRequest = new ClientCredentialsTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret
            };

            TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(clientTokenRequest);

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }


            return tokenResponse.AccessToken!;

        }
        
        public async Task<ServiceResult> CreateAccountAsync(SignUpViewModel signUpViewModel)
        {
            var token = await GetClientCredentialTokenAsAdminAsync();

            var address = $"{identityOption.AdminAddress}/users";

            client.SetBearerToken(token);

            var userCreateRequest = CreateUserCreateRequest(signUpViewModel);

            var response = await client.PostAsJsonAsync(address, userCreateRequest);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<KeycloakErrorResponse>();
                    return ServiceResult.Error(errorResponse!.ErrorMessage);
                }

                var error = await response.Content.ReadAsStringAsync();
                logger.LogError(error);
                return ServiceResult.Error("System Error occured. Please try again later.");
            }

            return ServiceResult.Success();

        }

        private static UserCreateRequest CreateUserCreateRequest(SignUpViewModel signUpViewModel)
        {
            return new UserCreateRequest
                (signUpViewModel.UserName,
                 signUpViewModel.FirstName,
                 signUpViewModel.LastName,
                 signUpViewModel.Email,
                 true,
                 [new Credential("password", signUpViewModel.Password, false)]);
        }
    }
}
