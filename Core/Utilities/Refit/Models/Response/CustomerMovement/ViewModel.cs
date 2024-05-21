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

        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }

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

        [JsonProperty("CreateDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("UpdateUserId")]
        public int UpdateUserId { get; set; }

        [JsonProperty("UpdateUserName")]
        public string UpdateUserName { get; set; }

        [JsonProperty("Deleted")]
        public bool? Deleted { get; set; }

        [JsonProperty("SaleId")]
        public int? SaleId { get; set; }

        [JsonProperty("PaymentType")]
        public byte? PaymentType { get; set; }

        [JsonProperty("ComplateType")]
        public byte? ComplateType { get; set; }
    }
}