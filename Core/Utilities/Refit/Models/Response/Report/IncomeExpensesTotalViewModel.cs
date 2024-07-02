using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Utilities.Refit.Models.Response.Report
{
    public class IncomeExpensesTotalViewModel
    {
        [JsonProperty("Total")]
        public Decimal Total { get; set; }
        
        [JsonProperty("TotalPrevious")]
        public Decimal TotalPrevious { get; set; }        
    }
}