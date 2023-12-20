using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.Pos
{
    public class ReceivedMoneyRequestModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [RegularExpression(@"^\d{0,8}(\.\d{1,2})?$", ErrorMessage = "Tutar 0.00 - 9999999999.99 aralığında olmalıdır. ")]
        public string ReceivedMoneyNumpadInput { get; set; }
    }
}
