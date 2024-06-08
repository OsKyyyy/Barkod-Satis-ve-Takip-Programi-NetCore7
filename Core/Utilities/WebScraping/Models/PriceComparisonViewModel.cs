using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.WebScraping.Models
{
    public class PriceComparisonViewModel
    {
        public string? MarketLogo { get; set; }
        public string? BarcodeLogo { get; set; }
        public string? ProductLogo { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPrice { get; set; }
        public string? LastPriceChangeDate { get; set; }
    }
}
