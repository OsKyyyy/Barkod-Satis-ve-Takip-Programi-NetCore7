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
    public class UpdateRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^\d{0,8}(\.\d{1,2})?$", ErrorMessage = "Tutar 0.00 - 9999999999.99 aralığında olmalıdır. ")]
        public string PurchasePrice { get; set; }

        //\d+(\.\d{1,2})$
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^\d{0,8}(\.\d{1,2})?$", ErrorMessage = "Tutar 0.00 - 9999999999.99 aralığında olmalıdır. ")]
        public string SalePrice { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^([0 - 9]\d|\d{8,14})$", ErrorMessage = "Lütfen geçerli bir barkod numarası girin")]
        public string Barcode { get; set; }

        public string? Image { get; set; }
        public bool ImageChanged { get; set; }
        //^([0 - 9]\d|\d{1,})$ STOCK
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Lütfen geçerli bir stok sayısı girin")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Lütfen geçerli bir kritik stok sayısı girin")]
        public int CriticalStock { get; set; }

        public bool Favorite { get; set; }

        public bool Status { get; set; }

        public int UpdateUserId { get; set; }
    }
}
