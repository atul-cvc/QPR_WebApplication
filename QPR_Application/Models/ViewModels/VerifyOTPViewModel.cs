using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.ViewModels
{
    public class VerifyOTPViewModel
    {
        [Required(ErrorMessage = "Please enter OTP.")]
        [Display(Name = "Enter OTP :")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Entry should be Number.")]
        public string Otp { get; set; }
    }
}
