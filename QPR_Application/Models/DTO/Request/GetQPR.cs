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
        public string CVCContactNo { get; set; }
        [Required(ErrorMessage = "Please enter mobile no.")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Please enter email id.")]
        public string CVOEmail { get; set; }
        [Required(ErrorMessage ="Please select")]
        public bool CVOFulltime { get; set; }
        [Required(ErrorMessage ="Please select")]
        public bool CVOParttime { get; set; }
    }

}
