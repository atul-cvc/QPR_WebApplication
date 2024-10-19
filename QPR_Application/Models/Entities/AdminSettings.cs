using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.Entities
{
    public class AdminSettings
    {
        [Key]
        public int Id { get; set; } // Identity column

        [Required]
        [Display(Name ="Route for VAW URL")]
        public string VAW_URL { get; set; } // NVARCHAR(500)

        [Required]
        [Display(Name ="Enable Two Factor Authentication vio OTP")]
        public bool Enable_Multi_Auth { get; set; } // BIT
        public DateTime? created_at { get; set; } // DATETIME
        public DateTime? updated_at { get; set; } // DATETIME

        [Required]
        [Display(Name ="IsActive")]
        public bool IsActive { get; set; } // BIT

        public string SecretKey { get; set; }
    }
}
