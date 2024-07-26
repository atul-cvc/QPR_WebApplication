using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.DTO.Request
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please enter old password")]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessage = "Please enter new password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter same as new password")]
        public string ConfirmPassword { get; set; }

    }
}
