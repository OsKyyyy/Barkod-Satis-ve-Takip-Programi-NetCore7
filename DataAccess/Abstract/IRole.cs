using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Category;

namespace DataAccess.Abstract
{
    public interface IRoleDal
    {
        bool HasAccessToPage(int userId, string pageName);
    }
}
