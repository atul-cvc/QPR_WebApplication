namespace QPR_Application.Models.DTO.Response
{
    public class UserRequestsDTO
    {
        public int request_id { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public DateTime created_date { get; set; }
        public DateOnly? updated_date { get; set; }
        public int qtrreport { get; set; }
        public int qtryear { get; set; }
        public bool isActive { get; set; }
        public bool isResolved { get; set; }
        public string ActionTaken { get; set; }
        public string Remarks { get; set; }
        public string qtrQuarterName { get; set; }
        public UserRequestsDTO()
        {
            qtrreport = 0;
            qtryear = 0;
            Remarks = "";
        }
    }
}
