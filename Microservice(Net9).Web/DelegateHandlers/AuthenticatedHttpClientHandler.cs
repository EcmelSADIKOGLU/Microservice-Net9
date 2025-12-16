using Duende.IdentityModel.Client;
using Microservice_Net9_.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Microservice_Net9_.Web.DelegateHandlers
{
    // Intercept (Requestte araya giren işlemler)
    public class AuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor, TokenService tokenService)
    : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is null) return await base.SendAsync(request, cancellationToken);

            var user = httpContextAccessor.HttpContext.User;
            if (!user.Identity!.IsAuthenticated) return await base.SendAsync(request, cancellationToken);


            var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (string.IsNullOrEmpty(accessToken)) throw new UnauthorizedAccessException("No access token found.");

            request.SetBearerToken(accessToken);

            var response = await base.SendAsync(request, cancellationToken);


            if (response.StatusCode != HttpStatusCode.Unauthorized) return response;


            var refreshToken =
                await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            if (string.IsNullOrEmpty(refreshToken)) throw new UnauthorizedAccessException("No refresh token found.");


            var tokenResponse = await tokenService.GetTokensByRefreshToken(refreshToken);


            if (tokenResponse.IsError) throw new UnauthorizedAccessException("Failed to refresh access token.");

            // TODO : Store the new tokens in the authentication properties

            var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);
            var userClaim = httpContextAccessor.HttpContext.User.Claims;

            var claimIdentity = new ClaimsIdentity(userClaim, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role);


            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);


            await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal, authenticationProperties);


            request.SetBearerToken(tokenResponse.AccessToken!);

            return await base.SendAsync(request, cancellationToken);
        }
    }

    internal class AuthenticatedHttpClientHandler1(IHttpContextAccessor httpContextAccessor, TokenService tokenService) : DelegatingHandler
    {
        override protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (httpContextAccessor.HttpContext is null) return await base.SendAsync(request, cancellationToken);

            if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated) return await base.SendAsync(request, cancellationToken);

            var acsessToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken); 

            if (string.IsNullOrEmpty(acsessToken))
            {
                throw new UnauthorizedAccessException("No access token found");
            }

            request.SetBearerToken(acsessToken);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode != HttpStatusCode.Unauthorized)
            {
                return response;
            }

            var refreshToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new UnauthorizedAccessException("No refresh token found");
            }

            var tokenResponse = await tokenService.GetTokensByRefreshTokenAsync(refreshToken);

            if (tokenResponse.IsError)
            {
                throw new UnauthorizedAccessException($"Failed to refresh access token :{tokenResponse.Error}");

            }

            //TODO: Create Cookie
            var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);
            var userClaim = httpContextAccessor.HttpContext.User.Claims;

            var claimIdentity = new ClaimsIdentity(userClaim, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role);


            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);


            await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal, authenticationProperties);

            //TODO: Control and understand

            request.SetBearerToken(acsessToken);


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
