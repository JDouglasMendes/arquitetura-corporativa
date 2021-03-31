using System.Collections.Generic;
using System.Security.Claims;

namespace Infra.CrossCutting.Identity
{
    public interface IUser
    {
        string Name { get; }

        bool IsAuthenticated();

        IEnumerable<Claim> GetClaimsIdentity();
    }
}