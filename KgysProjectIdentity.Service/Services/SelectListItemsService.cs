using KgysProjectIdentity.Repository.Models;

namespace KgysProjectIdentity.Service.Services
{
    public class SelectListItemsService : ISelectListItemsService
    {
        private readonly AppDbContext _context;

        public SelectListItemsService(AppDbContext context)
        {
            _context = context;
        }

        public List<StatusSelectList> AplicationSelect()
        {
            List<StatusSelectList> AplicationSelectLists = new()
           {
               new(){Data="Planlanan", Value="Planlanan"},
               new(){Data="Yeni Tesis", Value="Yeni Tesis"},
               new(){Data="Nakil", Value="Nakil"},
           };

            return AplicationSelectLists;
        }

        public List<StatusSelectList> EtmysSelect()
        {
            List<StatusSelectList> etmysLists = new()
           {
               new(){Data="Depoda", Value="Depoda"},
               new(){Data="Personel Kullanımında", Value="Personel Kullanımında"},
               new(){Data="Sahada Kullanımda", Value="Sahada Kullanımda"},
               new(){Data="Tamir Edilecek", Value="Tamir Edilecek"},
               new(){Data="ETMYS Çıkış Yap", Value="ETMYS Çıkış Yap"},
               new(){Data="ETMYS Çıkış Yapıldı", Value="ETMYS Çıkış Yapıldı"},
               new(){Data="Hek Bekliyor", Value="Hek Bekliyor"},
               new(){Data="ETMYS Çıkış Yapıldı - HEK", Value="ETMYS Çıkış Yapıldı -HEK"},
           };

            return etmysLists;
        }

        public List<StatusSelectList> KgysPtsSelect()
        {
            List<StatusSelectList> kgysPtsLists = new()
           {
               new(){Data="KGYS", Value="KGYS"},
               new(){Data="PTS", Value="PTS"},

           };

            return kgysPtsLists;
        }

        public List<StatusSelectList> ParkSelect()
        {
            List<StatusSelectList> parksLists = new()
           {
               new(){Data="Başlanılmadı", Value="Başlanılmadı"},
               new(){Data="Devam Ediyor", Value="Devam Ediyor"},
               new(){Data="Projesi Hazır", Value="Projesi Hazır"},
               new(){Data="Tamamlandı", Value="Tamamlandı"},

           };

            return parksLists;
        }

        public List<StatusSelectList> PaymentSelect()
        {
            List<StatusSelectList> paymentLists = new()
           {
               new(){Data="Harcama", Value="Harcama"},
               new(){Data="Gelen Ödenek", Value="Gelen Ödenek"},


           };

            return paymentLists;
        }

        public List<StatusSelectList> ReportToEgmSelect()
        {
            List<StatusSelectList> reportSelectLists = new()
           {
               new(){Data="Hayır", Value="Hayır"},
               new(){Data="Evet", Value="Evet"},

           };

            return reportSelectLists;
        }

        public List<StatusSelectList> RequestAddSelect()
        {
            List<StatusSelectList> requestAddSelectLists = new()
           {
               new(){Data="Planlanan", Value="Planlanan"},
               new(){Data="Mevcut", Value="Mevcut"},

           };

            return requestAddSelectLists;
        }

        public List<StatusSelectList> RequestSelect()
        {
            List<StatusSelectList> RequestSelectLists = new()
           {
               new(){Data="Talep", Value="Talep"},
               new(){Data="Planlanan", Value="Planlanan"},
               new(){Data="Mevcut", Value="Mevcut"},

           };

            return RequestSelectLists;
        }

