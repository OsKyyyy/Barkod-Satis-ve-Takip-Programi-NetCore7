using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.Sale
{
    public class ViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("CustomerId")]
        public int CustomerId { get; set; }

        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }

        [JsonProperty("Amount")]
        public Decimal Amount { get; set; }

        [JsonProperty("PaymentType")]
        public byte PaymentType { get; set; }

        [JsonProperty("CreateUserId")]
        public int CreateUserId { get; set; }

        [JsonProperty("CreateUserName")]
        public string CreateUserName { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("Deleted")]
        public bool? Deleted { get; set; }
    }
}