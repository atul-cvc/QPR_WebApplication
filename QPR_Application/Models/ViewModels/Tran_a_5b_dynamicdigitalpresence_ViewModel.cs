using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_a_5b_dynamicdigitalpresence_ViewModel
    {
        public int ID { get; set; }
        [Display(Name = "VAW Year")]
        public string VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }    
        public string CvoOrgCode { get; set; }      
        public string CvoId { get; set; }

        [Required(ErrorMessage = "Whether Regular Maintenance Of Website Updation Done is required.")]
        [Display(Name = "Whether Regular Maintenance Of Website Updation Done")]
        public string WhetherRegularMaintenanceOfWebsiteUpdationDone { get; set; }
        public IEnumerable<SelectListItem> YesNoOptions { get; set; }

        [Required(ErrorMessage = "The System Introduced For Updation And Review is required.")]
        [Display(Name = "System Introduced For Updation And Review")]
        public string SystemIntroducedForUpdationAndReview { get; set; }

        [Required(ErrorMessage = "Whether Additional Areas, Activities, or Services Brought Online is required.")]
        [Display(Name = "Whether Additional Areas, Activities, or Services Brought Online")]
        public string WhetherAdditionalAreas_Activities_ServicesBroughtOnline { get; set; }
        public IEnumerable<SelectListItem> YesNoOptionsforAdditionalAreas { get; set; }
                
        [Display(Name = "Details Of Additional Activities")]
        public string DetailsOfAdditionalActivities { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}