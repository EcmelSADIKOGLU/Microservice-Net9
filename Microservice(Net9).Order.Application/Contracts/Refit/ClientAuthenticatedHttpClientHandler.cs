using Duende.IdentityModel.Client;
using Microservice_Net9_.Shared.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Microservice_Net9_.Order.Application.Contracts.Refit
{
    internal class ClientAuthenticatedHttpClientHandler(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory) : DelegatingHandler
    {
        override protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization is not null) return await base.SendAsync(request, cancellationToken);

            using (var scope = serviceProvider.CreateScope())
            {
                var identityOption = scope.ServiceProvider.GetRequiredService<IdentityOption>();
                var clientSecretOption = scope.ServiceProvider.GetRequiredService<ClientSecretOption>();

                var discoveryRequest = new DiscoveryDocumentRequest() 
                { 
                    Address = identityOption.Address,
                    Policy = new Duende.IdentityModel.Client.DiscoveryPolicy()
                    {
                        RequireHttps = false
                    }
                };

                var client = httpClientFactory.CreateClient("IdentityClient");

                client.BaseAddress = new Uri(identityOption.Address);
                var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest, cancellationToken);

                if (discoveryResponse.IsError)
                {
                    throw new Exception(discoveryResponse.Error);  
                }

                var clientTokenRequest = new ClientCredentialsTokenRequest()
                {
                    Address = discoveryResponse.TokenEndpoint,
                    ClientId = clientSecretOption.ClientId,
                    ClientSecret = clientSecretOption.ClientSecret
                }; 

                TokenResponse tokenResponse =  await client.RequestClientCredentialsTokenAsync(clientTokenRequest);

                if (tokenResponse.IsError)
                {
                    throw new Exception(tokenResponse.Error);
                }

                request.SetBearerToken(tokenResponse.AccessToken!);

                // request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

                return await base.SendAsync(request, cancellationToken);

            }   
            
        }
    }
}