        public List<StatusSelectList> StatusEpmysSelect()
        {
            List<StatusSelectList> statusEpmysSelectLists = new()
           {
               new(){Data="Yeni Başvuru", Value="Yeni Başvuru"},
               new(){Data="EPMY Düşmedi", Value="EPMY Düşmedi"},
               new(){Data="Onay Aşamasında", Value="Onay Aşamasında"},
               new(){Data="Bütçe Bekliyor", Value="Bütçe Bekliyor"},
               new(){Data="Ruhsat Bekleniyor", Value="Ruhsat Bekleniyor"},
               new(){Data="Müşteri Nedeniyle Askıda", Value="Müşteri Nedeniyle Askıda"},
               new(){Data="Başlanılmadı", Value="Başlanılmadı"},
               new(){Data="Firmanın Başlaması Bekleniyor", Value="Firmanın Başlaması Bekleniyor"},
               new(){Data="Yeraltı İmalatı Bekleniyor", Value="Yeraltı İmalatı Bekleniyor"},
               new(){Data="Kablo Çekimi Devam Ediyor", Value="Kablo Çekimi Devam Ediyor"},
               new(){Data="Firma Ek/Sonlandırma Aşamasında", Value="Firma Ek/Sonlandırma Aşamasında"},
               new(){Data="Servise Alma Aşamasında", Value="Servise Alma Aşamasında"},
               new(){Data="Tamamlandı", Value="Tamamlandı"},
               new(){Data="Bütçeden Red Edildi", Value="Bütçeden Red Edildi"},
               new(){Data="Fizibiliteden Red Edildi", Value="Fizibiliteden Red Edildi"},
               new(){Data="Kazı İzni Verilmedi", Value="Kazı İzni Verilmedi"},
               new(){Data="Devam Ediyor", Value="Devam Ediyor"},
           };

            return statusEpmysSelectLists;
        }

        public List<StatusSelectList> StatusSelect()
        {
            List<StatusSelectList> StatusSelectLists = new()
           {
               new(){Data="Başlanılmadı", Value="Başlanılmadı"},
               new(){Data="Devam Ediyor", Value="Devam Ediyor"},
               new(){Data="EGM Tarafından İptal Edildi", Value="EGM Tarafından İptal Edildi"},
               new(){Data="Telekom Tarafından İptal Edildi", Value="Telekom Tarafından İptal Edildi"},
               new(){Data="Tamamlandı", Value="Tamamlandı"},
               new(){Data="Yok", Value="Yok"},
               new(){Data="Mevcut", Value="Mevcut"},
               new(){Data="-", Value="-"},
               new(){Data="-2500", Value="-2500"},
               new(){Data="+2500", Value="+2500"},
           };

            return StatusSelectLists;
        }

        public List<StatusSelectList> TenderSelect()
        {
            List<StatusSelectList> tenderLists = new()
           {
               new(){Data="Başlanılmadı", Value="Başlanılmadı"},
               new(){Data="Bütçe Bekleniyor", Value="Bütçe Bekleniyor"},
               new(){Data="İhale Aşamasında", Value="İhale Aşamasında"},
               new(){Data="Sözleşme Bekleniyor", Value="Sözleşme Bekleniyor"},
               new(){Data="Devam Ediyor", Value="Devam Ediyor"},
               new(){Data="Kabul Aşamasında", Value="Kabul Aşamasında"},
               new(){Data="Hakediş Bekleniyor", Value="Hakediş Bekleniyor"},
               new(){Data="Tamamlandı", Value="Tamamlandı"},

           };

            return tenderLists;
        }

        public List<AppUser> Users()
        {
            var user = _context.UserRoles.Where(x => x.RoleId == "5a08b3a7-0083-4122-be9b-5c4e8f13506b").ToList(); //Sadece Şartname Komisyon Üye Yetkili Kullanıcılarını Çekmek İçin
            List<AppUser> users = new();
            foreach (var item in user)
            {
                var officials = _context.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                if (officials != null) { users.Add(officials); }
            }
            return users;
        }
        public List<AppUser> CommissionUsers()
        {
            var commissinOfficial = _context.UserRoles.Where(x => x.RoleId == "5b1d3d0d-a450-4749-9c4f-d1204cfca595").ToList();
            List<AppUser> commissionUsers = new();
            foreach (var item in commissinOfficial)
            {
                var officials = _context.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                if (officials != null) { commissionUsers.Add(officials); }
            }
            return commissionUsers;
        }
        public List<AppUser> UsersReponse()
        {
            var userResponse = _context.UserRoles.Where(x => x.RoleId == "804d1375-ae06-4b94-8dbf-440a5b799017").ToList(); //Sadece Planlama kullanıcılarını çekmek için
            List<AppUser> usersReponse = new();
            foreach (var item in userResponse)
            {
                var officials = _context.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                if (officials != null) { usersReponse.Add(officials); }
            }
            return usersReponse;
        }
        public List<StatusSelectList> CctvReasonSelect()
        {
            List<StatusSelectList> cctvSelectLists = new()
           {
               new(){Data="Genişleme", Value="Genişleme"},
               new(){Data="Modernizasyon", Value="Modernizasyon"}, 
               new(){Data="Genişleme/Modernizasyon", Value="Genişleme/Modernizasyon"},
               new(){Data="Yeni Kurulum", Value="Yeni Kurulum"},
           };

            return cctvSelectLists;
        }
    }
}
