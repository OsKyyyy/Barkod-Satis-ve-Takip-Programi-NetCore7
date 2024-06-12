using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.WebScraping.Models
{
    public class SavePhotoRequestModel
    {
        public string ImgUrl { get; set; }
        public string Barcode { get; set; }
    }
}
