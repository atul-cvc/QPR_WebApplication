using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_3b_outreach_involvingcollegestudents_ViewModel
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

        [Required(ErrorMessage = "Please enter name of City/Town/Village.")]
        [Display(Name = "Name of City / Town / Village")]
        public string City_Town_Village_Name { get; set; }

        [Required(ErrorMessage = "Please enter name of College.")]
        [Display(Name = "Name of College")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Please enter Activity Details.")]
        [Display(Name = "Details of Activities Conducted")]
        public string ActivityDetails { get; set; }

        [Required(ErrorMessage = "Please enter no. of students involved.")]
        [Display(Name = "No of Students Involved")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int NoOfStudentsInvolved { get; set; }

        [Required(ErrorMessage = "Please enter name of State.")]
        [Display(Name = "Name of State")]
        public string StateName { get; set; }
        public IEnumerable<SelectListItem> StateNameList { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}