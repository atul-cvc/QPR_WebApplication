using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.DTO.Request
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please enter old password")]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessage = "Please enter new password")]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter same as new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
