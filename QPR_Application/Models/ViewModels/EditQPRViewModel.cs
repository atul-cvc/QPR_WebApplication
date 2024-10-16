using QPR_Application.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.ViewModels
{
    public class EditQPRViewModel
    {
        public qpr qpr { get; set; }

        //[Required]
        //[Display(Name ="Employment Type")]
        public string EmploymentType { get; set; }
    }
}
