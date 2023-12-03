using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.User
{
    public class RegisterModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string? Phone { get; set; }

        [JsonProperty("PasswordHash")]
        public byte[] PasswordHash { get; set; }
        
        [JsonProperty("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }

        [JsonProperty("Status")]
        public bool Status { get; set; }
    }
}
