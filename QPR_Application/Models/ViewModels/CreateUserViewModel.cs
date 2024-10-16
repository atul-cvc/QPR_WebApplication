using QPR_Application.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.ViewModels
{
    public class CreateUserViewModel
    {
        public List<SelectListItem> organizationList { get; set; }

        [Display(Name = "User ID")]
        [Required(ErrorMessage = "User ID is required.")]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "User ID can only contain letters, numbers, and underscores.")]
        public string userid { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format.")]
        public string email { get; set; }

        [Display(Name = "Organisation")]
        [Required(ErrorMessage = "Organisation is required.")]
        public string OrgCode { get; set; }

        [Display(Name = "Login Type")]
        [Required(ErrorMessage = "Login Type is required.")]
        public string logintype { get; set; }

        [Display(Name = "Login Roll")]
        [Required(ErrorMessage = "Login Roll is required.")]
        public string? loginroll { get; set; }

        [Display(Name = "Designation")]
        public string desiganation { get; set; }

        [Display(Name = "Tenure")]
        public string tenure { get; set; }

        [Display(Name = "BO Officer Code")]
        public string? bofficercode { get; set; }

        [Display(Name = "CVO Code")]
        public string? cvocode { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please enter mobile number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile Number must be 10 digits.")]
        public string mobilenumber { get; set; }

        [Display(Name = "Is Locked")]
        [Required(ErrorMessage = "Please select")]
        public string islocked { get; set; }

        [Display(Name = "CVO Contact Number (Office)")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Contact Number must be a number.")]
        public string cvocontactnumberoffice { get; set; }

        [Display(Name = "Employment Type")]
        [Required(ErrorMessage = "Employment Type is required.")]
        public string EmploymentType { get; set; }
    }
}
