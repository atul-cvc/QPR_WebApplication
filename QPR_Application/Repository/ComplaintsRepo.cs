using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using System;
using System.Data;
using System.Diagnostics;

namespace QPR_Application.Repository
{
    public class ComplaintsRepo : IComplaintsRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        //private readonly IConfiguration _iConfig;
        private readonly string _connString;
        public ComplaintsRepo(QPRContext dbContext, IHttpContextAccessor httpContext, IConfiguration iConfig)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
            //_iConfig = iConfig;
            _connString = iConfig.GetConnectionString("SQLConnection");
        }
        public async Task<complaintsqrs> GetComplaintsData(string refNum)
        {
            try
            {
                return await _dbContext.complaintsqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<viginvestigationqrs> GetVigilanceInvestigationData(string refNum)
        {
            try
            {
                return await _dbContext.viginvestigationqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<ProsecutionSanctionsViewModel?> GetProsecutionSanctionsViewData(string refNum)
        {
            try
            {
                //prosecutionsanctionsqrs prsSec = ;
                //List<agewisependency> ageWise = 

                ProsecutionSanctionsViewModel proSecViewModel = new ProsecutionSanctionsViewModel();
                proSecViewModel.Prosecutionsanctionsqrs = await GetProsecutionSanctionsData(refNum);
                proSecViewModel.Agewisependency = await _dbContext.agewisependency.AsNoTracking().Where(i => i.qpr_id == Convert.ToInt64(refNum)).ToListAsync(); ;

                return proSecViewModel;
                //return await _dbContext.prosecutionsanctionsqrs.FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<prosecutionsanctionsqrs?> GetProsecutionSanctionsData(string refNum)
        {
            try
            {
                return await _dbContext.prosecutionsanctionsqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public async Task<DepartmentalProceedingsViewModel> GetDepartmentalProceedingsViewModelData(string refNum)
        {
            try
            {
                DepartmentalProceedingsViewModel deptViewModel = new DepartmentalProceedingsViewModel();
                deptViewModel.Departmentalproceedingsqrs = await _dbContext.departmentalproceedingsqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt32(refNum));
                deptViewModel.AgainstChargedTables = await _dbContext.againstchargedtable.Where(a => a.qpr_id == Convert.ToInt64(refNum)).ToListAsync();
                return deptViewModel;
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<departmentalproceedingsqrs?> GetDepartmentalProceedingsData(string refNum)
        {
            try
            {
                return await _dbContext.departmentalproceedingsqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt32(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<AdviceOfCvcViewModel> GetAdviceOfCVCViewModel(string refNum)
        {
            try
            {
                AdviceOfCvcViewModel advice = new AdviceOfCvcViewModel();
                advice.AdviceOfCvc = await _dbContext.adviceofcvcqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
                advice.CvcAdvices = await _dbContext.cvcadvicetable.AsNoTracking().Where(i => i.qpr_id == Convert.ToInt64(refNum)).ToListAsync();
                advice.AppeleateAuthorities = await _dbContext.appellateauthoritytable.AsNoTracking().Where(i => i.qpr_id == Convert.ToInt64(refNum)).ToListAsync();

                return advice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public async Task<adviceofcvcqrs?> GetAdviceOfCVCData(string refNum)
        {
            try
            {
                return await _dbContext.adviceofcvcqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<StatusOfPendencyViewModel?> GetStatusPendencyViewModel(string refNum)
        {
            try
            {
                StatusOfPendencyViewModel statusVM = new StatusOfPendencyViewModel();
                statusVM.StatusOfPendency = await GetStatusPendencyData(refNum) ?? new statusofpendencyqrs();

                statusVM.FiCasesQPRs = await _dbContext.ficasesqpr.AsNoTracking().Where(i => i.qpr_id == Convert.ToInt64(refNum)).ToListAsync();
                if (statusVM.FiCasesQPRs.Count == 0)
                {
                    List<ficasesqpr> pends = GetFurtherClarification(_httpContext.HttpContext.Session.GetString("orgcode"));
                }

                statusVM.CaCasesQPRs = await _dbContext.cacasesqpr.AsNoTracking().Where(i => i.qpr_id == Convert.ToInt64(refNum)).ToListAsync();
                if (statusVM.CaCasesQPRs.Count == 0)
                {
                    statusVM.CaCasesQPRs = GetCommentsAwaited(_httpContext.HttpContext.Session.GetString("orgcode"));
                }

                if (statusVM.StatusOfPendency.pendency_status_id != 0)
                {
                    _httpContext.HttpContext?.Session.SetString("pendency_status_id", Convert.ToString(statusVM.StatusOfPendency.pendency_status_id));
                }
                return statusVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public async Task<statusofpendencyqrs?> GetStatusPendencyData(string refNum)
        {
            try
            {
                return await _dbContext.statusofpendencyqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return null;
        }
        public async Task<punitivevigilanceqrs?> GetPunitiveVigilanceData(string refNum)
        {
            try
            {
                punitivevigilanceqrs pVig = await _dbContext.punitivevigilanceqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum)); 
                //?? new punitivevigilanceqrs();
                if(pVig.punitive_vigilance_id != 0)
                {
                    _httpContext.HttpContext?.Session.SetString("punitive_vigilance_id", Convert.ToString(pVig.punitive_vigilance_id));
                }
                return pVig;
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<preventivevigilanceqrs?> GetPreventiveVigilanceData(string refNum)
        {
            try
            {
                return await _dbContext.preventivevigilanceqrs.FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt32(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<vigilanceactivitiescvcqrs?> GetPreventiveVigilanceActiviteiesData(string refNum)
        {
            try
            {
                return await _dbContext.vigilanceactivitiescvcqrs.FirstOrDefaultAsync(i => i.qpr_id == refNum);
            }
            catch (Exception ex) { }
            return null;
        }
        public List<ficasesqpr> GetFurtherClarification(string OrgCode)
        {
            try
            {
                List<ficasesqpr> fiCases = new List<ficasesqpr>();
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    int count = 0;

                    using (SqlCommand cmd = new SqlCommand("GetFurtherClarification", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        cmd.Parameters.AddWithValue("@OrgCode", OrgCode);
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ficasesqpr fiCase = new ficasesqpr();
                                fiCase.qpr_id = 0;
                                fiCase.ip= String.Empty;
                                fiCase.user_id = String.Empty;
                                fiCase.submission_date = null;
                                fiCase.ficasesqpr_id = 0;

                                fiCase.cvc_case_reg_no = reader["cvc_case_reg_no"].ToString();
                                fiCase.cvc_file_no = reader["cvc_file_no"].ToString();
                                fiCase.date_since_pend = DateOnly.FromDateTime(Convert.ToDateTime(reader["date_since_pend"]));
                                fiCase.name_officer = reader["name_officer"].ToString();
                                fiCase.dept_ref_no = reader["dept_ref_no"].ToString();
                                fiCase.present_status = reader["present_status"].ToString();
                                fiCase.remark = reader["remark"].ToString();
                                fiCase.status = Convert.ToBoolean(reader["status"]);

                                fiCases.Add(fiCase);
                            }
                        }
                        cmd.Dispose();
                    }
                }
                return fiCases;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return null;
        }
        public List<cacasesqpr> GetCommentsAwaited(string OrgCode)
        {
            try
            {
                List<cacasesqpr> caCases = new List<cacasesqpr>();
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    int count = 0;

                    using (SqlCommand cmd = new SqlCommand("GetCommentsAwaited", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        cmd.Parameters.AddWithValue("@OrgCode", OrgCode);
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cacasesqpr caCase = new cacasesqpr();
                                caCase.qpr_id = 0;
                                caCase.ip= String.Empty;
                                caCase.user_id = String.Empty;
                                caCase.submission_date = null;
                                caCase.cacasesqpr_id = 0;

                                caCase.cvc_case_reg_no = reader["cvc_case_reg_no"].ToString();
                                caCase.cvc_file_no = reader["cvc_file_no"].ToString();
                                caCase.date_since_pend = DateOnly.FromDateTime(Convert.ToDateTime(reader["date_since_pend"]));
                                caCase.name_officer = reader["name_officer"].ToString();
                                caCase.dept_ref_no = reader["dept_ref_no"].ToString();
                                caCase.present_status = reader["present_status"].ToString();
                                caCase.remark = reader["remark"].ToString();
                                caCase.status = Convert.ToBoolean(reader["status"]);

                                caCases.Add(caCase);
                            }
                        }
                        cmd.Dispose();
                    }
                }
                return caCases;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return null;
        }
    }
}
