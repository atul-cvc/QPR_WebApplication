using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QPR_Application.Models.DTO.Response;
using System.Text.Json;
using QPR_Application.Repository;
using QPR_Application.Models.Entities;
using QPR_Application.Models.DTO.Request;
using Microsoft.AspNetCore.Authorization;
//using System;
//using Microsoft.AspNetCore.Http;
//using System.Net;
using QPR_Application.Models.ViewModels;
using QPR_Application.Util;

namespace QPR_Application.Controllers
{
    [Authorize(Roles = "ROLE_CVO")]
    public class QPRController : Controller
    {
        private readonly ILogger<QPRController> _logger;
        private readonly IQprRepo _qprRepo;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IComplaintsRepo _complaintsRepo;
        private readonly IChangePasswordRepo _changePasswordRepo;
        private readonly QPRUtilility _qprUtil;
        private registration? userObject;

        public QPRController(ILogger<QPRController> logger, IQprRepo qprRepo, IHttpContextAccessor httpContext, IComplaintsRepo complaintsRepo, IChangePasswordRepo changePasswordRepo, QPRUtilility qPRUtilility)
        {
            _logger = logger;
            _qprRepo = qprRepo;
            _httpContext = httpContext;
            _complaintsRepo = complaintsRepo;
            _changePasswordRepo = changePasswordRepo;
            _qprUtil = qPRUtilility;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (_httpContext.HttpContext?.Session.GetString("CurrentUser") != null)
                {
                    if (_httpContext.HttpContext?.Session.GetString("referenceNumber") != null)
                    {
                        _httpContext.HttpContext.Session.Remove("referenceNumber");
                    }

                    userObject = JsonSerializer.Deserialize<registration>(_httpContext.HttpContext.Session.GetString("CurrentUser"));
                    ViewBag.User = userObject;
                    List<SelectListItem> quarterItems = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "1", Text = "January to March" },
                        new SelectListItem { Value = "2", Text = "April to June" },
                        new SelectListItem { Value = "3", Text = "July to September" },
                        new SelectListItem { Value = "4", Text = "October to December" }
                    };
                    var quarterList = new SelectList(quarterItems, "Value", "Text");
                    ViewBag.Quarters = quarterList;

