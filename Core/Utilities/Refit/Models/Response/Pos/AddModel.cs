using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.Pos
{
    public class AddModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("ProductUnitPrice")]
        public Decimal ProductUnitPrice { get; set; } 
        
        [JsonProperty("ProductQuantity")]
        public int ProductQuantity { get; set; }
    }
}
