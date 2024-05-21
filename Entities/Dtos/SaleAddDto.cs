using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class SaleAddDto : IDto
    {
        public byte Basket { get; set; }
        public int? CustomerId { get; set; }
        public string Amount { get; set; }
        public byte PaymentType { get; set; }
        public string PartialPaymentAmount { get; set; }
        public byte ComplateType { get; set; }
        public int CreateUserId { get; set; }
    }
}
