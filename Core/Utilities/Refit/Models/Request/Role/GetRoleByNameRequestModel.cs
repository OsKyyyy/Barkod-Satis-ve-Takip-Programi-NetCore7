using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.Role
{
    public class GetRoleByNameRequestModel
    {
        public string Name { get; set; }
    }
}
