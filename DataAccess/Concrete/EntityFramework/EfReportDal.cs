﻿using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Report;
using SaleViewModel = Core.Utilities.Refit.Models.Response.Sale.ViewModel;
using CustomerMovementViewModel = Core.Utilities.Refit.Models.Response.CustomerMovement.ViewModel;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReportDal : EfEntityRepositoryBase<Sale, DataBaseContext>, IReportDal
    {
        public List<SalesReportViewModel> SalesReport()
        {
            using (var context = new DataBaseContext())
            {

                var data = context.Sales
                .GroupBy(s => s.CreateDate.Date)
                .Select(s => new SalesReportViewModel
                {
                    CreateDate = s.Key,
                    Amount = s.Where(x => x.Deleted == false).Sum(s => s.Amount),
                    Piece = s.Count(x => x.Deleted == false),
                    DeletedPiece = s.Count(x => x.Deleted == true) 
                })
                .OrderByDescending(d => d.CreateDate.Date)
                .ToList();

                return data;
            }
        }

        public List<SalesDetailReportViewModel> SalesDetailReport(DateTime date)
        {
            using (var context = new DataBaseContext())
            {
                var data = context.Sales.GroupJoin(
                    context.Customers,
                    s => s.CustomerId,
                    c => c.Id,
                    (s, c) => new { s, c })
                    .Select(sale =>
                        new SalesDetailReportViewModel
                        {
                            Id = sale.s.Id,
                            CreateDate = sale.s.CreateDate,
                            CustomerId = sale.c.FirstOrDefault().Id,
                            CustomerName = sale.c.FirstOrDefault().Name,
                            Amount = sale.s.Amount,
                            PaymentType = sale.s.PaymentType,
                            Deleted = sale.s.Deleted
                        }
                    )
                    .Where(s => s.CreateDate.Date == date.Date)
                    .OrderByDescending(s => s.CreateDate)
                    .ToList();

                return data;
            }
        }

        public List<SalesProductsDetailReportViewModel> SalesProductsDetailReport(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = from sp in context.SaleProducts
                                join sa in context.Sales on sp.SaleId equals sa.Id into salesGroup
                                from sa in salesGroup.DefaultIfEmpty()
                                join cu in context.Customers on sa.CustomerId equals cu.Id into customersGroup
                                from cu in customersGroup.DefaultIfEmpty()
                                join pr in context.Products on sp.Barcode equals pr.Barcode into productsGroup
                                from pr in productsGroup.DefaultIfEmpty()
                                where sa != null && sa.Id == id
                                select new SalesProductsDetailReportViewModel
                                {
                                    Id = sp.Id,
                                    SaleId = sp.SaleId,
                                    ProductId = pr.Id,
                                    ProductImage = pr.Image,
                                    ProductBarcode = sp.Barcode,
                                    ProductName = sp.ProductName,
                                    ProductUnitPrice = sp.ProductUnitPrice,
                                    ProductQuantity = sp.ProductQuantity,
                                    Amount = sa.Amount,
                                    PaymentType = sa.PaymentType,
                                    CreateDate = sa.CreateDate,
                                    Deleted = sa.Deleted,
                                    CustomerId = cu.Id,
                                    CustomerName = cu.Name,
                                    CustomerAddress = cu.Address,
                                    CustomerPhone = cu.Phone,
                                    CustomerEmail = cu.Email
                                };

                return result.ToList();
            }
        }

        public SalesDetailReportViewModel SalesDetailReportById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var data = context.Sales.GroupJoin(
                    context.Customers,
                    s => s.CustomerId,
                    c => c.Id,
                    (s, c) => new { s, c })
                    .Select(sale =>
                        new SalesDetailReportViewModel
                        {
                            Id = sale.s.Id,
                            CreateDate = sale.s.CreateDate,
                            CustomerId = sale.c.FirstOrDefault().Id,
                            CustomerName = sale.c.FirstOrDefault().Name,
                            Amount = sale.s.Amount,
                            PaymentType = sale.s.PaymentType,
                            Deleted = sale.s.Deleted
                        }
                    )
                    .Where(s => s.Id == id)
                    .FirstOrDefault();

                return data;
            }
        }

        public void SalesDelete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Sales.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;

                context.SaveChanges();
            }
        }

        public void UpdateStock(string barcode, int quantity)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.FirstOrDefault(x => x.Barcode == barcode);

                result.Stock += quantity;

                context.SaveChanges();
            }
        }

        public List<SaleViewModel> GetLastCustomerWithDebt()
        {
            using (var context = new DataBaseContext())
            {

                var result = context.Sales
                    .Join(context.Customers,
                    s => s.CustomerId,
                    c => c.Id, (s, c) => new { s, c })
                    .Join(context.Users,
                    su => su.s.CreateUserId,
                    u => u.Id, (su, u) => new { su, u })
                    .Where(x => x.su.s.Deleted == false && x.su.s.CustomerId != null)
                    .Select(l => new SaleViewModel
                    {
                        Id = l.su.s.Id,
                        CustomerId = l.su.c.Id,
                        CustomerName = l.su.c.Name,
                        Amount = l.su.s.Amount,
                        PaymentType = l.su.s.PaymentType,
                        CreateUserId = l.u.Id,
                        CreateUserName = l.u.FirstName + " " + l.u.LastName,
                        CreateDate = l.su.s.CreateDate,
                        Deleted = l.su.s.Deleted,
                        
                    })
                    .OrderByDescending(x => x.CreateDate)
                    .Take(1000)
                    .ToList();

                return result;
            }
        }

        public List<CustomerMovementViewModel> GetLastCustomerWithDebtPayment()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.CustomerMovements
                    .Join(context.Customers,
                    s => s.CustomerId,
                    c => c.Id, (s, c) => new { s, c })
                    .Join(context.Users,
                    su => su.s.CreateUserId,
                    u => u.Id, (su, u) => new { su, u })
                    .Where(x => x.su.s.Deleted == false && x.su.s.ProcessType == 2)
                    .Select(l => new CustomerMovementViewModel
                    {
                        Id = l.su.s.Id,
                        CustomerId = l.su.c.Id,
                        CustomerName = l.su.c.Name,
                        Amount = l.su.s.Amount,
                        ProcessType = l.su.s.ProcessType,
                        Note = l.su.s.Note,
                        UpdateUserId = l.su.s.Id,
                        UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                        UpdateDate = l.su.s.UpdateDate,
                        CreateDate = l.su.s.CreateDate,
                        Deleted = l.su.s.Deleted,

                    })
                    .OrderByDescending(x => x.CreateDate)
                    .Take(1000)
                    .ToList();

                return result;
            }
        }

        public CustomerTotalDebtViewModel GetCustomerTotalDebt()
        {
            using (var context = new DataBaseContext())
            {
                var totalDebt = context.CustomerMovements
                    .Where(cm => cm.Deleted == false)
                    .Sum(cm => cm.ProcessType != 1 ? cm.Amount : -cm.Amount);

                var result = new CustomerTotalDebtViewModel
                {
                    TotalDebt = totalDebt
                };

                return result;
            }
        }
        
        public List<CustomerDebtViewModel> GetCustomerDebt()
        {
            using (var context = new DataBaseContext())
            {
                var data = context.CustomerMovements
                    .Where(cm => cm.Deleted == false)
                    .GroupBy(cm => cm.CustomerId)
                    .Select(group => new
                    {
                        CustomerId = group.Key,
                        TotalDebt = group.Sum(cm => cm.ProcessType != 1 ? -cm.Amount : cm.Amount),
                        LastPaymentDate = group.Where(cm => cm.ProcessType == 2)
                                               .Max(cm => cm.CreateDate),
                        LastPaymentAmount = group.Where(cm => cm.ProcessType == 2)
                                                 .OrderByDescending(cm => cm.CreateDate)
                                                 .Select(cm => cm.Amount)
                                                 .FirstOrDefault()
                    })
                    .OrderByDescending(cm => cm.TotalDebt)
                    .Join(context.Customers,
                          cm => cm.CustomerId,
                          c => c.Id,
                          (cm, c) => new
                          {
                              cm,
                              c.Name,
                              c.Id,
                              c.CreateUserId
                          })
                    .Join(context.Users,
                          cmc => cmc.CreateUserId,
                          u => u.Id,
                          (cmc, u) => new CustomerDebtViewModel
                          {
                              UserId = u.Id,
                              UserName = u.FirstName + " " + u.LastName,
                              CustomerId = cmc.Id,
                              CustomerName = cmc.Name,
                              TotalDebt = cmc.cm.TotalDebt,
                              LastPaymentDate = cmc.cm.LastPaymentDate,
                              LastPaymentAmount = cmc.cm.LastPaymentAmount
                          })
                    .ToList();

                return data;
            }
        }

        public List<CustomerNonPayersViewModel> GetCustomerNonPayers()
        {
            using (var context = new DataBaseContext())
            {
                var customerMovementsQuery = context.CustomerMovements
                    .Where(cm => cm.Deleted == false)
                    .GroupBy(cm => cm.CustomerId)
                    .Select(grouped => new
                    {
                        CustomerId = grouped.Key,
                        LastPaymentDate = grouped.Max(cm => cm.CreateDate),
                        LastPaymentAmount = context.CustomerMovements
                            .Where(innerCM => innerCM.ProcessType == 2 && innerCM.CustomerId == grouped.Key)
                            .OrderByDescending(innerCM => innerCM.CreateDate)
                            .Select(innerCM => innerCM.Amount)
                            .FirstOrDefault(),
                        TotalDebt = grouped.Sum(cm => cm.ProcessType != 1 ? -cm.Amount : cm.Amount)
                    });

                var customersQuery = customerMovementsQuery
                    .AsEnumerable()
                    .Select(cm => new
                    {
                        cm.CustomerId,
                        cm.LastPaymentDate,
                        cm.LastPaymentAmount,
                        DaysSinceLastPayment = (DateTime.Now - cm.LastPaymentDate).Days,
                        cm.TotalDebt
                    })
                    .Where(result => result.TotalDebt > 0)
                    .OrderByDescending(result => result.DaysSinceLastPayment)
                    .Join(context.Customers,
                        cm => cm.CustomerId,
                        c => c.Id,
                        (cm, c) => new
                        {
                            cm.CustomerId,
                            cm.LastPaymentDate,
                            cm.LastPaymentAmount,
                            cm.DaysSinceLastPayment,
                            cm.TotalDebt,
                            CustomerName = c.Name,
                            c.CreateUserId
                        });

                var usersQuery = customersQuery
                    .Join(context.Users,
                        c => c.CreateUserId,
                        u => u.Id,
                        (c, u) => new CustomerNonPayersViewModel
                        {
                            UserId = u.Id,
                            UserName = u.FirstName + " " + u.LastName,
                            CustomerId = c.CustomerId,
                            CustomerName = c.CustomerName,
                            TotalDebt = c.TotalDebt,
                            LastPaymentDate = c.LastPaymentDate,
                            LastPaymentAmount = c.LastPaymentAmount,
                            DaysSinceLastPayment = c.DaysSinceLastPayment
                        })
                    .ToList();

                return usersQuery;
            }
        }
    }
}
