using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.WholeSalerMovement
{
    public class UpdateRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public int WholeSalerId { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^\d{0,8}(\.\d{1,2})?$", ErrorMessage = "Tutar 0.00 - 9999999999.99 aralığında olmalıdır. ")]
        public string Amount { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public DateTime InvoiceDate { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public byte ProcessType { get; set; }

        public string? Note { get; set; }

        public string? Image { get; set; }

        public bool ImageChanged { get; set; }

        public bool Status { get; set; }

        public int UpdateUserId { get; set; }
    }
}
