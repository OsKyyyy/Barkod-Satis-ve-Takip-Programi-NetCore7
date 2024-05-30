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
    public class WholeSalerDebtViewModel
    {
        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("WholeSalerId")]
        public int WholeSalerId { get; set; }

        [JsonProperty("WholeSalerName")]
        public string WholeSalerName { get; set; }

        [JsonProperty("TotalDebt")]
        public Decimal TotalDebt { get; set; }

        [JsonProperty("LastPaymentAmount")]
        public Decimal? LastPaymentAmount { get; set; }

        [JsonProperty("LastPaymentDate")]
        public DateTime? LastPaymentDate { get; set; }
    }
}