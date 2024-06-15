using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.IncomeAndExpenses
{
    public class UpdateRequestModel
    {
        public int Id { get; set; }

        public int IncomeExpensesTypeId { get; set; }

        public bool Type { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^\d{0,8}(\.\d{1,2})?$", ErrorMessage = "Tutar 0.00 - 9999999999.99 aralığında olmalıdır. ")]
        public string Amount { get; set; }

        public bool Status { get; set; }

        public int UpdateUserId { get; set; }
    }
}
