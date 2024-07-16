using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Core.Entities;
using Core.Utilities.Refit.Models.Response.Category;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRoleDal : IRoleDal
    {
        private readonly DataBaseContext _context;

        public EfRoleDal(DataBaseContext context)
        {
            _context = context;
        }

        public bool HasAccessToPage(int userId, string pageName)
        {
            var operationClaimIds = _context.PageClaims
                .Where(pc => pc.PageName == pageName)
                .Select(pc => pc.OperationClaimId)
                .ToList();

            var userOperationClaims = _context.UserOperationClaims
                .Where(uoc => uoc.UserId == userId)
                .Select(uoc => uoc.OperationClaimId)
                .ToList();

            return operationClaimIds.Intersect(userOperationClaims).Any();
        }
    }
}
