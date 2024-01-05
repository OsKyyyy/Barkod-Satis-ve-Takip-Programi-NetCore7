using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.Product
{
    public class ViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
        
        [JsonProperty("PurchasePrice")]
        public Decimal PurchasePrice { get; set; }
        
        [JsonProperty("SalePrice")]
        public Decimal SalePrice { get; set; }
        
        [JsonProperty("PreviousSellingPrice")]
        public Decimal PreviousSellingPrice { get; set; }
        
        [JsonProperty("Barcode")]
        public string Barcode { get; set; }
        
        [JsonProperty("Stock")]
        public int Stock { get; set; }
        
        [JsonProperty("Image")]
        public string? Image { get; set; }
        
        [JsonProperty("Favorite")]
        public bool Favorite { get; set; }
        
        [JsonProperty("Status")]
        public bool Status { get; set; }
        
        [JsonProperty("UpdateDate")]
        public DateTime UpdateDate { get; set; }
        
        [JsonProperty("UpdateUserId")]
        public int UpdateUserId { get; set; }

        [JsonProperty("UpdateUserName")]
        public string UpdateUserName { get; set; }

        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
        
        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }
    }
}