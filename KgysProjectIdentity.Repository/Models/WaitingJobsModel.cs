namespace KgysProjectIdentity.Repository.Models
{
    public class WaitingJobsModel
    {
        public int Id { get; set; }
        public string? EbysNumber { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string? Subject { get; set; }
        public string? District { get; set; }
        public string? Summary { get; set; }
        public string? TalkToManager { get; set; }
        public string? StatusMessage { get; set; }
        //public List<string> Officials { get; set; }
        public DateTime? PlanedDate { get; set; }
        public string? FinishOrNot { get; set; }
        public string? Material { get; set; }
        public string? Discription { get; set; }
        public string? Answer { get; set; }
        public string? EBYSanswer { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Status { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedUser { get; set; }



    }
}
