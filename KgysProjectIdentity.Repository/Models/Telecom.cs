namespace KgysProjectIdentity.Repository.Models
{
    public class Telecom
    {
        private readonly AppDbContext _context;
        public Telecom(AppDbContext context)
        {
            _context = context;

        }

        public string Status(int status)
        {
            string telecomStatus = "Başlanılmadı";
            var telecom = _context.Products.Find(status);
            if (telecom != null)
            {
                if (telecom!.EpymStatus == "Devam Ediyor")
                {
                    telecomStatus = "EPMYS'de";

                }

                if (telecom!.EomStatus == "Devam Ediyor")
                {
                    telecomStatus = "EOM'da";
                }

                if (telecom!.MplsOperationStatus == "Devam Ediyor")
                {
                    telecomStatus = "MPLS'de";
                }

                if (telecom!.MplsNmsStatus == "Devam Ediyor")
                {
                    telecomStatus = "MPLSNMS'de";
                }

                if (telecom!.MplsNmsStatus == "Tamamlandı")
                {
                    telecomStatus = "Tamamlandı";
                }
                if (telecom!.CheckEGM == "Devam Ediyor")
                {
                    telecomStatus = "EGM'yi bekliyor";
                }
                if (telecom!.CheckEGM == "Tamamlandı")
                {
                    telecomStatus = "Tamamlandı";
                }
                if (telecom!.EpymStatus == "Bütçeden Red Edildi" || telecom!.EpymStatus == "Fizibiliteden Red Edildi" || telecom!.EpymStatus == "Kazı İzni Verilmedi" || telecom!.EpymStatus == "Müşteri Nedeniyle Askıda")
                {
                    telecomStatus = "EPMYS'den İptal";
                }
                if (telecom!.EomStatus == "EGM Tarafından İptal Edildi" || telecom!.EomStatus == "Telekom Tarafından İptal Edildi")
                {
                    telecomStatus = "EOM'dan İptal";
                }
                if (telecom!.MplsOperationStatus == "EGM Tarafından İptal Edildi" || telecom!.MplsOperationStatus == "Telekom Tarafından İptal Edildi")
                {
                    telecomStatus = "MPLS'den İptal";
                }
                if (telecom!.MplsNmsStatus == "EGM Tarafından İptal Edildi" || telecom!.MplsNmsStatus == "Telekom Tarafından İptal Edildi")
                {
                    telecomStatus = "MPLSNMS'den İptal";
                }
                if (telecom!.CheckEGM == "EGM Tarafından İptal Edildi" || telecom!.CheckEGM == "Telekom Tarafından İptal Edildi")
                {
                    telecomStatus = "EGM'den İptal";
                }
            }
            return telecomStatus;
        }
        public string KuppaDistance(int status)
        {
            string kuppaDistance = "Henüz Belli Değil";
            if (_context.Products.Find(status) == null)
            {
                kuppaDistance = "Telekom Başvurusu Bulunamadı";

            }
            else if (_context.Products.Find(status)!.KuppaDistance! != null)
            {
                kuppaDistance = _context.Products.Find(status)!.KuppaDistance.ToString()!;

            }


            return kuppaDistance;
        }

        public string KuppaPrice(int status)
        {
            string kuppaPrice = "Henüz Hesaplanmadı";
            if (_context.Products.Find(status) == null)
            {
                kuppaPrice = "Telekom Başvurusu Bulunamadı";

            }
            else if (_context.Products.Find(status)!.KuppaPrice! != null)
            {
                kuppaPrice = _context.Products.Find(status)!.KuppaPrice!;
            }

            return kuppaPrice;
        }
        public string KuppaStatus(int status)
        {
            string kuppaStatus = "Henüz Bildirilmedi";
            if (_context.Products.Find(status) == null)
            {
                kuppaStatus = "Telekom Başvurusu Bulunamadı";

            }
            else if (_context.Products.Find(status)!.KuppaPStatus! != null)
            {
                kuppaStatus = _context.Products.Find(status)!.KuppaPStatus!;
            }

            return kuppaStatus;
        }

    }
}
