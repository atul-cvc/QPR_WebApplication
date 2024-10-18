using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_1a_integritypledge : CommonModel
    {
        public int VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }
        public DateTime DateOfActivity { get; set; }
        public int TotalNoOfEmployees_UndertakenPledge { get; set; }
        public int TotalNoOfCustomers_UndertakenPledge { get; set; }
        public int TotalNoOfCitizen_UndertakenPledge { get; set; }
    }
}



