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
    public class SalesDetailReportViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("CustomerId")]
        public int? CustomerId { get; set; }

        [JsonProperty("CustomerName")]
        public string? CustomerName { get; set; }

        [JsonProperty("Amount")]
        public Decimal Amount { get; set; }

        [JsonProperty("PaymentType")]
        public byte PaymentType { get; set; }

        [JsonProperty("Deleted")]
        public bool? Deleted { get; set; }
    }
}