﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using QPR_Application.Repository;
using QPR_Application.Util;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_CVO")]
    public class DownloadQPRController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IComplaintsRepo _complaintsRepo;
        private readonly QPRUtility _qprUtil;
        private readonly IQprRepo _qprRepo;

        public DownloadQPRController(IHttpContextAccessor httpContext, IComplaintsRepo complaintsRepo, QPRUtility qprUtil, IQprRepo qprRepo)
        {
            _httpContext = httpContext;
            _complaintsRepo = complaintsRepo;
            _qprUtil = qprUtil;
            _qprRepo = qprRepo;
        }
        public async Task<IActionResult> DownloadQPR(string qprId)
        {
            try
            {
                string refNum = (!string.IsNullOrEmpty(qprId)) ? qprId : _httpContext.HttpContext.Session.GetString("referenceNumber");
                QPRReportViewModel qprVM = await GetQPRDownloadData(refNum) ?? new QPRReportViewModel();
                ViewBag.OrgName = _httpContext?.HttpContext?.Session.GetString("OrgName");
                ViewBag.ReportYear = _httpContext.HttpContext.Session.GetString("QPRYear");
                switch (Convert.ToInt32(_httpContext.HttpContext.Session.GetString("qtrreport")))
                {
                    case 1: ViewBag.QtrReport = "January to March"; break;
                    case 2: ViewBag.QtrReport = "April to June"; break;
                    case 3: ViewBag.QtrReport = "July to September"; break;
                    case 4: ViewBag.QtrReport = "October to December"; break;
                }

                if (DateTime.TryParse(_httpContext.HttpContext.Session.GetString("QPRSubmissionDate"), out DateTime submissionDate))
                {
                    // Set ViewBag to just the date part
                    ViewBag.SubmissionDate = submissionDate.Date.ToShortDateString(); // This will give you just the date part
                }
                else
                {
                    // Handle the case where the date is not valid
                    ViewBag.SubmissionDate = "N/A"; // or some default value
                }
                ViewBag.IsAnnualReport = false;

                return View(qprVM);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index", "QPR");
        }

        public async Task<QPRReportViewModel> GetQPRDownloadData(string refNum)
        {
            if (!string.IsNullOrEmpty(refNum))
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
                qprVM.preventivevigilanceqrsList.Add(qprVM.preventiveViewModel.PreventiveVigilanceQRS);
                return qprVM;
            }
            else
            {
                throw new Exception("QPR ID not found");
            }
        }
        public async Task<IActionResult> AnnualReport()
        {
            string userId = _httpContext.HttpContext.Session.GetString("UserName");
            List<Years> years = await _qprRepo.GetYearsFinalSubmit(userId);
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
                    List<string> qprIds = await _complaintsRepo.GetAllQPRIds(yearSelected);
                    List<QPRReportViewModel> qprQuarterDataList = new List<QPRReportViewModel>();
                    if (qprIds.Count > 0)
                    {
                        for (int i = 0; i < qprIds.Count; i++)
                        {
                            QPRReportViewModel qprData = await GetQPRDownloadData(qprIds[i]);
                            qprQuarterDataList.Add(qprData);
                        }
                    }
                    qprAnnualData = await _qprUtil.PopulateAnnualReportData(qprQuarterDataList);
                    for (int i = 0; i < qprQuarterDataList.Count; i++)
                    {
                        qprAnnualData.preventivevigilanceqrsList.Add(qprQuarterDataList[i].preventiveViewModel.PreventiveVigilanceQRS);
                    }

                    ViewBag.OrgName = _httpContext?.HttpContext?.Session.GetString("OrgName");
                    ViewBag.ReportYear = yearSelected ?? _httpContext.HttpContext.Session.GetString("QPRYear");
                    switch (Convert.ToInt32(_httpContext.HttpContext.Session.GetString("qtrreport")))
                    {
                        case 1: ViewBag.QtrReport = "January to March"; break;
                        case 2: ViewBag.QtrReport = "April to June"; break;
                        case 3: ViewBag.QtrReport = "July to September"; break;
                        case 4: ViewBag.QtrReport = "October to December"; break;
                    }

                    ViewBag.IsAnnualReport = true;

                    return View("DownloadQPR", qprAnnualData);
                }
            }
            catch (Exception ex)
            {
                return View("DownloadQPR", qprAnnualData);
            }
            return RedirectToAction("Index", "QPR");
        }
    }
}
