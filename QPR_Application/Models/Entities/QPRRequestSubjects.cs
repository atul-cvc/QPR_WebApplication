using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.Entities
{
    public class QPRRequestSubjects
    {
        [Key]
        public int subject_id { get; set; }
        public string subject_name { get; set; }
        public bool isActive { get; set; }
    }
}
