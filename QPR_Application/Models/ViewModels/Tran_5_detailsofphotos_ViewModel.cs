using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_5_detailsofphotos_ViewModel
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

        [Required(ErrorMessage = "Please enter Name of Activity")]
        [Display(Name = "Name of Activity")]
        public string NameOfActivity { get; set; }

        [Required(ErrorMessage = "Please enter No of Photos")]
        [Display(Name = "No Of Photos")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int NoOfPhotos { get; set; }

        [Required(ErrorMessage = "Please select Whether Photos Sent As Soft Copy Or Hard Copy")]
        [Display(Name = "Whether Photos Sent As Soft Copy Or Hard Copy")]
        public string WhetherPhotosSentAsSoftCopyOrHardCopy { get; set; }

        [Required(ErrorMessage = "Please enter No of Photos")]
        [Display(Name = "(If in soft copy) No Of CDs / Pendrives")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int SoftCopy_NoOfCd { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}