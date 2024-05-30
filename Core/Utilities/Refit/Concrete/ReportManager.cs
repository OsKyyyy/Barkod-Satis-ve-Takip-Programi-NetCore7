using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Report;
using CustomerMovementViewModel = Core.Utilities.Refit.Models.Response.CustomerMovement.ViewModel;
using WholeSalerMovementViewModel = Core.Utilities.Refit.Models.Response.WholeSalerMovement.ViewModel;
using SaleViewModel = Core.Utilities.Refit.Models.Response.Sale.ViewModel;
using SaleProductViewModel = Core.Utilities.Refit.Models.Response.SaleProduct.ViewModel;
using Newtonsoft.Json;
using Refit;
using Core.Utilities.Refit.Models.Response.SaleProduct;

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

        public async Task<DataResult<List<SaleViewModel>>> SalesDetailReport([Header("Authorization")] string token, DateTime date)
        {
            DataResult<List<SaleViewModel>> dataResult = new DataResult<List<SaleViewModel>>();

            try
            {
                dataResult = await myAPI.SalesDetailReport(token, date);

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

        public async Task<DataResult<List<SaleProductViewModel>>> SaleProductListById([Header("Authorization")] string token, int id)
        {
            DataResult<List<SaleProductViewModel>> dataResult = new DataResult<List<SaleProductViewModel>>();

            try
            {
                dataResult = await myAPI.SaleProductListById(token, id);

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

        public async Task<DataResult<List<CustomerMovementViewModel>>> GetLastCustomerWithDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<CustomerMovementViewModel>> dataResult = new DataResult<List<CustomerMovementViewModel>>();

            try
            {
                dataResult = await myAPI.GetLastCustomerWithDebt(token);

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

        public async Task<DataResult<List<CustomerMovementViewModel>>> GetLastCustomerWithDebtPayment([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<CustomerMovementViewModel>> dataResult = new DataResult<List<CustomerMovementViewModel>>();

            try
            {
                dataResult = await myAPI.GetLastCustomerWithDebtPayment(token);

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

        public async Task<DataResult<CustomerTotalDebtViewModel>> GetCustomerTotalDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<CustomerTotalDebtViewModel> dataResult = new DataResult<CustomerTotalDebtViewModel>();

            try
            {
                dataResult = await myAPI.GetCustomerTotalDebt(token);

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

        public async Task<DataResult<List<CustomerDebtViewModel>>> GetCustomerDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<CustomerDebtViewModel>> dataResult = new DataResult<List<CustomerDebtViewModel>>();

            try
            {
                dataResult = await myAPI.GetCustomerDebt(token);

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

        public async Task<DataResult<List<CustomerNonPayersViewModel>>> GetCustomerNonPayers([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<CustomerNonPayersViewModel>> dataResult = new DataResult<List<CustomerNonPayersViewModel>>();

            try
            {
                dataResult = await myAPI.GetCustomerNonPayers(token);

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

        public async Task<DataResult<CustomerTotalDebtViewModel>> GetCustomerThisMonthDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<CustomerTotalDebtViewModel> dataResult = new DataResult<CustomerTotalDebtViewModel>();

            try
            {
                dataResult = await myAPI.GetCustomerThisMonthDebt(token);

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

        public async Task<DataResult<CustomerTotalDebtViewModel>> GetCustomerPreviousMonthDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<CustomerTotalDebtViewModel> dataResult = new DataResult<CustomerTotalDebtViewModel>();

            try
            {
                dataResult = await myAPI.GetCustomerPreviousMonthDebt(token);

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

        public async Task<DataResult<List<CustomerMonthlyDebtViewModel>>> GetCustomerMonthlyDebtOfOneYear([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<CustomerMonthlyDebtViewModel>> dataResult = new DataResult<List<CustomerMonthlyDebtViewModel>>();

            try
            {
                dataResult = await myAPI.GetCustomerMonthlyDebtOfOneYear(token);

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


        public async Task<DataResult<List<WholeSalerMovementViewModel>>> GetLastWholeSalerWithDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<WholeSalerMovementViewModel>> dataResult = new DataResult<List<WholeSalerMovementViewModel>>();

            try
            {
                dataResult = await myAPI.GetLastWholeSalerWithDebt(token);

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

        public async Task<DataResult<List<WholeSalerMovementViewModel>>> GetLastWholeSalerWithDebtPayment([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<WholeSalerMovementViewModel>> dataResult = new DataResult<List<WholeSalerMovementViewModel>>();

            try
            {
                dataResult = await myAPI.GetLastWholeSalerWithDebtPayment(token);

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

        public async Task<DataResult<WholeSalerTotalDebtViewModel>> GetWholeSalerTotalDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<WholeSalerTotalDebtViewModel> dataResult = new DataResult<WholeSalerTotalDebtViewModel>();

            try
            {
                dataResult = await myAPI.GetWholeSalerTotalDebt(token);

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

        public async Task<DataResult<List<WholeSalerDebtViewModel>>> GetWholeSalerDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<WholeSalerDebtViewModel>> dataResult = new DataResult<List<WholeSalerDebtViewModel>>();

            try
            {
                dataResult = await myAPI.GetWholeSalerDebt(token);

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

        public async Task<DataResult<List<WholeSalerNonPayersViewModel>>> GetWholeSalerNonPayers([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<WholeSalerNonPayersViewModel>> dataResult = new DataResult<List<WholeSalerNonPayersViewModel>>();

            try
            {
                dataResult = await myAPI.GetWholeSalerNonPayers(token);

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

        public async Task<DataResult<WholeSalerTotalDebtViewModel>> GetWholeSalerThisMonthDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<WholeSalerTotalDebtViewModel> dataResult = new DataResult<WholeSalerTotalDebtViewModel>();

            try
            {
                dataResult = await myAPI.GetWholeSalerThisMonthDebt(token);

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

        public async Task<DataResult<WholeSalerTotalDebtViewModel>> GetWholeSalerPreviousMonthDebt([Header("Authorization: Bearer")] string token)
        {
            DataResult<WholeSalerTotalDebtViewModel> dataResult = new DataResult<WholeSalerTotalDebtViewModel>();

            try
            {
                dataResult = await myAPI.GetWholeSalerPreviousMonthDebt(token);

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

        public async Task<DataResult<List<WholeSalerMonthlyDebtViewModel>>> GetWholeSalerMonthlyDebtOfOneYear([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<WholeSalerMonthlyDebtViewModel>> dataResult = new DataResult<List<WholeSalerMonthlyDebtViewModel>>();

            try
            {
                dataResult = await myAPI.GetWholeSalerMonthlyDebtOfOneYear(token);

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
