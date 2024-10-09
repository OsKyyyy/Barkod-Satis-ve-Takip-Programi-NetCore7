using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class ProductUpdateDto : IDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string PurchasePrice { get; set; }
        public string SalePrice { get; set; }
        public string Barcode { get; set; }
        public string? Image { get; set; }
        public bool ImageChanged { get; set; }
        public int Stock { get; set; }
        public int CriticalStock { get; set; }
        public bool Favorite { get; set; }
        public string? Origin { get; set; }
        public string? UnitPrice { get; set; }
        public string? UnitType { get; set; }
        public bool Status { get; set; }
        public int UpdateUserId { get; set; }
    }
}
