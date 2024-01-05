using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.CustomerMovement
{
    public class ViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("CustomerId")]
        public int CustomerId { get; set; }
        
        [JsonProperty("ProcessType")]
        public byte ProcessType { get; set; }
        
        [JsonProperty("Amount")]
        public Decimal Amount { get; set; }

        [JsonProperty("ProductInformation")]
        public string? ProductInformation { get; set; }
        
        [JsonProperty("Note")]
        public string? Note { get; set; }

        [JsonProperty("Status")]
        public bool Status { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty("UpdateUserName")]
        public string UpdateUserName { get; set; }
    }
}