using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_2a_orgactivities_conductofcompetitions : CommonModel
    {
        public int VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }
        public DateTime DateOfActivity { get; set; }
        public string NameOfState { get; set; }
        public string City { get; set; }
        public string SpecificProgram { get; set; }
        public int NoOfParticipant { get; set; }
        public string Remarks { get; set; }
    }
}
