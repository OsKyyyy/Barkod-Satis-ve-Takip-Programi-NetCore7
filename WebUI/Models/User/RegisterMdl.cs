﻿using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.User
{
    public class RegisterMdl
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([1-9]{1})\)?([0-9]{2})[. ]?([0-9]{3})[. ]?([0-9]{4})$", ErrorMessage = "Lütfen telefon numarasını istenilen biçimde giriniz")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir eposta adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Bu alan en az 8 karakter olmalıdır")]

        public string Password { get; set; }

        public bool Status { get; set; }
    }
}
