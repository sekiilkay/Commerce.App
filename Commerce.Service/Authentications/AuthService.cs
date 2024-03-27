using Commerce.Core.Entities;
using Commerce.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Service.Authentications
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<(bool, List<string>)> SignInAsync(SignInViewModel request)
        {
            var hasUser = await _userManager.FindByEmailAsync(request.Email!);

            if (hasUser == null)
                return (false, new List<string> { "Email veya şifre yanlış" });

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, request.Password!, false, false);

            if (!signInResult.Succeeded)
                return (false, new List<string> { "Email veya şifre yanlış" });

            return (true, null!);
        }

        public async Task<(bool, List<string>)> SignUpAsync(SignUpViewModel request)
        {
            var identityResult = await _userManager.CreateAsync(new()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            }, request.Password!);

            if (!identityResult.Succeeded)
                return (false, identityResult.Errors.Select(x => x.Description).ToList());

            return (true, null!);
        }
    }
}
