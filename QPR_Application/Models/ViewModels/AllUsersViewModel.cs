using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.ViewModels
{
    public class AllUsersViewModel
    {
        public long UserCode { get; set; }

        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Organisation")]
        public string Organisation { get; set; }

        [Display(Name = "User Role")]
        public string Logintype { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Display(Name = "Last Modified")]
        public string LastModified { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "CVO Contact Number")]
        public string CvoContactNumberOffice { get; set; }

        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; }

        [Display(Name = "Created Date")]
        public string createdate { get; set; }

        [Display(Name = "Tenure")]
        public string tenure { get; set; }

        [Display(Name = "BO Officer Code")]
        public string bofficercode { get; set; }

        [Display(Name = "CVO Code")]
        public string cvocode { get; set; }

        [Display(Name = "Mobile Number")]
        public string mobilenumber { get; set; }

        [Display(Name = "Account Locked")]
        public string islocked { get; set; }

        [Display(Name = "Post ID")]
        public string postid { get; set; }

        [Display(Name = "First Login")]
        public string firstlogin { get; set; }
        [Display(Name = "Full Time")]
        public string fulltime { get; set; }

        [Display(Name = "Part Time")]
        public string parttime { get; set; }
    }
}
