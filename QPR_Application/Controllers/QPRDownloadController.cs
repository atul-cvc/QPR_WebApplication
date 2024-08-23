using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using QPR_Application.Models.ViewModels;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_CVO")]
    public class QPRDownloadController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IComplaintsRepo _complaintsRepo;

        public QPRDownloadController(IHttpContextAccessor httpContext, IComplaintsRepo complaintsRepo)
        {
            _httpContext = httpContext;
            _complaintsRepo = complaintsRepo;
        }
        //public IActionResult Index(string refNum)
        //{
        //    return View();
        //}
        public async Task<IActionResult> DownloadQPR(string qprId)
        {
            try
            {
                string refNum = (!string.IsNullOrEmpty(qprId)) ? qprId : _httpContext.HttpContext.Session.GetString("referenceNumber");
                QPRReportViewModel qprVM = await GetQPRDownloadData(refNum) ?? new QPRReportViewModel();
                return View(qprVM);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index", "QPR");
        }

        public async Task<QPRReportViewModel> GetQPRDownloadData(string refNum)
        {
            QPRReportViewModel qprVM = new QPRReportViewModel();
            qprVM.complaints = await _complaintsRepo.GetComplaintsData(refNum);
            qprVM.vigInvestigation = await _complaintsRepo.GetVigilanceInvestigationData(refNum);
            qprVM.prosecVM = await _complaintsRepo.GetProsecutionSanctionsViewData(refNum);
            qprVM.deptVM = await _complaintsRepo.GetDepartmentalProceedingsViewModel(refNum);
            qprVM.adviceViewModel = await _complaintsRepo.GetAdviceOfCVCViewModel(refNum);
            qprVM.statusVM = await _complaintsRepo.GetStatusPendencyViewModel(refNum);
            qprVM.punitiveVig = await _complaintsRepo.GetPunitiveVigilanceData(refNum);
            qprVM.preventiveViewModel = await _complaintsRepo.GetPreventiveVigilanceViewModel(refNum);
            qprVM.preventiveActivitiesVM = await _complaintsRepo.GetPreventiveVigilanceActivitiesData(refNum);
            return qprVM;
        }
    }
}
