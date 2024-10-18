using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_a_3b_updation_circular_guidelines_manuals_ViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter year.")]
        [Display(Name = "VAW Year")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Entry should be a valid year (1900-2099)")]
        public string VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }

        [Required(ErrorMessage = "Choose From date.")]
        [Display(Name = "From Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Choose To date.")]
        [Display(Name = "To Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Whether guidelines/circulars and manual were updated during the campaign period")]
        public string WhetherUpdatedDuingCampaign { get; set; }

        public IEnumerable<SelectListItem> YesNoOptions { get; set; }

        [Required(ErrorMessage = "Please enter Brief Details")]
        [Display(Name = "Brief Details")]
        public string BriefDetails { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}