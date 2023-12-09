using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.Category
{
    public class ViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Status")]
        public bool Status { get; set; }

        [JsonProperty("Status")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty("Status")]
        public string UpdateUserName { get; set; }
    }
}