using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.User
{
    public class LoginMdl
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage = "Bu alan en az 8 karakter olmalıdır")]
        public string Password { get; set; }
    }
}
