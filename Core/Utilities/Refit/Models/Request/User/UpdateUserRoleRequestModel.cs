using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Models.Request.User
{
    public class UpdateUserRoleRequestModel
    {
        public int Id { get; set; }

        public int OperationClaimId { get; set; }
    }
}
