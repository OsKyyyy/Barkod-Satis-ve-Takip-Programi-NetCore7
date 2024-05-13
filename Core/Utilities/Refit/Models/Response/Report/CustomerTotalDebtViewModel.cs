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
    public class CustomerTotalDebtViewModel
    {
        [JsonProperty("TotalDebt")]
        public Decimal TotalDebt { get; set; }        
    }
}