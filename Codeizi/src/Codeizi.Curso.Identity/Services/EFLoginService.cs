using Codeizi.Curso.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Codeizi.Curso.Identity.Services
{
    public class EFLoginService : ILoginService<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EFLoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> FindByUsername(string user)
            => await _userManager.FindByEmailAsync(user);

        public async Task<bool> ValidateCredentials(ApplicationUser user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public Task SignIn(ApplicationUser user)
            => _signInManager.SignInAsync(user, true);

        public Task SignInAsync(ApplicationUser user, AuthenticationProperties properties, string authenticationMethod = null)
            => _signInManager.SignInAsync(user, properties, authenticationMethod);
    }
}