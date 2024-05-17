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
    public class CustomerMonthlyDebtViewModel
    {
        [JsonProperty("Year")]
        public int Year { get; set; }

        [JsonProperty("Month")]
        public int Month { get; set; }

        [JsonProperty("TotalDebt")]
        public Decimal TotalDebt { get; set; }
    }
}