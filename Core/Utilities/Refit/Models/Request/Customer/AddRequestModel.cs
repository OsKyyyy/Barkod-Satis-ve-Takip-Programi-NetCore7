using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.Customer
{
    public class AddRequestModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [RegularExpression(@"^\(?([1-9]{1})\)?([0-9]{2})[. ]?([0-9]{3})[. ]?([0-9]{4})$", ErrorMessage = "Lütfen telefon numarasını istenilen biçimde giriniz")]
        public string? Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir eposta adresi giriniz")]
        public string? Email { get; set; }

        public bool Status { get; set; }

        public int CreateUserId { get; set; }
    }
}
