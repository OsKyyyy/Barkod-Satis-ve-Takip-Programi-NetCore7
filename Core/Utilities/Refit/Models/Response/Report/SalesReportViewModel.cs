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
    [Keyless]
    public class SalesReportViewModel
    {
        [JsonProperty("CreateDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("Amount")]
        public Decimal Amount { get; set; }

        [JsonProperty("Piece")]
        public int Piece { get; set; }
    }
}