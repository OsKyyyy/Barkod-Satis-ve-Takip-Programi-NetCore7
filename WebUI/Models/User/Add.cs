using System.ComponentModel.DataAnnotations;

namespace WebUI.Model.User
{
    public class Add
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
