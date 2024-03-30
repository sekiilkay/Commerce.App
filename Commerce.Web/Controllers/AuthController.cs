using Commerce.Core.ViewModels;
using Commerce.Service.Authentications;
using Microsoft.AspNetCore.Authentication;
using Shared.Library.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authenticationService;
        public AuthController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var (isSuccess, errors) = await _authenticationService.SignUpAsync(request);

            if (!isSuccess)
            {
                ModelState.AddModelErrorList(errors);
                return View();
            }

            return RedirectToAction(nameof(AuthController.SignIn));
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var (isSuccess, errors) = await _authenticationService.SignInAsync(request);

            if (!isSuccess)
            {
                ModelState.AddModelErrorList(errors);
                return View();
            }

            return RedirectToAction(nameof(UserProductController.Main), "UserProduct");
        }
        public async Task<IActionResult> LogOut()
        {
            await _authenticationService.LogOutAsync();
            return RedirectToAction(nameof(AuthController.SignIn));
        }
    }
}
