using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.Sale
{
    public class AddRequestModel
    {
        public byte Basket { get; set; }
        public int? CustomerId { get; set; }
        public string Amount { get; set; }
        public byte PaymentType { get; set; }
        public int? CreateUserId { get; set; }
    }
}
