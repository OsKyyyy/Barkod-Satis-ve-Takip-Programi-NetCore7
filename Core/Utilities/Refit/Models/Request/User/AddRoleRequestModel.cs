using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.User
{
    public class AddRoleRequestModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public List<string> SelectedItems { get; set; }
    }
}
