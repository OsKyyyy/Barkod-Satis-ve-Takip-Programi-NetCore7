using Core.Entities.Concrete;
using System;
using System.Collections;
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
        public static string ProductStockEntry = "Ürüne Stok Girişi Yapıldı";
        public static string ProductUpdated = "Ürün Güncellendi";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string FavoriteProductsListed = "Favori Ürünler Listelendi";
        public static string ProductDeleted = "Ürün Silindi";
        public static string ProductNotFound = "Ürün Bulunamadı";
        public static string ProductOutOfStock = "Ürün Stokta Yok";
        public static string MaximumStockReached = "Kullanılabilir Maximum Stoka Ulaşıldı";
        public static string ProductInfoListed = "Ürün Bilgileri Listelendi";
        public static string ProductAlreadyExists = "Ürün Zaten Mevcut";
        public static string ImagePropertyError = "Dosya 'PNG,JPG' formatında ve 2MB'dan az olmalıdır";

        public static string PosNotFound = "Sepette Ürün Bulunamadı";
        public static string PosListed = "Ürün Sepet Bilgileri Listelendi";
        public static string PosQuantityChange = "Sepet Adet Bilgisi Güncellendi";
        public static string PosDeleted = "Ürün Sepetten Silindi";
        public static string PosCancel = "Sepet İptal Edildi";

        public static string CustomerAdded = "Müşteri Eklendi";
        public static string CustomersListed = "Müşteriler Listelendi";
        public static string CustomerUpdated = "Müşteri Güncellendi";
        public static string CustomerDeleted = "Müşteri Silindi";
        public static string CustomerNotFound = "Müşteri Bulunamadı";
        public static string CustomerInfoListed = "Müşteri Bilgileri Listelendi";

        public static string CustomerMovementAdded = "Müşteri Hareketi Eklendi";
        public static string CustomerMovementInfoListed = "Müşteri Hareket Bilgileri Listelendi";
        public static string CustomerMovementDeleted = "Müşteri Hareketi Silindi";
        public static string CustomerMovementNotFound = "Müşteri Hareketi Bulunamadı";
        public static string CustomerMovementUpdated = "Müşteri Hareketi Güncellendi";

        public static string WholeSalerAdded = "Toptancı Eklendi";
        public static string WholeSalerListed = "Toptancılar Listelendi";
        public static string WholeSalerDeleted = "Toptancı Silindi";
        public static string WholeSalerNotFound = "Toptancı Bulunamadı";
        public static string WholeSalerInfoListed = "Toptancı Bilgileri Listelendi";

        public static string WholeSalerMovementAdded = "Toptancı Hareketi Eklendi";
        public static string WholeSalerMovementInfoListed = "Toptancı Hareket Bilgileri Listelendi";
        public static string WholeSalerMovementDeleted = "Toptancı Hareketi Silindi";
        public static string WholeSalerMovementNotFound = "Toptancı Hareketi Bulunamadı";
        public static string WholeSalerMovementUpdated = "Toptancı Hareketi Güncellendi";

        public static string SaleAdded = "Sipariş Tamamlandı";

        public static string SaleReportListed = "Satış Raporları Listelendi";
        public static string SaleDetailReportListed = "Satış Detay Raporları Listelendi";
        public static string SaleProductsDetailReportListed = "Satış Ürün Detay Raporları Listelendi";
        public static string SaleDetailReportNotFound = "Satış Detay Raporları Bulunamadı";
        public static string SalesDeleted = "Satış Silindi, Stoklar Güncellendi";
        public static string SalesUpdateStock = "Stoklar Güncellendi";

        public static string GetLastCustomerWithDebtListed = "Son Alışveriş Yapan Müşteri Listelendi";
        public static string GetLastCustomerWithDebtPaymentListed = "Son Ödeme Yapan Müşteri Listelendi";
        public static string GetCustomerTotalDebtListed = "Tüm Müşteri Borçları Tutarı Listelendi";
        public static string GetCustomerDebtListed = "Müşteri Borçları Çoktan Az Olarak Listelendi";
        public static string GetCustomerNonPayersListed = "Geri Ödemesi Yapmayanlar Listelendi";
        public static string GetCustomerThisMonthDebtListed = "Bu Ayın Tüm Borçları Tutarı Listelendi";
        public static string GetCustomerPreviousMonthDebtListed = "Bir Önceki Ayın Tüm Borçları Tutarı Listelendi";
        public static string GetCustomerMonthlyDebtListed = "Müşteri Borçları Aylık Olarak Listelendi";

        public static string GetLastWholeSalerWithDebtListed = "Son Alışveriş Yapılan Toptancı Listelendi";
        public static string GetLastWholeSalerWithDebtPaymentListed = "Son Ödeme Yapılan Toptancı Listelendi";
        public static string GetWholeSalerTotalDebtListed = "Tüm Toptancı Borçları Tutarı Listelendi";
        public static string GetWholeSalerDebtListed = "Toptancı Borçları Çoktan Az Olarak Listelendi";
        public static string GetWholeSalerNonPayersListed = "Geri Ödemesi Yapılmayan Toptancılar Listelendi";
        public static string GetWholeSalerThisMonthDebtListed = "Bu Ayın Tüm Borçları Tutarı Listelendi";
        public static string GetWholeSalerPreviousMonthDebtListed = "Bir Önceki Ayın Tüm Borçları Tutarı Listelendi";
        public static string GetWholeSalerMonthlyDebtListed = "Toptancı Borçları Aylık Olarak Listelendi";

        public static string IncomeAndExpensesTypeAdded = "Gelir-Gider Türü Eklendi";
        public static string IncomeAndExpensesTypeListed = "Gelir-Gider Türleri Listelendi";
        public static string IncomeAndExpensesTypeUpdated = "Gelir-Gider Türleri Güncellendi";
        public static string IncomeAndExpensesTypeNotFound = "Gelir-Gider Türü Bulunamadı";
        public static string IncomeAndExpensesTypeInfoListed = "Gelir-Gider Tür Bilgileri Listelendi";
        public static string IncomeAndExpensesTypeDeleted = "Gelir-Gider Türü Silindi";

        public static string IncomeAndExpensesAdded = "Gelir-Gider Eklendi";
        public static string IncomeAndExpensesListed = "Gelir-Giderler Listelendi";
        public static string IncomeAndExpensesDeleted = "Gelir-Gider Silindi";
        public static string IncomeAndExpensesNotFound = "Gelir-Gider Bulunamadı";
        public static string IncomeAndExpensesInfoListed = "Gelir-Gider Bilgileri Listelendi";

        public static string MonthlyExternalIncomeListed = "Aylık Harici Gelir Listelendi";
        public static string MonthlyExternalExpensesListed = "Aylık Harici Gider Listelendi";
        public static string MonthlySalesIncomeListed = "Aylık Satış Geliri Listelendi";
        public static string MonthlyWholeSalerExpensesListed = "Aylık Toptancı Gideri Listelendi";
    }

}
