using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_a_4b_disposalofcomplaints_ViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter year.")]
        [Display(Name = "VAW Year")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Entry should be a valid year (1900-2099)")]
        public string VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }

        [Required(ErrorMessage = "Please enter no of complaints.")]
        [Display(Name = "Complaints Received on or before 30.06.2024 pending as on 16.08.2024")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int NoOf_ComplaintsRecvd_OnOrBefore_3006_Pending_AsOn_1608 { get; set; }

        [Display(Name = "Remarks, if any for Complaints Received on or before 30.06.2024 pending as on 16.08.2024")]
        public string Remarks_ComplaintsRecvd_OnOrBefore_3006_Pending_AsOn_1608 { get; set; }

        [Required(ErrorMessage = "Please enter no of complaints.")]
        [Display(Name = "Complaints Received on or before 30.06.2024 disposed during campaign period")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int NoOf_ComplaintsRecvd_OnOrBefore_3006_DisposedDuringCampaign { get; set; }

        [Display(Name = "Remarks, if any for Complaints Received on or before 30.06.2024 disposed during campaign period")]
        public string Remarks_ComplaintsRecvd_OnOrBefore_3006_DisposedDuringCampaign { get; set; }

        [Required(ErrorMessage = "Please enter no of complaints.")]
        [Display(Name = "Complaints Received on or before 30.06.2024 pending as on 15.11.2024")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int NoOf_ComplaintsRecvd_OnOrBefore_3006_PendingAsOn_1511 { get; set; }

        [Display(Name = "Remarks, if any for Complaints Received on or before 30.06.2024 pending as on 15.11.2024")]
        public string Remarks_ComplaintsRecvd_OnOrBefore_3006_PendingAsOn_1511 { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}