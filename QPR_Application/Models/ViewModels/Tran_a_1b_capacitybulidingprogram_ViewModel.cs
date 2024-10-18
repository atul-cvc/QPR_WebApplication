using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QPR_Application.Models.ViewModels
{
    public class Tran_a_1b_capacitybulidingprogram_ViewModel
    {
        public int ID { get; set; }
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

        [Display(Name = "Training Name")]
        [Required(ErrorMessage = "Enter Training Name")]
        public string TrainingName { get; set; }       
        public IEnumerable<SelectListItem> TrainingNameList { get; set; }

        [Display(Name = "Total Employees Trained")]
        [Required(ErrorMessage = "Enter Total Employees Trained")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Entry should be Number ")]
        public int EmployeesTrained { get; set; }

        [Display(Name = "Brief Description")]
        [Required(ErrorMessage = "Enter Brief Description")]
        public string BriefDescription { get; set; }

        [Display(Name = "CVO Organisation")]
        public string OrganisationName { get; set; }
    }
}