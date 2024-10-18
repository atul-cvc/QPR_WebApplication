using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_4_otheractivities : CommonModel
    {
        public int VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }
        public DateTime DateOfActivity { get; set; }
        public string DisplayOfBannerPosterDetails { get; set; }
        public int NoOfGrievanceRedressalCampsHeld { get; set; }
        public string UserOfScocialMedia { get; set; }
    }
}
