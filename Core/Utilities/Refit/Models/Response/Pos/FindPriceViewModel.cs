using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.Pos
{
    public class FindPriceViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Barcode")]
        public string Barcode { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("SalePrice")]
        public Decimal SalePrice { get; set; }
    }
}