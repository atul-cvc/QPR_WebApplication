using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_3a_outreach_involvingschoolstudents : CommonModel
    {
        public int VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }
        public DateTime DateOfActivity { get; set; }
        public string StateName { get; set; }
        public string City_Town_Village_Name { get; set; }
        public string SchoolName { get; set; }
        public string ActivityDetails { get; set; }
        public int NoOfStudentsInvolved { get; set; }
    }
}
