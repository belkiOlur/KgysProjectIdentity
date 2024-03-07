using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class WaitingJobsViewModel
    {
        public int Id { get; set; }

        [Remote(action: "EBYSNumberCheck", controller: "WaitingJobs")]
        //[Required(ErrorMessage = "EBYS Numarası Zorunludur, Boş Geçilemez.")]
        //[StringLength(19, MinimumLength = 19, ErrorMessage = "EBYS Numarası Eksik Veya Yanlış.")]
        public string? EbysNumber { get; set; }
        [Required(ErrorMessage = "Evrak Geliş Tarihi Boş Geçilemez.")]
        public DateTime ArrivalDate { get; set; }
        [Required(ErrorMessage = "İlgili Birim Boş Geçilemez.")]
        public string? Subject { get; set; }
        [Required(ErrorMessage = "İlçe Adı Boş Geçilemez.")]
        public string? District { get; set; }
        [Required(ErrorMessage = "Konu Boş Geçilemez.")]
        public string? Summary { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez.")]
        public string? TalkToManager { get; set; }

        public string? StatusMessage { get; set; }
        public List<string> Officials { get; set; }
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime? PlanedDate { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez.")]
        public string? FinishOrNot { get; set; }

        public string? Material { get; set; }
        public string? Discription { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez.")]
        public string? Answer { get; set; }

        //[Required(ErrorMessage = "Yazılan EBYS Numarasını Giriniz.")]
        //[StringLength(19, ErrorMessage = "EBYS Numarası Eksik Veya Yanlış.")]
        public string? EBYSanswer { get; set; }

        //[Required(ErrorMessage = "Boş Geçilemez.")]
        public DateTime? FinishDate { get; set; }
        public string? Status { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez.")]
        public DateTime UpdatedTime { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez.")]
        public string? UpdatedUser { get; set; }

    }
}
