﻿using Core.Entities.Concrete;
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
        public static string ImagePropertyError = "Dosya 'PNG,JPG' formatında ve 2MB'dan az olmalıdır";

    }

}
