using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_a_4b_disposalofcomplaints : CommonModel
    {
        public string VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }
        public int NoOf_ComplaintsRecvd_OnOrBefore_3006_Pending_AsOn_1608 { get; set; }
        public string Remarks_ComplaintsRecvd_OnOrBefore_3006_Pending_AsOn_1608 { get; set; }
        public int NoOf_ComplaintsRecvd_OnOrBefore_3006_DisposedDuringCampaign { get; set; }
        public string Remarks_ComplaintsRecvd_OnOrBefore_3006_DisposedDuringCampaign { get; set; }
        public int NoOf_ComplaintsRecvd_OnOrBefore_3006_PendingAsOn_1511 { get; set; }
        public string Remarks_ComplaintsRecvd_OnOrBefore_3006_PendingAsOn_1511 { get; set; }
    }
}
