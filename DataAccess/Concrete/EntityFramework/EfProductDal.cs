﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Core.Entities;
using Core.Utilities.Refit.Models.Response.Product;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Models.Response.HomePage;
using Core.Utilities.Refit.Models.Response.Report;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, DataBaseContext>, IProductDal
    {
        public void Add(Product product)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(product).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public bool StockEntry(StockEntryRequestModel stockEntryRequestModel)
        {
            using (var context = new DataBaseContext())
            {                
                foreach (var product in stockEntryRequestModel.Product)
                {
                    var result = context.Products.FirstOrDefault(x => x.Barcode == product.Barcode);

                    if (result != null)
                    {
                        result.Stock += product.Quantity;
                        result.UpdateUserId = stockEntryRequestModel.UpdateUserId;
                    }                    
                }
                context.SaveChanges();
                return true;                                                        
            }
        }

        public void Update(Product product)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.FirstOrDefault(c => c.Id == product.Id);

                if (result.SalePrice != product.SalePrice)
                {
                    result.PreviousSellingPrice = result.SalePrice;
                }
                result.CategoryId = product.CategoryId;
                result.Name = product.Name;
                result.PurchasePrice = product.PurchasePrice;
                result.SalePrice = product.SalePrice;
                result.Barcode = product.Barcode;
                result.Stock = product.Stock;
                result.CriticalStock = product.CriticalStock;
                result.Image = product.Image;
                result.Favorite = product.Favorite;
                result.Origin = product.Origin;
                result.UnitPrice = product.UnitPrice;
                result.UnitType = product.UnitType;
                result.Status = product.Status;
                result.UpdateDate = product.UpdateDate;
                result.UpdateUserId = product.UpdateUserId;

                context.SaveChanges();
            }
        }

        public void UpdateStock(string barcode, int quantity)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.FirstOrDefault(x => x.Barcode == barcode);

                result.Stock -= quantity;

                context.SaveChanges();
            }
        }

        public void UpdateAddStock(string barcode, int quantity)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.FirstOrDefault(x => x.Barcode == barcode);

                result.Stock += quantity;

                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {

                var result = context.Products.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();
            }
        }

        public List<ViewModel> List()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.Join(context.Categories,
                    p => p.CategoryId,
                    c => c.Id, (p, c) => new { p, c }).Join(context.Users,
                    pu => pu.p.UpdateUserId,
                    u => u.Id, (pu, u) => new { pu, u }).Where(x=>x.pu.p.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.pu.p.Id,
                    Name = l.pu.p.Name,
                    PurchasePrice = l.pu.p.PurchasePrice,
                    SalePrice = l.pu.p.SalePrice,
                    PreviousSellingPrice = l.pu.p.PreviousSellingPrice,
                    Barcode = l.pu.p.Barcode,
                    Stock = l.pu.p.Stock,
                    CriticalStock = l.pu.p.CriticalStock,
                    Image = l.pu.p.Image,
                    Favorite = l.pu.p.Favorite,
                    Origin = l.pu.p.Origin,
                    UnitPrice = l.pu.p.UnitPrice,
                    UnitType = l.pu.p.UnitType,
                    Status = l.pu.p.Status,
                    UpdateDate = l.pu.p.UpdateDate,
                    UpdateUserId = l.pu.p.UpdateUserId,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                    CategoryId = l.pu.p.CategoryId,
                    CategoryName = l.pu.c.Name,
                }).ToList();

                return result;
            }
        }

        public List<ViewModel> ListByFavorite()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.Join(context.Categories,
                    p => p.CategoryId,
                    c => c.Id, (p, c) => new { p, c }).Join(context.Users,
                    pu => pu.p.UpdateUserId,
                    u => u.Id, (pu, u) => new { pu, u }).Where(x => x.pu.p.Deleted == false).Where(x => x.pu.p.Favorite == true).Select(l => new ViewModel
                {
                    Id = l.pu.p.Id,
                    Name = l.pu.p.Name,
                    PurchasePrice = l.pu.p.PurchasePrice,
                    SalePrice = l.pu.p.SalePrice,
                    PreviousSellingPrice = l.pu.p.PreviousSellingPrice,
                    Barcode = l.pu.p.Barcode,
                    Stock = l.pu.p.Stock,
                    CriticalStock = l.pu.p.CriticalStock,
                    Image = l.pu.p.Image,
                    Favorite = l.pu.p.Favorite,
					Origin = l.pu.p.Origin,
					UnitPrice = l.pu.p.UnitPrice,
					UnitType = l.pu.p.UnitType,
					Status = l.pu.p.Status,
                    UpdateDate = l.pu.p.UpdateDate,
                    UpdateUserId = l.pu.p.UpdateUserId,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                    CategoryId = l.pu.p.CategoryId,
                    CategoryName = l.pu.c.Name,
                }).ToList();

                return result;
            }
        }

        public List<ViewModel> ListByCategory(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.Join(context.Categories,
                    p => p.CategoryId,
                    c => c.Id, (p, c) => new { p, c }).Join(context.Users,
                    pu => pu.p.UpdateUserId,
                    u => u.Id, (pu, u) => new { pu, u })
                    .Where(x => x.pu.p.Deleted == false)
                    .Where(x => x.pu.p.CategoryId == id)
                    .Select(l => new ViewModel
                    {
                        Id = l.pu.p.Id,
                        Name = l.pu.p.Name,
                        PurchasePrice = l.pu.p.PurchasePrice,
                        SalePrice = l.pu.p.SalePrice,
                        PreviousSellingPrice = l.pu.p.PreviousSellingPrice,
                        Barcode = l.pu.p.Barcode,
                        Stock = l.pu.p.Stock,
                        CriticalStock = l.pu.p.CriticalStock,
                        Image = l.pu.p.Image,
                        Favorite = l.pu.p.Favorite,
						Origin = l.pu.p.Origin,
						UnitPrice = l.pu.p.UnitPrice,
						UnitType = l.pu.p.UnitType,
						Status = l.pu.p.Status,
                        UpdateDate = l.pu.p.UpdateDate,
                        UpdateUserId = l.pu.p.UpdateUserId,
                        UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                        CategoryId = l.pu.p.CategoryId,
                        CategoryName = l.pu.c.Name,
                    }).ToList();

                return result;
            }
        }

        public List<ViewModel> ListByName(string name)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.Join(context.Categories,
                    p => p.CategoryId,
                    c => c.Id, (p, c) => new { p, c }).Join(context.Users,
                    pu => pu.p.UpdateUserId,
                    u => u.Id, (pu, u) => new { pu, u }).Where(x => x.pu.p.Deleted == false).Where(x => x.pu.p.Name.Contains(name)).Select(l => new ViewModel
                {
                    Id = l.pu.p.Id,
                    Name = l.pu.p.Name,
                    PurchasePrice = l.pu.p.PurchasePrice,
                    SalePrice = l.pu.p.SalePrice,
                    PreviousSellingPrice = l.pu.p.PreviousSellingPrice,
                    Barcode = l.pu.p.Barcode,
                    Stock = l.pu.p.Stock,
                    CriticalStock = l.pu.p.CriticalStock,
                    Image = l.pu.p.Image,
                    Favorite = l.pu.p.Favorite,
					Origin = l.pu.p.Origin,
					UnitPrice = l.pu.p.UnitPrice,
					UnitType = l.pu.p.UnitType,
					Status = l.pu.p.Status,
                    UpdateDate = l.pu.p.UpdateDate,
                    UpdateUserId = l.pu.p.UpdateUserId,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                    CategoryId = l.pu.p.CategoryId,
                    CategoryName = l.pu.c.Name,
                }).ToList();

                return result;
            }
        }

        public ViewModel ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.Join(context.Categories,
                    p => p.CategoryId,
                    c => c.Id, (p, c) => new { p, c }).Join(context.Users,
                    p2 => p2.p.UpdateUserId,
                    u => u.Id, (p2, u) => new { p2, u }).Where(x => x.p2.p.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.p2.p.Id,
                    Name = l.p2.p.Name,
                    PurchasePrice = l.p2.p.PurchasePrice,
                    SalePrice = l.p2.p.SalePrice,
                    PreviousSellingPrice = l.p2.p.PreviousSellingPrice,
                    Barcode = l.p2.p.Barcode,
                    Stock = l.p2.p.Stock,
                    CriticalStock = l.p2.p.CriticalStock,
                    Image = l.p2.p.Image,
                    Favorite = l.p2.p.Favorite,
					Origin = l.p2.p.Origin,
					UnitPrice = l.p2.p.UnitPrice,
					UnitType = l.p2.p.UnitType,
					Status = l.p2.p.Status,
                    UpdateDate = l.p2.p.UpdateDate,
                    UpdateUserId = l.p2.p.UpdateUserId,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                    CategoryId = l.p2.p.CategoryId,
                    CategoryName = l.p2.c.Name,
                }).FirstOrDefault(p => p.Id == id);

                return result;
            }
        }

        public ViewModel ListToPos(string barcode)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.Join(context.Categories,
                    p => p.CategoryId,
                    c => c.Id, (p, c) => new { p, c }).Join(context.Users,
                    p2 => p2.p.UpdateUserId,
                    u => u.Id, (p2, u) => new { p2, u })
                    .Where(x => x.p2.p.Status == true)
                    .Where(x => x.p2.p.Deleted == false)
                    .Where(x => x.p2.c.Status == true)
                    .Select(l => new ViewModel
                {
                    Id = l.p2.p.Id,
                    Name = l.p2.p.Name,
                    PurchasePrice = l.p2.p.PurchasePrice,
                    SalePrice = l.p2.p.SalePrice,
                    PreviousSellingPrice = l.p2.p.PreviousSellingPrice,
                    Barcode = l.p2.p.Barcode,
                    Stock = l.p2.p.Stock,
                    CriticalStock = l.p2.p.CriticalStock,
                    Image = l.p2.p.Image,
                    Favorite = l.p2.p.Favorite,
					Origin = l.p2.p.Origin,
					UnitPrice = l.p2.p.UnitPrice,
					UnitType = l.p2.p.UnitType,
					Status = l.p2.p.Status,
                    UpdateDate = l.p2.p.UpdateDate,
                    UpdateUserId = l.p2.p.UpdateUserId,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                    CategoryId = l.p2.p.CategoryId,
                    CategoryName = l.p2.c.Name,
                }).FirstOrDefault(p => p.Barcode == barcode);

                return result;
            }
        }

        public ViewModel CheckExistsByBarcode(string barcode)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.Join(context.Categories,
                    p => p.CategoryId,
                    c => c.Id, (p, c) => new { p, c }).Join(context.Users,
                    p2 => p2.p.UpdateUserId,
                    u => u.Id, (p2, u) => new { p2, u }).Where(x=>x.p2.p.Deleted != true).Select(l => new ViewModel
                {
                    Id = l.p2.p.Id,
                    Name = l.p2.p.Name,
                    PurchasePrice = l.p2.p.PurchasePrice,
                    SalePrice = l.p2.p.SalePrice,
                    PreviousSellingPrice = l.p2.p.PreviousSellingPrice,
                    Barcode = l.p2.p.Barcode,
                    Stock = l.p2.p.Stock,
                    CriticalStock = l.p2.p.CriticalStock,
                    Image = l.p2.p.Image,
                    Favorite = l.p2.p.Favorite,
                    Status = l.p2.p.Status,
                    UpdateDate = l.p2.p.UpdateDate,
                    UpdateUserId = l.p2.p.UpdateUserId,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                    CategoryId = l.p2.p.CategoryId,
                    CategoryName = l.p2.c.Name,
                }).FirstOrDefault(p => p.Barcode == barcode);

                return result;
            }
        }

        public bool CheckExistsByBarcodeAndId(int id, string barcode)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products
                    .Where(x => x.Barcode == barcode && x.Id != id)
                    .Any();

                return result;
            }
        }

        public bool ListToSavePhoto(string barcode)
        {
            using (var context = new DataBaseContext())
            {
                bool result = context.Products
                    .Where(l => l.Barcode == barcode)
                    .Any();

                return result;
            }
        }

        public void UpdateImage(UpdateImageRequestModel updateImageRequestModel)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.FirstOrDefault(c => c.Barcode == updateImageRequestModel.Barcode);
                            
                result.Image = updateImageRequestModel.Image;               

                context.SaveChanges();
            }
        }

        public StockQuantityViewModel GetStockQuantity()
        {
            using (var context = new DataBaseContext())
            {
                var totalStock = context.Products.Sum(x => x.Stock);

                var result = new StockQuantityViewModel
                {
                    StockQuantity = totalStock
                };

                return result;
            }
        }

        public StockValueViewModel GetStockValue()
        {
            using (var context = new DataBaseContext())
            {
                var totalValue = context.Products.Sum(x => x.Stock * x.SalePrice);

                var result = new StockValueViewModel
                {
                    StockValue = totalValue
                };

                return result;
            }
        }
    }
}
