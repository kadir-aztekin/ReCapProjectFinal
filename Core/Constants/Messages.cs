using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Constants
{
    
    public static class Messages
    {
        public static string Listed = "Listelendi!";
        public static string Added = "Eklendi!";
        public static string Updated = "Güncellendi!";
        public static string Deleted = "Silindi!";
        public static string NotListed = "Listeleme Yapılamadı";
        public static string NotAdded = "Ekleme Yapılamadı";
        public static string NotUpdated = "Güncelleme Yapılamadı";
        public static string NotDeleted = "Silme İşlemi Yapılamadı";
        public static string NameInvalid = "Geçersiz İsim";
        public static string MaintenanceTime = "Bakım Zamanı";
        public static string InvalidEntry = "Geçersiz Giriş";
        public static string NotAvailable = "Mevcut Değil";
        public static string InvalidExtension = "Geçersiz Resim Formatı";

        //Araç Logo Mesajları
        public static string BrandListed = "Markalar Listelendi!";
        public static string logoLimitExceeded ="Markkanın 1 Logosu olabilir";
        public static string BrandImageAdded = "Marka Logosu Eklendi";
        public static string BrandImageUpdated = "Marka Logosu Güncellendi";
        public static string BrandImageDeleted = "Marka Logosu Silindi";
        //Araç Logo Mesajları
        public static string CarsListed = "Araçlar Listelendi!";
        public static string CarImageLimitExceeded = "Araç Resim Sınırı Aşıldı";
        public static string CarsImageAdded = "Araç Resmi Eklendi";
        public static string CarsImageUpdated = "Araç Resmi Güncellendi";
        public static string CarsImageDeleted = "Araç Resmi Silindi";
        // Doğrulama Mesajları
        public static string WrongValidationType = "Yanlış Doğrulama Türü";
        public static string CanNotBeBlank = "Boş Bırakılamaz";
        public static string InvalidEmailAddress = "Geçersiz E-Mail Formatı";



        //Güvenlik Mesajları
        public static string AuthorizationDenied = "Yetkin Yok"; 
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifre Hatalı";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UserAlreadyExists = "Bu Kullanıcı Zaten Mevcut";
        public static string UserRegistered = "Kullanıcı Başarılı Bir Şekilde Kaydoldu";
        public static string AccessTokenCreated = "Token Oluşturuldu ";

    }
}