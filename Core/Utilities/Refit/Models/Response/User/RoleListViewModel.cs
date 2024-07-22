using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.User
{
    public class RoleListViewModel
    {
        [JsonProperty("OperationClaimId")]
        public int OperationClaimId { get; set; }

        [JsonProperty("RoleName")]
        public string RoleName { get; set; }

        [JsonProperty("PageNames")]
        public List<string> PageNames { get; set; }

        [JsonProperty("UserCount")]
        public int UserCount { get; set; }

    }
}