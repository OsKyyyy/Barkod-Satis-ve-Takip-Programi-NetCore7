using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.IncomeAndExpenses
{
    public class AddTypeRequestModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public bool Status { get; set; }

        public int CreateUserId { get; set; }
    }
}
