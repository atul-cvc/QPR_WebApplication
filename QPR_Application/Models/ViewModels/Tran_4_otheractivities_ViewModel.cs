using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_4_otheractivities_ViewModel
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

        [Required(ErrorMessage = "Please enter details regarding Display Of Banner/Poster etc.")]
        [Display(Name = "Display Of Banner/Poster etc")]
        public string DisplayOfBannerPosterDetails { get; set; }

        [Required(ErrorMessage = "Please enter No of Grievance Redressal Camps Held")]
        [Display(Name = "No Of Grievance Redressal Camps Held")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int NoOfGrievanceRedressalCampsHeld { get; set; }

        [Required(ErrorMessage = "Please enter details regarding Use Of Scocial Media")]
        [Display(Name = "Use Of Scocial Media")]
        public string UserOfScocialMedia { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}