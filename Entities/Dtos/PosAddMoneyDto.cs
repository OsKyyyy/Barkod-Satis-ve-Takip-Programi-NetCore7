using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class PosAddMoneyDto : IDto
    {
        public byte Basket { get; set; }
        public string? ProductName { get; set; }
        public Decimal ProductUnitPrice { get; set; }
        public int CreateUserId { get; set; }
    }
}
