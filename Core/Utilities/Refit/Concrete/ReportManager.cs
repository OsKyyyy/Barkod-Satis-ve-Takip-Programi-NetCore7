﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Report;
using Newtonsoft.Json;
using Refit;

namespace Core.Utilities.Refit.Concrete
{
    public class ReportManager : IReport
    {
        private IReport myAPI = RestService.For<IReport>("http://localhost:63067/api");       

        public async Task<DataResult<List<SalesReportViewModel>>> SalesReport([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<SalesReportViewModel>> dataResult = new DataResult<List<SalesReportViewModel>>();

            try
            {
                dataResult = await myAPI.SalesReport(token);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }

        public async Task<DataResult<List<SalesDetailReportViewModel>>> SalesDetailReport([Header("Authorization")] string token,DateTime date)
        {
            DataResult<List<SalesDetailReportViewModel>> dataResult = new DataResult<List<SalesDetailReportViewModel>>();

            try
            {
                dataResult = await myAPI.SalesDetailReport(token,date);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }

        public async Task<DataResult<List<SalesProductsDetailReportViewModel>>> SalesProductsDetailReport([Header("Authorization")] string token, int id)
        {
            DataResult<List<SalesProductsDetailReportViewModel>> dataResult = new DataResult<List<SalesProductsDetailReportViewModel>>();

            try
            {
                dataResult = await myAPI.SalesProductsDetailReport(token, id);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }

        public async Task<Result> SalesDelete([Header("Authorization: Bearer")] string token, int id)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.SalesDelete(token, id);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }
    }
}
