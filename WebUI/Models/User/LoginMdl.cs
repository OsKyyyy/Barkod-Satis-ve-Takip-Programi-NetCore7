using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.User
{
    public class LoginMdl
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Password { get; set; }
    }
}
