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
                    adviceVM.AdviceOfCvc.create_date = new DateOnly();

                    await _dbContext.adviceofcvcqrs.AddAsync(adviceVM.AdviceOfCvc);
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
                    newData.update_date = new DateOnly();
                    newData.advice_cvc_id = prevData.advice_cvc_id;
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
