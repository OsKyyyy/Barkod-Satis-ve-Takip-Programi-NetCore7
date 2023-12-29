using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constant
{
    public static class Messages
    {
        public static string AccessTokenCreated = "Giriş Başarılı";
        public static string AuthorizationDenied = "Yetkiniz Yok";
        public static string UserRegistered = "Kullanıcı Eklendi";
        public static string UserUpdated = "Kullanıcı GÜncellendi";
        public static string UserDeleted = "Kullanıcı Silindi";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifre Hatalı";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UsersListed = "Kullanıcılar Listelendi";
        public static string UserInfoListed = "Kullanıcı Bilgileri Listelendi";
        public static string UserAlreadyExists = "Kullanıcı Zaten Mevcut";

        public static string CategoryAdded = "Kategori Eklendi";
        public static string CategoryNotFound = "Kategori Bulunamadı";
        public static string CategoryInfoListed = "Kategori Bilgileri Listelendi";
        public static string CategoryDeleted = "Kategori Silindi";
        public static string CategoryUpdated = "Kategori Güncellendi";
        public static string CategoriesListed = "Kategoriler Listelendi";

        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductUpdated = "Ürün Güncellendi";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string FavoriteProductsListed = "Favori Ürünler Listelendi";
        public static string ProductDeleted = "Ürün Silindi";
        public static string ProductNotFound = "Ürün Bulunamadı";
        public static string ProductInfoListed = "Ürün Bilgileri Listelendi";
        public static string ProductAlreadyExists = "Ürün Zaten Mevcut";
        public static string ImagePropertyError = "Dosya 'PNG,JPG' formatında ve 2MB'dan az olmalıdır";

        public static string PosNotFound = "Ürün Sepette Bulunamadı";
        public static string PosListed = "Ürün Sepet Bilgileri Listelendi";
        public static string PosQuantityChange = "Sepet Adet Bilgisi Güncellendi";
        public static string PosDeleted = "Ürün Sepetten Silindi";

        public static string CustomerAdded = "Müşteri Eklendi";
        public static string CustomersListed = "Müşteriler Listelendi";
        public static string CustomerUpdated = "Müşteri Güncellendi";
        public static string CustomerDeleted = "Müşteri Silindi";
        public static string CustomerNotFound = "Müşteri Bulunamadı";
        public static string CustomerInfoListed = "Müşteri Bilgileri Listelendi";

        public static string WholeSalerAdded = "Toptancı Eklendi";
        public static string WholeSalerListed = "Toptancılar Listelendi";
        public static string WholeDeleted = "Toptancı Silindi";
        public static string WholeSalerNotFound = "Toptancı Bulunamadı";
        public static string WholeSalerInfoListed = "Toptancı Bilgileri Listelendi";
    }

}
