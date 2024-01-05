using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.WholeSalerMovement
{
    public class ViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("WholeSalerId")]
        public int WholeSalerId { get; set; }
        
        [JsonProperty("ProcessType")]
        public byte ProcessType { get; set; }
        
        [JsonProperty("Amount")]
        public Decimal Amount { get; set; }

        [JsonProperty("InvoiceDate")]
        public DateTime InvoiceDate { get; set; }

        [JsonProperty("Note")]
        public string? Note { get; set; }

        [JsonProperty("Image")]
        public string? Image { get; set; }

        [JsonProperty("Status")]
        public bool Status { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty("UpdateUserName")]
        public string UpdateUserName { get; set; }
    }
}