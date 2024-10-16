using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.ViewModels
{
    public class EditUserViewModel
    {
        public long UserCode { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public string OrgCode { get; set; }

        [Required]
        [Display(Name = "Login Type")]
        public string LoginType { get; set; }

        [Required]
        [Display(Name = "Login Roll")]
        public string? LoginRoll { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Display(Name = "Tenure")]
        public string Tenure { get; set; }

        [Required]
        [Display(Name = "BO Officer Code")]
        public string BoOfficerCode { get; set; }

        [Required]
        [Display(Name = "CVO Code")]
        public string Cvocode { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format.")]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "Is Locked")]
        public string IsLocked { get; set; }

        [Display(Name = "Post ID")]
        public string? PostId { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "CVO Contact Number (Office)")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid phone number format.")]
        public string CvocontactNumberOffice { get; set; }

        [Required]
        [Display(Name = "Employment Type")]
        public string EmploymentType { get; set; }

        public string? OrgName { get; set; }
    }
}
