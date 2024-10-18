using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_5_detailsofphotos : CommonModel
    {
        public int VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }
        public DateTime DateOfActivity { get; set; }
        public string NameOfActivity { get; set; }
        public int NoOfPhotos { get; set; }
        public string WhetherPhotosSentAsSoftCopyOrHardCopy { get; set; }
        public int SoftCopy_NoOfCd { get; set; }
    }
}
