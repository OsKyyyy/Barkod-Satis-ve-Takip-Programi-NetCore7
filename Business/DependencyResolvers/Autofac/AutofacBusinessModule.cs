﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();
            
            builder.RegisterType<PosManager>().As<IPosService>();
            builder.RegisterType<EfPosDal>().As<IPosDal>();

            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();

            builder.RegisterType<CustomerMovementManager>().As<ICustomerMovementService>();
            builder.RegisterType<EfCustomerMovementDal>().As<ICustomerMovementDal>();

            builder.RegisterType<WholeSalerManager>().As<IWholeSalerService>();
            builder.RegisterType<EfWholeSalerDal>().As<IWholeSalerDal>();

            builder.RegisterType<WholeSalerMovementManager>().As<IWholeSalerMovementService>();
            builder.RegisterType<EfWholeSalerMovementDal>().As<IWholeSalerMovementDal>();

            builder.RegisterType<SaleManager>().As<ISaleService>();
            builder.RegisterType<EfSaleDal>().As<ISaleDal>();

            builder.RegisterType<ReportManager>().As<IReportService>();
            builder.RegisterType<EfReportDal>().As<IReportDal>();

            builder.RegisterType<SaleProductManager>().As<ISaleProductService>();
            builder.RegisterType<EfSaleProductDal>().As<ISaleProductDal>();

            builder.RegisterType<IncomeAndExpensesTypeManager>().As<IIncomeAndExpensesTypeService>();
            builder.RegisterType<EfIncomeAndExpensesTypeDal>().As<IIncomeAndExpensesTypeDal>();

            builder.RegisterType<IncomeAndExpensesManager>().As<IIncomeAndExpensesService>();
            builder.RegisterType<EfIncomeAndExpensesDal>().As<IIncomeAndExpensesDal>();

            builder.RegisterType<HomePageManager>().As<IHomePageService>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
