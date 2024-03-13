using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace KgysProjectIdentity.Service.Services
{
    public class LogDifferenceDetectService:ILogDifferenceDetectService
    {
        private readonly AppDbContext _context;
        public LogDifferenceDetectService(AppDbContext context)
        {
            _context = context;
        }
        public string AccidentKgysAdd(AccidentKgysViewModel accidentKgysViewModel)
        {
            string difference = accidentKgysViewModel.UpdateUser + " kullanıcısı " + accidentKgysViewModel.UpdateDate + " tarihinde " + accidentKgysViewModel.AccidentKgysName + " isimli kazalı noktanın; ilçesini  = " + accidentKgysViewModel.AccidentKgysDistrict + ", türünü  = " + accidentKgysViewModel.AccidenKgysType + ", ";

            if (accidentKgysViewModel.AccidentDate != null)
            {
                difference += "tarihini  = " + accidentKgysViewModel.AccidentDate + ", ";
            }
            if (accidentKgysViewModel.AccidentInfo != null)
            {
                difference += "bilgisi  = " + accidentKgysViewModel.AccidentInfo + ", ";
            }
            if (accidentKgysViewModel.AccidentEbysNo != null)
            {
                difference += "Ebys numarası  = " + accidentKgysViewModel.AccidentEbysNo + ", ";
            }
            if (accidentKgysViewModel.AlcoholTest != null)
            {
                difference += "alkol testi  = " + accidentKgysViewModel.AlcoholTest + ", ";
            }
            if (accidentKgysViewModel.AccidentReport != null)
            {
                difference += "kaza rapor durumu  = " + accidentKgysViewModel.AccidentReport + ", ";
            }
            if (accidentKgysViewModel.AccidentInsuranceName != null)
            {
                difference += "sigorta adı  = " + accidentKgysViewModel.AccidentInsuranceName + ", ";
            }
            if (accidentKgysViewModel.AccidentPolicyNo != null)
            {
                difference += "poliçe no  = " + accidentKgysViewModel.AccidentPolicyNo + ", ";
            }
            if (accidentKgysViewModel.AccidentComplementInsuranceName != null)
            {
                difference += "kasko şirketi adı  = " + accidentKgysViewModel.AccidentComplementInsuranceName + ", ";
            }
            if (accidentKgysViewModel.AccidentComplementPolicyNo != null)
            {
                difference += "kasko poliçe no = " + accidentKgysViewModel.AccidentComplementPolicyNo + ", ";
            }
            if (accidentKgysViewModel.AccidentRegistrationDate != null)
            {
                difference += "hasar kayıt tarihi  = " + accidentKgysViewModel.AccidentRegistrationDate + ", ";
            }
            if (accidentKgysViewModel.AccidentRegistrationNo != null)
            {
                difference += "hasar kayıt no  = " + accidentKgysViewModel.AccidentRegistrationNo + ", ";
            }
            if (accidentKgysViewModel.AccidentRegistrationStatus != null)
            {
                difference += "hasar kayıt durumu  = " + accidentKgysViewModel.AccidentRegistrationStatus + ", ";
            }
            if (accidentKgysViewModel.AccidentDamagePrice != null)
            {
                difference += "tahmini hasar bedeli  = " + accidentKgysViewModel.AccidentDamagePrice + ", ";
            }
            if (accidentKgysViewModel.AccidentExpertiseCompany != null)
            {
                difference += "expertiz şirketi  = " + accidentKgysViewModel.AccidentExpertiseCompany + ", ";
            }
            if (accidentKgysViewModel.AccidentExpertiseTelephone != null)
            {
                difference += "expertiz telefonu  = " + accidentKgysViewModel.AccidentExpertiseTelephone + ", ";
            }
            if (accidentKgysViewModel.AccidentExpertiseEmail != null)
            {
                difference += "expertiz emaili  = " + accidentKgysViewModel.AccidentExpertiseEmail + ", ";
            }
            if (accidentKgysViewModel.AccidentExpertiseEmailSend != null)
            {
                difference += "expertize email atıldı mı  = " + accidentKgysViewModel.AccidentExpertiseEmailSend + ", ";
            }
            if (accidentKgysViewModel.Explanation != null)
            {
                difference += "açıklama  = " + accidentKgysViewModel.Explanation + ", ";
            }
            if (accidentKgysViewModel.PriceStatus != null)
            {
                difference += "ödeme durumu = " + accidentKgysViewModel.PriceStatus + ", ";
            }
            if (accidentKgysViewModel.AmountPaid != null)
            {
                difference += "ödenen miktar = " + accidentKgysViewModel.AmountPaid + ", ";
            }
            if (accidentKgysViewModel.RepairCompany != null)
            {
                difference += "onarım şirketi = " + accidentKgysViewModel.RepairCompany + ", ";
            }
            if (accidentKgysViewModel.RepairFinishDate != null)
            {
                difference += "onarım bitiş tarihi  = " + accidentKgysViewModel.RepairFinishDate + ", ";
            }

            if (accidentKgysViewModel.Status != null)
            {
                difference += "son durum  = " + accidentKgysViewModel.Status + ", ";
            }

            difference += "olacak şekilde ekledi.";


            return difference;
        }

        public string AccidentKgysDifference(AccidentKgysViewModel accidentKgysViewModel)
        {
            string difference = accidentKgysViewModel.UpdateUser + " kullanıcısı " + accidentKgysViewModel.UpdateDate + " tarihinde " + accidentKgysViewModel.AccidentKgysName + " isimli kazalı noktanın; ";
            var request = _context.AccidentKgys.AsNoTracking().Where(x => x.Id == accidentKgysViewModel.Id).ToList();
            foreach (var item in request)
            {
                if (item.AccidentKgysName != accidentKgysViewModel.AccidentKgysName)
                {
                    difference += "ismini " + item.AccidentKgysName + " = " + accidentKgysViewModel.AccidentKgysName + ", ";
                }
                if (item.AccidentKgysDistrict != accidentKgysViewModel.AccidentKgysDistrict)
                {
                    difference += "ilçesini " + item.AccidentKgysDistrict + " = " + accidentKgysViewModel.AccidentKgysDistrict + ", ";
                }
                if (item.AccidenKgysType != accidentKgysViewModel.AccidenKgysType)
                {
                    difference += "türünü " + item.AccidenKgysType + " = " + accidentKgysViewModel.AccidenKgysType + ", ";
                }
                if (item.AccidentDate != accidentKgysViewModel.AccidentDate)
                {
                    difference += "tarihini " + item.AccidentDate + " = " + accidentKgysViewModel.AccidentDate + ", ";
                }
                if (item.AccidentInfo != accidentKgysViewModel.AccidentInfo)
                {
                    difference += "bilgisi " + item.AccidentInfo + " = " + accidentKgysViewModel.AccidentInfo + ", ";
                }
                if (item.AccidentEbysNo != accidentKgysViewModel.AccidentEbysNo)
                {
                    difference += "Ebys numarası " + item.AccidentEbysNo + " = " + accidentKgysViewModel.AccidentEbysNo + ", ";
                }
                if (item.AlcoholTest != accidentKgysViewModel.AlcoholTest)
                {
                    difference += "alkol testi " + item.AlcoholTest + " = " + accidentKgysViewModel.AlcoholTest + ", ";
                }
                if (item.AccidentReport != accidentKgysViewModel.AccidentReport)
                {
                    difference += "kaza rapor durumu " + item.AccidentReport + " = " + accidentKgysViewModel.AccidentReport + ", ";
                }
                if (item.AccidentInsuranceName != accidentKgysViewModel.AccidentInsuranceName)
                {
                    difference += "sigorta adı " + item.AccidentInsuranceName + " = " + accidentKgysViewModel.AccidentInsuranceName + ", ";
                }
                if (item.AccidentPolicyNo != accidentKgysViewModel.AccidentPolicyNo)
                {
                    difference += "poliçe no " + item.AccidentPolicyNo + " = " + accidentKgysViewModel.AccidentPolicyNo + ", ";
                }
                if (item.AccidentComplementInsuranceName != accidentKgysViewModel.AccidentComplementInsuranceName)
                {
                    difference += "kasko şirketi adı " + item.AccidentComplementInsuranceName + " = " + accidentKgysViewModel.AccidentComplementInsuranceName + ", ";
                }
                if (item.AccidentComplementPolicyNo != accidentKgysViewModel.AccidentComplementPolicyNo)
                {
                    difference += "kasko poliçe no " + item.AccidentComplementPolicyNo + " = " + accidentKgysViewModel.AccidentComplementPolicyNo + ", ";
                }
                if (item.AccidentRegistrationDate != accidentKgysViewModel.AccidentRegistrationDate)
                {
                    difference += "hasar kayıt tarihi " + item.AccidentRegistrationDate + " = " + accidentKgysViewModel.AccidentRegistrationDate + ", ";
                }
                if (item.AccidentRegistrationNo != accidentKgysViewModel.AccidentRegistrationNo)
                {
                    difference += "hasar kayıt no " + item.AccidentRegistrationNo + " = " + accidentKgysViewModel.AccidentRegistrationNo + ", ";
                }
                if (item.AccidentRegistrationStatus != accidentKgysViewModel.AccidentRegistrationStatus)
                {
                    difference += "hasar kayıt durumu " + item.AccidentRegistrationStatus + " = " + accidentKgysViewModel.AccidentRegistrationStatus + ", ";
                }
                if (item.AccidentDamagePrice != accidentKgysViewModel.AccidentDamagePrice)
                {
                    difference += "tahmini hasar bedeli " + item.AccidentDamagePrice + " = " + accidentKgysViewModel.AccidentDamagePrice + ", ";
                }
                if (item.AccidentExpertiseCompany != accidentKgysViewModel.AccidentExpertiseCompany)
                {
                    difference += "expertiz şirketi " + item.AccidentExpertiseCompany + " = " + accidentKgysViewModel.AccidentExpertiseCompany + ", ";
                }
                if (item.AccidentExpertiseTelephone != accidentKgysViewModel.AccidentExpertiseTelephone)
                {
                    difference += "expertiz telefonu " + item.AccidentExpertiseTelephone + " = " + accidentKgysViewModel.AccidentExpertiseTelephone + ", ";
                }
                if (item.AccidentExpertiseEmail != accidentKgysViewModel.AccidentExpertiseEmail)
                {
                    difference += "expertiz emaili " + item.AccidentExpertiseEmail + " = " + accidentKgysViewModel.AccidentExpertiseEmail + ", ";
                }
                if (item.AccidentExpertiseEmailSend != accidentKgysViewModel.AccidentExpertiseEmailSend)
                {
                    difference += "expertize email atıldı mı " + item.AccidentExpertiseEmailSend + " = " + accidentKgysViewModel.AccidentExpertiseEmailSend + ", ";
                }
                if (item.Explanation != accidentKgysViewModel.Explanation)
                {
                    difference += "açıklama " + item.Explanation + " = " + accidentKgysViewModel.Explanation + ", ";
                }
                if (item.PriceStatus != accidentKgysViewModel.PriceStatus)
                {
                    difference += "ödeme durumu " + item.PriceStatus + " = " + accidentKgysViewModel.PriceStatus + ", ";
                }
                if (item.AmountPaid != accidentKgysViewModel.AmountPaid)
                {
                    difference += "ödenen miktar " + item.AmountPaid + " = " + accidentKgysViewModel.AmountPaid + ", ";
                }
                if (item.RepairCompany != accidentKgysViewModel.RepairCompany)
                {
                    difference += "onarım şirketi " + item.RepairCompany + " = " + accidentKgysViewModel.RepairCompany + ", ";
                }
                if (item.RepairFinishDate != accidentKgysViewModel.RepairFinishDate)
                {
                    difference += "onarım bitiş tarihi " + item.RepairFinishDate + " = " + accidentKgysViewModel.RepairFinishDate + ", ";
                }

                if (item.Status != accidentKgysViewModel.Status)
                {
                    difference += "son durum " + item.Status + " = " + accidentKgysViewModel.Status + ", ";
                }
            }
            difference += " olacak şekilde güncellendi.";

            return difference;
        }

        public string KgysRequestAdd(KgysRequestViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.KgysName + " isimli talebi; ";

            if (newRequest.District != null)
            {
                difference += "İlçesini  = " + newRequest.District + ", ";
            }
            if (newRequest.Neighbourhood != null)
            {
                difference += "Mahallesini  = " + newRequest.Neighbourhood + ", ";
            }
            if (newRequest.RequestMethod != null)
            {
                difference += "Metodunu  = " + newRequest.RequestMethod + ", ";
            }
            if (newRequest.RequestMethodDetail != null)
            {
                difference += "Metot detayını  = " + newRequest.RequestMethodDetail + ", ";
            }
            if (newRequest.RequestArraviedDate != null)
            {
                difference += "Geliş tarihini  = " + newRequest.RequestArraviedDate + ", ";
            }
            if (newRequest.RequestedByWho != null)
            {
                difference += "Talep edeni  = " + newRequest.RequestedByWho + ", ";
            }
            if (newRequest.RequestedByWhoDetail != null)
            {
                difference += "Talep eden detayını  = " + newRequest.RequestedByWhoDetail + ", ";
            }
            if (newRequest.RequestedTelephoneNumber != null)
            {
                difference += "Talep eden telefonunu  = " + newRequest.RequestedTelephoneNumber + ", ";
            }
            if (newRequest.RequestAddress != null)
            {
                difference += "Talep adresini  = " + newRequest.RequestAddress + ", ";
            }

            if (newRequest.RequestCoordinate != null)
            {
                difference += "Talep koordinatını  = " + newRequest.RequestCoordinate + ", ";
            }
            if (newRequest.RequestEvaluation != null)
            {
                difference += "Talep Değerlendirmesi = " + newRequest.RequestEvaluation + ", ";
            }
            if (newRequest.RequestEvaluationDetail != null)
            {
                difference += "Talep Değerlendirme Detayını  = " + newRequest.RequestEvaluationDetail + ", ";
            }
            if (newRequest.RequestGoToDiscovery != null)
            {
                difference += "Talep Keşfi = " + newRequest.RequestGoToDiscovery + ", ";
            }
            if (newRequest.RequestFirstDiscovery != null)
            {
                difference += "Talep Keşfi Değerlendirmesini = " + newRequest.RequestFirstDiscovery + ", ";
            }
            if (newRequest.RequestType != null)
            {
                difference += "Talep Tipini = " + newRequest.RequestType + ", ";
            }
            if (newRequest.RequestAskToDistrict != null)
            {
                difference += "İlçeye Soruldu mu? = " + newRequest.RequestAskToDistrict + ", ";
            }
            if (newRequest.RequestAskToDistrictEbysNumber != null)
            {
                difference += "İlçeye yazılan EBYS No = " + newRequest.RequestAskToDistrictEbysNumber + ", ";
            }
            if (newRequest.RequestAnswerFromDisctrictEbysNumber != null)
            {
                difference += "İlçe Cevap EBYS No = " + newRequest.RequestAnswerFromDisctrictEbysNumber + ", ";
            }
            if (newRequest.RequestArrangementNumber != null)
            {
                difference += "İlçe Talep Sırası = " + newRequest.RequestArrangementNumber + ", ";
            }
            if (newRequest.TelecomFoId != null)
            {
                difference += "Telekom Idsi = " + newRequest.TelecomFoId + ", ";
            }
            difference += " olacak şekilde ekledi.";
            return difference;

        }
        public string KgysRequestUpdate(KgysRequestViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.KgysName + " isimli talebi; ";
            var request = _context.KgysRequest.AsNoTracking().Where(x => x.Id == newRequest.Id).ToList();
            foreach (var item in request)
            {
                if (item.KgysName != newRequest.KgysName)
                {
                    difference += "ismini  " + item.KgysName + " = " + newRequest.KgysName + ", ";
                }
                if (item.District != newRequest.District)
                {
                    difference += "ilçesini  " + item.District + " = " + newRequest.District + ", ";
                }
                if (item.Neighbourhood != newRequest.Neighbourhood)
                {
                    difference += "mahallesini  " + item.Neighbourhood + " = " + newRequest.Neighbourhood + ", ";
                }
                if (item.RequestMethod != newRequest.RequestMethod)
                {
                    difference += "Metodunu " + item.RequestMethod + " = " + newRequest.RequestMethod + ", ";
                }
                if (item.RequestMethodDetail != newRequest.RequestMethodDetail)
                {
                    difference += "Metot detayını " + item.RequestMethodDetail + " = " + newRequest.RequestMethodDetail + ", ";
                }
                if (item.RequestArraviedDate != newRequest.RequestArraviedDate)
                {
                    difference += "Geliş tarihini " + item.RequestArraviedDate + " = " + newRequest.RequestArraviedDate + ", ";
                }
                if (item.RequestedByWho != newRequest.RequestedByWho)
                {
                    difference += "Talep edeni " + item.RequestedByWho + " = " + newRequest.RequestedByWho + ", ";
                }
                if (item.RequestedByWhoDetail != newRequest.RequestedByWhoDetail)
                {
                    difference += "Talep eden detayını " + item.RequestedByWhoDetail + " = " + newRequest.RequestedByWhoDetail + ", ";
                }
                if (item.RequestedTelephoneNumber != newRequest.RequestedTelephoneNumber)
                {
                    difference += "Talep eden telefonunu " + item.RequestedTelephoneNumber + "  = " + newRequest.RequestedTelephoneNumber + ", ";
                }
                if (item.RequestAddress != newRequest.RequestAddress)
                {
                    difference += "Talep eden adresini" + item.RequestAddress + "  = " + newRequest.RequestAddress + ", ";
                }
                if (item.RequestCoordinate != newRequest.RequestCoordinate)
                {
                    difference += "Talep koordinatını " + item.RequestCoordinate + " = " + newRequest.RequestCoordinate + ", ";
                }
                if (item.RequestEvaluation != newRequest.RequestEvaluation)
                {
                    difference += "Talep Değerlendirmesi " + item.RequestEvaluation + " = " + newRequest.RequestEvaluation + ", ";
                }
                if (item.RequestEvaluationDetail != newRequest.RequestEvaluationDetail)
                {
                    difference += "Talep Değerlendirme Detayını " + item.RequestEvaluationDetail + "  = " + newRequest.RequestEvaluationDetail + ", ";
                }
                if (item.RequestGoToDiscovery != newRequest.RequestGoToDiscovery)
                {
                    difference += "Talep Keşfi " + item.RequestGoToDiscovery + " = " + newRequest.RequestGoToDiscovery + ", ";
                }
                if (item.RequestFirstDiscovery != newRequest.RequestFirstDiscovery)
                {
                    difference += "Talep Keşfi Değerlendirmesini " + item.RequestFirstDiscovery + " = " + newRequest.RequestFirstDiscovery + ", ";
                }
                if (item.RequestType != newRequest.RequestType)
                {
                    difference += "Talep Tipini " + item.RequestType + " = " + newRequest.RequestType + ", ";
                }
                if (item.RequestAskToDistrict != newRequest.RequestAskToDistrict)
                {
                    difference += "İlçeye Soruldu mu? " + item.RequestAskToDistrict + " = " + newRequest.RequestAskToDistrict + ", ";
                }
                if (item.RequestAskToDistrictEbysNumber != newRequest.RequestAskToDistrictEbysNumber)
                {
                    difference += "İlçeye yazılan EBYS No " + item.RequestAskToDistrictEbysNumber + " = " + newRequest.RequestAskToDistrictEbysNumber + ", ";
                }
                if (item.RequestAnswerFromDisctrictEbysNumber != newRequest.RequestAnswerFromDisctrictEbysNumber)
                {
                    difference += "İlçe Cevap EBYS No " + item.RequestAnswerFromDisctrictEbysNumber + " = " + newRequest.RequestAnswerFromDisctrictEbysNumber + ", ";
                }
                if (item.RequestArrangementNumber != newRequest.RequestArrangementNumber)
                {
                    difference += "İlçe Talep Sırası " + item.RequestArrangementNumber + " = " + newRequest.RequestArrangementNumber + ", ";
                }
                if (item.TelecomFoId != newRequest.TelecomFoId)
                {
                    difference += "Telekom Idsi " + item.TelecomFoId + " = " + newRequest.TelecomFoId + ", ";
                }
                if (item.LastStatus != newRequest.LastStatus)
                {
                    difference += "Son Durumunu " + item.LastStatus + " = " + newRequest.LastStatus + ", ";
                }
                if (item.ProjectName != newRequest.ProjectName)
                {
                    difference += "Proje ismini " + item.ProjectName + " = " + newRequest.ProjectName + ", ";
                }
            }
            difference += " olacak şekilde güncelledi.";

            return difference;

        }

        public string ParksAndRecreationAdd(ParkAndRecreationsViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.ParkName + " isimli Park veya Rekreasyon Alanını; ";

            if (newRequest.ParkDistrict != null)
            {
                difference += "ilçesini  = " + newRequest.ParkDistrict + ", ";
            }
            if (newRequest.ParkNeighboor != null)
            {
                difference += "mahallesini  = " + newRequest.ParkNeighboor + ", ";
            }
            if (newRequest.ParkAdress != null)
            {
                difference += "Adresini  = " + newRequest.ParkAdress + ", ";
            }
            if (newRequest.ParkCoordinate != null)
            {
                difference += "Talep koordinatını  = " + newRequest.ParkCoordinate + ", ";
            }
            if (newRequest.ParkCamerasSetup != null)
            {
                difference += "Kamera kurulum Durumunu  = " + newRequest.ParkCamerasSetup + ", ";
            }
            if (newRequest.ParkCamerasIsActive != null)
            {
                difference += "Kamera Aktif Mi?  = " + newRequest.ParkCamerasIsActive + ", ";
            }
            if (newRequest.ParkCamerasActiveDate != null)
            {
                difference += "Kamera Aktif Olma Zamanı = " + newRequest.ParkCamerasActiveDate + ", ";
            }
            if (newRequest.ParkPoleCount != null)
            {
                difference += "Kamera Direk Sayısı  = " + newRequest.ParkPoleCount + ", ";
            }
            if (newRequest.ParkTotalCameraCount != null)
            {
                difference += "Toplam Kamera Sayısı  = " + newRequest.ParkTotalCameraCount + ", ";
            }
            if (newRequest.ParkFixedCameraCount != null)
            {
                difference += "Sabit Kamera Sayısı = " + newRequest.ParkFixedCameraCount + ", ";
            }
            if (newRequest.ParkDomeCameraCount != null)
            {
                difference += "Hareketli Kamera Sayısı  = " + newRequest.ParkDomeCameraCount + ", ";
            }
            if (newRequest.ParkNvrAdress != null)
            {
                difference += "Kaydın Tutulduğu Yer = " + newRequest.ParkNvrAdress + ", ";
            }
            if (newRequest.ParkLiveMonitoring != null)
            {
                difference += "Canlı İzleniyor Mu? = " + newRequest.ParkLiveMonitoring + ", ";
            }
            if (newRequest.ParkExplain != null)
            {
                difference += "Açıklama = " + newRequest.ParkExplain + ", ";
            }
            if (newRequest.ParkSupervisor != null)
            {
                difference += "Park Sorumlusu = " + newRequest.ParkSupervisor + ", ";
            }
            if (newRequest.ParkSupervisorTel != null)
            {
                difference += "Park Sorumlusu Telefonu = " + newRequest.ParkSupervisorTel + ", ";
            }

            difference += " olacak şekilde ekledi.";
            return difference;

        }

        public string ParksAndRecreationUpdate(ParkAndRecreationsViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.ParkName + " isimli Park veya Rekreasyon Alanını; ";
            var request = _context.ParkAndRecreations.AsNoTracking().Where(x => x.Id == newRequest.Id).ToList();
            foreach (var item in request)
            {
                if (item.ParkName != newRequest.ParkName)
                {
                    difference += "İsmini  " + item.ParkName + " = " + newRequest.ParkName + ", ";
                }
                if (item.ParkDistrict != newRequest.ParkDistrict)
                {
                    difference += "İlçesini  " + item.ParkDistrict + " = " + newRequest.ParkDistrict + ", ";
                }
                if (item.ParkNeighboor != newRequest.ParkNeighboor)
                {
                    difference += "Mahallesini  " + item.ParkNeighboor + " = " + newRequest.ParkNeighboor + ", ";
                }
                if (item.ParkAdress != newRequest.ParkAdress)
                {
                    difference += "Talep eden adresini" + item.ParkAdress + "  = " + newRequest.ParkAdress + ", ";
                }
                if (item.ParkCoordinate != newRequest.ParkCoordinate)
                {
                    difference += "Talep koordinatını " + item.ParkCoordinate + " = " + newRequest.ParkCoordinate + ", ";
                }
                if (item.ParkCamerasSetup != newRequest.ParkCamerasSetup)
                {
                    difference += "Kamera Kurulum durumu  " + item.ParkCamerasSetup + " = " + newRequest.ParkCamerasSetup + ", ";
                }
                if (item.ParkCamerasIsActive != newRequest.ParkCamerasIsActive)
                {
                    difference += "Kamera Aktif Mi? " + item.ParkCamerasIsActive + " = " + newRequest.ParkCamerasIsActive + ", ";
                }
                if (item.ParkCamerasActiveDate != newRequest.ParkCamerasActiveDate)
                {
                    difference += "Aktif Olma Tarihi " + item.ParkCamerasActiveDate + " = " + newRequest.ParkCamerasActiveDate + ", ";
                }
                if (item.ParkPoleCount != newRequest.ParkPoleCount)
                {
                    difference += "Kamera Direk Sayısı " + item.ParkPoleCount + " = " + newRequest.ParkPoleCount + ", ";
                }
                if (item.ParkTotalCameraCount != newRequest.ParkTotalCameraCount)
                {
                    difference += "Toplam Kamera Sayısı " + item.ParkTotalCameraCount + " = " + newRequest.ParkTotalCameraCount + ", ";
                }
                if (item.ParkFixedCameraCount != newRequest.ParkFixedCameraCount)
                {
                    difference += "Sabit Kamera Sayısı " + item.ParkFixedCameraCount + "  = " + newRequest.ParkFixedCameraCount + ", ";
                }

                if (item.ParkDomeCameraCount != newRequest.ParkDomeCameraCount)
                {
                    difference += "Hareketli Kamera Sayısı " + item.ParkDomeCameraCount + " = " + newRequest.ParkDomeCameraCount + ", ";
                }
                if (item.ParkNvrAdress != newRequest.ParkNvrAdress)
                {
                    difference += "Kayıt Yeri " + item.ParkNvrAdress + "  = " + newRequest.ParkNvrAdress + ", ";
                }
                if (item.ParkLiveMonitoring != newRequest.ParkLiveMonitoring)
                {
                    difference += "Canlı İzleniyor Mu? " + item.ParkLiveMonitoring + " = " + newRequest.ParkLiveMonitoring + ", ";
                }
                if (item.ParkExplain != newRequest.ParkExplain)
                {
                    difference += "Açıklama " + item.ParkExplain + " = " + newRequest.ParkExplain + ", ";
                }
                if (item.ParkSupervisor != newRequest.ParkSupervisor)
                {
                    difference += "Park Sorumlusu " + item.ParkSupervisor + " = " + newRequest.ParkSupervisor + ", ";
                }
                if (item.ParkSupervisorTel != newRequest.ParkSupervisorTel)
                {
                    difference += "Park Sorumlusu Telefonu " + item.ParkSupervisorTel + " = " + newRequest.ParkSupervisorTel + ", ";
                }

            }
            difference += " olacak şekilde güncelledi.";

            return difference;

        }

        public string ProjectAdd(KgysPlannedViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.KgysName + " isimli Planlanan noktayı; ";

            if (newRequest.District != null)
            {
                difference += "İlçesini  = " + newRequest.District + ", ";
            }
            if (newRequest.Neighbourhood != null)
            {
                difference += "Mahallesini  = " + newRequest.Neighbourhood + ", ";
            }
            if (newRequest.CameraCount != null)
            {
                difference += "Sabit Kamera Sayısı  = " + newRequest.CameraCount + ", ";
            }
            if (newRequest.DomeCameraCount != null)
            {
                difference += "Hareketli Kamera Sayısı  = " + newRequest.DomeCameraCount + ", ";
            }
            if (newRequest.PtsCameraCount != null)
            {
                difference += "PTS Kamera Sayısı  = " + newRequest.PtsCameraCount + ", ";
            }
            if (newRequest.RequestType != null)
            {
                difference += "Tipi  = " + newRequest.RequestType + ", ";
            }
            if (newRequest.ProjectCoordinate != null)
            {
                difference += "Koordinatı = " + newRequest.ProjectCoordinate + ", ";
            }
            if (newRequest.ProjectProtocol != null)
            {
                difference += "Protokol Var Mı?  = " + newRequest.ProjectProtocol + ", ";
            }
            if (newRequest.ProjectExcavation != null)
            {
                difference += "Kazı Yapıldı Mı?  = " + newRequest.ProjectExcavation + ", ";
            }
            if (newRequest.ProjectBasis != null)
            {
                difference += "Ankaraj Tamamlandı Mı? = " + newRequest.ProjectBasis + ", ";
            }
            if (newRequest.ProjectPole != null)
            {
                difference += "Direk Dikildi Mi?  = " + newRequest.ProjectPole + ", ";
            }
            if (newRequest.ProjectCabinet != null)
            {
                difference += "Kabinet Takıldı Mı? = " + newRequest.ProjectCabinet + ", ";
            }
            if (newRequest.ProjectAssembly != null)
            {
                difference += "Kamera Montajı Yapıldı mı? = " + newRequest.ProjectAssembly + ", ";
            }
            if (newRequest.ProjectEnergyCable != null)
            {
                difference += "Enerji Kablosu Çekildi Mi? = " + newRequest.ProjectEnergyCable + ", ";
            }
            if (newRequest.ProjectEnergyConnect != null)
            {
                difference += "Enerji Bağlandı Mı? = " + newRequest.ProjectEnergyConnect + ", ";
            }
            if (newRequest.ProjectFiberOptic != null)
            {
                difference += "Fiber Çekildi Mi? = " + newRequest.ProjectFiberOptic + ", ";
            }
            if (newRequest.ProjectConnection != null)
            {
                difference += "Bağlantı Sağlandı Mı? = " + newRequest.ProjectConnection + ", ";
            }
            if (newRequest.ProjectRecording != null)
            {
                difference += "Kayıt Başladı Mı? = " + newRequest.ProjectRecording + ", ";
            }
            if (newRequest.ProjectDescription != null)
            {
                difference += "Açıklama = " + newRequest.ProjectDescription + ", ";
            }
            if (newRequest.ProjectTeam != null)
            {
                difference += "İş Hangi Takımda = " + newRequest.ProjectTeam + ", ";
            }
            if (newRequest.ProjectStatus != null)
            {
                difference += "Proje Durumu = " + newRequest.ProjectStatus + ", ";
            }
            if (newRequest.ProjectName != null)
            {
                difference += "Proje Adı = " + newRequest.ProjectName + ", ";
            }
            if (newRequest.MaximoId != null)
            {
                difference += "Maximo ID = " + newRequest.MaximoId + ", ";
            }
            if (newRequest.LastStatus != null)
            {
                difference += "Son Durum = " + newRequest.LastStatus + ", ";
            }
            if (newRequest.DateOfPlanned != null)
            {
                difference += "Planlandığı Tarih = " + newRequest.DateOfPlanned + ", ";
            }
            if (newRequest.DateOfActivated != null)
            {
                difference += "Aktif Olduğu Tarih = " + newRequest.DateOfActivated + ", ";
            }
            difference += " olacak şekilde ekledi.";
            return difference;

        }

        public string ProjectUptade(KgysPlannedModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.KgysName + " isimli Planlanan noktayı; ";
            var request = _context.KgysPlanned.AsNoTracking().Where(x => x.Id == newRequest.Id).ToList();
            foreach (var item in request)
            {
                if (item.KgysName != newRequest.KgysName)
                {
                    difference += "İsmini  " + item.KgysName + " = " + newRequest.KgysName + ", ";
                }
                if (item.District != newRequest.District)
                {
                    difference += "İlçesini  = " + newRequest.District + ", ";
                }
                if (item.Neighbourhood != newRequest.Neighbourhood)
                {
                    difference += "Mahallesini  = " + newRequest.Neighbourhood + ", ";
                }
                if (item.CameraCount != newRequest.CameraCount)
                {
                    difference += "Sabit Kamera Sayısını  = " + newRequest.CameraCount + ", ";
                }
                if (item.DomeCameraCount != newRequest.DomeCameraCount)
                {
                    difference += "Hareketli Kamera Sayısını  = " + newRequest.DomeCameraCount + ", ";
                }
                if (item.PtsCameraCount != newRequest.PtsCameraCount)
                {
                    difference += "PTS Kamera Sayısını  = " + newRequest.PtsCameraCount + ", ";
                }
                if (item.RequestType != newRequest.RequestType)
                {
                    difference += "Tipini  = " + newRequest.RequestType + ", ";
                }
                if (item.ProjectCoordinate != newRequest.ProjectCoordinate)
                {
                    difference += "Koordinatını = " + newRequest.ProjectCoordinate + ", ";
                }
                if (item.ProjectProtocol != newRequest.ProjectProtocol)
                {
                    difference += "Protokol Var Mı?  = " + newRequest.ProjectProtocol + ", ";
                }
                if (item.ProjectExcavation != newRequest.ProjectExcavation)
                {
                    difference += "Kazı Yapıldı Mı?  = " + newRequest.ProjectExcavation + ", ";
                }
                if (item.ProjectBasis != newRequest.ProjectBasis)
                {
                    difference += "Ankaraj Tamamlandı Mı? = " + newRequest.ProjectBasis + ", ";
                }
                if (item.ProjectPole != newRequest.ProjectPole)
                {
                    difference += "Direk Dikildi Mi?  = " + newRequest.ProjectPole + ", ";
                }
                if (item.ProjectCabinet != newRequest.ProjectCabinet)
                {
                    difference += "Kabinet Takıldı Mı? = " + newRequest.ProjectCabinet + ", ";
                }
                if (item.ProjectAssembly != newRequest.ProjectAssembly)
                {
                    difference += "Kamera Montajı Yapıldı mı? = " + newRequest.ProjectAssembly + ", ";
                }
                if (item.ProjectEnergyCable != newRequest.ProjectEnergyCable)
                {
                    difference += "Enerji Kablosu Çekildi Mi? = " + newRequest.ProjectEnergyCable + ", ";
                }
                if (item.ProjectEnergyConnect != newRequest.ProjectEnergyConnect)
                {
                    difference += "Enerji Bağlandı Mı? = " + newRequest.ProjectEnergyConnect + ", ";
                }
                if (item.ProjectFiberOptic != newRequest.ProjectFiberOptic)
                {
                    difference += "Fiber Çekildi Mi? = " + newRequest.ProjectFiberOptic + ", ";
                }
                if (item.ProjectConnection != newRequest.ProjectConnection)
                {
                    difference += "Bağlantı Sağlandı Mı? = " + newRequest.ProjectConnection + ", ";
                }
                if (item.ProjectRecording != newRequest.ProjectRecording)
                {
                    difference += "Kayıt Başladı Mı? = " + newRequest.ProjectRecording + ", ";
                }
                if (item.ProjectDescription != newRequest.ProjectDescription)
                {
                    difference += "Açıklama = " + newRequest.ProjectDescription + ", ";
                }
                if (item.ProjectTeam != newRequest.ProjectTeam)
                {
                    difference += "İş Hangi Takımda = " + newRequest.ProjectTeam + ", ";
                }
                if (item.ProjectStatus != newRequest.ProjectStatus)
                {
                    difference += "Proje Durumu = " + newRequest.ProjectStatus + ", ";
                }
                if (item.ProjectName != newRequest.ProjectName)
                {
                    difference += "Proje Adı = " + newRequest.ProjectName + ", ";
                }
                if (item.MaximoId != newRequest.MaximoId)
                {
                    difference += "Maximo ID = " + newRequest.MaximoId + ", ";
                }
                if (item.LastStatus != newRequest.LastStatus)
                {
                    difference += "Son Durum = " + newRequest.LastStatus + ", ";
                }
                if (item.DateOfPlanned != newRequest.DateOfPlanned)
                {
                    difference += "Planlandığı Tarih = " + newRequest.DateOfPlanned + ", ";
                }
                if (item.DateOfActivated != newRequest.DateOfActivated)
                {
                    difference += "Aktif Olduğu Tarih = " + newRequest.DateOfActivated + ", ";
                }
            }
            difference += " olacak şekilde güncelledi.";
            return difference;

        }

        public string SecondaryRequestAdd(SecondaryRequestViewModel newRequest)
        {

            string difference = newRequest.UpdatedPersonel + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.Name + " isimli Tali İzleme Başvurusunu; ";

            if (newRequest.District != null)
            {
                difference += "İlçesini  = " + newRequest.District + ", ";
            }
            if (newRequest.InstitutialFO != null)
            {
                difference += "Kurumsal altyapıyı  = " + newRequest.InstitutialFO + ", ";
            }
            if (newRequest.TtDistance != null)
            {
                difference += "TTNET Metrajını  = " + newRequest.TtDistance + ", ";
            }
            if (newRequest.TtRequest != null)
            {
                difference += "TTNET Talebini  = " + newRequest.TtRequest + ", ";
            }
            if (newRequest.TtStatus != null)
            {
                difference += "TTNET Durumunu  = " + newRequest.TtStatus + ", ";
            }
            if (newRequest.Switch != null)
            {
                difference += "Switchi = " + newRequest.Switch + ", ";
            }
            if (newRequest.Configuration != null)
            {
                difference += "Konfigürasyon = " + newRequest.Configuration + ", ";
            }
            if (newRequest.Cable != null)
            {
                difference += "Kablolama Durumu  = " + newRequest.Cable + ", ";
            }
            if (newRequest.PcImaje != null)
            {
                difference += "PC Imajı Atıldı Mı?  = " + newRequest.PcImaje + ", ";
            }
            if (newRequest.Status != null)
            {
                difference += "Genel Durumu = " + newRequest.Status + ", ";
            }

            difference += " olacak şekilde ekledi.";
            return difference;

        }


        public string SecondaryRequestUpdate(SecondaryRequestViewModel newRequest)
        {

            string difference = newRequest.UpdatedPersonel + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.Name + " isimli Tali İzleme Başvurusunu; ";
            var request = _context.SecondaryRequestModels.AsNoTracking().Where(x => x.Id == newRequest.Id).ToList();
            foreach (var item in request)
            {
                if (item.Name != newRequest.Name)
                {
                    difference += "İsmini  = " + newRequest.Name + ", ";
                }
                if (item.District != newRequest.District)
                {
                    difference += "İlçesini  = " + newRequest.District + ", ";
                }
                if (item.InstitutialFO != newRequest.InstitutialFO)
                {
                    difference += "Kurumsal altyapıyı  = " + newRequest.InstitutialFO + ", ";
                }
                if (item.TtDistance != newRequest.TtDistance)
                {
                    difference += "TTNET Metrajını  = " + newRequest.TtDistance + ", ";
                }
                if (item.TtRequest != newRequest.TtRequest)
                {
                    difference += "TTNET Talebini  = " + newRequest.TtRequest + ", ";
                }
                if (item.TtStatus != newRequest.TtStatus)
                {
                    difference += "TTNET Durumunu  = " + newRequest.TtStatus + ", ";
                }
                if (item.Switch != newRequest.Switch)
                {
                    difference += "Switchi = " + newRequest.Switch + ", ";
                }
                if (item.Configuration != newRequest.Configuration)
                {
                    difference += "Konfigürasyon = " + newRequest.Configuration + ", ";
                }
                if (item.Cable != newRequest.Cable)
                {
                    difference += "Kablolama Durumu  = " + newRequest.Cable + ", ";
                }
                if (item.PcImaje != newRequest.PcImaje)
                {
                    difference += "PC Imajı Atıldı Mı?  = " + newRequest.PcImaje + ", ";
                }
                if (item.Status != newRequest.Status)
                {
                    difference += "Genel Durumu = " + newRequest.Status + ", ";
                }
            }
            difference += " olacak şekilde güncelledi.";
            return difference;

        }
        public string TelecomFoAdd(TelecomFoViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.Name + " isimli Tali İzleme Başvurusunu; ";

            if (newRequest.District != null)
            {
                difference += "İlçesini  = " + newRequest.District + ", ";
            }
            if (newRequest.EbysNumber != null)
            {
                difference += "Ebys Numarasını  = " + newRequest.EbysNumber + ", ";
            }
            if (newRequest.Aplication != null)
            {
                difference += "Başvuru Tipi  = " + newRequest.Aplication + ", ";
            }
            if (newRequest.TemosNumber != null)
            {
                difference += "Devre Numarasını  = " + newRequest.TemosNumber + ", ";
            }
            if (newRequest.Vlan != null)
            {
                difference += "Vlan'ını  = " + newRequest.Vlan + ", ";
            }
            if (newRequest.IpAddress != null)
            {
                difference += "Ip Adresini = " + newRequest.IpAddress + ", ";
            }
            if (newRequest.Coordinate != null)
            {
                difference += "Koordinatını = " + newRequest.Coordinate + ", ";
            }
            if (newRequest.CreatedTime != null)
            {
                difference += "Başvuru Tarihini  = " + newRequest.CreatedTime + ", ";
            }
            if (newRequest.SlaStartTime != null)
            {
                difference += "SLA Başlangıç Tarihi  = " + newRequest.SlaStartTime + ", ";
            }
            if (newRequest.EpymStatus != null)
            {
                difference += "Epmy = " + newRequest.EpymStatus + ", ";
            }
            if (newRequest.EomStatus != null)
            {
                difference += "Eom = " + newRequest.EomStatus + ", ";
            }
            if (newRequest.MplsOperationStatus != null)
            {
                difference += "Mplss  = " + newRequest.MplsOperationStatus + ", ";
            }
            if (newRequest.MplsNmsStatus != null)
            {
                difference += "MplsNms = " + newRequest.MplsNmsStatus + ", ";
            }
            if (newRequest.CheckEGM != null)
            {
                difference += "EGM Kontrol = " + newRequest.CheckEGM + ", ";
            }
            if (newRequest.ReportToEgm != null)
            {
                difference += "ReportToEgm = " + newRequest.ReportToEgm + ", ";
            }
            if (newRequest.Description != null)
            {
                difference += "Açıklama = " + newRequest.Description + ", ";
            }
            if (newRequest.KuppaDate != null)
            {
                difference += "Kuppa Tarihi = " + newRequest.KuppaDate + ", ";
            }
            if (newRequest.KuppaID != null)
            {
                difference += "KuppaID = " + newRequest.KuppaID + ", ";
            }
            if (newRequest.KuppaPStatus != null)
            {
                difference += "Kuppa Durumu = " + newRequest.KuppaPStatus + ", ";
            }
            if (newRequest.KuppaDistance != null)
            {
                difference += "Kuppa Mesafesi = " + newRequest.KuppaDistance + ", ";
            }
            if (newRequest.KuppaPrice != null)
            {
                difference += "Kuppa Bedeli = " + newRequest.KuppaPrice + ", ";

            }
            if (newRequest.Team != null)
            {
                difference += "Telekom Takımı = " + newRequest.Team + ", ";
            }
            difference += " olacak şekilde ekledi.";
            return difference;

        }

        public string TelecomFoUpdate(TelecomFoViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.Name + " isimli Tali İzleme Başvurusunu; ";
            var request = _context.Products.AsNoTracking().Where(x => x.Id == newRequest.Id).ToList();
            foreach (var item in request)
            {
                if (item.Name != newRequest.Name)
                {
                    difference += "İsmini  = " + newRequest.Name + ", ";
                }
                if (item.District != newRequest.District)
                {
                    difference += "İlçesini  = " + newRequest.District + ", ";
                }
                if (item.EbysNumber != newRequest.EbysNumber)
                {
                    difference += "Ebys Numarasını  = " + newRequest.EbysNumber + ", ";
                }
                if (item.Aplication != newRequest.Aplication)
                {
                    difference += "Başvuru Tipi  = " + newRequest.Aplication + ", ";
                }
                if (item.Vlan != newRequest.Vlan)
                {
                    difference += "Vlan'ını  = " + newRequest.Vlan + ", ";
                }
                if (item.IpAddress != newRequest.IpAddress)
                {
                    difference += "Ip Adresini = " + newRequest.IpAddress + ", ";
                }
                if (item.Coordinate != newRequest.Coordinate)
                {
                    difference += "Koordinatını = " + newRequest.Coordinate + ", ";
                }
                if (item.CreatedTime != newRequest.CreatedTime)
                {
                    difference += "Başvuru Tarihini  = " + newRequest.CreatedTime + ", ";
                }
                if (item.SlaStartTime != newRequest.SlaStartTime)
                {
                    difference += "SLA Başlangıç Tarihi  = " + newRequest.SlaStartTime + ", ";
                }
                if (item.EpymStatus != newRequest.EpymStatus)
                {
                    difference += "Epmy = " + newRequest.EpymStatus + ", ";
                }
                if (item.EomStatus != newRequest.EomStatus)
                {
                    difference += "Eom = " + newRequest.EomStatus + ", ";
                }
                if (item.MplsOperationStatus != newRequest.MplsOperationStatus)
                {
                    difference += "Mplss  = " + newRequest.MplsOperationStatus + ", ";
                }
                if (item.MplsNmsStatus != newRequest.MplsNmsStatus)
                {
                    difference += "MplsNms = " + newRequest.MplsNmsStatus + ", ";
                }
                if (item.CheckEGM != newRequest.CheckEGM)
                {
                    difference += "EGM Kontrol = " + newRequest.CheckEGM + ", ";
                }
                if (item.ReportToEgm != newRequest.ReportToEgm)
                {
                    difference += "ReportToEgm = " + newRequest.ReportToEgm + ", ";
                }
                if (item.Description != newRequest.Description)
                {
                    difference += "Açıklama = " + newRequest.Description + ", ";
                }
                if (item.KuppaDate != newRequest.KuppaDate)
                {
                    difference += "Kuppa Tarihi = " + newRequest.KuppaDate + ", ";
                }
                if (item.KuppaID != newRequest.KuppaID)
                {
                    difference += "KuppaID = " + newRequest.KuppaID + ", ";
                }
                if (item.KuppaPStatus != newRequest.KuppaPStatus)
                {
                    difference += "Kuppa Durumu = " + newRequest.KuppaPStatus + ", ";
                }
                if (item.KuppaDistance != newRequest.KuppaDistance)
                {
                    difference += "Kuppa Mesafesi = " + newRequest.KuppaDistance + ", ";
                }
                if (item.KuppaPrice != newRequest.KuppaPrice)
                {
                    difference += "Kuppa Bedeli = " + newRequest.KuppaPrice + ", ";

                }
                if (item.Team != newRequest.Team)
                {
                    difference += "Telekom Takımı = " + newRequest.Team + ", ";
                }
            }
            difference += " olacak şekilde güncelledi.";
            return difference;
        }

        public string TenderAdd(TenderProjectsViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.ProjectName + " isimli İhaleyi; ";

            if (newRequest.RequestEbysNo != null)
            {
                difference += "Talep EBYS No  = " + newRequest.RequestEbysNo + ", ";
            }
            if (newRequest.WhoRequested != null)
            {
                difference += "Talep Eden  = " + newRequest.WhoRequested + ", ";
            }
            if (newRequest.Evaluation != null)
            {
                difference += "Değerlendirme  = " + newRequest.Evaluation + ", ";
            }
            if (newRequest.AnswerEbysNo != null)
            {
                difference += "Cevap EBYS No  = " + newRequest.AnswerEbysNo + ", ";
            }
            if (newRequest.CommissionerConfirmation != null)
            {
                difference += "Şartname Komisyon oluru alındı mı?  = " + newRequest.CommissionerConfirmation + ", ";
            }
            if (newRequest.ProjectName != null)
            {
                difference += "İşin Adı = " + newRequest.ProjectName + ", ";
            }
            if (newRequest.ProjectDescription != null)
            {
                difference += "Açıklaması = " + newRequest.ProjectDescription + ", ";
            }
            if (newRequest.Specification != null)
            {
                difference += "Şartname Geldi Mi?  = " + newRequest.Specification + ", ";
            }
            if (newRequest.SpecificationApproval != null)
            {
                difference += "Şartname Onayı ?  = " + newRequest.SpecificationApproval + ", ";
            }
            if (newRequest.UnitOfTender != null)
            {
                difference += "İhale Birimi = " + newRequest.UnitOfTender + ", ";
            }
            if (newRequest.SendOfTender != null)
            {
                difference += "İhale Birimine Yazıldı Mı? = " + newRequest.SendOfTender + ", ";
            }
            if (newRequest.TenderTime != null)
            {
                difference += "İhale Tarihi = " + newRequest.TenderTime + ", ";
            }
            if (newRequest.WhoResponse != null)
            {
                difference += "Kontrol Personeli = " + newRequest.WhoResponse + ", ";
            }
            if (newRequest.DateOfStart != null)
            {
                difference += "İşe Başlama  = " + newRequest.DateOfStart + ", ";
            }
            if (newRequest.Status != null)
            {
                difference += "İşin Aşaması = " + newRequest.Status + ", ";
            }
            if (newRequest.DateOfEnd != null)
            {
                difference += "İş Bitimi = " + newRequest.DateOfEnd + ", ";
            }
            //if (newRequest.Commissioner != null)
            //{
            //    difference += "Komisyon Üyesi = " + newRequest.Commissioner + ", ";
            //}
            if (newRequest.WhoHasJob != null)
            {
                difference += "İhaleyi Alan = " + newRequest.WhoHasJob + ", ";
            }
            if (newRequest.PriceOfWork != null)
            {
                difference += "İhale Bedeli = " + newRequest.PriceOfWork + ", ";
            }
            if (newRequest.PriceStatus != null)
            {
                difference += "Hakediş Ödendi Mi? = " + newRequest.PriceStatus + ", ";
            }
            if (newRequest.PriceEbysNo != null)
            {
                difference += "Hakediş EBYS No = " + newRequest.PriceEbysNo + ", ";

            }

            difference += " olacak şekilde ekledi.";
            return difference;

        }

        public string TenderUpdate(TenderProjectsViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.ProjectName + " isimli İhalenin; ";
            var request = _context.TenderProjects.AsNoTracking().Where(x => x.Id == newRequest.Id).ToList();
            foreach (var item in request)
            {
                if (item.RequestEbysNo != newRequest.RequestEbysNo)
                {
                    difference += "Talep EBYS No  = " + newRequest.RequestEbysNo + ", ";
                }
                if (item.WhoRequested != newRequest.WhoRequested)
                {
                    difference += "Talep Eden  = " + newRequest.WhoRequested + ", ";
                }
                if (item.Evaluation != newRequest.Evaluation)
                {
                    difference += "Değerlendirme  = " + newRequest.Evaluation + ", ";
                }
                if (item.AnswerEbysNo != newRequest.AnswerEbysNo)
                {
                    difference += "Cevap EBYS No  = " + newRequest.AnswerEbysNo + ", ";
                }
                if (item.CommissionerConfirmation != newRequest.CommissionerConfirmation)
                {
                    difference += "Şartname Komisyon Oluru Alındı Mı?  = " + newRequest.CommissionerConfirmation + ", ";
                }
                if (item.ProjectName != newRequest.ProjectName)
                {
                    difference += "İşin Adı = " + newRequest.ProjectName + ", ";
                }
                if (item.ProjectDescription != newRequest.ProjectDescription)
                {
                    difference += "Açıklaması = " + newRequest.ProjectDescription + ", ";
                }
                if (item.Specification != newRequest.Specification)
                {
                    difference += "Şartname Geldi Mi?  = " + newRequest.Specification + ", ";
                }
                if (item.SpecificationApproval != newRequest.SpecificationApproval)
                {
                    difference += "Şartname Onayı Alını Mı?  = " + newRequest.SpecificationApproval + ", ";
                }
                if (item.UnitOfTender != newRequest.UnitOfTender)
                {
                    difference += "İhale Birimi = " + newRequest.UnitOfTender + ", ";
                }
                if (item.SendOfTender != newRequest.SendOfTender)
                {
                    difference += "İhale Birimine Yazıldı Mı? = " + newRequest.SendOfTender + ", ";
                }
                if (item.TenderTime != newRequest.TenderTime)
                {
                    difference += "İhale Tarihi = " + newRequest.TenderTime + ", ";
                }
                if (item.WhoResponse != newRequest.WhoResponse)
                {
                    difference += "Kontrol Personeli = " + newRequest.WhoResponse + ", ";
                }
                if (item.DateOfStart != newRequest.DateOfStart)
                {
                    difference += "İşe Başlama  = " + newRequest.DateOfStart + ", ";
                }
                if (item.Status != newRequest.Status)
                {
                    difference += "İşin Aşaması = " + newRequest.Status + ", ";
                }
                if (item.DateOfEnd != newRequest.DateOfEnd)
                {
                    difference += "İş Bitimi = " + newRequest.DateOfEnd + ", ";
                }
                //if (item.Commissioner != newRequest.Commissioner)
                //{
                //    difference += "Komisyon Üyesi = " + newRequest.Commissioner + ", ";
                //}
                if (item.WhoHasJob != newRequest.WhoHasJob)
                {
                    difference += "İhaleyi Alan = " + newRequest.WhoHasJob + ", ";
                }
                if (item.PriceOfWork != newRequest.PriceOfWork)
                {
                    difference += "İhale Bedeli = " + newRequest.PriceOfWork + ", ";
                }
                if (item.PriceStatus != newRequest.PriceStatus)
                {
                    difference += "Hakediş Ödendi Mi? = " + newRequest.PriceStatus + ", ";
                }
                if (item.PriceEbysNo != newRequest.PriceEbysNo)
                {
                    difference += "Hakediş EBYS No = " + newRequest.PriceEbysNo + ", ";

                }
            }
            difference += " olacak şekilde güncelledi.";
            return difference;
        }

        public string WaitingJobsAdd(WaitingJobsViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.Summary + " işini; ";

            if (newRequest.EbysNumber != null)
            {
                difference += "EBYS No  = " + newRequest.EbysNumber + ", ";
            }
            if (newRequest.District != null)
            {
                difference += "Yazan Birim  = " + newRequest.District + ", ";
            }
            if (newRequest.Summary != null)
            {
                difference += "Özeti  = " + newRequest.Summary + ", ";
            }
            if (newRequest.TalkToManager != null)
            {
                difference += "Birim Amiriyle GÖrüşüldü Mü?  = " + newRequest.TalkToManager + ", ";
            }
            if (newRequest.StatusMessage != null)
            {
                difference += "Talimat = " + newRequest.StatusMessage + ", ";
            }
            if (newRequest.PlanedDate != null)
            {
                difference += "Yapılması Planlanan TArih  = " + newRequest.PlanedDate + ", ";
            }
            if (newRequest.FinishOrNot != null)
            {
                difference += "Tamamlandı Mı?  = " + newRequest.FinishOrNot + ", ";
            }
            if (newRequest.Material != null)
            {
                difference += "Kullanılan Malzeme = " + newRequest.Material + ", ";
            }
            if (newRequest.Discription != null)
            {
                difference += "Açıklama = " + newRequest.Discription + ", ";
            }
            if (newRequest.Answer != null)
            {
                difference += "Cevap Yazıldı Mı? = " + newRequest.Answer + ", ";
            }
            if (newRequest.EBYSanswer != null)
            {
                difference += "Cevap EBYS No = " + newRequest.EBYSanswer + ", ";
            }
            if (newRequest.FinishDate != null)
            {
                difference += "İşe Bittiği  = " + newRequest.FinishDate + ", ";
            }
            if (newRequest.Status != null)
            {
                difference += "İşin Durumu = " + newRequest.Status + ", ";
            }
            difference += " olacak şekilde ekledi.";
            return difference;

        }

        public string WaitingJobsUpdate(WaitingJobsViewModel newRequest)
        {

            string difference = newRequest.UpdatedUser + " kullanıcısı " + newRequest.UpdatedTime + " tarihinde " + newRequest.Summary + " işini; ";
            var request = _context.WaitingJobs.AsNoTracking().Where(x => x.Id == newRequest.Id).ToList();
            foreach (var item in request)
            {
                if
                    (item.EbysNumber != newRequest.EbysNumber)
                {
                    difference += "EBYS No  = " + newRequest.EbysNumber + ", ";
                }
                if (item.ArrivalDate != newRequest.ArrivalDate)
                {
                    difference += "Geliş Tarihi  = " + newRequest.ArrivalDate + ", ";
                }
                if (item.District != newRequest.District)
                {
                    difference += "Yazan Birim  = " + newRequest.District + ", ";
                }
                if (item.Summary != newRequest.Summary)
                {
                    difference += "Özeti  = " + newRequest.Summary + ", ";
                }
                if (item.TalkToManager != newRequest.TalkToManager)
                {
                    difference += "Birim Amiriyle GÖrüşüldü Mü?  = " + newRequest.TalkToManager + ", ";
                }
                if (item.StatusMessage != newRequest.StatusMessage)
                {
                    difference += "Talimat = " + newRequest.StatusMessage + ", ";
                }
                if (item.PlanedDate != newRequest.PlanedDate)
                {
                    difference += "Yapılması Planlanan TArih  = " + newRequest.PlanedDate + ", ";
                }
                if (item.FinishOrNot != newRequest.FinishOrNot)
                {
                    difference += "Tamamlandı Mı?  = " + newRequest.FinishOrNot + ", ";
                }
                if (item.Material != newRequest.Material)
                {
                    difference += "Kullanılan Malzeme = " + newRequest.Material + ", ";
                }
                if (item.Discription != newRequest.Discription)
                {
                    difference += "Açıklama = " + newRequest.Discription + ", ";
                }
                if (item.Answer != newRequest.Answer)
                {
                    difference += "Cevap Yazıldı Mı? = " + newRequest.Answer + ", ";
                }
                if (item.EBYSanswer != newRequest.EBYSanswer)
                {
                    difference += "Cevap EBYS No = " + newRequest.EBYSanswer + ", ";
                }
                if (item.FinishDate != newRequest.FinishDate)
                {
                    difference += "İşe Bittiği  = " + newRequest.FinishDate + ", ";
                }
                if (item.Status != newRequest.Status)
                {
                    difference += "İşin Durumu = " + newRequest.Status + ", ";
                }
            }
            difference += " olacak şekilde güncelledi.";
            return difference;
        }
        public string UserEdit(UserEditViewModel newRequest)
        {
            var currentUser = _context.Users.AsNoTracking().Where(x => x.Id == newRequest.Id);
            string difference = " isimli kullanıcıya ait ";
            foreach (var item in currentUser)
            {
                if (item.FullName != newRequest.FullName)
                {
                    difference += "Adını = " + newRequest.FullName + ", ";
                }
                if (item.Email != newRequest.Email)
                {
                    difference += "E-mailini = " + newRequest.Email + ", ";
                }
                if (item.UserName != newRequest.UserName)
                {
                    difference += "Kullanıcı Adını = " + newRequest.Email + ", ";
                }
            }
            return difference;
        }

        public string MaterialsDetailDifference(MaterialsDetailViewModel newRequest)
        {
            var currentMaterial = _context.MaterialsDetail.AsNoTracking().Where(x => x.Id == newRequest.Id);
            string difference = newRequest.UpdateUser + " kullanıcısı " + newRequest.UpdateTime + " tarihinde " + newRequest.SerialNo + " seri nolu ürünü; ";
            foreach (var item in currentMaterial)
            {
                if (item.Material != newRequest.Material)
                {
                    difference += "ana ürünü = " + newRequest.Material + ", ";
                }
                if (item.MaterialBrand != newRequest.MaterialBrand)
                {
                    difference += "Marka / Modelini = " + newRequest.MaterialBrand + ", ";
                }
                if (item.MaterialBrandCode!= newRequest.MaterialBrandCode)
                {
                    difference += "Kodunu = " + newRequest.MaterialBrandCode + ", ";
                }
                if (item.TechnicalSpecification1 != newRequest.TechnicalSpecification1)
                {
                    difference += "Teknik Özelliğini = " + newRequest.TechnicalSpecification1 + ", ";
                }
                if (item.SerialNo != newRequest.SerialNo)
                {
                    difference += "Seri Numarasını = " + newRequest.SerialNo + ", ";
                }
                if (item.SerialNo != newRequest.SerialNo)
                {
                    difference += "Seri Numarasını = " + newRequest.SerialNo + ", ";
                }
                if (item.Explanation != newRequest.Explanation)
                {
                    difference += "Geliş Açıklamasını = " + newRequest.Explanation + ", ";
                }
                if (item.Description != newRequest.Description)
                {
                    difference += "Çıkış Açıklamasını = " + newRequest.Description + ", ";
                }
                if (item.Year != newRequest.Year)
                {
                    difference += "Yılını = " + newRequest.Year + ", ";
                }
                if (item.EtmysNumber != newRequest.EtmysNumber)
                {
                    difference += "Etmys numarasını = " + newRequest.EtmysNumber + ", ";
                }
                if (item.WhoTake != newRequest.WhoTake)
                {
                    difference += "Verildiği Yer = " + newRequest.WhoTake + ", ";
                }
                if (item.Shelf != newRequest.Shelf)
                {
                    difference += "Rafını = " + newRequest.Shelf + ", ";
                }
                if (item.AddTime != newRequest.AddTime)
                {
                    difference += "Eklenme Zamanını = " + newRequest.AddTime + ", ";
                }
                if (item.RemoveTime != newRequest.RemoveTime)
                {
                    difference += "Çıkış Zamanını = " + newRequest.RemoveTime + ", ";
                }
                if (item.Status != newRequest.Status)
                {
                    difference += "Durumunu = " + newRequest.Status + ", ";
                }
               


            }
            return difference;
        }

        public string IpPhoneAdd(IpPhoneViewModel newRequest)
        {
            string difference = DateTime.Now + " tarihinde, ";
            if (newRequest.Campus != null)
            {
                difference += newRequest.Campus + " binasında, ";
            }
            if (newRequest.Unit != null)
            {
                difference += newRequest.Unit + " birimine, ";
            }
            if (newRequest.CentralBrand != null)
            {
                difference += newRequest.CentralBrand + " markalı santrali olduğu, ";
            }
            if (newRequest.PoeSwitch != false)
            {
                difference += " poe switch olduğu, ";
            }
            if (newRequest.CableIsTrue != false)
            {
                difference += " altyapısının olduğu, ";
            }
            if (newRequest.PhonePieces != 0)
            {
                difference += newRequest.PhonePieces+" adet Ip Telefon, ";
            }
            if (newRequest.Priority != null)
            {
                difference += newRequest.Priority+ " önceliği, ";
            }
            difference += "bilgileri ile IP Telefon Kurulumu Projesi ekledi.";
            return difference;
        }

        public string IpPhoneUpdate(IpPhoneModel newRequest)
        {
            string difference = " tarihinde, ";
            var exRequest = _context.IpPhoneProject.Where(x=>x.Id == newRequest.Id).FirstOrDefault()!;
            if (newRequest.Campus != exRequest.Campus)
            {
                difference += exRequest.Campus + " binasını " + newRequest.Campus+", ";
            }
            if (newRequest.Unit != exRequest.Unit)
            {
                difference += exRequest.Unit + " birimini " + newRequest.Unit + ", ";
            }
            if (newRequest.CentralBrand != exRequest.CentralBrand)
            {
                difference += exRequest.CentralBrand + " markalı santralini, "+newRequest.CentralBrand + ", ";
            }
            if (newRequest.PhonePieces != exRequest.PhonePieces)
            {
                difference += exRequest.PhonePieces+" telefon adetini " +newRequest.PhonePieces + ", ";
            }
            if (newRequest.Priority != exRequest.Priority)
            {
                difference += exRequest.Priority + " önceliğini "+ newRequest.Priority + ", ";
            }
            difference += "bilgileri olacak şekilde IP Telefon Kurulumu Projesi bilgilerini güncelledi.";
            return difference;
        }

        public string CctvProjectDetailUpdate(CctvModel newRequest)
        {
            string difference =DateTime.Now+ " tarihinde, ";
            var exRequest = _context.CctvProjectDetail.AsNoTracking().Where(x => x.Id == newRequest.Id).FirstOrDefault()!;
            if (newRequest.ProjectName != exRequest.ProjectName)
            {
                difference += exRequest.ProjectName + " fazını " + newRequest.ProjectName + ", ";
            }
            if (newRequest.ProjectDistrict != exRequest.ProjectDistrict)
            {
                difference += exRequest.ProjectDistrict + " ilçesi " + newRequest.ProjectDistrict + ", ";
            }
            if (newRequest.Unit != exRequest.Unit)
            {
                difference += exRequest.Unit + " birimini " + newRequest.Unit + ", ";
            }
            if (newRequest.ExProjectName != exRequest.ExProjectName)
            {
                difference += exRequest.ExProjectName + " eski projesini, " + newRequest.ExProjectName + ", ";
            }
            if (newRequest.ProjectReason != exRequest.ProjectReason)
            {
                difference += exRequest.ProjectReason + " nedenini " + newRequest.ProjectReason + ", ";
            }           
            difference += "bilgileri olacak şekilde Cctv Kurulum Projesi bilgilerini güncelledi.";
            return difference;
        }

        public string CctvProjectProductUpdate(CctvProductsModel newRequest)
        {
            var exRequest = _context.CctvProducts.AsNoTracking().Where(x => x.Id == newRequest.Id).FirstOrDefault()!;
            var detail = _context.CctvProjectDetail.Find(exRequest.DetailId)!;
            string difference = DateTime.Now + " tarihinde, "+ detail.ProjectName + " projesinde" + detail.ProjectDistrict + " ilçesi " + detail.Unit + " projesi ";
            if (newRequest.FloorOfSystem != exRequest.FloorOfSystem)
            {
                difference += exRequest.FloorOfSystem + " sistem odası katını " + newRequest.FloorOfSystem + ", ";
            }
            if (newRequest.ProductName != exRequest.ProductName)
            {
                difference += exRequest.ProductName + " malzemesini " + newRequest.ProductName + ", ";
            }
            if (newRequest.ProductModel != exRequest.ProductModel)
            {
                difference += exRequest.ProductModel + " modelini " + newRequest.ProductModel + ", ";
            }
            if (newRequest.FloorOfProduct != exRequest.FloorOfProduct)
            {
                difference += exRequest.FloorOfProduct + " planlandığı katı, " + newRequest.FloorOfProduct + ", ";
            }
            if (newRequest.PlannedPlace != exRequest.PlannedPlace)
            {
                difference += exRequest.PlannedPlace + " planlandığı yeri," + newRequest.PlannedPlace + ", ";
            }
            if (newRequest.ProductPieces != exRequest.ProductPieces)
            {
                difference += exRequest.ProductPieces + " planlandığı adeti," + newRequest.ProductPieces + ", ";
            }
            difference += "bilgileri olacak şekilde Cctv Kurulum Detayı Projesi malzeme bilgilerini güncelledi.";
            return difference;
        }

        public string CctvEk1Update(CctvEk1Model newRequest)
        {
            var exRequest = _context.CctvEk1.AsNoTracking().Where(x => x.Id == newRequest.Id).FirstOrDefault()!;
            var detail = _context.CctvProjectDetail.Find(newRequest.DetailId)!;
            string difference = DateTime.Now + " tarihinde, " + detail.ProjectName + " projesinde" + detail.ProjectDistrict + " ilçesi " + detail.Unit + " projesi ";
            if (newRequest.KameraYazilim != exRequest.KameraYazilim)
            {
                difference += exRequest.KameraYazilim + " KAMERA KONTROL/GÖRÜNTÜLEME VE YÖNETİM SİSTEMİ YAZILIMI SAYISINI " + newRequest.KameraYazilim + ", ";
            }
            if (newRequest.NvrTip1 != exRequest.NvrTip1)
            {
                difference += exRequest.NvrTip1 + " AĞ GÖRÜNTÜ KAYIT CİHAZI (NVR) TİP-1 SAYISINI " + newRequest.NvrTip1 + ", ";
            }
            if (newRequest.NvrTip2 != exRequest.NvrTip2)
            {
                difference += exRequest.NvrTip2 + " AĞ GÖRÜNTÜ KAYIT CİHAZI (NVR) TİP-2 SAYISINI " + newRequest.NvrTip2 + ", ";
            }
            if (newRequest.DomeTip1 != exRequest.DomeTip1)
            {
                difference += exRequest.DomeTip1 + " İÇ ORTAM SABİT DOME KAMERA TİP-1 SAYISINI " + newRequest.DomeTip1 + ", ";
            }
            if (newRequest.DomeTip2 != exRequest.DomeTip2)
            {
                difference += exRequest.DomeTip2 + " İÇ ORTAM SABİT DOME KAMERA TİP-2 SAYISINI " + newRequest.DomeTip2 + ", ";
            }
            if (newRequest.Mic != exRequest.Mic)
            {
                difference += exRequest.Mic + " İFADE ALMA ODASI MİKROFONU SAYISINI " + newRequest.Mic + ", ";
            }
            if (newRequest.DisBullet != exRequest.DisBullet)
            {
                difference += exRequest.DisBullet + " DIŞ ORTAM SABİT BULLET TİPİ KAMERA SAYISINI " + newRequest.DisBullet + ", ";
            }
            if (newRequest.PtzKamera != exRequest.PtzKamera)
            {
                difference += exRequest.PtzKamera + " HAREKETLİ (PTZ) KAMERA SAYISINI " + newRequest.PtzKamera + ", ";
            }
            if (newRequest.KameraKontrol != exRequest.KameraKontrol)
            {
                difference += exRequest.KameraKontrol + " KAMERA KONTROL ÜNİTESİ SAYISINI " + newRequest.KameraKontrol + ", ";
            }
            if (newRequest.SahaSwitch != exRequest.SahaSwitch)
            {
                difference += exRequest.SahaSwitch + " SAHA AĞ ANAHTARI SAYISINI " + newRequest.SahaSwitch + ", ";
            }
            if (newRequest.IcSwitch != exRequest.IcSwitch)
            {
                difference += exRequest.IcSwitch + " İÇ ORTAM AĞ ANAHTARI SAYISINI " + newRequest.IcSwitch + ", ";
            }
            if (newRequest.PcTip1 != exRequest.PcTip1)
            {
                difference += exRequest.PcTip1 + " OPERATÖR BİLGİSAYARI ( İZLEME VE YÖNETİM MERKEZİ İÇİN ) TİP-1 SAYISINI " + newRequest.PcTip1 + ", ";
            }
            if (newRequest.PcTip2 != exRequest.PcTip2)
            {
                difference += exRequest.PcTip2 + " OPERATÖR BİLGİSAYARI ( İZLEME NOKTASI İÇİN) TİP-2 SAYISINI " + newRequest.PcTip2 + ", ";
            }
            if (newRequest.Monitor != exRequest.Monitor)
            {
                difference += exRequest.Monitor + " OPERATÖR BİLGİSAYARI MONİTÖRÜ SAYISINI " + newRequest.Monitor + ", ";
            }
            if (newRequest.LedTv != exRequest.LedTv)
            {
                difference += exRequest.LedTv + " ÇERÇEVESİZ DUVAR EKRAN SAYISINI " + newRequest.LedTv + ", ";
            }
            if (newRequest.DuvarKabin != exRequest.DuvarKabin)
            {
                difference += exRequest.DuvarKabin + " DUVAR TİPİ RACK KABİN SAYISINI " + newRequest.DuvarKabin + ", ";
            }
            if (newRequest.RackKabin != exRequest.RackKabin)
            {
                difference += exRequest.RackKabin + " RACK KABİN SAYISINI " + newRequest.RackKabin + ", ";
            }
            if (newRequest.SahaKabin != exRequest.SahaKabin)
            {
                difference += exRequest.SahaKabin + " SAHA KABİN SAYISINI " + newRequest.SahaKabin + ", ";
            }
            if (newRequest.KGK != exRequest.KGK)
            {
                difference += exRequest.KGK + " KGK SAYISINI " + newRequest.KGK + ", ";
            }
            if (newRequest.Cat6Ic != exRequest.Cat6Ic)
            {
                difference += exRequest.Cat6Ic + " İÇ ORTAM CAT6 METRAJINI " + newRequest.Cat6Ic + ", ";
            }
            if (newRequest.Caat6Dis != exRequest.Caat6Dis)
            {
                difference += exRequest.Caat6Dis + " DIŞ ORTAM CAT6 METRAJINI " + newRequest.Caat6Dis + ", ";
            }
            if (newRequest.Cat6Panel != exRequest.Cat6Panel)
            {
                difference += exRequest.Cat6Panel + " CAT6 PANEL SAYISINI " + newRequest.Cat6Panel + ", ";
            }
            if (newRequest.FoSingle != exRequest.FoSingle)
            {
                difference += exRequest.FoSingle + " FİBER OPTİK METRAJINI " + newRequest.FoSingle + ", ";
            }
            if (newRequest.FoPanel != exRequest.FoPanel)
            {
                difference += exRequest.FoPanel + " FİBER PANEL SAYISINI " + newRequest.FoPanel + ", ";
            }
            if (newRequest.EnerjiKablo != exRequest.EnerjiKablo)
            {
                difference += exRequest.EnerjiKablo + " ENERJİ KABLOSU METRAJINI " + newRequest.EnerjiKablo + ", ";
            }
            if (newRequest.DataPriz != exRequest.DataPriz)
            {
                difference += exRequest.DataPriz + " DATA PRİZ SETİ " + newRequest.DataPriz + ", ";
            }
            if (newRequest.EnerjiPriz != exRequest.EnerjiPriz)
            {
                difference += exRequest.EnerjiPriz + " ENERJİ PRİZ SETİ " + newRequest.EnerjiPriz + ", ";
            }
            if (newRequest.Kanalet != exRequest.Kanalet)
            {
                difference += exRequest.Kanalet + " KANALET METRAJINI " + newRequest.Kanalet + ", ";
            }
            if (newRequest.MetakKanal != exRequest.MetakKanal)
            {
                difference += exRequest.MetakKanal + " METAL KANAL METRAJINI " + newRequest.MetakKanal + ", ";
            }
            if (newRequest.EnerjiPano != exRequest.EnerjiPano)
            {
                difference += exRequest.EnerjiPano + " ENERJİ PANOSU SAYISINI " + newRequest.EnerjiPano + ", ";
            }
            if (newRequest.OtoSigorta != exRequest.OtoSigorta)
            {
                difference += exRequest.OtoSigorta + " ANAHTARLI OTOMATİK SİGORTA SAYISINI " + newRequest.EnerjiPano + ", ";
            }
            if (newRequest.KaziToprak != exRequest.KaziToprak)
            {
                difference += exRequest.KaziToprak + " TOPRAK KAZI METRAJINI " + newRequest.KaziToprak + ", ";
            }
            if (newRequest.KaziAsfalt != exRequest.KaziAsfalt)
            {
                difference += exRequest.KaziAsfalt + " ASFALT KAZI METRAJINI " + newRequest.KaziAsfalt + ", ";
            }
            if (newRequest.BoruEnerji != exRequest.BoruEnerji)
            {
                difference += exRequest.BoruEnerji + " ENERJİ BORUSU METRAJINI " + newRequest.BoruEnerji + ", ";
            }
            if (newRequest.BoruData != exRequest.BoruData)
            {
                difference += exRequest.BoruData + " DATA BORUSU METRAJINI " + newRequest.BoruData + ", ";
            }
            if (newRequest.KameraDirek3M != exRequest.KameraDirek3M)
            {
                difference += exRequest.KameraDirek3M + " 3M DİREK SAYISI " + newRequest.KameraDirek3M + ", ";
            }
            if (newRequest.KameraDirek4M != exRequest.KameraDirek4M)
            {
                difference += exRequest.KameraDirek4M + " 4M DİREK SAYISI " + newRequest.KameraDirek4M + ", ";
            }
            if (newRequest.KameraDirek5M != exRequest.KameraDirek5M)
            {
                difference += exRequest.KameraDirek5M + " 5M DİREK SAYISI " + newRequest.KameraDirek5M + ", ";
            }
            if (newRequest.KameraDirek6M != exRequest.KameraDirek6M)
            {
                difference += exRequest.KameraDirek6M + " 6M DİREK SAYISI " + newRequest.KameraDirek6M + ", ";
            }
            if (newRequest.KameraDirek7M != exRequest.KameraDirek7M)
            {
                difference += exRequest.KameraDirek7M + " 7M DİREK SAYISI " + newRequest.KameraDirek7M + ", ";
            }
            if (newRequest.KameraDirek8M != exRequest.KameraDirek8M)
            {
                difference += exRequest.KameraDirek8M + " 8M DİREK SAYISI " + newRequest.KameraDirek8M + ", ";
            }
            if (newRequest.KameraDirek9M != exRequest.KameraDirek9M)
            {
                difference += exRequest.KameraDirek9M + " 9M DİREK SAYISI " + newRequest.KameraDirek9M + ", ";
            }
            if (newRequest.KameraDirek10M != exRequest.KameraDirek10M)
            {
                difference += exRequest.KameraDirek10M + " 10M DİREK SAYISI " + newRequest.KameraDirek10M + ", ";
            }
            if (newRequest.UyariLevha != exRequest.UyariLevha)
            {
                difference += exRequest.UyariLevha + " UYARI LEVHA SAYISI " + newRequest.UyariLevha + ", ";
            }
            if (newRequest.SistemPerformans != exRequest.SistemPerformans)
            {
                difference += exRequest.SistemPerformans + " SİSTEM PERFORMANSI ARTTIRIMI KAPSAMINDA YAPILACAK İŞ VE İŞLEMLER SETİ SAYISINI " + newRequest.SistemPerformans + ", ";
            }
            difference += "bilgileri olacak şekilde Cctv Projesi EK-1 malzeme bilgilerini güncelledi.";
            return difference;
        }
    }
}