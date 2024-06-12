using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Pos : IEntity
    {
        public int Id { get; set; }
        public byte Basket { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public Decimal ProductUnitPrice { get; set; }
        public Decimal ProductUnitPurchasePrice { get; set; }        
        public int ProductQuantity { get; set; }
        public int CreateUserId { get; set; }
    }
}
