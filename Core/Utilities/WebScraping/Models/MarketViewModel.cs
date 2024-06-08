using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.WebScraping.Models
{
    public class MarketViewModel
    {
        public string? ProductLogo { get; set; }
        public string? ProductName { get; set; }
        public string? OldPrice { get; set; }
        public string? NewPrice { get; set; }       
        public string? MarketInfo { get; set; }
    }
}
