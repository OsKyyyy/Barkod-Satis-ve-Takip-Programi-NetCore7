using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class IncomeAndExpensesAddDto : IDto
    {
        public int IncomeExpensesTypeId { get; set; }
        public bool Type { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }      
        public bool Status { get; set; }
        public int CreateUserId { get; set; }
    }
}
