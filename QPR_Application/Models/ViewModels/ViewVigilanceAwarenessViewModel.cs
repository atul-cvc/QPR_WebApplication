using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace QPR_Application.Models.ViewModels
{
    public class ViewVigilanceAwarenessViewModel
    {
        public List<Tran_a_1b_capacitybulidingprogram_ViewModel> CapacityBuiliding_VM {  get; set; }
        public List<Tran_a_2b_sysimp_ViewModel> Sys_Improvement_VM { get; set; }
        public List<Tran_a_3b_updation_circular_guidelines_manuals_ViewModel> UpdationCirculars_VM {  get; set; }
        public List<Tran_a_4b_disposalofcomplaints_ViewModel> DisposalComplaints_VM { get; set; }
        public List<Tran_a_5b_dynamicdigitalpresence_ViewModel> DynamicDigitalPresence_VM { get; set; }
        public IEnumerable<SelectListItem> YearsList { get; set; }
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public IEnumerable<SelectListItem> OrgList { get; set; }
        public string CurrentOrgCode { get; set; }
        public ViewVigilanceAwarenessViewModel()
        {
            CapacityBuiliding_VM = new List<Tran_a_1b_capacitybulidingprogram_ViewModel>();
            Sys_Improvement_VM = new List<Tran_a_2b_sysimp_ViewModel>();
            UpdationCirculars_VM = new List<Tran_a_3b_updation_circular_guidelines_manuals_ViewModel>();
            DisposalComplaints_VM = new List<Tran_a_4b_disposalofcomplaints_ViewModel>();
            DynamicDigitalPresence_VM = new List<Tran_a_5b_dynamicdigitalpresence_ViewModel>();
        }
    }
}