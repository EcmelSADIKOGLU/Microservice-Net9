using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Microservice_Net9_.Order.Application.Contracts.Refit
{
    // Intercept (Requestte araya giren işlemler)
    internal class AuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (httpContextAccessor.HttpContext is null) 
                return await base.SendAsync(request, cancellationToken);

            if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated) return await base.SendAsync(request, cancellationToken);

            string? token = null;

            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                token = authHeader.ToString().Replace("Bearer ","");
            }
            
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
