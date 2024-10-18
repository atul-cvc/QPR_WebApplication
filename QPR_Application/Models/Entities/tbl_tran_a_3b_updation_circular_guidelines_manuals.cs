using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_a_3b_updation_circular_guidelines_manuals : CommonModel
    {
        public string VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string WhetherUpdatedDuingCampaign { get; set; }
        public string BriefDetails { get; set; }
    }
}
