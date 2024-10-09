using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.Entities
{
    public class Training_CVO
    {
        [Key]
        public int Record_Id { get; set; }

        public int Training_Id { get; set; }

        public string Training_Name { get; set; }

        public string Other_Description { get; set; }

        public int No_of_Trg_Conducted { get; set; }

        public int No_of_Emp_Trained { get; set; }

        public string user_id { get; set; }

        public DateTime create_date { get; set; }

        public DateTime? update_date { get; set; } // Nullable if the record may not have an update date

        [StringLength(50)] // Assuming a reasonable length for an IP address
        public string ip { get; set; }

        public long qpr_id { get; set; }

        public string? update_user_id { get; set; }
    }
}
