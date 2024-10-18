using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_2a_orgactivities_conductofcompetitions_ViewModel
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

        [Required(ErrorMessage = "Please enter Name of State.")]
        [Display(Name = "Name of State")]
        public string NameOfState { get; set; }
        public IEnumerable<SelectListItem> NameOfStateList { get; set; }

        [Required(ErrorMessage = "Please enter Name of City.")]
        [Display(Name = "City / Place")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select a program.")]
        [Display(Name = "Specific Program")]
        public string SpecificProgram { get; set; }
        public IEnumerable<SelectListItem> SpecificProgramList { get; set; }

        [Required(ErrorMessage = "Please enter no. of participants.")]
        [Display(Name = "No. of Participants")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int NoOfParticipant { get; set; }

        [Required(ErrorMessage = "Please enter Remarks")]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}