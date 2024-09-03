using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.Entities
{
    public class UserRequests
    {
        [Key]
        public string request_id { get; set; }
        public string subject { get; set; }
        public int subject_id { get; set; }
        public string description { get; set; }
        public string userid { get; set; }
        public DateTime created_date { get; set; }
        public DateOnly? updated_date { get; set; }
        public int? qtrreport { get; set; }
        public int? qtryear { get; set; }
        public string ip { get; set; }
        public bool isActive { get; set; }
        public bool? isResolved { get; set; }
    }
}
