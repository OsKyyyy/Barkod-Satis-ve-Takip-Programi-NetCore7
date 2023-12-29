﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Response.WholeSaler
{
    public class ViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
        
        [JsonProperty("AuthorizedPerson")]
        public string AuthorizedPerson { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Status")]
        public bool Status { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty("UpdateUserName")]
        public string UpdateUserName { get; set; }
    }
}