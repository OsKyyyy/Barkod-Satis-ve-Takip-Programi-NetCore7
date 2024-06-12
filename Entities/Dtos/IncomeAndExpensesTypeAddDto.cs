using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class IncomeAndExpensesTypeAddDto : IDto
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public int CreateUserId { get; set; }
    }
}
