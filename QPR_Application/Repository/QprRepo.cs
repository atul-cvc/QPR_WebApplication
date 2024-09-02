using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using System;
using System.Data;
using System.Reflection;

namespace QPR_Application.Repository
{
    public class QprRepo : IQprRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IComplaintsRepo _complaintsRepo;
        private string _connString = string.Empty;

        public QprRepo(QPRContext DbContext, IConfiguration config, IHttpContextAccessor httpContext, IComplaintsRepo complaintsRepo)
        {
            _dbContext = DbContext;
            //_config = config;
            _connString = config.GetConnectionString("SQLConnection") ?? String.Empty;
            _httpContext = httpContext;
            _complaintsRepo = complaintsRepo;
        }
        public async Task<List<Years>> GetYears()
        {
            List<Years> years = new List<Years>();
            try
            {
                years = await _dbContext.Set<Years>().FromSqlRaw("EXEC GetYearsFromQPR").ToListAsync();
                return years;
            }
            catch (Exception ex)
            {
            }
            return years;
        }
        public async Task<List<Years>> GetYearsFinalSubmit(string userId)
        {
            List<Years> years = new List<Years>();
            try
            {
                years = await _dbContext.Set<Years>().FromSqlRaw("EXECUTE dbo.GetYearsFromQPRFinallySubmitted @UserID", new SqlParameter("@UserID", userId)).ToListAsync();
                return years;
            }
            catch (Exception ex)
            {
            }
            return years;
        }
        public string GetReferenceNumber(GetQPR qprDetails, string UserId)
        {
            //var connString = _config.GetConnectionString("SQLConnection");
            string refNum = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("GetQPRReferenceNumber", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@QtrYear", qprDetails.SelectedYear);
                    cmd.Parameters.AddWithValue("@QtrReport", qprDetails.SelectedQuarter);

                    conn.Open();
                    refNum = Convert.ToString(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex) { }
            return refNum;
        }
        public string GenerateReferenceNumber(GetQPR qprDetails, string UserId, string ip)
        {
            string? refNum = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("CreateQPRReferenceNumber", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@QtrYear", qprDetails.SelectedYear);
                    cmd.Parameters.AddWithValue("@QtrReport", qprDetails.SelectedQuarter);
                    cmd.Parameters.AddWithValue("@ContactNumber", qprDetails.CVCContactNo);
                    cmd.Parameters.AddWithValue("@Fulltime", qprDetails.CVOFulltime);
                    cmd.Parameters.AddWithValue("@Parttime", qprDetails.CVOParttime);
                    cmd.Parameters.AddWithValue("@ipAddress", ip);

                    conn.Open();
                    refNum = Convert.ToString(cmd.ExecuteScalar());
                    if (String.IsNullOrEmpty(refNum))
                    {
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex) { throw ex; }
            return refNum;
        }
        public string GetPreviousReferenceNumber(string UserId, string qtrYear, string qtrReport)
        {
            string refNum = "";
            int qReport = Convert.ToInt32(qtrReport) - 1;
            int qYear = Convert.ToInt32(qtrYear);
            if (qReport == 0)
            {
                qReport = 4;
                qYear -= 1; ;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    int count = 0;

                    using (SqlCommand cmd = new SqlCommand("SP_GetPreviousQprId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@QtrYear", qYear);
                        cmd.Parameters.AddWithValue("@QtrReport", qReport);
                        conn.Open();
                        refNum = Convert.ToString(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex) { }
            return refNum;
        }
        public async Task CreateComplaints(complaintsqrs complaint)
        {
            try
            {
                await _dbContext.complaintsqrs.AddAsync(complaint);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveComplaints(complaintsqrs complaint)
        {
            try
            {
                _dbContext.complaintsqrs.Update(complaint);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CreateVigilanceInvestigation(viginvestigationqrs vigInv)
        {
            try
            {
                await _dbContext.viginvestigationqrs.AddAsync(vigInv);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveVigilanceInvestigation(viginvestigationqrs vigInv)
        {
            try
            {
                _dbContext.viginvestigationqrs.Update(vigInv);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CreateProsecutionSanctionsViewModel(ProsecutionSanctionsViewModel prosecViewModel)
        {
            try
            {
                if (prosecViewModel.Prosecutionsanctionsqrs != null)
                {
                    await _dbContext.prosecutionsanctionsqrs.AddAsync(prosecViewModel.Prosecutionsanctionsqrs);
                }

                if (prosecViewModel.NewAgewisependency.qpr_id != null && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingnamedesig) && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingstatusrequest) && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingnameauthority) && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingcbifirno) && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingsanctionpc))
                {
                    await _dbContext.agewisependency.AddAsync(prosecViewModel.NewAgewisependency);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveProsecutionSanctionsViewModel(ProsecutionSanctionsViewModel prosecViewModel)
        {
            try
            {
                if (prosecViewModel.Prosecutionsanctionsqrs != null)
                {
                    _dbContext.prosecutionsanctionsqrs.Update(prosecViewModel.Prosecutionsanctionsqrs);
                }

                if (prosecViewModel.NewAgewisependency.qpr_id != null && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingnamedesig) && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingstatusrequest) && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingnameauthority) && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingcbifirno) && !String.IsNullOrEmpty(prosecViewModel.NewAgewisependency.prosependingsanctionpc))
                {
                    await _dbContext.agewisependency.AddAsync(prosecViewModel.NewAgewisependency);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CreateDepartmentalProceedings(DepartmentalProceedingsViewModel deptViewModel)
        {
            try
            {
                if (deptViewModel.Departmentalproceedingsqrs != null)
                {
                    deptViewModel.Departmentalproceedingsqrs.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    deptViewModel.Departmentalproceedingsqrs.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    deptViewModel.Departmentalproceedingsqrs.user_id = _httpContext.HttpContext?.Session?.GetString("UserName");
                    deptViewModel.Departmentalproceedingsqrs.create_date = DateTime.Now.Date.ToString("dd-MM-yyyy");

                    await _dbContext.departmentalproceedingsqrs.AddAsync(deptViewModel.Departmentalproceedingsqrs);
                }

                againstchargedtable newAgainst = deptViewModel.NewAgainstChargedTable;
                if (newAgainst != null && !String.IsNullOrEmpty(newAgainst.departproceedings_detailsinquiry_chargedofficer) && !String.IsNullOrEmpty(newAgainst.departproceedings_detailsinquiry_remarks))
                {
                    newAgainst.used_ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    newAgainst.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session?.GetString("referenceNumber"));

                    await _dbContext.againstchargedtable.AddAsync(newAgainst);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveDepartmentalProceedings(DepartmentalProceedingsViewModel deptViewModel, departmentalproceedingsqrs prevData)
        {
            try
            {
                if (deptViewModel.Departmentalproceedingsqrs != null)
                {

                    deptViewModel.Departmentalproceedingsqrs.qpr_id = prevData.qpr_id;
                    deptViewModel.Departmentalproceedingsqrs.user_id = prevData.user_id;
                    deptViewModel.Departmentalproceedingsqrs.last_user_id = _httpContext.HttpContext?.Session?.GetString("UserName"); ;
                    deptViewModel.Departmentalproceedingsqrs.ip = _httpContext.HttpContext.Session?.GetString("ipAddress");
                    //deptViewModel.Departmentalproceedingsqrs.create_date = prevData.create_date;
                    deptViewModel.Departmentalproceedingsqrs.update_date = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    deptViewModel.Departmentalproceedingsqrs.departproceedings_id = prevData.departproceedings_id;

                    _dbContext.departmentalproceedingsqrs.Update(deptViewModel.Departmentalproceedingsqrs);
                }

                againstchargedtable newAgainst = deptViewModel.NewAgainstChargedTable;
                if (newAgainst != null && !String.IsNullOrEmpty(newAgainst.departproceedings_detailsinquiry_chargedofficer) && !String.IsNullOrEmpty(newAgainst.departproceedings_detailsinquiry_remarks))
                {
                    newAgainst.used_ip = _httpContext.HttpContext.Session?.GetString("ipAddress");
                    newAgainst.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));

                    await _dbContext.againstchargedtable.AddAsync(newAgainst);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }
        public async Task CreateAdviceCVC(AdviceOfCvcViewModel adviceVM)
        {
            try
            {
                if (adviceVM.AdviceOfCvc != null)
                {
                    adviceVM.AdviceOfCvc.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    adviceVM.AdviceOfCvc.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    adviceVM.AdviceOfCvc.user_id = _httpContext.HttpContext?.Session?.GetString("UserName");
                    adviceVM.AdviceOfCvc.create_date = DateOnly.FromDateTime(DateTime.Now);

                    await _dbContext.adviceofcvcqrs.AddAsync(adviceVM.AdviceOfCvc);

                    await AddCvcAdvice(adviceVM.NewCvcAdvice);

                    await AddAppelleate(adviceVM.NewAppeleateAuthority);

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveAdviceCVC(AdviceOfCvcViewModel adviceVM)
        {//, adviceofcvcqrs prevData
            try
            {
                if (adviceVM.AdviceOfCvc != null)
                {
                    adviceofcvcqrs newData = adviceVM.AdviceOfCvc;
                    adviceofcvcqrs prevData = await _complaintsRepo.GetAdviceOfCVCData(_httpContext.HttpContext?.Session.GetString("referenceNumber"));

                    newData.qpr_id = prevData.qpr_id;
                    newData.last_user_id = _httpContext.HttpContext?.Session?.GetString("UserName");
                    newData.update_date = DateOnly.FromDateTime(DateTime.Now);
                    newData.advice_cvc_id = prevData.advice_cvc_id;
                    newData.create_date = prevData.create_date;
                    newData.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");

                    _dbContext.adviceofcvcqrs.Update(adviceVM.AdviceOfCvc);
                }

                await AddCvcAdvice(adviceVM.NewCvcAdvice);

                await AddAppelleate(adviceVM.NewAppeleateAuthority);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CreateStatusPendency(StatusOfPendencyViewModel statusVM)
        {
            try
            {
                if (statusVM.StatusOfPendency != null)
                {
                    statusVM.StatusOfPendency.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    statusVM.StatusOfPendency.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    statusVM.StatusOfPendency.create_date = DateOnly.FromDateTime(DateTime.Now);
                    statusVM.StatusOfPendency.user_id = _httpContext.HttpContext?.Session?.GetString("UserName");

                    await _dbContext.statusofpendencyqrs.AddAsync(statusVM.StatusOfPendency);

                    if (statusVM.FiCasesQPRs != null)
                    {
                        for (int i = 0; i < statusVM.FiCasesQPRs.Count; i++)
                        {
                            //await _dbContext.ficasesqpr.AddAsync(statusVM.FiCasesQPRs[i]);
                            await AddNewFiCaseQpr(statusVM.FiCasesQPRs[i]);
                        }
                    }

                    if (statusVM.CaCasesQPRs != null)
                    {
                        for (int i = 0; i < statusVM.CaCasesQPRs.Count; i++)
                        {
                            //await _dbContext.cacasesqpr.AddAsync(statusVM.CaCasesQPRs[i]);
                            await AddNewCaCasesQpr(statusVM.CaCasesQPRs[i]);
                        }
                    }

                    if (statusVM.NewFICase != null)
                        await AddNewFiCaseQpr(statusVM.NewFICase);

                    if (statusVM.NewCACase != null)
                        await AddNewCaCasesQpr(statusVM.NewCACase);

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveStatusPendency(StatusOfPendencyViewModel statusVM)
        {
            try
            {
                if (statusVM.StatusOfPendency != null)
                {
                    statusofpendencyqrs newData = statusVM.StatusOfPendency;
                    statusofpendencyqrs prevData = await _complaintsRepo.GetStatusPendencyData(_httpContext.HttpContext?.Session.GetString("referenceNumber"));

                    newData.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    newData.qpr_id = prevData.qpr_id;
                    newData.create_date = prevData.create_date;
                    newData.update_date = DateOnly.FromDateTime(DateTime.Now);
                    newData.user_id = prevData.user_id;
                    newData.pendency_status_id = prevData.pendency_status_id;
                    newData.last_user_id = _httpContext.HttpContext?.Session?.GetString("UserName");

                    _dbContext.statusofpendencyqrs.Update(newData);

                    if (statusVM.FiCasesQPRs != null)
                    {
                        for (int i = 0; i < statusVM.FiCasesQPRs.Count; i++)
                        {
                            //await AddNewFiCaseQpr(statusVM.FiCasesQPRs[i]);
                            ficasesqpr newDataFI = statusVM.FiCasesQPRs[i];
                            ficasesqpr oldDataFI = await _dbContext.ficasesqpr.AsNoTracking().FirstOrDefaultAsync(f => f.ficasesqpr_id == statusVM.FiCasesQPRs[i].ficasesqpr_id);
                            newDataFI.qpr_id = oldDataFI.qpr_id;
                            newDataFI.user_id = oldDataFI.user_id;
                            newDataFI.status = oldDataFI.status;
                            newDataFI.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                            newDataFI.last_user_id = _httpContext.HttpContext?.Session?.GetString("UserName");

                            _dbContext.ficasesqpr.Update(newDataFI);
                        }
                    }

                    if (statusVM.CaCasesQPRs != null)
                    {
                        for (int i = 0; i < statusVM.CaCasesQPRs.Count; i++)
                        {
                            cacasesqpr newDataCA = statusVM.CaCasesQPRs[i];
                            cacasesqpr oldDataCA = await _dbContext.cacasesqpr.AsNoTracking().FirstOrDefaultAsync(c => c.cacasesqpr_id == statusVM.CaCasesQPRs[i].cacasesqpr_id);
                            if (oldDataCA != null)
                            {
                                newDataCA.qpr_id = oldDataCA.qpr_id;
                                newDataCA.user_id = oldDataCA.user_id;
                                newDataCA.status = oldDataCA.status;
                                newDataCA.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                                newDataCA.last_user_id = _httpContext.HttpContext?.Session?.GetString("UserName");
                                newDataCA.update_date = DateOnly.FromDateTime(DateTime.Now);

                                _dbContext.cacasesqpr.Update(newDataCA);
                            }
                        }
                    }

                    if (statusVM.NewFICase != null)
                        await AddNewFiCaseQpr(statusVM.NewFICase);

                    if (statusVM.NewCACase != null)
                        await AddNewCaCasesQpr(statusVM.NewCACase);

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CreatePunitiveVigilance(punitivevigilanceqrs pVig)
        {
            try
            {
                if (pVig != null)
                {
                    pVig.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    pVig.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    pVig.create_date = DateOnly.FromDateTime(DateTime.Now);
                    pVig.user_id = _httpContext.HttpContext?.Session?.GetString("UserName");

                    await _dbContext.punitivevigilanceqrs.AddAsync(pVig);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SavePunitiveVigilance(punitivevigilanceqrs pVig)
        {
            try
            {
                if (pVig != null)
                {
                    punitivevigilanceqrs prevData = await _complaintsRepo.GetPunitiveVigilanceData(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    pVig.create_date = prevData.create_date;
                    pVig.qpr_id = prevData.qpr_id;
                    pVig.punitive_vigilance_id = prevData.punitive_vigilance_id;
                    pVig.user_id = prevData.user_id;
                    pVig.update_date = DateOnly.FromDateTime(DateTime.Now);
                    pVig.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    pVig.last_user_id = _httpContext.HttpContext?.Session?.GetString("UserName");

                    _dbContext.punitivevigilanceqrs.Update(pVig);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CreatePreventiveVigilance(PreventiveVigilanceViewModel pVig)
        {
            try
            {
                if (pVig != null)
                {
                    pVig.PreventiveVigilanceQRS.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    pVig.PreventiveVigilanceQRS.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    pVig.PreventiveVigilanceQRS.create_date = DateOnly.FromDateTime(DateTime.Now);
                    pVig.PreventiveVigilanceQRS.user_id = _httpContext.HttpContext?.Session?.GetString("UserName");

                    await _dbContext.preventivevigilanceqrs.AddAsync(pVig.PreventiveVigilanceQRS);
                    await _dbContext.SaveChangesAsync();

                    await AddNewPrevVigA(pVig.NewPreventivVigi_A, pVig.PreventiveVigilanceQRS.preventive_vigilance_id);
                    await AddNewPrevVigB(pVig.NewPreventivVigi_B, pVig.PreventiveVigilanceQRS.preventive_vigilance_id);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SavePreventiveVigilance(PreventiveVigilanceViewModel pVig)
        {
            try
            {
                preventivevigilanceqrs prevData = await _complaintsRepo.GetPreventiveVigilanceData(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                pVig.PreventiveVigilanceQRS.preventive_vigilance_id = prevData.preventive_vigilance_id;
                pVig.PreventiveVigilanceQRS.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                pVig.PreventiveVigilanceQRS.qpr_id = prevData.qpr_id;
                pVig.PreventiveVigilanceQRS.create_date = prevData.create_date;
                pVig.PreventiveVigilanceQRS.update_date = DateOnly.FromDateTime(DateTime.Now);
                pVig.PreventiveVigilanceQRS.user_id = prevData.user_id;
                pVig.PreventiveVigilanceQRS.last_user_id = _httpContext.HttpContext?.Session?.GetString("UserName");
                if (String.IsNullOrEmpty(pVig.PreventiveVigilanceQRS.file1))
                    pVig.PreventiveVigilanceQRS.file1 = prevData.file1;

                _dbContext.preventivevigilanceqrs.Update(pVig.PreventiveVigilanceQRS);
                await _dbContext.SaveChangesAsync();

                if (pVig.PrevVigiA.Count > 0)
                {
                    for (int i = 0; i < pVig.PrevVigiA.Count; i++)
                    {
                        pVig.PrevVigiA[i].qpr_id = prevData.qpr_id;
                        pVig.PrevVigiA[i].ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                        pVig.PrevVigiA[i].preventive_vigilance_id = prevData.preventive_vigilance_id;
                        if (AreAllPropertiesSet(pVig.PrevVigiA[i], ["preventivevigi_a_id", "preventivevigi_a_detailsvigi_subsidiaries_serial_number", "preventive_vigilance_id"]))
                        {
                            _dbContext.preventivevigi_a_qpr.Update(pVig.PrevVigiA[i]);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }

                if (pVig.PrevVigiB.Count > 0)
                {
                    for (int i = 0; i < pVig.PrevVigiB.Count; i++)
                    {
                        pVig.PrevVigiB[i].qpr_id = prevData.qpr_id;
                        pVig.PrevVigiB[i].ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                        pVig.PrevVigiB[i].preventive_vigilance_id = prevData.preventive_vigilance_id;
                        if (AreAllPropertiesSet(pVig.PrevVigiB[i], ["preventivevigi_b_id", "preventivevigi_b_detailsvigi_subsidiaries_serial_number", "preventive_vigilance_id"]))
                        {
                            _dbContext.preventivevigi_b_qpr.Update(pVig.PrevVigiB[i]);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }

                if (pVig.NewPreventivVigi_A != null)
                    await AddNewPrevVigA(pVig.NewPreventivVigi_A, prevData.preventive_vigilance_id);

                if (pVig.NewPreventivVigi_A != null)
                    await AddNewPrevVigB(pVig.NewPreventivVigi_B, prevData.preventive_vigilance_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CreatePreventiveVigilanceActivities(vigilanceactivitiescvcqrs vigilanceactivities)
        {
            try
            {
                if (vigilanceactivities != null)
                {
                    vigilanceactivities.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    vigilanceactivities.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                    vigilanceactivities.create_date = DateOnly.FromDateTime(DateTime.Now);
                    vigilanceactivities.user_id = _httpContext.HttpContext?.Session?.GetString("UserName");

                    await _dbContext.vigilanceactivitiescvcqrs.AddAsync(vigilanceactivities);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SavePreventiveVigilanceActivities(vigilanceactivitiescvcqrs vigilanceactivities)
        {
            try
            {
                vigilanceactivitiescvcqrs prevData = await _complaintsRepo.GetPreventiveVigilanceActivitiesData(_httpContext.HttpContext?.Session.GetString("referenceNumber"));
                vigilanceactivities.qpr_id = prevData.qpr_id;
                vigilanceactivities.create_date = prevData.create_date;
                vigilanceactivities.update_date = DateOnly.FromDateTime(DateTime.Now);
                vigilanceactivities.user_id = prevData.user_id;
                vigilanceactivities.last_user_id = _httpContext.HttpContext?.Session?.GetString("UserName");
                vigilanceactivities.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                vigilanceactivities.vigilance_activites_id = prevData.vigilance_activites_id;


                _dbContext.vigilanceactivitiescvcqrs.Update(vigilanceactivities);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddCvcAdvice(cvcadvicetable cvcAdvice)
        {
            try
            {
                if (AreAllPropertiesSet(cvcAdvice, ["pend_id", "qpr_id", "used_ip"]))
                {
                    cvcAdvice.used_ip = _httpContext.HttpContext.Session?.GetString("ipAddress");
                    cvcAdvice.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session?.GetString("referenceNumber"));

                    await _dbContext.cvcadvicetable.AddAsync(cvcAdvice);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddAppelleate(appellateauthoritytable appellateAuth)
        {
            try
            {
                if (AreAllPropertiesSet(appellateAuth, ["pend_id", "qpr_id", "used_ip"]))
                {
                    appellateAuth.used_ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    appellateAuth.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session?.GetString("referenceNumber"));

                    await _dbContext.appellateauthoritytable.AddAsync(appellateAuth);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteAgeWisePendency(int pend_id)
        {
            try
            {
                agewisependency ageWisePend = await _dbContext.agewisependency.FirstOrDefaultAsync(i => i.pend_id == pend_id);
                _dbContext.agewisependency.Remove(ageWisePend);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteAgainstChargedOfficers(int pend_id)
        {
            try
            {
                againstchargedtable againstCT = await _dbContext.againstchargedtable.FirstOrDefaultAsync(i => i.pend_id == pend_id);
                _dbContext.againstchargedtable.Remove(againstCT);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteCvcAdvice(int pend_id)
        {
            try
            {
                cvcadvicetable cvcAdvice = await _dbContext.cvcadvicetable.FirstOrDefaultAsync(i => i.pend_id == pend_id);
                _dbContext.cvcadvicetable.Remove(cvcAdvice);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteAppeleateAuthority(int pend_id)
        {
            try
            {
                appellateauthoritytable appelleateAuth = await _dbContext.appellateauthoritytable.FirstOrDefaultAsync(i => i.pend_id == pend_id);
                _dbContext.appellateauthoritytable.Remove(appelleateAuth);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddNewFiCaseQpr(ficasesqpr fiCase)
        {
            try
            {
                fiCase.ip = _httpContext.HttpContext.Session.GetString("ipAddress");
                fiCase.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session?.GetString("referenceNumber"));
                fiCase.status = true;
                fiCase.user_id = _httpContext.HttpContext.Session.GetString("UserName");
                fiCase.last_user_id = _httpContext.HttpContext.Session.GetString("UserName");
                if (AreAllPropertiesSet(fiCase, ["ficasesqpr_id", "qpr_id", "ip", "submission_date", "last_user_id", "update_date", "remark"]))
                {

                    await _dbContext.ficasesqpr.AddAsync(fiCase);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddNewCaCasesQpr(cacasesqpr caCase)
        {
            try
            {
                caCase.ip = _httpContext.HttpContext.Session.GetString("ipAddress");
                caCase.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session?.GetString("referenceNumber"));
                caCase.status = true;
                caCase.user_id = _httpContext.HttpContext.Session.GetString("UserName");
                if (AreAllPropertiesSet(caCase, ["cacasesqpr_id", "submission_date", "last_user_id", "update_date", "remark"]))
                {
                    await _dbContext.cacasesqpr.AddAsync(caCase);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddNewPrevVigA(preventivevigi_a_qpr prevVigA, int preventive_vigilance_id)
        {
            try
            {
                prevVigA.preventive_vigilance_id = preventive_vigilance_id;
                prevVigA.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session?.GetString("referenceNumber"));
                prevVigA.ip = _httpContext.HttpContext.Session.GetString("ipAddress");

                if (AreAllPropertiesSet(prevVigA, ["preventivevigi_a_id", "preventivevigi_a_detailsvigi_subsidiaries_serial_number", "preventive_vigilance_id"]))
                {
                    await _dbContext.preventivevigi_a_qpr.AddAsync(prevVigA);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddNewPrevVigB(preventivevigi_b_qpr prevVigB, int preventive_vigilance_id)
        {
            try
            {
                prevVigB.preventive_vigilance_id = preventive_vigilance_id;
                prevVigB.qpr_id = Convert.ToInt64(_httpContext.HttpContext?.Session?.GetString("referenceNumber"));
                prevVigB.ip = _httpContext.HttpContext.Session.GetString("ipAddress");

                if (AreAllPropertiesSet(prevVigB, ["preventivevigi_b_id", "preventivevigi_b_detailsvigi_subsidiaries_serial_number", "preventive_vigilance_id"]))
                {
                    await _dbContext.preventivevigi_b_qpr.AddAsync(prevVigB);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteFiCaseRow(int id)
        {
            try
            {
                ficasesqpr fiCase = await _dbContext.ficasesqpr.FirstOrDefaultAsync(i => i.ficasesqpr_id == id);
                _dbContext.ficasesqpr.Remove(fiCase);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteCaCaseRow(int id)
        {
            try
            {
                cacasesqpr caCase = await _dbContext.cacasesqpr.FirstOrDefaultAsync(i => i.cacasesqpr_id == id);
                _dbContext.cacasesqpr.Remove(caCase);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeletePrevVigi(int id, string tableName)
        {
            try
            {
                if (tableName.Equals("PrevVigiA"))
                {
                    preventivevigi_a_qpr prevA = await _dbContext.preventivevigi_a_qpr.FirstOrDefaultAsync(i => i.preventivevigi_a_id == id);
                    _dbContext.Remove(prevA);
                }
                if (tableName.Equals("PrevVigiB"))
                {
                    preventivevigi_b_qpr prevB = await _dbContext.preventivevigi_b_qpr.FirstOrDefaultAsync(i => i.preventivevigi_b_id == id);
                    _dbContext.Remove(prevB);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool AreAllPropertiesSet<T>(T obj, string[] excludeArr = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                // Skip properties that can't be read or are in the excludeArr 
                if (!property.CanRead || excludeArr.Contains(property.Name))
                {
                    continue;
                }

                object value = property.GetValue(obj);

                // For value types, check if it's the default value
                if (value == null || (property.PropertyType.IsValueType && Activator.CreateInstance(property.PropertyType).Equals(value)))
                {
                    return false; // Found a property with no value
                }
            }
            return true; // All properties have values
        }
    }
}
