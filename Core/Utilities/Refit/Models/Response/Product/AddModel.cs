using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.Product
{
    public class AddModel
    {
        public string CategoryId { get; set; }

        public string Name { get; set; }

        public string SalePrice { get; set; }
       
        public string Barcode { get; set; }

        public IFormFile Image { get; set; }

        public int Stock { get; set; }

        public bool Favorite { get; set; }

        public bool Status { get; set; }

        public int? CreateUserId { get; set; }
    }
}
