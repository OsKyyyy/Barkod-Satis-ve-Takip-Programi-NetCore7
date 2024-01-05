using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.Pos
{
    public class AddRequestModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public byte Basket { get; set; }
        public string Barcode { get; set; }
        public int? Quantity { get; set; }
        public string ProductName { get; set; }
        public Decimal ProductUnitPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int CreateUserId { get; set; }
    }
}
