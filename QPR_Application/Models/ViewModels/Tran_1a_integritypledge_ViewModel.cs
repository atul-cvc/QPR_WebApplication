using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_1a_integritypledge_ViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter year.")]
        [Display(Name = "VAW Year")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Entry should be a valid year (1900-2099)")]

        public int VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set; }
        public string CvoId { get; set; }

        [Required(ErrorMessage = "Choose Date of Activity.")]
        [Display(Name = "Date of Activity")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateOfActivity { get; set; }

        [Display(Name = "Total No. Of Employees Undertaken e-pledge")]
        [Required(ErrorMessage = "Enter Total No of Employees Undertaken e-pledge")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int TotalNoOfEmployees_UndertakenPledge { get; set; }

        [Display(Name = "Total No. Of Customers Undertaken e-pledge")]
        [Required(ErrorMessage = "Enter Total No of Customers Undertaken e-pledge")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int TotalNoOfCustomers_UndertakenPledge { get; set; }

        [Display(Name = "Total No. Of Citizens Undertaken e-pledge")]
        [Required(ErrorMessage = "Enter Total No of Citizens Undertaken e-pledge")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int TotalNoOfCitizen_UndertakenPledge { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}