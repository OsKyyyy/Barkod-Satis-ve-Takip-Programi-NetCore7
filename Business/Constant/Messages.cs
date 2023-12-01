using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AuthorizationDenied = "Yetkiniz Yok";
        public static string UserRegistered = "Kullanıcı Eklendi";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifre Hatalı";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UsersListed = "Kullanıcılar Listelendi";
        public static string UserInfoListed = "Kullanıcı Bilgileri Listelendi";
        public static string UserAlreadyExists = "Kullanıcı Zaten Mevcut";
        public static string AccessTokenCreated = "Giriş Başarılı";
    }
}
