﻿using Core.Utilities.Refit.Models.Request.Pos;
using Core.Utilities.Refit.Models.Response.Pos;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;
using ProductViewModel = Core.Utilities.Refit.Models.Response.Product.ViewModel;

namespace Core.Utilities.Refit.Abstract
{
    public interface IPos
    {
        [Post("/pos/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Post("/pos/AddMoney")]
        Task<Result> AddMoney([Header("Authorization")] string token, [Body] AddMoneyRequestModel addMoneyRequestModel);

        [Get("/pos/List")]
        Task<DataResult<List<ViewModel>>> List([Header("Authorization")] string token, int createUserId);

        [Put("/pos/QuantityIncrease")]
        Task<Result> QuantityIncrease([Header("Authorization")] string token, int id, string barcode);
        
        [Put("/pos/QuantityReduce")]
        Task<Result> QuantityReduce([Header("Authorization")] string token, int id);

        [Delete("/pos/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);

        [Get("/pos/CancelSale")]
        Task<Result> CancelSale([Header("Authorization")] string token, int basket, int createUserId);
    }
}
