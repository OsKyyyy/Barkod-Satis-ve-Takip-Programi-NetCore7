using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.CustomerMovement
{
    public class AddRequestModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^\d{0,8}(\.\d{1,2})?$", ErrorMessage = "Tutar 0.00 - 9999999999.99 aralığında olmalıdır. ")]
        public string Amount { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public byte ProcessType { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string? ProductInformation { get; set; }

        public string? Note { get; set; }

        public bool Status { get; set; }

        public int CreateUserId { get; set; }
    }
}
