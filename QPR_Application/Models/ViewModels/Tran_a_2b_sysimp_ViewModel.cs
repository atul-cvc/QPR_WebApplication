using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_a_2b_sysimp_ViewModel
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

        [Required(ErrorMessage = "Please enter System Improvements Implemented During Campaign Period")]
        [Display(Name = "System Improvements Implemented During Campaign Period")]
        public string Sys_Imp_Implemented_During_Campaign { get; set; }

        [Required(ErrorMessage = "Please enter System Improvements suggested during last 5 Years but pending for implementation")]
        [Display(Name = "System Improvements suggested last 5 years but pending for implementation")]
        public string Sys_Imp_Suggested_Last_5_Years_But_Pending { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }

        [Required(ErrorMessage = "Please enter No. Of Vigilance Cases Of The Past 5 Years Taken Up For Analysis")]
        [Display(Name = "No. Of Vigilance Cases Of The Past 5 Years Taken Up For Analysis")]
        public int NoOf_CasesTakenForAnalysis_past5Years { get; set; }

        [Required(ErrorMessage = "Please enter Key Areas Vulnerable To Corruption Detected Based On Analysis")]
        [Display(Name = "Key Areas Vulnerable To Corruption Detected Based On Analysis")]
        public string KeyAreasDetected_BasedonAnalysis { get; set; }

        //[Required(ErrorMessage = "Please enter Brief Details about Systemic Improvements identified and implemented / under implementation")]
        [Display(Name = "Systemic Improvements identified and implemented / under implementation (brief details may be given)")]
        public string Sys_Improvements_Identified_And_Impl_BasedOnAnalysis { get; set; }
    }
}