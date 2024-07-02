using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Report;
using SaleViewModel = Core.Utilities.Refit.Models.Response.Sale.ViewModel;
using CustomerMovementViewModel = Core.Utilities.Refit.Models.Response.CustomerMovement.ViewModel;
using WholeSalerMovementViewModel = Core.Utilities.Refit.Models.Response.WholeSalerMovement.ViewModel;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Core.Utilities.Refit.Models.Response.SaleProduct;

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

        public List<SaleViewModel> SalesDetailReport(DateTime date)
        {
            using (var context = new DataBaseContext())
            {
                var data = context.Sales.GroupJoin(
                    context.Customers,
                    s => s.CustomerId,
                    c => c.Id,
                    (s, c) => new { s, c })
                    .Select(sale =>
                        new SaleViewModel
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

        public List<CustomerMovementViewModel> GetLastCustomerWithDebt()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.CustomerMovements
                    .Where(cm => cm.Deleted == false && cm.ProcessType == 1)
                    .Join(context.Customers,
                        cm => cm.CustomerId,
                        c => c.Id,
                        (cm, c) => new { cm, c })
                    .Join(context.Users,
                        cmc => cmc.cm.CreateUserId,
                        u => u.Id,
                        (cmc, u) => new { cmc, u })
                    .GroupJoin(context.Sales,
                        cms => cms.cmc.cm.SaleId,
                        s => s.Id,
                        (cms, sales) => new { cms, sales = sales.DefaultIfEmpty() })
                    .SelectMany(
                        temp => temp.sales,
                        (temp, s) => new { temp.cms, s })
                    .Select(l => new CustomerMovementViewModel
                    {
                        Id = l.cms.cmc.cm.Id,
                        SaleId = l.s.Id,
                        CustomerId = l.cms.cmc.c.Id,
                        CustomerName = l.cms.cmc.c.Name,
                        Amount = l.cms.cmc.cm.Amount,
                        PaymentType = l.s.PaymentType,
                        ComplateType = l.s.ComplateType,
                        CreateDate = l.cms.cmc.cm.CreateDate,
                        Deleted = l.cms.cmc.cm.Deleted,
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
                    .Join(context.Customers,
                        cm => cm.CustomerId,
                        c => c.Id,
                        (cm, c) => new { cm, c })
                    .Where(x => x.cm.Deleted == false && x.c.Deleted == false)
                    .Sum(x => x.cm.ProcessType != 1 ? x.cm.Amount : -x.cm.Amount);

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
                        LastPaymentDate = group.Where(cm => cm.ProcessType == 2 && cm.CreateDate != null)
                                               .Max(cm => cm.CreateDate),
                        LastPaymentAmount = group.Where(cm => cm.ProcessType == 2 && cm.CreateDate != null)
                                                 .OrderByDescending(cm => cm.CreateDate)
                                                 .Select(cm => (decimal?)cm.Amount)
                                                 .FirstOrDefault()
                    })
                    .Where(cm => cm.TotalDebt > 0)
                    .OrderByDescending(cm => cm.TotalDebt)
                    .Join(context.Customers.Where(c => c.Deleted == false),
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
                        FirstDebtDate = context.CustomerMovements
                            .Where(innerCM => innerCM.ProcessType == 1 && innerCM.CustomerId == grouped.Key)
                            .OrderBy(innerCM => innerCM.CreateDate)
                            .Select(innerCM => innerCM.CreateDate)
                            .FirstOrDefault(),
                        LastPaymentDate = context.CustomerMovements
                            .Where(innerCM => innerCM.ProcessType == 2 && innerCM.CustomerId == grouped.Key)
                            .OrderByDescending(innerCM => innerCM.CreateDate)
                            .Select(innerCM => (DateTime?)innerCM.CreateDate)
                            .FirstOrDefault(),
                        LastPaymentAmount = context.CustomerMovements
                            .Where(innerCM => innerCM.ProcessType == 2 && innerCM.CustomerId == grouped.Key)
                            .OrderByDescending(innerCM => innerCM.CreateDate)
                            .Select(innerCM => (Decimal?)innerCM.Amount)
                            .FirstOrDefault(),
                    });

                var customersQuery = customerMovementsQuery
                    .AsEnumerable()
                    .Select(cm => new
                    {
                        cm.CustomerId,
                        cm.FirstDebtDate,
                        cm.LastPaymentDate,
                        cm.LastPaymentAmount,
                        DaysSinceLastPayment = cm.LastPaymentDate == null ? (DateTime.Now - cm.FirstDebtDate).Days : (DateTime.Now - cm.LastPaymentDate.Value).Days,
                        TotalDebt = context.CustomerMovements
                            .Where(innerCM => innerCM.CustomerId == cm.CustomerId && innerCM.Deleted == false)
                            .Sum(innerCM => innerCM.ProcessType != 1 ? -innerCM.Amount : innerCM.Amount)
                    })
                    .Where(result => result.TotalDebt > 0)
                    .OrderByDescending(result => result.DaysSinceLastPayment)
                    .Join(context.Customers,
                            cm => cm.CustomerId,
                            c => c.Id,
                            (cm, c) => new
                            {
                                cm,
                                c.Name,
                                c.Id,
                                c.CreateUserId,
                                c.Deleted // Müşterinin silinip silinmediğini kontrol etmek için ekledik
                            })
                    .Where(joined => joined.Deleted == false); // Müşterinin silinmemiş olması şartı

                var usersQuery = customersQuery
                    .Join(context.Users,
                            cmc => cmc.CreateUserId,
                            u => u.Id,
                            (cmc, u) => new CustomerNonPayersViewModel
                            {
                                UserId = u.Id,
                                UserName = u.FirstName + " " + u.LastName,
                                CustomerId = cmc.Id,
                                CustomerName = cmc.Name,
                                TotalDebt = cmc.cm.TotalDebt,
                                FirstDebtDate = cmc.cm.FirstDebtDate,
                                LastPaymentDate = cmc.cm.LastPaymentDate,
                                LastPaymentAmount = cmc.cm.LastPaymentAmount,
                                DaysSinceLastPayment = cmc.cm.DaysSinceLastPayment
                            })
                    .ToList();

                return usersQuery;
            }

            //using (var context = new DataBaseContext())
            //{
            //    var customerMovementsQuery = context.CustomerMovements
            //        .Where(cm => cm.Deleted == false)
            //        .GroupBy(cm => cm.CustomerId)
            //        .Select(grouped => new
            //        {
            //            CustomerId = grouped.Key,
            //            FirstDebtDate = context.CustomerMovements
            //                .Where(innerCM => innerCM.ProcessType == 1 && innerCM.CustomerId == grouped.Key)
            //                .OrderBy(innerCM => innerCM.CreateDate)
            //                .Select(innerCM => innerCM.CreateDate)
            //                .FirstOrDefault(),
            //            LastPaymentDate = context.CustomerMovements
            //                .Where(innerCM => innerCM.ProcessType == 2 && innerCM.CustomerId == grouped.Key)
            //                .OrderByDescending(innerCM => innerCM.CreateDate)
            //                .Select(innerCM => (DateTime?)innerCM.CreateDate)
            //                .FirstOrDefault(),
            //            LastPaymentAmount = context.CustomerMovements
            //                .Where(innerCM => innerCM.ProcessType == 2 && innerCM.CustomerId == grouped.Key)
            //                .OrderByDescending(innerCM => innerCM.CreateDate)
            //                .Select(innerCM => (Decimal?)innerCM.Amount)
            //                .FirstOrDefault(),
            //        });

            //    var customersQuery = customerMovementsQuery
            //        .AsEnumerable()
            //        .Select(cm => new
            //        {
            //            cm.CustomerId,
            //            cm.FirstDebtDate,
            //            cm.LastPaymentDate,
            //            cm.LastPaymentAmount,
            //            DaysSinceLastPayment = cm.LastPaymentDate == null ? (DateTime.Now - cm.FirstDebtDate).Days : (DateTime.Now - cm.LastPaymentDate.Value).Days,
            //            TotalDebt = context.CustomerMovements
            //                .Where(innerCM => innerCM.CustomerId == cm.CustomerId && innerCM.Deleted == false)
            //                .Sum(innerCM => innerCM.ProcessType != 1 ? -innerCM.Amount : innerCM.Amount)
            //        })
            //        .Where(result => result.TotalDebt > 0)
            //        .OrderByDescending(result => result.DaysSinceLastPayment)
            //        .Join(context.Customers,
            //                cm => cm.CustomerId,
            //                c => c.Id,
            //                (cm, c) => new
            //                {
            //                    cm,
            //                    c.Name,
            //                    c.Id,
            //                    c.CreateUserId
            //                });

            //    var usersQuery = customersQuery
            //        .Join(context.Users,
            //                cmc => cmc.CreateUserId,
            //                u => u.Id,
            //                (cmc, u) => new CustomerNonPayersViewModel
            //                {
            //                    UserId = u.Id,
            //                    UserName = u.FirstName + " " + u.LastName,
            //                    CustomerId = cmc.Id,
            //                    CustomerName = cmc.Name,
            //                    TotalDebt = cmc.cm.TotalDebt,
            //                    FirstDebtDate = cmc.cm.FirstDebtDate,
            //                    LastPaymentDate = cmc.cm.LastPaymentDate,
            //                    LastPaymentAmount = cmc.cm.LastPaymentAmount,
            //                    DaysSinceLastPayment = cmc.cm.DaysSinceLastPayment
            //                })
            //        .ToList();

            //    return usersQuery;
            //}
        }

        public CustomerTotalDebtViewModel GetCustomerThisMonthDebt()
        {
            using (var context = new DataBaseContext())
            {
                var totalDebt = context.CustomerMovements
                    .Join(context.Customers,
                        cm => cm.CustomerId,
                        c => c.Id,
                        (cm, c) => new { cm, c })
                    .Where(x => x.cm.CreateDate.Month == DateTime.Now.Month && x.cm.CreateDate.Year == DateTime.Now.Year && x.c.Deleted == false && x.cm.Deleted == false)
                    .Sum(x => x.cm.ProcessType == 1 ? x.cm.Amount : 0);
                
                var result = new CustomerTotalDebtViewModel
                {
                    TotalDebt = totalDebt
                };

                return result;
            }
        }

        public CustomerTotalDebtViewModel GetCustomerPreviousMonthDebt()
        {
            using (var context = new DataBaseContext())
            {
                var now = DateTime.Now;

                var previousMonth = now.AddMonths(-1).Month;
                var previousYear = now.AddMonths(-1).Year;

                var totalDebt = context.CustomerMovements
                     .Join(context.Customers,
                        cm => cm.CustomerId,
                        c => c.Id,
                        (cm, c) => new { cm, c })
                    .Where(x => x.cm.CreateDate.Month == previousMonth && x.cm.CreateDate.Year == previousYear && x.c.Deleted == false && x.cm.Deleted == false)
                    .Sum(x => x.cm.ProcessType == 1 ? x.cm.Amount : 0);

                var result = new CustomerTotalDebtViewModel
                {
                    TotalDebt = totalDebt
                };

                return result;
            }
        }

        public List<CustomerMonthlyDebtViewModel> GetCustomerMonthlyDebtOfOneYear()
        {
            using (var context = new DataBaseContext())
            {
                var now = DateTime.Now;
                var result = new List<CustomerMonthlyDebtViewModel>();

                for (int i = 0; i < 12; i++)
                {
                    var targetDate = now.AddMonths(-i);
                    var month = targetDate.Month;
                    var year = targetDate.Year;

                    var totalDebt = context.CustomerMovements
                         .Join(context.Customers,
                            cm => cm.CustomerId,
                            c => c.Id,
                            (cm, c) => new { cm, c })
                        .Where(x => x.cm.CreateDate.Month == month && x.cm.CreateDate.Year == year && x.c.Deleted == false && x.cm.Deleted == false)
                        .Sum(x => x.cm.ProcessType == 1 ? x.cm.Amount : 0);

                    result.Add(new CustomerMonthlyDebtViewModel
                    {
                        Year = year,
                        Month = month,
                        TotalDebt = totalDebt
                    });
                }

                return result.OrderBy(r => r.Year).ThenBy(r => r.Month).ToList();
            }
        }


        public List<WholeSalerMovementViewModel> GetLastWholeSalerWithDebt()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.WholeSalerMovements
                    .Where(wsm => wsm.Deleted == false && wsm.ProcessType == 1)
                    .Join(context.WholeSalers,
                        wsm => wsm.WholeSalerId,
                        ws => ws.Id,
                        (wsm, ws) => new { wsm, ws })
                    .Join(context.Users,
                        wsmc => wsmc.wsm.CreateUserId,
                        u => u.Id,
                        (wsmc, u) => new { wsmc, u })                    
                    .Select(x => new WholeSalerMovementViewModel
                    {
                        Id = x.wsmc.wsm.Id,
                        WholeSalerId = x.wsmc.ws.Id,
                        WholeSalerName = x.wsmc.ws.Name,
                        Amount = x.wsmc.wsm.Amount,
                        ProcessType = x.wsmc.wsm.ProcessType,
                        Image = x.wsmc.wsm.Image,
                        InvoiceDate = x.wsmc.wsm.InvoiceDate,
                        Note = x.wsmc.wsm.Note,
                        CreateDate = x.wsmc.wsm.CreateDate,
                        UpdateUserId = x.u.Id,
                        UpdateUserName = x.u.FirstName + " " + x.u.LastName,
                        Deleted = x.wsmc.wsm.Deleted,
                    })
                    .OrderByDescending(x => x.CreateDate)
                    .Take(1000)
                    .ToList();

                return result;
            }
        }

        public List<WholeSalerMovementViewModel> GetLastWholeSalerWithDebtPayment()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.WholeSalerMovements
                     .Join(context.WholeSalers,
                        wsm => wsm.WholeSalerId,
                        ws => ws.Id,
                        (wsm, ws) => new { wsm, ws })
                    .Join(context.Users,
                        wsmc => wsmc.wsm.CreateUserId,
                        u => u.Id,
                        (wsmc, u) => new { wsmc, u })         
                    .Where(z => z.wsmc.wsm.Deleted == false && z.wsmc.wsm.ProcessType == 2)
                    .Select(x => new WholeSalerMovementViewModel
                    {
                        Id = x.wsmc.wsm.Id,
                        WholeSalerId = x.wsmc.ws.Id,
                        WholeSalerName = x.wsmc.ws.Name,
                        Amount = x.wsmc.wsm.Amount,
                        ProcessType = x.wsmc.wsm.ProcessType,
                        Image = x.wsmc.wsm.Image,
                        InvoiceDate = x.wsmc.wsm.InvoiceDate,
                        Note = x.wsmc.wsm.Note,
                        CreateDate = x.wsmc.wsm.CreateDate,
                        UpdateUserId = x.u.Id,
                        UpdateUserName = x.u.FirstName + " " + x.u.LastName,
                        Deleted = x.wsmc.wsm.Deleted,

                    })
                    .OrderByDescending(x => x.CreateDate)
                    .Take(1000)
                    .ToList();

                return result;
            }
        }
        
        public WholeSalerTotalDebtViewModel GetWholeSalerTotalDebt()
        {
            using (var context = new DataBaseContext())
            {
                var totalDebt = context.WholeSalerMovements
                    .Join(context.WholeSalers,
                        wsm => wsm.WholeSalerId,
                        ws => ws.Id,
                        (wsm, ws) => new { wsm, ws })
                    .Where(x => x.wsm.Deleted == false && x.ws.Deleted == false)
                    .Sum(x => x.wsm.ProcessType != 1 ? x.wsm.Amount : -x.wsm.Amount);

                var result = new WholeSalerTotalDebtViewModel
                {
                    TotalDebt = totalDebt
                };

                return result;
            }
        }

        public List<WholeSalerDebtViewModel> GetWholeSalerDebt()
        {
            using (var context = new DataBaseContext())
            {
                var data = context.WholeSalerMovements
                    .Where(wsm => wsm.Deleted == false)
                    .GroupBy(wsm => wsm.WholeSalerId)
                    .Select(group => new
                    {
                        WholeSalerId = group.Key,
                        TotalDebt = group.Sum(wsm => wsm.ProcessType != 1 ? -wsm.Amount : wsm.Amount),
                        LastPaymentDate = group.Where(wsm => wsm.ProcessType == 2 && wsm.CreateDate != null)
                                               .Max(wsm => wsm.CreateDate),
                        LastPaymentAmount = group.Where(wsm => wsm.ProcessType == 2 && wsm.CreateDate != null)
                                                 .OrderByDescending(wsm => wsm.CreateDate)
                                                 .Select(wsm => (decimal?)wsm.Amount)
                                                 .FirstOrDefault()
                    })
                    .Where(wsm => wsm.TotalDebt > 0)
                    .OrderByDescending(wsm => wsm.TotalDebt)
                    .Join(context.WholeSalers.Where(ws => ws.Deleted == false),
                          wsm => wsm.WholeSalerId,
                          ws => ws.Id,
                          (wsm, ws) => new
                          {
                              wsm,
                              ws.Name,
                              ws.Id,
                              ws.CreateUserId
                          })
                    .Join(context.Users,
                          wsmc => wsmc.CreateUserId,
                          u => u.Id,
                          (wsmc, u) => new WholeSalerDebtViewModel
                          {
                              UserId = u.Id,
                              UserName = u.FirstName + " " + u.LastName,
                              WholeSalerId = wsmc.Id,
                              WholeSalerName = wsmc.Name,
                              TotalDebt = wsmc.wsm.TotalDebt,
                              LastPaymentDate = wsmc.wsm.LastPaymentDate,
                              LastPaymentAmount = wsmc.wsm.LastPaymentAmount
                          })
                    .ToList();

                return data;
            }
        }

        public List<WholeSalerNonPayersViewModel> GetWholeSalerNonPayers()
        {
            using (var context = new DataBaseContext())
            {
                var wholeSalerMovementsQuery = context.WholeSalerMovements
                    .Where(wsm => wsm.Deleted == false)
                    .GroupBy(wsm => wsm.WholeSalerId)
                    .Select(grouped => new
                    {
                        WholeSalerId = grouped.Key,
                        FirstDebtDate = grouped
                            .Where(innerWSM => innerWSM.ProcessType == 1)
                            .OrderBy(innerWSM => innerWSM.CreateDate)
                            .Select(innerWSM => innerWSM.CreateDate)
                            .FirstOrDefault(),
                        LastPaymentDate = grouped
                            .Where(innerWSM => innerWSM.ProcessType == 2)
                            .OrderByDescending(innerWSM => innerWSM.CreateDate)
                            .Select(innerWSM => (DateTime?)innerWSM.CreateDate)
                            .FirstOrDefault(),
                        LastPaymentAmount = grouped
                            .Where(innerWSM => innerWSM.ProcessType == 2)
                            .OrderByDescending(innerWSM => innerWSM.CreateDate)
                            .Select(innerWSM => (decimal?)innerWSM.Amount)
                            .FirstOrDefault(),
                        TotalDebt = grouped
                            .Sum(innerWSM => innerWSM.ProcessType != 1 ? -innerWSM.Amount : innerWSM.Amount)
                    })
                    .Where(result => result.TotalDebt > 0);

                var wholeSalerQuery = wholeSalerMovementsQuery
                    .Join(context.WholeSalers,
                          wsm => wsm.WholeSalerId,
                          ws => ws.Id,
                          (wsm, ws) => new { wsm, ws })
                    .Where(joined => joined.ws.Deleted == false) // Toptancının silinmemiş olması şartı
                    .Select(joined => new
                    {
                        joined.wsm.WholeSalerId,
                        joined.wsm.FirstDebtDate,
                        joined.wsm.LastPaymentDate,
                        joined.wsm.LastPaymentAmount,
                        DaysSinceLastPayment = joined.wsm.LastPaymentDate == null ? (DateTime.Now - joined.wsm.FirstDebtDate).Days : (DateTime.Now - joined.wsm.LastPaymentDate.Value).Days,
                        joined.wsm.TotalDebt,
                        joined.ws.Name,
                        joined.ws.Id,
                        joined.ws.CreateUserId
                    });

                var usersQuery = wholeSalerQuery
                    .Join(context.Users,
                          wsmc => wsmc.CreateUserId,
                          u => u.Id,
                          (wsmc, u) => new WholeSalerNonPayersViewModel
                          {
                              UserId = u.Id,
                              UserName = u.FirstName + " " + u.LastName,
                              WholeSalerId = wsmc.Id,
                              WholeSalerName = wsmc.Name,
                              TotalDebt = wsmc.TotalDebt,
                              FirstDebtDate = wsmc.FirstDebtDate,
                              LastPaymentDate = wsmc.LastPaymentDate,
                              LastPaymentAmount = wsmc.LastPaymentAmount,
                              DaysSinceLastPayment = wsmc.DaysSinceLastPayment
                          })
                    .ToList();

                return usersQuery;
            }
            //using (var context = new DataBaseContext())
            //{
            //    var wholeSalerMovementsQuery = context.WholeSalerMovements
            //        .Where(wsm => wsm.Deleted == false)
            //        .GroupBy(wsm => wsm.WholeSalerId)
            //        .Select(grouped => new
            //        {
            //            WholeSalerId = grouped.Key,
            //            FirstDebtDate = context.WholeSalerMovements
            //                .Where(innerWSM => innerWSM.ProcessType == 1 && innerWSM.WholeSalerId == grouped.Key)
            //                .OrderBy(innerWSM => innerWSM.CreateDate)
            //                .Select(innerWSM => innerWSM.CreateDate)
            //                .FirstOrDefault(),
            //            LastPaymentDate = context.WholeSalerMovements
            //                .Where(innerWSM => innerWSM.ProcessType == 2 && innerWSM.WholeSalerId == grouped.Key)
            //                .OrderByDescending(innerWSM => innerWSM.CreateDate)
            //                .Select(innerWSM => (DateTime?)innerWSM.CreateDate)
            //                .FirstOrDefault(),
            //            LastPaymentAmount = context.WholeSalerMovements
            //                .Where(innerWSM => innerWSM.ProcessType == 2 && innerWSM.WholeSalerId == grouped.Key)
            //                .OrderByDescending(innerWSM => innerWSM.CreateDate)
            //                .Select(innerWSM => (Decimal?)innerWSM.Amount)
            //                .FirstOrDefault(),
            //        });

            //    var wholeSalerQuery = wholeSalerMovementsQuery
            //        .AsEnumerable()
            //        .Select(wsm => new
            //        {
            //            wsm.WholeSalerId,
            //            wsm.FirstDebtDate,
            //            wsm.LastPaymentDate,
            //            wsm.LastPaymentAmount,
            //            DaysSinceLastPayment = wsm.LastPaymentDate == null ? (DateTime.Now - wsm.FirstDebtDate).Days : (DateTime.Now - wsm.LastPaymentDate.Value).Days,
            //            TotalDebt = context.WholeSalerMovements
            //                .Where(innerWSM => innerWSM.WholeSalerId == wsm.WholeSalerId && innerWSM.Deleted == false)
            //                .Sum(innerWSM => innerWSM.ProcessType != 1 ? -innerWSM.Amount : innerWSM.Amount)
            //        })
            //        .Where(result => result.TotalDebt > 0)
            //        .OrderByDescending(result => result.DaysSinceLastPayment)
            //        .Join(context.WholeSalers,
            //                wsm => wsm.WholeSalerId,
            //                ws => ws.Id,
            //                (wsm, ws) => new
            //                {
            //                    wsm,
            //                    ws.Name,
            //                    ws.Id,
            //                    ws.CreateUserId
            //                });

            //    var usersQuery = wholeSalerQuery
            //        .Join(context.Users,
            //                wsmc => wsmc.CreateUserId,
            //                u => u.Id,
            //                (wsmc, u) => new WholeSalerNonPayersViewModel
            //                {
            //                    UserId = u.Id,
            //                    UserName = u.FirstName + " " + u.LastName,
            //                    WholeSalerId = wsmc.Id,
            //                    WholeSalerName = wsmc.Name,
            //                    TotalDebt = wsmc.wsm.TotalDebt,
            //                    FirstDebtDate = wsmc.wsm.FirstDebtDate,
            //                    LastPaymentDate = wsmc.wsm.LastPaymentDate,
            //                    LastPaymentAmount = wsmc.wsm.LastPaymentAmount,
            //                    DaysSinceLastPayment = wsmc.wsm.DaysSinceLastPayment
            //                })
            //        .ToList();

            //    return usersQuery;
            //}
        }

        public WholeSalerTotalDebtViewModel GetWholeSalerThisMonthDebt()
        {
            using (var context = new DataBaseContext())
            {
                var totalDebt = context.WholeSalerMovements
                    .Join(context.WholeSalers,
                        wsm => wsm.WholeSalerId,
                        ws => ws.Id,
                        (wsm, ws) => new { wsm, ws })
                    .Where(x => x.wsm.CreateDate.Month == DateTime.Now.Month && x.wsm.CreateDate.Year == DateTime.Now.Year && x.ws.Deleted == false && x.wsm.Deleted == false)
                    .Sum(x => x.wsm.ProcessType == 1 ? x.wsm.Amount : 0);

                var result = new WholeSalerTotalDebtViewModel
                {
                    TotalDebt = totalDebt
                };

                return result;
            }
        }

        public WholeSalerTotalDebtViewModel GetWholeSalerPreviousMonthDebt()
        {
            using (var context = new DataBaseContext())
            {
                var now = DateTime.Now;

                var previousMonth = now.AddMonths(-1).Month;
                var previousYear = now.AddMonths(-1).Year;

                var totalDebt = context.WholeSalerMovements
                    .Join(context.WholeSalers,
                        wsm => wsm.WholeSalerId,
                        ws => ws.Id,
                        (wsm, ws) => new { wsm, ws })
                    .Where(x => x.wsm.CreateDate.Month == previousMonth && x.wsm.CreateDate.Year == previousYear && x.ws.Deleted == false && x.wsm.Deleted == false)
                    .Sum(x => x.wsm.ProcessType == 1 ? x.wsm.Amount : 0);

                var result = new WholeSalerTotalDebtViewModel
                {
                    TotalDebt = totalDebt
                };

                return result;
            }
        }

        public List<WholeSalerMonthlyDebtViewModel> GetWholeSalerMonthlyDebtOfOneYear()
        {
            using (var context = new DataBaseContext())
            {
                var now = DateTime.Now;
                var result = new List<WholeSalerMonthlyDebtViewModel>();

                for (int i = 0; i < 12; i++)
                {
                    var targetDate = now.AddMonths(-i);
                    var month = targetDate.Month;
                    var year = targetDate.Year;

                    var totalDebt = context.WholeSalerMovements
                         .Join(context.WholeSalers,
                            wsm => wsm.WholeSalerId,
                            ws => ws.Id,
                            (wsm, ws) => new { wsm, ws })
                        .Where(x => x.wsm.CreateDate.Month == month && x.wsm.CreateDate.Year == year && x.ws.Deleted == false && x.wsm.Deleted == false)
                        .Sum(x => x.wsm.ProcessType == 1 ? x.wsm.Amount : 0);

                    result.Add(new WholeSalerMonthlyDebtViewModel
                    {
                        Year = year,
                        Month = month,
                        TotalDebt = totalDebt
                    });
                }

                return result.OrderBy(r => r.Year).ThenBy(r => r.Month).ToList();
            }
        }


        public IncomeExpensesTotalViewModel MonthlyExternalIncome()
        {
            using (var context = new DataBaseContext())
            {
                var total = context.IncomeAndExpenses                   
                    .Where(x => x.Deleted == false 
                            && x.Type == true
                            && x.CreateDate.Month == DateTime.Now.Month
                            && x.CreateDate.Year == DateTime.Now.Year
                        )
                    .Sum(x => x.Amount);

                var totalPrevious = context.IncomeAndExpenses
                   .Where(x => x.Deleted == false
                           && x.Type == true
                           && x.CreateDate.Month == DateTime.Now.AddMonths(-1).Month
                           && x.CreateDate.Year == DateTime.Now.AddMonths(-1).Year
                       )
                   .Sum(x => x.Amount);

                var result = new IncomeExpensesTotalViewModel
                {
                    Total = total,
                    TotalPrevious = totalPrevious
                };

                return result;
            }
        }

        public IncomeExpensesTotalViewModel MonthlyExternalExpenses()
        {
            using (var context = new DataBaseContext())
            {
                var total = context.IncomeAndExpenses
                    .Where(x => x.Deleted == false
                            && x.Type == false
                            && x.CreateDate.Month == DateTime.Now.Month
                            && x.CreateDate.Year == DateTime.Now.Year
                        )
                    .Sum(x => x.Amount);

                var totalPrevious = context.IncomeAndExpenses
                   .Where(x => x.Deleted == false
                           && x.Type == false
                           && x.CreateDate.Month == DateTime.Now.AddMonths(-1).Month
                           && x.CreateDate.Year == DateTime.Now.AddMonths(-1).Year
                       )
                   .Sum(x => x.Amount);

                var result = new IncomeExpensesTotalViewModel
                {
                    Total = total,
                    TotalPrevious = totalPrevious
                };
                
                return result;
            }
        }

        public IncomeExpensesTotalViewModel MonthlySalesIncome()
        {
            using (var context = new DataBaseContext())
            {
                var total = context.Sales
                    .Where(x => x.Deleted == false
                            && x.CreateDate.Month == DateTime.Now.Month
                            && x.CreateDate.Year == DateTime.Now.Year
                        )
                    .Sum(x => x.Amount);

                var totalPrevious = context.Sales
                   .Where(x => x.Deleted == false
                           && x.CreateDate.Month == DateTime.Now.AddMonths(-1).Month
                           && x.CreateDate.Year == DateTime.Now.AddMonths(-1).Year
                       )
                   .Sum(x => x.Amount);

                var result = new IncomeExpensesTotalViewModel
                {
                    Total = total,
                    TotalPrevious = totalPrevious
                };

                return result;
            }
        }
        
        public IncomeExpensesTotalViewModel MonthlyWholeSalerExpenses()
        {
            using (var context = new DataBaseContext())
            {
                var total = context.WholeSalerMovements
                    .Where(x => x.Deleted == false
                            && x.ProcessType == 2
                            && x.CreateDate.Month == DateTime.Now.Month
                            && x.CreateDate.Year == DateTime.Now.Year
                        )
                    .Sum(x => x.Amount);

                var totalPrevious = context.WholeSalerMovements
                   .Where(x => x.Deleted == false
                           && x.ProcessType == 2
                           && x.CreateDate.Month == DateTime.Now.AddMonths(-1).Month
                           && x.CreateDate.Year == DateTime.Now.AddMonths(-1).Year
                       )
                   .Sum(x => x.Amount);

                var result = new IncomeExpensesTotalViewModel
                {
                    Total = total,
                    TotalPrevious = totalPrevious
                };

                return result;
            }
        }
    }
}
