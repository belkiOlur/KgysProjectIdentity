namespace KgysProjectIdentity.Repository.Models
{
    public class TableRowsColor
    {
        //TelecomFo/Index sayfasındaki hücreleri renklendirmek için gerekli kelimeyi belirler.
        public static string RowsColor(string status)
        {
            string typ = "table-primary";
            if (status == "Tamamlandı")
            {
                typ = "table-success";
            }
            if (status == "Başlanılmadı")
            {
                typ = "table-danger";
            }
            if (status == "Devam Ediyor")
            {
                typ = "table-warning";
            }
            if (status == "Evet")
            {
                typ = "table-success";
            }
            if (status == "Hayır")
            {
                typ = "table-danger";
            }
            if (status == "Yok")
            {
                typ = "table-danger";
            }
            if (status == "Pc Gelmedi")
            {
                typ = "table-warning";
            }
            if (status == "2500+")
            {
                typ = "table-danger";
            }
            if (status == "+2500")
            {
                typ = "table-danger";
            }
            if (status == "-2500")
            {
                typ = "table-success";
            }
            if (status == "2500-")
            {
                typ = "table-success";
            }
            if (status == "Mevcut")
            {
                typ = "table-success";
            }
            if (status == "Bütçeden Red Edildi")
            {
                typ = "table-dark";
            }
            if (status == "Fizibiliteden Red Edildi")
            {
                typ = "table-dark";
            }
            if (status == "Kazı İzni Verilmedi")
            {
                typ = "table-dark";
            }
            if (status == "Müşteri Nedeniyle Askıda")
            {
                typ = "table-info";
            }
            if (status == "EGM Tarafından İptal Edildi")
            {
                typ = "table-dark";
            }
            if (status == "Telekom Tarafından İptal Edildi")
            {
                typ = "table-dark";
            }
            if (status != null && status != "")
            {
                if (status.Contains("İptal"))
                {
                    typ = "table-dark";
                }
            }
            return typ;
        }
    }
}
