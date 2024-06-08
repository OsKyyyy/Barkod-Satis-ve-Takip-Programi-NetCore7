using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.WebScraping.Models
{
    public class MarketListViewModel
    {
        public string? MarketLogo { get; set; }
        public string? MarketName { get; set; }
        public string? MarketUrl { get; set; }       
    }
}
