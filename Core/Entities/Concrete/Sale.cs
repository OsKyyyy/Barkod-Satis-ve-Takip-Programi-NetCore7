using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Sale : IEntity
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public Decimal Amount { get; set; }
        public byte PaymentType { get; set; }
        public byte? ComplateType { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? Deleted { get; set; }
    }
}
