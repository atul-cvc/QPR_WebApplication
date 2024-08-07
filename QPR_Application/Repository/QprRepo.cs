using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using System;
using System.Data;

namespace QPR_Application.Repository
{
    public class QprRepo : IQprRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        private string _connString = string.Empty;

        public QprRepo(QPRContext DbContext, IConfiguration config, IHttpContextAccessor httpContext)
        {
            _dbContext = DbContext;
            //_config = config;
            _connString = config.GetConnectionString("SQLConnection") ?? String.Empty;
            _httpContext = httpContext;
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

        public async Task<string> GetReferenceNumber(GetQPR qprDetails, string UserId)
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

        public async Task<string> GenerateReferenceNumber(GetQPR qprDetails, string UserId, string ip)
        {
            string refNum = "";

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
                }
            }
            catch (Exception ex) { }
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
                if (newAgainst != null &&  !String.IsNullOrEmpty(newAgainst.departproceedings_detailsinquiry_chargedofficer) &&  !String.IsNullOrEmpty(newAgainst.departproceedings_detailsinquiry_remarks))
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
                if (newAgainst != null && !String.IsNullOrEmpty(newAgainst.departproceedings_detailsinquiry_chargedofficer)  && !String.IsNullOrEmpty(newAgainst.departproceedings_detailsinquiry_remarks))
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
    }
}
