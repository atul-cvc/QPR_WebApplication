using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using QPR_Application.Repository;
using QPR_Application.Util;
using System;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_CVO")]
    public class DownloadQPRController : Controller
    {
        private readonly ILogger<DownloadQPRController> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IQPRRepo _qprRepo;
        private readonly QPRUtility _qprUtil;
        private readonly IQprCRUDRepo _qprCRUDRepo;

        public DownloadQPRController(ILogger<DownloadQPRController> logger, IHttpContextAccessor httpContext, IQPRRepo qprRepo, QPRUtility qprUtil, IQprCRUDRepo qprCRUDRepo)
        {
            _logger = logger;
            _httpContext = httpContext;
            _qprRepo = qprRepo;
            _qprUtil = qprUtil;
            _qprCRUDRepo = qprCRUDRepo;
        }
        public async Task<IActionResult> DownloadQPR(string qprId)
        {
            try
            {
                QPRReportViewModel qprVM = await GetQPRReport(qprId);
                return View(qprVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while generating QPR.");
            }
            return RedirectToAction("Index", "QPR");
        }

        public async Task<IActionResult> AnnualReport()
        {
            string userId = _httpContext?.HttpContext?.Session.GetString("UserName");
            List<Years> years = await _qprCRUDRepo.GetYearsFinalSubmit(userId);
            return View(years);
        }

        [HttpPost]
        public async Task<IActionResult> DownloadAnnualReport(string yearSelected)
        {
            QPRReportViewModel qprAnnualData = new QPRReportViewModel();
            try
            {
                if (!string.IsNullOrEmpty(yearSelected))
                {
                    List<string> qprIds = await _qprRepo.GetAllQPRIds(yearSelected);
                    List<QPRReportViewModel> qprQuarterDataList = new List<QPRReportViewModel>();
                    if (qprIds.Count > 0)
                    {
                        for (int i = 0; i < qprIds.Count; i++)
                        {
                            QPRReportViewModel qprData = await _qprRepo.GetQPRDownloadData(qprIds[i]);
                            qprQuarterDataList.Add(qprData);
                        }
                    }
                    qprAnnualData = await _qprUtil.PopulateAnnualReportData(qprQuarterDataList);
                    for (int i = 0; i < qprQuarterDataList.Count; i++)
                    {
                        qprAnnualData.preventivevigilanceqrsList.Add(qprQuarterDataList[i].preventiveViewModel.PreventiveVigilanceQRS);
                    }

                    ViewBag.IsAnnualReport = true;
                    ViewBag.OrgName = _httpContext?.HttpContext?.Session.GetString("OrgName");
                    ViewBag.ReportYear = yearSelected ?? _httpContext?.HttpContext?.Session.GetString("QPRYear");
                    ViewBag.QtrReport = _qprUtil.GetQuarterReport();

                    return View("DownloadQPR", qprAnnualData);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading QPR Annual Report.");
                return View("DownloadQPR", qprAnnualData);
            }
            return RedirectToAction("Index", "QPR");
        }

        public async Task<IActionResult> PrintPreviewQPR(string qprId)
        {
            try
            {
                QPRReportViewModel qprVM = await GetQPRReport(qprId);
                ViewBag.SubmissionDate = "Not Submitted";
                return View("DownloadQPR", qprVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading QPR Print Preview.");
            }
            return RedirectToAction("Index", "QPR");
        }

        public async Task<QPRReportViewModel> GetQPRReport(string refNum)
        {
            QPRReportViewModel qprVM = new QPRReportViewModel();
            try
            {
                qprVM = await _qprRepo.GetQPRDownloadData(refNum);
                ViewBag.OrgName = _httpContext?.HttpContext?.Session.GetString("OrgName") ?? "";
                ViewBag.ReportYear = _httpContext?.HttpContext?.Session.GetString("QPRYear") ?? "";
                ViewBag.QtrReport = _qprUtil.GetQuarterReport();
                ViewBag.SubmissionDate = _qprUtil.GetSubmissionDate();
                ViewBag.IsAnnualReport = false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetQPRReport failed to retrieve QPR Data");
            }
            return qprVM ?? new QPRReportViewModel();
        }
    }
}
