using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_3d_outreach_seminarsworkshops_ViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter year.")]
        [Display(Name = "VAW Year")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Entry should be a valid year (1900-2099)")]
        public int VAW_Year { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CvoOrgCode { get; set;}
        public string CvoId { get; set;}

        [Required(ErrorMessage = "Choose From date.")]
        [Display(Name = "Date of Activity")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateOfActivity { get; set;}

        [Required(ErrorMessage = "Please enter name of State.")]
        [Display(Name = "Name of State")]
        public string StateName { get; set;}
        public IEnumerable<SelectListItem> StateNameList { get; set;}

        [Required(ErrorMessage = "Please enter name of City/Town/Village.")]
        [Display(Name = "Name of City/Town/Village")]
        public string City_Town_Village_Name { get; set;}

        [Required(ErrorMessage = "Please enter no of Seminars/Workshops.")]
        [Display(Name = "No Of Seminars/Workshops Organised")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int NoOfSeminarsWorkshops { get; set;}

        [Required(ErrorMessage = "Please enter Activity Details.")]
        [Display(Name = "Details of Activities Conducted")]
        public string ActivityDetails { get; set;}

        [Required(ErrorMessage = "Please enter no of public or citizen participated.")]
        [Display(Name = "No Of Public / Citizen Participated")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int NoOfPublicOrCitizenParticipated { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}