using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.Pos
{
    public class FindPriceRequestModel
    {
        public string Barcode { get; set; }
    }
}
