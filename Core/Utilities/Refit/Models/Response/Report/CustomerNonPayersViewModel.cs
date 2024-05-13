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
    public class CustomerNonPayersViewModel
    {
        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("CustomerId")]
        public int CustomerId { get; set; }

        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }        

        [JsonProperty("DaysSinceLastPayment")]
        public int DaysSinceLastPayment { get; set; }

        [JsonProperty("LastPaymentDate")]
        public DateTime LastPaymentDate { get; set; }

        [JsonProperty("LastPaymentAmount")]
        public Decimal LastPaymentAmount { get; set; }

        [JsonProperty("TotalDebt")]
        public Decimal TotalDebt { get; set; }
    }
}