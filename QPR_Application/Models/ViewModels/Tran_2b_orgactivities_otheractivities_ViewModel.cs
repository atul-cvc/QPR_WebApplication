using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_2b_orgactivities_otheractivities_ViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter year.")]
        [Display(Name = "VAW Year")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Entry should be a valid year (1900-2099)")]

        public int VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }

        [Required(ErrorMessage = "Choose From date.")]
        [Display(Name = "Date of Activity")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateOfActivity { get; set; }

        [Required(ErrorMessage = "Please enter details regarding distribution of pamphlets and banners.")]
        [Display(Name = "Distribution of Pamphlets / Banners")]
        public string DistributionOfPamphletsAndBanners_Details { get; set; }

        [Required(ErrorMessage = "Please enter details regarding Conduct of Workshops/Sensitization programmes.")]
        [Display(Name = "Conduct of Workshop / Sensitization programmes")]
        public string ConductOfWorkshopAndSensitizationProgram_Details { get; set; }

        [Required(ErrorMessage = "Please enter details regarding Issue Of Journal And Newsletter Details.")]
        [Display(Name = "Issue Of Journal / Newsletter Details")]
        public string IssueOfJornalAndNwesletter_Details { get; set; }

        [Required(ErrorMessage = "Please enter details regarding Any Other Activities Details.")]
        [Display(Name = "Any Other Activities Details")]
        public string AnyOtherActivities_Details { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}