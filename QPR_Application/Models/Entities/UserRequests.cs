using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.Entities
{
    public class UserRequests
    {
        [Key]
        public int request_id { get; set; }
        public string subject { get; set; }
        public int subject_id { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 20, ErrorMessage = "The description must be at least 20 characters long.")]
        public string description { get; set; }
        public string userid { get; set; }
        public DateTime created_date { get; set; }
        public DateTime? updated_date { get; set; }
        public int qtrreport { get; set; }
        public int qtryear { get; set; }
        public string? approvedby { get; set; }
        public string qtrQuarterName { get; set; }
        public string ip { get; set; }
        public bool isActive { get; set; }
        public bool isResolved { get; set; }
        public string? ActionTaken { get; set; }
        public string orgCode { get; set; }
        public string? Remarks { get; set; }
        public UserRequests()
        {
            qtrreport = 0;
            qtryear = 0;
            Remarks = "";
            ActionTaken = "";
            qtrQuarterName = "";
        }
    }
}
