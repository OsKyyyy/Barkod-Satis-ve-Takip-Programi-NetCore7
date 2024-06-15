using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class IncomeAndExpenses : IEntity
    {
        public int Id { get; set; }
        public int IncomeExpensesTypeId { get; set; }
        public bool Type { get; set; }        
        public string? Description { get; set; }
        public Decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
    }
}
