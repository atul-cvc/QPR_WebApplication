using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QPR_Application.Models.Entities;

namespace QPR_Application.Models.ViewModels
{
    public class ViewIntegrityPledgeViewModel
    {
        public List<Tran_1a_integritypledge_ViewModel> IntegrityPledges { get; set; }
        public List<Tran_2a_orgactivities_conductofcompetitions_ViewModel> OrgActivities_ConductOfCompetitions { get; set; }
        public List<Tran_2b_orgactivities_otheractivities_ViewModel> OrgActivities_OtherActivities { get; set; }
        public List<Tran_3a_outreach_involvingschoolstudents_ViewModel> OutActivities_InvolvingStudentsInSchool { get; set; }
        public List<Tran_3b_outreach_involvingcollegestudents_ViewModel> OutActivities_InvolvingStudentsInColleges { get; set; }
        public List<Tran_3c_outreach_awarenessgramsabhas_ViewModel> OutActivities_AwarenesGramSabha { get; set; }
        public List<Tran_3d_outreach_seminarsworkshops_ViewModel> OutActivities_SeminarsWorkshops { get; set; }
        public List<Tran_4_otheractivities_ViewModel> OtherActivities { get; set; }
        public List<Tran_5_detailsofphotos_ViewModel> DetailsOfPhotos{ get; set; }
        public List<Tran_6_otherinformation_ViewModel> OtherInformations { get; set; }
        public IEnumerable<SelectListItem> YearsList { get; set; }
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public IEnumerable<SelectListItem> OrgList { get; set; }
        public string CurrentOrgCode { get; set; } 
        public ViewIntegrityPledgeViewModel()
        {
            IntegrityPledges = new List<Tran_1a_integritypledge_ViewModel>();
            OrgActivities_ConductOfCompetitions = new List<Tran_2a_orgactivities_conductofcompetitions_ViewModel>();
            OrgActivities_OtherActivities = new List<Tran_2b_orgactivities_otheractivities_ViewModel>();
            OutActivities_InvolvingStudentsInSchool = new List<Tran_3a_outreach_involvingschoolstudents_ViewModel>();
            OutActivities_InvolvingStudentsInColleges = new List<Tran_3b_outreach_involvingcollegestudents_ViewModel>();
            OutActivities_AwarenesGramSabha = new List<Tran_3c_outreach_awarenessgramsabhas_ViewModel>();
            OutActivities_SeminarsWorkshops = new List<Tran_3d_outreach_seminarsworkshops_ViewModel>();
            OtherActivities = new List<Tran_4_otheractivities_ViewModel>();
            DetailsOfPhotos = new List<Tran_5_detailsofphotos_ViewModel>();
            OtherInformations = new List<Tran_6_otherinformation_ViewModel>();
        }
    }
}