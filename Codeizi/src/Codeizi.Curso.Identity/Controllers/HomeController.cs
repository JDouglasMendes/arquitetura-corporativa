using Codeizi.Curso.Identity;
using Codeizi.Curso.Identity.Models;
using Codeizi.Curso.Identity.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Codeizi.Curso.Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;        
        private readonly IRedirectService _redirectSvc;

        public HomeController(IIdentityServerInteractionService interaction,IRedirectService redirectSvc)
        {
            _interaction = interaction;
            _redirectSvc = redirectSvc;
        }

        public IActionResult Index()
            => View();
        
        public IActionResult ReturnToOriginalApplication(string returnUrl)
        {
            if (returnUrl != null)
                return Redirect(_redirectSvc.ExtractRedirectUriFromReturnUrl(returnUrl));
            else
                return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}