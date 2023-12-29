using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
            serviceCollection.AddSingleton<IWholeSaler, WholeSalerManager>();
        }
    }
}
