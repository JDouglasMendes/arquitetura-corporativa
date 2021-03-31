using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;

namespace Infra.CrossCutting.Identity
{
    [ExcludeFromCodeCoverage]
    public class UserContext : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public UserContext(IHttpContextAccessor accessor)
            => _accessor = accessor;

        public string Name => _accessor.HttpContext.User.Identity.Name ??
                              _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public bool IsAuthenticated()
            => _accessor.HttpContext.User.Identity.IsAuthenticated;

        public IEnumerable<Claim> GetClaimsIdentity()
            => _accessor.HttpContext.User.Claims;
    }
}