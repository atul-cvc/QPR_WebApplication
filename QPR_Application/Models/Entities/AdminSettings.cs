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
        [Display(Name ="Enable Two Factor Authentication via OTP")]
        public bool Enable_Multi_Auth { get; set; } // BIT

        [Display(Name ="Created Date")]
        public DateTime? created_at { get; set; } // DATETIME

        [Display(Name = "Updated Date")]
        public DateTime? updated_at { get; set; } // DATETIME

        [Required]
        [Display(Name ="IsActive")]
        public bool IsActive { get; set; } // BIT

        [Required]
        [Display(Name = "QPR Active From")]
        public DateTime? QPR_Active_From { get; set; }

        [Required]
        [Display(Name = "QPR Active Till")]
        public DateTime? QPR_Active_To { get; set; }

        [Required]
        [Display(Name = "Secret Key")]
        public string SecretKey { get; set; }

        [Display(Name = "Test String")]
        public string? Test_String { get; set; }
    }
}
