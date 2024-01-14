using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class SaleProduct : IEntity
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public Decimal ProductUnitPrice { get; set; }
        public int ProductQuantity { get; set; }
        public bool Deleted { get; set; }
    }
}
