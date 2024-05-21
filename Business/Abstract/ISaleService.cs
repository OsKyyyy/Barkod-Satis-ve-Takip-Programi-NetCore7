﻿using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.Sale;

namespace Business.Abstract
{
    public interface ISaleService
    {
        IResult Add(SaleAddDto sale);
        IResult Delete(int id);
        IDataResult<ViewModel> ListById(int id);
    }
}
