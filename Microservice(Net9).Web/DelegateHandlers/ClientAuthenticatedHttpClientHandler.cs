using Duende.IdentityModel.Client;
using Microservice_Net9_.Web.Pages.Options;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using static Duende.IdentityModel.OidcConstants;

namespace Microservice_Net9_.Web.DelegateHandlers
{
    internal class ClientAuthenticatedHttpClientHandler(TokenService tokenService, IHttpContextAccessor httpContextAccessor) : DelegatingHandler
    {
        override protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (httpContextAccessor.HttpContext is null) return await base.SendAsync(request, cancellationToken);

            if (request.Headers.Authorization is not null) return await base.SendAsync(request, cancellationToken);


            var tokenResponse = await tokenService.GetClientAccessTokenAsync();


            if (tokenResponse.IsError)
            {
                throw new UnauthorizedAccessException($"Failed to client access token :{tokenResponse.Error}");

            }

            request.SetBearerToken(tokenResponse.AccessToken!);
            return await base.SendAsync(request, cancellationToken);

        }
    }
}
