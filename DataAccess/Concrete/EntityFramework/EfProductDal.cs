using Core.DataAccess.EntityFramework;
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

        public void Update(Product product)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.FirstOrDefault(c => c.Id == product.Id);

                result.CategoryId = product.CategoryId;
                result.Name = product.Name;
                result.PurchasePrice = product.PurchasePrice;
                result.SalePrice = product.SalePrice;
                result.Barcode = product.Barcode;
                result.Stock = product.Stock;
                result.Image = product.Image;
                result.Favorite = product.Favorite;
                result.Status = product.Status;
                result.UpdateDate = product.UpdateDate;
                result.UpdateUserId = product.UpdateUserId;

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
                    Barcode = l.pu.p.Barcode,
                    Stock = l.pu.p.Stock,
                    Image = l.pu.p.Image,
                    Favorite = l.pu.p.Favorite,
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
                    Barcode = l.p2.p.Barcode,
                    Stock = l.p2.p.Stock,
                    Image = l.p2.p.Image,
                    Favorite = l.p2.p.Favorite,
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
                    Barcode = l.p2.p.Barcode,
                    Stock = l.p2.p.Stock,
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
                    Barcode = l.p2.p.Barcode,
                    Stock = l.p2.p.Stock,
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
    }
}
