using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Utilities.Refit.Models.Response.Report
{
    public class SalesProductsDetailReportViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("SaleId")]
        public int SaleId { get; set; }

        [JsonProperty("ProductBarcode")]
        public string ProductBarcode { get; set; }

        [JsonProperty("ProductId")]
        public int ProductId { get; set; }

        [JsonProperty("ProductImage")]
        public string? ProductImage { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("ProductUnitPrice")]
        public Decimal ProductUnitPrice { get; set; }

        [JsonProperty("ProductQuantity")]
        public int ProductQuantity { get; set; }       

        [JsonProperty("Amount")]
        public Decimal Amount { get; set; }

        [JsonProperty("PaymentType")]
        public byte PaymentType { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("Deleted")]
        public bool? Deleted { get; set; }

        [JsonProperty("CustomerId")]
        public int? CustomerId { get; set; }

        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }

        [JsonProperty("CustomerAddress")]
        public string CustomerAddress { get; set; }

        [JsonProperty("CustomerPhone")]
        public string? CustomerPhone { get; set; }

        [JsonProperty("CustomerEmail")]
        public string? CustomerEmail { get; set; }
    }
}