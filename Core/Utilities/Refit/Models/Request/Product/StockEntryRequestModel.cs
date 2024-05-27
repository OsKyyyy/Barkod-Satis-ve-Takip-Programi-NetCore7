using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.Product
{
    public class StockEntryRequestModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^([0 - 9]\d|\d{8,14})$", ErrorMessage = "Lütfen geçerli bir barkod numarası girin")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Lütfen geçerli bir adet sayısı girin")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public int WholeSalerId { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^\d{0,8}(\.\d{1,2})?$", ErrorMessage = "Tutar 0.00 - 9999999999.99 aralığında olmalıdır. ")]
        public string TotalCost { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^\d{0,8}(\.\d{1,2})?$", ErrorMessage = "Tutar 0.00 - 9999999999.99 aralığında olmalıdır. ")]
        public string PaymentAmount { get; set; }

        public int UpdateUserId { get; set; }

        public string? Image { get; set; }
    }
}
