using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.User
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir eposta adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Bu alan en az 8 karakter olmalıdır")]
        public string Password { get; set; }
    }
}