                    List<Years> years = await _qprRepo.GetYears();
                    int currentYear = DateTime.Now.Year;
                    if (years.Count == 0 || years.Last().Year != currentYear.ToString())
                    {
                        years.Add(new Years { Year = currentYear.ToString() });
                    }
                    ViewBag.Years = years;
                    return View();
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(GetQPR qprDetails)
        {
            string UserId = _httpContext.HttpContext.Session.GetString("UserName").ToString();
            if (ModelState.IsValid)
            {
                _httpContext.HttpContext.Session.SetString("qtryear", qprDetails.SelectedYear.ToString());
                _httpContext.HttpContext.Session.SetString("qtrreport", qprDetails.SelectedQuarter);

                var refNum = await _qprRepo.GetReferenceNumber(qprDetails, UserId);
                string ip = _httpContext.HttpContext.Session.GetString("ipAddress").ToString();
                if (String.IsNullOrEmpty(refNum))
                {
                    refNum = await _qprRepo.GenerateReferenceNumber(qprDetails, UserId, ip);
                }
                _httpContext.HttpContext.Session.SetString("referenceNumber", refNum);

                return RedirectToAction("Complaints");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Complaints(string message = "")
        {
            try
            {
                //ClearSessionData();
                _httpContext.HttpContext?.Session.Remove("complaint_id");
                //display message
                ViewBag.ComplaintsMessage = message;

                if (!String.IsNullOrEmpty(_httpContext.HttpContext?.Session.GetString("referenceNumber")))
                {
                    string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");

                    complaintsqrs complaint = await _complaintsRepo.GetComplaintsData(refNum);
                    complaintsqrs complaintPrevious = await _complaintsRepo.GetComplaintsData(GetPreviousReferenceNumber());

                    if (complaint == null)
                    {
                        complaint = new complaintsqrs();
                    }
                    else
                    {
                        _httpContext.HttpContext.Session.SetString("complaint_id", Convert.ToString(complaint.complaints_id));
                    }

                    if (complaintPrevious != null)
                    {
                        complaint.comcvcopeningbalance = complaintPrevious.comcvcbalance_pending;
                        complaint.comotheropeningbalance = complaintPrevious.comotherbalance_pending;
                        complaint.comtotalopeningbalance = complaintPrevious.comtotalbalance_pending;
                        complaint.cvcpidpi_opening_balance = complaintPrevious.cvcpidpi_balance_pending;
                        complaint.otherpidpi_opening_balance = complaintPrevious.otherpidpi_balance_pending;
                        complaint.totalpidpi_opening_balance = complaintPrevious.totalpidpi_balance_pending;
                        complaint.cvcpidpiinvestadvicecvc = complaintPrevious.cvcpidpiinvestotaladvicereceive - complaintPrevious.cvcpidpiinvesactionduringquarter;
                        complaint.cvopidpiinvestadvicecvc = complaintPrevious.cvopidpiinvestotaladvicereceive - complaintPrevious.cvopidpiinvesactionduringquarter;
                        complaint.totalpidpiinvestadvicecvc = complaintPrevious.totalpidpiinvestotaladvicereceive - complaintPrevious.totalpidpiinvesactionduringquarter;
                        complaint.napidpibroughtforward = complaintPrevious.napidpipendingqtr;
                        complaint.scrutinyreportbfpreviousyear = complaintPrevious.scrutinyreportpendinginvestigation;
                        complaint.scrutinyreportbfpreviousyearconcurrent = complaintPrevious.scrutinyreportpendinginvestigationconcurrent;
                        complaint.scrutinyreportbfpreviousyearinternal = complaintPrevious.scrutinyreportpendinginvestigationinternal;
                        complaint.scrutinyreportbfpreviousyearstatutory = complaintPrevious.scrutinyreportpendinginvestigationstatutory;
                        complaint.scrutinyreportbfpreviousyearothers = complaintPrevious.scrutinyreportbfpreviousyearothers;
                        complaint.scrutinyreportbfpreviousyeartotal = complaintPrevious.scrutinyreportpendinginvestigationtotal;
                    }

                    return View(complaint);
                }
                else
                {
                    throw new Exception("reference number not found");
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complaints(complaintsqrs complaint)
        {
            try
            {
                await SaveComplaints(complaint);
                return RedirectToAction("VigilanceInvestigation");
            }
            catch (Exception ex)
            {

            }
            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveComplaints(complaintsqrs complaint)
        {
            string message = "";
            try
            {
                //if is it existing record
                if (!String.IsNullOrEmpty(_httpContext.HttpContext?.Session.GetString("complaint_id")))
                {
                    complaintsqrs complaintOldData = await _complaintsRepo.GetComplaintsData(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    //if (complaintOldData == null)
                    //{
                    //    throw new Exception("No previous record present");
                    //}
                    //else
                    //{
                    //}
                    complaint.complaints_id = complaintOldData.complaints_id;
                    complaint.user_id = complaintOldData.user_id;
                    complaint.create_date = complaintOldData.create_date;
                    complaint.qpr_id = complaintOldData.qpr_id;
                    complaint.update_user_id = _httpContext.HttpContext.Session?.GetString("UserName");
                    complaint.used_ip = _httpContext.HttpContext.Session?.GetString("ipAddress");
                    complaint.update_date = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    await _qprRepo.SaveComplaints(complaint);
                }
                else
                {
                    complaint.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    complaint.used_ip = _httpContext.HttpContext?.Session.GetString("ipAddress");
                    complaint.user_id = _httpContext.HttpContext?.Session.GetString("UserName");
                    complaint.create_date = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    await _qprRepo.CreateComplaints(complaint);
                }
                message = "Saved";
            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("Complaints", "QPR", new { message });
        }
        public async Task<IActionResult> VigilanceInvestigation(string message = "")
        {
            try
            {
                //ClearSessionData();
                _httpContext.HttpContext?.Session.Remove("viginvestigations_id");

                //display message
                ViewBag.ComplaintsMessage = message;
                if (!String.IsNullOrEmpty(_httpContext.HttpContext?.Session.GetString("referenceNumber")))
                {
                    string refNum = _httpContext.HttpContext?.Session.GetString("referenceNumber");

                    viginvestigationqrs vigInv = await _complaintsRepo.GetVigilanceInvestigationData(refNum);
                    viginvestigationqrs vigInvPrev = await _complaintsRepo.GetVigilanceInvestigationData(GetPreviousReferenceNumber());
                    if (vigInv == null)
                    {
                        vigInv = new viginvestigationqrs();
                    }
                    else
                    {
                        _httpContext.HttpContext?.Session.SetString("viginvestigations_id", Convert.ToString(vigInv.viginvestigations_id));
                    }

                    if (vigInvPrev != null)
                    {
                        vigInv.viginvescvcopeningbalance = vigInvPrev.viginvescvcbalancepending;
                        vigInv.viginvescvoopeningbalance = vigInvPrev.viginvescvobalancepending;
                        vigInv.viginvestotalopeningbalance = vigInvPrev.viginvestotalbalancepending;
                        vigInv.viginvespendingopeningpending = vigInvPrev.viginvespendingbalancepending;
                        vigInv.viginvestacbireportqtr = vigInvPrev.viginvestacbibalancepending;
                        vigInv.viginvestacvoreportqtr = vigInvPrev.viginvestacvobalancepending;
                        vigInv.viginvestbcbireportbfqtr = vigInvPrev.viginvestbcbibalancepending;
                        vigInv.viginvestbcvoreportbfqtr = vigInvPrev.viginvestbcvobalancepending;
                        vigInv.viginvestatotalreportqtr = vigInvPrev.viginvestatotalbalancepending;
                    }

                    return View(vigInv);
                }

            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveVigInvestigation(viginvestigationqrs vigInv)
        {
            string message = "";
            try
            {
                //if is it existing record
                if (!String.IsNullOrEmpty(_httpContext.HttpContext?.Session.GetString("viginvestigations_id")))
                {
                    viginvestigationqrs? vigInvOldData = await _complaintsRepo.GetVigilanceInvestigationData(_httpContext.HttpContext.Session.GetString("referenceNumber"));

                    vigInv.viginvestigations_id = vigInvOldData.viginvestigations_id;
                    vigInv.user_id = vigInvOldData.user_id;
                    vigInv.create_date = vigInvOldData.create_date;
                    vigInv.qpr_id = vigInvOldData.qpr_id;
                    vigInv.last_user_id = _httpContext.HttpContext.Session?.GetString("UserName");
                    vigInv.ip = _httpContext.HttpContext.Session?.GetString("ipAddress");
                    vigInv.update_date = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    await _qprRepo.SaveVigilanceInvestigation(vigInv);
                }
                else
                {
                    vigInv.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    vigInv.ip = _httpContext.HttpContext?.Session.GetString("ipAddress");
                    vigInv.user_id = _httpContext.HttpContext?.Session.GetString("UserName");
                    vigInv.last_user_id = vigInv.user_id;
                    vigInv.create_date = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    await _qprRepo.CreateVigilanceInvestigation(vigInv);
                }
                message = "Saved";
            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("VigilanceInvestigation", "QPR", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VigilanceInvestigation(viginvestigationqrs vigInv)
        {
            try
            {
                await SaveVigInvestigation(vigInv);
                return RedirectToAction("ProsecutionSanctions");
            }
            catch (Exception ex)
            {
            }
            return View(vigInv);
        }
        public async Task<IActionResult> ProsecutionSanctions(string message = "")
        {
            try
            {
                //ClearSessionData();
                _httpContext.HttpContext?.Session.Remove("prosecutionsanctions_id");
                //display message
                ViewBag.ComplaintsMessage = message;

                if (!String.IsNullOrEmpty(_httpContext.HttpContext?.Session.GetString("referenceNumber")))
                {
                    string refNum = _httpContext.HttpContext?.Session.GetString("referenceNumber");

                    ProsecutionSanctionsViewModel proSec = await _complaintsRepo.GetProsecutionSanctionsViewData(refNum);
                    ProsecutionSanctionsViewModel proSecPrev = await _complaintsRepo.GetProsecutionSanctionsViewData(GetPreviousReferenceNumber());


                    if (proSec.Prosecutionsanctionsqrs == null)
                    {
                        proSec.Prosecutionsanctionsqrs = new prosecutionsanctionsqrs();
                    }
                    else
                    {
                        _httpContext.HttpContext?.Session.SetString("prosecutionsanctions_id", Convert.ToString(proSec.Prosecutionsanctionsqrs.prosecutionsanctions_id));
                    }
                    //proSec.NewAgewisependency = new agewisependency();

                    if (proSecPrev != null)
                    {
                        proSec.Prosecutionsanctionsqrs.prosesanctgroupcopeningbalance = proSecPrev.Prosecutionsanctionsqrs.prosesanctgroupcbalancepending;
                        proSec.Prosecutionsanctionsqrs.prosesanctgroupbopeningbalance = proSecPrev.Prosecutionsanctionsqrs.prosesanctgroupbbalancepending;
                        proSec.Prosecutionsanctionsqrs.prosesanctgroupaopeningbalance = proSecPrev.Prosecutionsanctionsqrs.prosesanctgroupabalancepending;
                        proSec.Prosecutionsanctionsqrs.prosesanctjsopeningbalance = proSecPrev.Prosecutionsanctionsqrs.prosesanctjsbalancepending;
                        proSec.Prosecutionsanctionsqrs.prosevigiofficersuspension = proSecPrev.Prosecutionsanctionsqrs.prosevigisuspensionendqtr;
                    }

                    return View(proSec);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveProsecutionSanctions(ProsecutionSanctionsViewModel prosecViewModel)
        {
            string message = "";
            try
            {
                prosecViewModel.NewAgewisependency.used_ip = _httpContext.HttpContext.Session?.GetString("ipAddress");
                prosecViewModel.NewAgewisependency.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));

                //if is it existing record
                prosecutionsanctionsqrs proSecNewData = prosecViewModel.Prosecutionsanctionsqrs;
                if (!String.IsNullOrEmpty(_httpContext.HttpContext?.Session?.GetString("prosecutionsanctions_id")))
                {
                    prosecutionsanctionsqrs? proSecOldData = await _complaintsRepo.GetProsecutionSanctionsData(_httpContext.HttpContext?.Session?.GetString("referenceNumber"));
                    proSecNewData.user_id = proSecOldData.user_id;
                    proSecNewData.last_user_id = _httpContext.HttpContext.Session?.GetString("UserName");
                    proSecNewData.ip = _httpContext.HttpContext.Session?.GetString("ipAddress");
                    proSecNewData.update_date = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //proSecNewData.create_date = proSecOldData.create_date;
                    proSecNewData.qpr_id = proSecOldData.qpr_id;
                    proSecNewData.prosecutionsanctions_id = Convert.ToInt32(_httpContext.HttpContext?.Session?.GetString("prosecutionsanctions_id"));
                    await _qprRepo.SaveProsecutionSanctionsViewModel(prosecViewModel);
                }
                else // for new record
                {
                    proSecNewData.create_date = DateTime.Now.Date.ToString("dd-MM-yyyy"); ;
                    proSecNewData.user_id = _httpContext.HttpContext?.Session?.GetString("UserName");
                    proSecNewData.last_user_id = _httpContext.HttpContext?.Session?.GetString("UserName");
                    proSecNewData.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session?.GetString("referenceNumber"));
                    proSecNewData.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    await _qprRepo.CreateProsecutionSanctionsViewModel(prosecViewModel);
                }
                message = "Saved";
            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("ProsecutionSanctions", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProsecutionSanctions(ProsecutionSanctionsViewModel prosecViewModel)
        {
            try
            {
                await SaveProsecutionSanctions(prosecViewModel);
                return RedirectToAction("DepartmentalProceedings");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public async Task<IActionResult> DepartmentalProceedings(string message = "")
        {
            try
            {
                //ClearSessionData();
                _httpContext.HttpContext?.Session.Remove("departproceedings_id");

                //display message
                ViewBag.ComplaintsMessage = message;

                if (!String.IsNullOrEmpty(_httpContext.HttpContext?.Session.GetString("referenceNumber")))
                {
                    string refNum = _httpContext.HttpContext?.Session.GetString("referenceNumber");

                    DepartmentalProceedingsViewModel deptViewModel = await _complaintsRepo.GetDepartmentalProceedingsViewModelData(refNum);
                    DepartmentalProceedingsViewModel deptViewModelPrev = await _complaintsRepo.GetDepartmentalProceedingsViewModelData(GetPreviousReferenceNumber());

                    if (deptViewModel.Departmentalproceedingsqrs == null)
                    {
                        deptViewModel.Departmentalproceedingsqrs = new departmentalproceedingsqrs();
                    }
                    else
                    {
                        _httpContext.HttpContext?.Session.SetString("departproceedings_id", Convert.ToString(deptViewModel.Departmentalproceedingsqrs.departproceedings_id));
                    }

                    if (deptViewModelPrev != null)
                    {
                        deptViewModel.Departmentalproceedingsqrs.departproceedingsmajor_cvc_lastqtr = deptViewModelPrev.Departmentalproceedingsqrs.departproceedingsmajor_cvc_enquiries;
                        deptViewModel.Departmentalproceedingsqrs.departproceedingsmajor_other_lastqtr = deptViewModelPrev.Departmentalproceedingsqrs.departproceedingsmajor_other_enquiries;
                        deptViewModel.Departmentalproceedingsqrs.departproceedingsmajor_total_lastqtr = deptViewModelPrev.Departmentalproceedingsqrs.departproceedingsmajor_total_enquiries;
                        deptViewModel.Departmentalproceedingsqrs.departproceedings_minor_cvc_lastqtr = deptViewModelPrev.Departmentalproceedingsqrs.departproceedings_minor_cvc_enquiries;
                        deptViewModel.Departmentalproceedingsqrs.departproceedings_minor_other_lastqtr = deptViewModelPrev.Departmentalproceedingsqrs.departproceedings_minor_other_enquiries;
                        deptViewModel.Departmentalproceedingsqrs.departproceedings_minor_total_lastqtr = deptViewModelPrev.Departmentalproceedingsqrs.departproceedings_minor_total_enquiries;

                    }
                    return View(deptViewModel);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDepartmentalProceedings(DepartmentalProceedingsViewModel deptVM)
        {
            string message = "";
            try
            {
                //if is it existing record
                if (!String.IsNullOrEmpty(_httpContext.HttpContext?.Session?.GetString("departproceedings_id")))
                {
                    departmentalproceedingsqrs deptProcOldData = await _complaintsRepo.GetDepartmentalProceedingsData(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    await _qprRepo.SaveDepartmentalProceedings(deptVM, deptProcOldData);
                }
                else
                {
                    // for new record
                    await _qprRepo.CreateDepartmentalProceedings(deptVM);
                }
                message = "Saved";
            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("DepartmentalProceedings", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepartmentalProceedings(DepartmentalProceedingsViewModel deptProc)
        {
            try
            {
                await SaveDepartmentalProceedings(deptProc);
                return RedirectToAction("AdviceOfCVC");
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public async Task<IActionResult> AdviceOfCVC(string message = "")
        {
            try
            {
                //ClearSessionData();
                _httpContext.HttpContext?.Session.Remove("advice_cvc_id");

                //display message
                ViewBag.ComplaintsMessage = message;

                if (!String.IsNullOrEmpty(_httpContext.HttpContext.Session.GetString("referenceNumber")))
                {
                    string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                    AdviceOfCvcViewModel adviceViewModel = await _complaintsRepo.GetAdviceOfCVCViewModel(refNum);

                    adviceofcvcqrs adviceOfCvcPrev = await _complaintsRepo.GetAdviceOfCVCData(GetPreviousReferenceNumber());

                    if (adviceViewModel.AdviceOfCvc == null)
                    {
                        adviceViewModel.AdviceOfCvc = new adviceofcvcqrs();
                    }
                    else
                    {
                        _httpContext.HttpContext?.Session.SetString("advice_cvc_id", Convert.ToString(adviceViewModel.AdviceOfCvc.advice_cvc_id));
                    }

                    if (adviceOfCvcPrev != null)
                    {
                        adviceViewModel.AdviceOfCvc.advice_cvc_first_casespreviousqtr = adviceOfCvcPrev.advice_cvc_first_adviceawaitedcvc;
                        adviceViewModel.AdviceOfCvc.advice_cvc_second_casespreviousqtr = adviceOfCvcPrev.advice_cvc_second_adviceawaitedcvc;
                        adviceViewModel.AdviceOfCvc.advice_cvc_firstreconsider_casespreviousqtr = adviceOfCvcPrev.advice_cvc_firstreconsider_adviceawaitedcvc;
                        adviceViewModel.AdviceOfCvc.advice_cvc_secondreconsider_casespreviousqtr = adviceOfCvcPrev.advice_cvc_secondreconsider_adviceawaitedcvc;
                        adviceViewModel.AdviceOfCvc.action_cvc_firstmajor_openingbalance = adviceOfCvcPrev.action_cvc_firstmajor_balancepending;
                        adviceViewModel.AdviceOfCvc.action_cvc_firstminor_openingbalance = adviceOfCvcPrev.action_cvc_firstminor_balancepending;
                        adviceViewModel.AdviceOfCvc.action_cvc_secondmajor_openingbalance = adviceOfCvcPrev.action_cvc_secondmajor_balancepending;
                        adviceViewModel.AdviceOfCvc.action_cvc_secondminor_openingbalance = adviceOfCvcPrev.action_cvc_secondminor_balancepending;
                    }
                    adviceViewModel.StageTypes = _qprUtil.GetStageTypesAdviceCVC();
                    return View(adviceViewModel);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAdviceOfCVC(AdviceOfCvcViewModel adviceVM)
        {
            string message = "";
            try
            {
                //if is it existing record
                if (!String.IsNullOrEmpty(_httpContext.HttpContext?.Session?.GetString("advice_cvc_id")))
                {
                    await _qprRepo.SaveAdviceCVC(adviceVM);
                }
                else
                {
                    // for new record
                    await _qprRepo.CreateAdviceCVC(adviceVM);
                }
                message = "Saved";
            }
            catch (Exception ex)
            {
                message = "Error";
            }
            return RedirectToAction("AdviceOfCVC", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdviceOfCVC(AdviceOfCvcViewModel adviceVM)
        {
            try
            {
                await SaveAdviceOfCVC(adviceVM);
                return RedirectToAction("StatusofPendencyFIandCACases");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public async Task<IActionResult> StatusofPendencyFIandCACases(string message = "")
        {
            try
            {
                _httpContext.HttpContext?.Session.Remove("pendency_status_id");

                //display message
                ViewBag.ComplaintsMessage = message;

                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    StatusOfPendencyViewModel statusVM = await _complaintsRepo.GetStatusPendencyViewModel(refNum);
                    statusofpendencyqrs statusPrev = await _complaintsRepo.GetStatusPendencyData(GetPreviousReferenceNumber());

                    statusVM.StatusOfPendency.pendency_status_fi_previousqtr = statusPrev.pendency_status_fi_reply_pending;
                    statusVM.StatusOfPendency.pendency_status_ca_previousqtr = statusPrev.pendency_status_ca_comments_pending;

                    return View(statusVM);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveStatusofPendencyFIandCACases(StatusOfPendencyViewModel statusVM)
        {
            string message = "";
            try
            {
                if(!String.IsNullOrEmpty(_httpContext.HttpContext?.Session.GetString("pendency_status_id")))
                {
                    //Save
                }
                else
                {
                    //Create new
                    await _qprRepo.CreateStatusPendency(statusVM);
                }
                message = "Saved";

            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("StatusofPendencyFIandCACases", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StatusofPendencyFIandCACases(statusofpendencyqrs statusPend)
        {
            try
            {
                return RedirectToAction("PunitiveVigilance");
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public async Task<IActionResult> PunitiveVigilance(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var data = await _complaintsRepo.GetPunitiveVigilanceData(refNum);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePunitiveVigilance(punitivevigilanceqrs pvig)
        {
            string message = "";
            try
            {
                message = "Saved";

            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("PunitiveVigilance", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PunitiveVigilance(punitivevigilanceqrs pvig)
        {
            try
            {
                return RedirectToAction("PreventiveVigilance");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public async Task<IActionResult> PreventiveVigilance(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var data = await _complaintsRepo.GetPreventiveVigilanceData(refNum);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePreventiveVigilance(preventivevigilanceqrs prev_vig)
        {
            string message = "";
            try
            {
                message = "Saved";
            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("PreventiveVigilance", new { message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PreventiveVigilance(preventivevigilanceqrs prev_vig)
        {
            try
            {
                return RedirectToAction("PreventiveVigilanceActivities");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public async Task<IActionResult> PreventiveVigilanceActivities(string message = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ComplaintsMessage = message;
                }
                string refNum = _httpContext.HttpContext.Session.GetString("referenceNumber");
                if (!String.IsNullOrEmpty(refNum))
                {
                    var data = await _complaintsRepo.GetPreventiveVigilanceActiviteiesData(refNum);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePreventiveVigilanceActivities(vigilanceactivitiescvcqrs activities)
        {
            string message = "";
            try
            {
                message = "Saved";
            }
            catch (Exception ex)
            {
                message = "Error occured while saving. Please try after sometime";
            }
            return RedirectToAction("PreventiveVigilanceActivities", new { message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PreventiveVigilanceActivities(vigilanceactivitiescvcqrs activities)
        {
            try
            {
                return RedirectToAction("Download QPR");
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public IActionResult Instructions()
        {
            return View();
        }

        public IActionResult ChangePassword(string code = "")
        {
            try
            {
                if (_httpContext.HttpContext.Session.GetString("CurrentUser") != null)
                {
                    userObject = JsonSerializer.Deserialize<registration>(_httpContext.HttpContext.Session.GetString("CurrentUser"));
                    ViewBag.User = userObject;

                    if (!string.IsNullOrEmpty(code))
                    {
                        string message = "";
                        switch (code)
                        {
                            case "iop": message = "Incorrect Old Password"; break;
                            case "npop": message = "New password cannot be same as old password"; break;
                            case "npsop": message = "New Password cannot be same as last two previous passwords"; break;
                            default: message = ""; break;
                        }
                        ViewBag.ComplaintsMessage = message;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword cp)
        {
            try
            {
                var ip = _httpContext.HttpContext.Connection.RemoteIpAddress.ToString();
                userObject = JsonSerializer.Deserialize<registration>(_httpContext.HttpContext.Session.GetString("CurrentUser"));
                if (userObject != null)
                {
                    if (cp.OldPassword != userObject.password)
                    {
                        return RedirectToAction("ChangePassword", new { code = "iop" });
                    }

                    if (cp.NewPassword == userObject.password)
                    {
                        return RedirectToAction("ChangePassword", new { code = "npop" });
                    }

                    if (cp.NewPassword == userObject.passwordone || cp.NewPassword == userObject.passwordtwo)
                    {
                        return RedirectToAction("ChangePassword", new { code = "npsop" });
                    }

                    // call change password service
                    var changeSuccessful = await _changePasswordRepo.ChangePassword(cp, userObject.userid, ip);
                }
            }
            catch (Exception ex)
            {
            }
            return View();
            //return RedirectToAction("Index", "Login");
        }

        public string GetPreviousReferenceNumber()
        {
            return _qprRepo.GetPreviousReferenceNumber(
                        _httpContext.HttpContext.Session.GetString("UserName"),
                        _httpContext.HttpContext.Session.GetString("qtryear"),
                        _httpContext.HttpContext.Session.GetString("qtrreport")
                        );
        }

        public async Task<IActionResult> DeleteAgeWisePendency(int pend_id)
        {
            await _qprRepo.DeleteAgeWisePendency(pend_id);
            return RedirectToAction("ProsecutionSanctions", new { message = "Deleted successfully" });
        }
        public async Task<IActionResult> DeleteAgainstChargedOfficers(int pend_id)
        {
            await _qprRepo.DeleteAgainstChargedOfficers(pend_id);
            return RedirectToAction("DepartmentalProceedings", new { message = "Deleted successfully" });
        }
        public async Task<IActionResult> DeleteCvcAdvice(int pend_id)
        {
            await _qprRepo.DeleteCvcAdvice(pend_id);
            return RedirectToAction("AdviceOfCVC", new { message = "Deleted successfully" });
        }
        public async Task<IActionResult> DeleteAppeleateAuthority(int pend_id)
        {
            await _qprRepo.DeleteAppeleateAuthority(pend_id);
            return RedirectToAction("AdviceOfCVC", new { message = "Deleted successfully" });
        }
        public IActionResult GetNatureListByStageType(string stageName)
        {
            List<string> itemList = new List<string>();
            try
            {
                itemList = _qprUtil.GetNatureTypesAdviceCVC(stageName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message ?? ex.InnerException.ToString());
            }
            return Json(itemList);
        }
    }
}
