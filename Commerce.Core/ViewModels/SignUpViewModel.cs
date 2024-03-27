using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.ViewModels
{
    public class SignUpViewModel
    {
        [Display(Name = "Ad : ")]
        [Required(ErrorMessage = "Ad alanı zorunludur!")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Soyad : ")]
        [Required(ErrorMessage = "Soyad alanı zorunludur!")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Kullanıcı Adı : ")]
        [Required(ErrorMessage = "Kullanıcı adı alanı zorunludur!")]
        public string UserName { get; set; } = null!;

        [Display(Name = "Email : ")]
        [Required(ErrorMessage = "Email alanı zorunludur!")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Şifre : ")]
        [Required(ErrorMessage = "Şifre alanı zorunludur!")]
        public string Password { get; set; } = null!;
    }
}
