﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RH.Service.Api
{
    public class DisableAuthenticationPolicyEvaluator : IPolicyEvaluator
    {
        public virtual async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var testScheme = "FakeScheme";
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(new[] {
                                                        new Claim("Permission", "CanViewPage"),
                                                        new Claim("Manager", "yes"),
                                                        new Claim(ClaimTypes.Role, "Administrator"),
                                                        new Claim(ClaimTypes.NameIdentifier, "Codeizi"),
                                                        },
                                                        testScheme));

            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal,
                new AuthenticationProperties(),
                testScheme)));
        }

        public virtual async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy,
            AuthenticateResult authenticationResult,
            HttpContext context,
            object resource)
        {
            return await Task.FromResult(PolicyAuthorizationResult.Success());
        }
    }
}