using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.WebScraping.Models
{
    public class PriceProduceMarketViewModel
    {
        public string? UnitName { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPrice { get; set; }
    }
}
