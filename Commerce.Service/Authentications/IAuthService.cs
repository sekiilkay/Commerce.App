using Commerce.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Service.Authentications
{
    public interface IAuthService
    {
        Task<(bool, List<string>)> SignUpAsync(SignUpViewModel request);
        Task<(bool, List<string>)> SignInAsync(SignInViewModel request);
        Task LogOutAsync();
    }
}
