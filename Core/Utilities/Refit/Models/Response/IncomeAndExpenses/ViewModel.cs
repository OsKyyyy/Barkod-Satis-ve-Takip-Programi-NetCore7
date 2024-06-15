using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.IncomeAndExpenses
{
    public class ViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("IncomeExpensesTypeId")]
        public int IncomeExpensesTypeId { get; set; }

        [JsonProperty("IncomeExpensesTypeName")]
        public string IncomeExpensesTypeName { get; set; }

        [JsonProperty("Type")]
        public bool Type { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Amount")]
        public Decimal Amount { get; set; }

        [JsonProperty("Status")]
        public bool Status { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty("UpdateUserName")]
        public string UpdateUserName { get; set; }

        [JsonProperty("Deleted")]
        public bool Deleted { get; set; }
    }
}