using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class PosAddDto : IDto
    {
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public Decimal ProductUnitPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int CreateUserId { get; set; }
    }
}
