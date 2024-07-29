using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.HomePage
{
    public class StockQuantityViewModel
    {
        [JsonProperty("StockQuantity")]
        public int StockQuantity { get; set; }       
    }
}