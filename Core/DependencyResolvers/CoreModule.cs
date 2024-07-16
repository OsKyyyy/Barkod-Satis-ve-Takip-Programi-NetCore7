using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Core.Utilities.WebScraping.Abstract;
using Core.Utilities.WebScraping.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Concrete;
using Core.Utilities.WebScraping;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Core;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<IUser, UserManager>();
            serviceCollection.AddSingleton<ICategory, CategoryManager>();
            serviceCollection.AddSingleton<IProduct, ProductManager>();
            serviceCollection.AddSingleton<IPos, PosManager>();
            serviceCollection.AddSingleton<ICustomer, CustomerManager>();
            serviceCollection.AddSingleton<ICustomerMovement, CustomerMovementManager>();
            serviceCollection.AddSingleton<IWholeSaler, WholeSalerManager>();
            serviceCollection.AddSingleton<IWholeSalerMovement, WholeSalerMovementManager>();
            serviceCollection.AddSingleton<ISale, SaleManager>();
            serviceCollection.AddSingleton<IReport, ReportManager>();
            serviceCollection.AddSingleton<IPriceComparisonService, PriceComparisonManager>();
            serviceCollection.AddSingleton<IPriceTrackingService, PriceTrackingManager>();
            serviceCollection.AddSingleton<IPriceProduceMarketService, PriceProduceMarketManager>();
            serviceCollection.AddSingleton<IIncomeAndExpensesType, IncomeAndExpensesTypeManager>();
            serviceCollection.AddSingleton<IIncomeAndExpenses, IncomeAndExpensesManager>();
            //serviceCollection.AddSingleton<ISaleProduct, SaleProductManager>();
            serviceCollection.AddSingleton<IRoleDal, EfRoleDal>();
        }
    }
}
