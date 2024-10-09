using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.Entities
{
    public class MasterTraining
    {
        [Key]
        public int Id { get; set; }
        public string Training_Name { get; set; }
    }
}
