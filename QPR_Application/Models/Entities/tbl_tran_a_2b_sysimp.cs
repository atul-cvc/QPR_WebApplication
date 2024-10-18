using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_a_2b_sysimp : CommonModel
    {
        public string VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Sys_Imp_Implemented_During_Campaign { get; set; }
        public string Sys_Imp_Suggested_Last_5_Years_But_Pending { get; set; }
        public int NoOf_CasesTakenForAnalysis_past5Years { get; set; }
        public string KeyAreasDetected_BasedonAnalysis { get; set; }
        public string Sys_Improvements_Identified_And_Impl_BasedOnAnalysis { get; set; }
    }
}
