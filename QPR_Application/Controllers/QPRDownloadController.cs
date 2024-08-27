using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using QPR_Application.Repository;
using QPR_Application.Util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_CVO")]
    public class QPRDownloadController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IComplaintsRepo _complaintsRepo;
        private readonly QPRUtilility _qprUtil;

        public QPRDownloadController(IHttpContextAccessor httpContext, IComplaintsRepo complaintsRepo, QPRUtilility qprUtil)
        {
            _httpContext = httpContext;
            _complaintsRepo = complaintsRepo;
            _qprUtil = qprUtil;
        }
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
        public async Task<IActionResult> AnnualReport()
        {
            return View();
        }
        public async Task<IActionResult> DownloadAnnualReport(string year)
        {
            try
            {
                if (!string.IsNullOrEmpty(year))
                {
                    List<string> qprIds = await _complaintsRepo.GetAllQPRIds(year);
                    List<QPRReportViewModel> qprQuarterDataList = new List<QPRReportViewModel>();
                    QPRReportViewModel qprAnnualData = new QPRReportViewModel();
                    if (qprIds.Count > 0)
                    {
                        for (int i = 0; i < qprIds.Count; i++)
                        {
                            QPRReportViewModel qprData = await GetQPRDownloadData(qprIds[i]);
                            qprQuarterDataList.Add(qprData);
                        }
                    }
                    qprAnnualData = _qprUtil.PopulateAnnualReportData(qprQuarterDataList);
                    return View(qprAnnualData);
                }
            }
            catch (Exception ex)
            {
            }
            //return View();
            return RedirectToAction("Index", "QPR");
        }
    }
}
