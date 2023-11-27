using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Utilities.Refit.Models.Response
{
    public class ListDataResult<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
