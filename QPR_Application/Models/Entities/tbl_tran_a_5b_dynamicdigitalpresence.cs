using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_a_5b_dynamicdigitalpresence : CommonModel
    {
        public string VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }
        public string WhetherRegularMaintenanceOfWebsiteUpdationDone { get; set; }
        public string SystemIntroducedForUpdationAndReview { get; set; }
        public string WhetherAdditionalAreas_Activities_ServicesBroughtOnline { get; set; }
        public string DetailsOfAdditionalActivities { get; set; }
    }
}
