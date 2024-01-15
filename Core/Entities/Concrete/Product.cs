using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public Decimal PurchasePrice { get; set; }
        public Decimal SalePrice { get; set; }
        public Decimal PreviousSellingPrice { get; set; }
        public string Barcode { get; set; }
        public int Stock { get; set; }
        public int CriticalStock { get; set; }
        public string? Image { get; set; }
        public bool Favorite { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public bool Deleted { get; set; }
    }
}
