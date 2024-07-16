using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class PageClaim : IEntity
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public int OperationClaimId { get; set; }
    }
}
