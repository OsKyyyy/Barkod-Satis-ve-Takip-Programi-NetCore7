using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.Product
{
    public class UpdateImageRequestModel
    {
        public string Barcode { get; set; }

        public string Image { get; set; }      
    }
}
