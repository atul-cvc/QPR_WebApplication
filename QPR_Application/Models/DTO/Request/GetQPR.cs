using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.DTO.Request
{
    public class GetQPR
    {
        [Required(ErrorMessage = "Please select a quarter.")]
        public string SelectedQuarter { get; set; }
        
        [Required(ErrorMessage = "Please select a year.")]
        public int SelectedYear { get; set; }

        [Required(ErrorMessage = "Please enter contact no.")]
        [MinLength(5, ErrorMessage = "Please enter a valid number.")]
        public string CVCContactNo { get; set; }

        [Required(ErrorMessage = "Please enter mobile no.")]
        [MinLength(10, ErrorMessage = "Please enter a valid number.")]
        [MaxLength(10, ErrorMessage = "Please enter a valid number.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please enter email id.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address format.")]
        public string CVOEmail { get; set; }

        [Required(ErrorMessage ="Please select")]
        public bool CVOFulltime { get; set; }
        
        [Required(ErrorMessage ="Please select")]
        public bool CVOParttime { get; set; }
    }

}
