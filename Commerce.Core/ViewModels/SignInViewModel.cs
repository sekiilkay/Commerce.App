using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.ViewModels
{
    public class SignInViewModel
    {
        [Display(Name = "Email : ")]
        [Required(ErrorMessage = "Email alanı zorunludur!")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre : ")]
        [Required(ErrorMessage = "Şifre alanı zorunludur!")]
        public string? Password { get; set; }
    }
}
