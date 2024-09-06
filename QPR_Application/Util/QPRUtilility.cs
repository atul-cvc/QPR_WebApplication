using Microsoft.Data.SqlClient;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Repository;
using System.Data;
using QPR_Application.Models.ViewModels;
using QPR_Application.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QPR_Application.Util
{
    public class QPRUtility
    {
        private readonly IQprRepo _qprRepo;
        private readonly IHttpContextAccessor _httpContext;
        private string _connString = string.Empty;
        private readonly IComplaintsRepo _complaintsRepo;

        public static List<SelectListItem> quarterItems = new List<SelectListItem>
                    {new SelectListItem { Value = "", Text = "Select" },
                        new SelectListItem { Value = "1", Text = "January to March" },
                        new SelectListItem { Value = "2", Text = "April to June" },
                        new SelectListItem { Value = "3", Text = "July to September" },
                        new SelectListItem { Value = "4", Text = "October to December" },
                        new SelectListItem { Value = "5", Text = "All" }
                    };
        public QPRUtility(IQprRepo qprRepo, IHttpContextAccessor httpContext, IConfiguration config, IComplaintsRepo complaintsRepo)
        {
            _qprRepo = qprRepo;
            _httpContext = httpContext;
            _connString = config.GetConnectionString("SQLConnection") ?? String.Empty;
            _complaintsRepo = complaintsRepo;
        }
        public string GetPreviousReferenceNumber()
        {
            return _qprRepo.GetPreviousReferenceNumber(
                        _httpContext.HttpContext.Session.GetString("UserName"),
                        _httpContext.HttpContext.Session.GetString("qtryear"),
                        _httpContext.HttpContext.Session.GetString("qtrreport")
                        );
        }

        public List<TypesAdviceCVC> GetStageTypesAdviceCVC()
        {
            List<TypesAdviceCVC> stageList = new List<TypesAdviceCVC>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("GetStageTypesAdviceCVC", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new TypesAdviceCVC
                            {
                                Name = reader["name"].ToString(), // Cast to string
                                Type = Convert.ToInt32(reader["type"])
                            };
                            stageList.Add(item);
                        }
                    }
                    cmd.Dispose();
                    return stageList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public List<string> GetNatureTypesAdviceCVC(string stageName)
        {
            List<string> natureList = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("GetNatureOfAdviceCVC", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StageName", stageName);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = reader["name"].ToString();
                            natureList.Add(item ?? "");
                        }
                    }
                    cmd.Dispose();
                }
                return natureList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            try
            {
                string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/QPRUploads");

                // Ensure the uploads folder exists
                if (!Directory.Exists(_uploadsFolder))
                {
                    Directory.CreateDirectory(_uploadsFolder);
                }
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("No file provided.");
                }
                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(_uploadsFolder, fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                //return new string("");
                return $"/QPRUploads/{fileName}";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<QPRReportViewModel> PopulateAnnualReportData(List<QPRReportViewModel> dataList)
        {
            QPRReportViewModel annualData = new QPRReportViewModel();
            if (dataList != null && dataList.Count > 0)
            {
                // complaints start
                #region complaints

                annualData.complaints.comcvcopeningbalance = dataList[0].complaints.comcvcopeningbalance;
                annualData.complaints.comotheropeningbalance = dataList[0].complaints.comotheropeningbalance;
                annualData.complaints.comtotalopeningbalance = dataList[0].complaints.comcvcopeningbalance + dataList[0].complaints.comotheropeningbalance;

                annualData.complaints.comtotalreceived_during_quarter = annualData.complaints.comcvcreceived_during_quarter + annualData.complaints.comotherreceived_during_quarter;
                annualData.complaints.comcvctotal = annualData.complaints.comcvcopeningbalance + annualData.complaints.comcvcreceived_during_quarter;
                annualData.complaints.comothertotal = annualData.complaints.comotheropeningbalance + annualData.complaints.comotherreceived_during_quarter;

                annualData.complaints.comtotal = annualData.complaints.comcvctotal + annualData.complaints.comothertotal;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.comcvcreceived_during_quarter += dataList[i].complaints.comcvcreceived_during_quarter;
                    annualData.complaints.comotherreceived_during_quarter += dataList[i].complaints.comotherreceived_during_quarter;
                    annualData.complaints.comcvcdisposal_during_quarter += dataList[i].complaints.comcvcdisposal_during_quarter;
                    annualData.complaints.comotherdisposal_during_quarter += dataList[i].complaints.comotherdisposal_during_quarter;
                }

                annualData.complaints.comtotaldisposal_during_quarter = annualData.complaints.comcvcdisposal_during_quarter + annualData.complaints.comotherdisposal_during_quarter;

                annualData.complaints.comcvcbalance_pending = dataList[3].complaints.comcvcbalance_pending;
                annualData.complaints.comotherbalance_pending = dataList[3].complaints.comotherbalance_pending;
                annualData.complaints.comtotalbalance_pending = annualData.complaints.comcvcbalance_pending + annualData.complaints.comotherbalance_pending;

                annualData.complaints.comcvcage_less_one = dataList[3].complaints.comcvcage_less_one;
                annualData.complaints.comcvcage_pendency_betweenone_three = dataList[3].complaints.comcvcage_pendency_betweenone_three;
                annualData.complaints.comcvcage_pendency_betweenthree_six = dataList[3].complaints.comcvcage_pendency_betweenthree_six;

                annualData.complaints.comotherage_less_one = dataList[3].complaints.comotherage_less_one;
                annualData.complaints.comotherage_pendency_betweenone_three = dataList[3].complaints.comotherage_pendency_betweenone_three;
                annualData.complaints.comotherage_pendency_betweenthree_six = dataList[3].complaints.comotherage_pendency_betweenthree_six;

                annualData.complaints.comtotalage_less_one = annualData.complaints.comcvcage_less_one + annualData.complaints.comotherage_less_one;
                annualData.complaints.comtotalage_pendency_betweenone_three = annualData.complaints.comcvcage_pendency_betweenone_three + annualData.complaints.comotherage_pendency_betweenone_three;
                annualData.complaints.comtotalage_pendency_betweenthree_six = annualData.complaints.comcvcage_pendency_betweenthree_six + annualData.complaints.comotherage_pendency_betweenthree_six;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.complaints_signed_complaints += dataList[i].complaints.complaints_signed_complaints;
                    annualData.complaints.complaints_department += dataList[i].complaints.complaints_department;
                    annualData.complaints.complaints_cbi += dataList[i].complaints.complaints_cbi;
                    annualData.complaints.complaints_other += dataList[i].complaints.complaints_other;
                    annualData.complaints.complaints_audit += dataList[i].complaints.complaints_audit;
                    annualData.complaints.complaints_detected += dataList[i].complaints.complaints_detected;
                    annualData.complaints.complaints_cbi_action += dataList[i].complaints.complaints_cbi_action;
                    annualData.complaints.complaints_cvoinvestigation += dataList[i].complaints.complaints_cvoinvestigation;
                    annualData.complaints.complaints_signed_filed += dataList[i].complaints.complaints_signed_filed;
                    annualData.complaints.complaints_signed_notconfirmed += dataList[i].complaints.complaints_signed_notconfirmed;
                    annualData.complaints.complaints_actionfornovigilance += dataList[i].complaints.complaints_actionfornovigilance;
                }
                annualData.complaints.cvcpidpi_opening_balance = dataList[0].complaints.cvcpidpi_opening_balance;
                annualData.complaints.otherpidpi_opening_balance = dataList[0].complaints.otherpidpi_opening_balance;
                annualData.complaints.totalpidpi_opening_balance = annualData.complaints.cvcpidpi_opening_balance + annualData.complaints.otherpidpi_opening_balance;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.cvcpidpi_receivequarter += dataList[i].complaints.cvcpidpi_receivequarter;
                    annualData.complaints.otherpidpi_receivequarter += dataList[i].complaints.otherpidpi_receivequarter;
                }
                annualData.complaints.totalpidpi_receivequarter = annualData.complaints.cvcpidpi_receivequarter + annualData.complaints.otherpidpi_receivequarter;

                annualData.complaints.cvcpidpi_total = annualData.complaints.cvcpidpi_opening_balance + annualData.complaints.cvcpidpi_receivequarter;
                annualData.complaints.otherpidpi_total = annualData.complaints.otherpidpi_opening_balance + annualData.complaints.otherpidpi_receivequarter;
                annualData.complaints.totalpidpi_total = annualData.complaints.cvcpidpi_total + annualData.complaints.otherpidpi_total;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.cvcpidpi_report_quarter += dataList[i].complaints.cvcpidpi_report_quarter;
                    annualData.complaints.otherpidpi_report_quarter += dataList[i].complaints.otherpidpi_report_quarter;
                }
                annualData.complaints.totalpidpi_report_quarter = annualData.complaints.cvcpidpi_report_quarter + annualData.complaints.otherpidpi_report_quarter;

                annualData.complaints.cvcpidpi_balance_pending = dataList[3].complaints.cvcpidpi_balance_pending;
                annualData.complaints.otherpidpi_balance_pending = dataList[3].complaints.otherpidpi_balance_pending;
                annualData.complaints.totalpidpi_balance_pending = dataList[3].complaints.totalpidpi_balance_pending;
                annualData.complaints.cvcpidpi_three_month = dataList[3].complaints.cvcpidpi_three_month;
                annualData.complaints.cvcpidpi_three_to_six = dataList[3].complaints.cvcpidpi_three_to_six;
                annualData.complaints.cvcpidpi_less_six = dataList[3].complaints.cvcpidpi_less_six;
                annualData.complaints.cvcpidpi_delay_one_month = dataList[3].complaints.cvcpidpi_delay_one_month;
                annualData.complaints.otherpidpi_three_month = dataList[3].complaints.otherpidpi_three_month;
                annualData.complaints.otherpidpi_three_to_six = dataList[3].complaints.otherpidpi_three_to_six;
                annualData.complaints.otherpidpi_less_six = dataList[3].complaints.otherpidpi_less_six;
                annualData.complaints.otherpidpi_delay_one_month = dataList[3].complaints.otherpidpi_delay_one_month;

                annualData.complaints.toatlpidpi_three_month = annualData.complaints.cvcpidpi_three_month + annualData.complaints.otherpidpi_three_month;
                annualData.complaints.toatlpidpi_three_to_six = annualData.complaints.cvcpidpi_three_to_six + annualData.complaints.otherpidpi_three_to_six;
                annualData.complaints.totalpidpi_less_six = annualData.complaints.cvcpidpi_less_six + annualData.complaints.otherpidpi_less_six;

                annualData.complaints.toatlpidpi_delay_one_month = dataList[3].complaints.toatlpidpi_delay_one_month;

                annualData.complaints.cvcpidpiinvestadvicecvc = dataList[0].complaints.cvcpidpiinvestadvicecvc;
                annualData.complaints.cvopidpiinvestadvicecvc = dataList[0].complaints.cvopidpiinvestadvicecvc;
                annualData.complaints.totalpidpiinvestadvicecvc = annualData.complaints.cvcpidpiinvestadvicecvc + annualData.complaints.cvopidpiinvestadvicecvc;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.cvcpidpiinvesquarterreportbycvo += dataList[i].complaints.cvcpidpiinvesquarterreportbycvo;
                    annualData.complaints.cvopidpiinvesquarterreportbycvo += dataList[i].complaints.cvopidpiinvesquarterreportbycvo;
                    annualData.complaints.cvcpidpiinvesquarteradvicereceive += dataList[i].complaints.cvcpidpiinvesquarteradvicereceive;
                    annualData.complaints.cvopidpiinvesquarteradvicereceive += dataList[i].complaints.cvopidpiinvesquarteradvicereceive;
                }
                annualData.complaints.totalpidpiinvesquarterreportbycvo = annualData.complaints.cvcpidpiinvesquarterreportbycvo + annualData.complaints.cvopidpiinvesquarterreportbycvo;
                annualData.complaints.totalpidpiinvesquarteradvicereceive = annualData.complaints.cvcpidpiinvesquarteradvicereceive + annualData.complaints.cvopidpiinvesquarteradvicereceive;

                annualData.complaints.cvcpidpiinvestotaladvicereceive = annualData.complaints.cvcpidpiinvestadvicecvc + annualData.complaints.cvcpidpiinvesquarteradvicereceive;
                annualData.complaints.cvopidpiinvestotaladvicereceive = annualData.complaints.cvopidpiinvestotaladvicereceive + annualData.complaints.cvopidpiinvestotaladvicereceive;
                annualData.complaints.totalpidpiinvestotaladvicereceive = annualData.complaints.cvcpidpiinvestotaladvicereceive + annualData.complaints.cvopidpiinvestotaladvicereceive;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.cvcpidpiinvesactionduringquarter += dataList[i].complaints.cvcpidpiinvesactionduringquarter;
                    annualData.complaints.cvopidpiinvesactionduringquarter += dataList[i].complaints.cvopidpiinvesactionduringquarter;
                }
                annualData.complaints.totalpidpiinvesactionduringquarter = annualData.complaints.cvcpidpiinvesactionduringquarter + annualData.complaints.cvopidpiinvesactionduringquarter;
                annualData.complaints.cvcpidpiinvesbalancegreterthreemonth = dataList[3].complaints.cvcpidpiinvesbalancegreterthreemonth;
                annualData.complaints.cvcpidpiinvesbalancebetweenthreetosix = dataList[3].complaints.cvcpidpiinvesbalancebetweenthreetosix;
                annualData.complaints.cvcpidpiinvesbalancebetweensixtotwelve = dataList[3].complaints.cvcpidpiinvesbalancebetweensixtotwelve;
                annualData.complaints.cvcpidpiinvesbalancelesstwelve = dataList[3].complaints.cvcpidpiinvesbalancelesstwelve;

                annualData.complaints.cvopidpiinvesbalancegreterthreemonth = dataList[3].complaints.cvopidpiinvesbalancegreterthreemonth;
                annualData.complaints.cvopidpiinvesbalancebetweenthreetosix = dataList[3].complaints.cvopidpiinvesbalancebetweenthreetosix;
                annualData.complaints.cvopidpiinvesbalancebetweensixtotwelve = dataList[3].complaints.cvopidpiinvesbalancebetweensixtotwelve;
                annualData.complaints.cvopidpiinvesbalancelesstwelve = dataList[3].complaints.cvopidpiinvesbalancelesstwelve;

                annualData.complaints.totalpidpiinvesbalancegreterthreemonth = annualData.complaints.cvcpidpiinvesbalancegreterthreemonth + annualData.complaints.cvopidpiinvesbalancegreterthreemonth;
                annualData.complaints.totalpidpiinvesbalancebetweenthreetosix = annualData.complaints.cvcpidpiinvesbalancebetweenthreetosix + annualData.complaints.cvopidpiinvesbalancebetweenthreetosix;
                annualData.complaints.totalpidpiinvesbalancebetweensixtotwelve = annualData.complaints.cvcpidpiinvesbalancebetweensixtotwelve + annualData.complaints.cvopidpiinvesbalancebetweensixtotwelve;
                annualData.complaints.totalpidpiinvesbalancelesstwelve = annualData.complaints.cvcpidpiinvesbalancelesstwelve + annualData.complaints.cvopidpiinvesbalancelesstwelve;

                annualData.complaints.napidpibroughtforward = dataList[0].complaints.napidpibroughtforward;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.napidpireceiveqtr += dataList[i].complaints.napidpireceiveqtr;
                }
                annualData.complaints.napidpitotal = annualData.complaints.napidpibroughtforward + annualData.complaints.napidpireceiveqtr;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.napidpiclosedwithoutaction += dataList[i].complaints.napidpiclosedwithoutaction;
                    annualData.complaints.napidpifurtherinvestigation += dataList[i].complaints.napidpifurtherinvestigation;
                    annualData.complaints.napidpiadminaction += dataList[i].complaints.napidpiadminaction;
                    annualData.complaints.napidpiimpositionpenalty += dataList[i].complaints.napidpiimpositionpenalty;
                }
                annualData.complaints.napidpidisposedqtr = annualData.complaints.napidpiclosedwithoutaction + annualData.complaints.napidpiadminaction + annualData.complaints.napidpiimpositionpenalty;
                annualData.complaints.napidpipendingqtr = dataList[3].complaints.napidpipendingqtr;

                annualData.complaints.scrutinyreportbfpreviousyear = dataList[0].complaints.scrutinyreportbfpreviousyear;
                annualData.complaints.scrutinyreportbfpreviousyearconcurrent = dataList[0].complaints.scrutinyreportbfpreviousyearconcurrent;
                annualData.complaints.scrutinyreportbfpreviousyearinternal = dataList[0].complaints.scrutinyreportbfpreviousyearinternal;
                annualData.complaints.scrutinyreportbfpreviousyearstatutory = dataList[0].complaints.scrutinyreportbfpreviousyearstatutory;
                annualData.complaints.scrutinyreportbfpreviousyearothers = dataList[0].complaints.scrutinyreportbfpreviousyearothers;
                annualData.complaints.scrutinyreportbfpreviousyeartotal = annualData.complaints.scrutinyreportbfpreviousyear + annualData.complaints.scrutinyreportbfpreviousyearconcurrent + annualData.complaints.scrutinyreportbfpreviousyearinternal + annualData.complaints.scrutinyreportbfpreviousyearstatutory + annualData.complaints.scrutinyreportbfpreviousyearothers;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.scrutinyreportexaminedqtr += dataList[i].complaints.scrutinyreportexaminedqtr;
                    annualData.complaints.scrutinyreportidentifiedexami += dataList[i].complaints.scrutinyreportidentifiedexami;
                    annualData.complaints.scrutinyreportcompleteinvestigation += dataList[i].complaints.scrutinyreportcompleteinvestigation;

                    annualData.complaints.scrutinyreportexaminedqtrconcurrent += dataList[i].complaints.scrutinyreportexaminedqtrconcurrent;
                    annualData.complaints.scrutinyreportidentifiedexamiconcurrent += dataList[i].complaints.scrutinyreportidentifiedexamiconcurrent;
                    annualData.complaints.scrutinyreportcompleteinvestigationconcurrent += dataList[i].complaints.scrutinyreportcompleteinvestigationconcurrent;
                    annualData.complaints.scrutinyreportexaminedqtrinternal += dataList[i].complaints.scrutinyreportexaminedqtrinternal;
                    annualData.complaints.scrutinyreportidentifiedexamiinternal += dataList[i].complaints.scrutinyreportidentifiedexamiinternal;
                    annualData.complaints.scrutinyreportcompleteinvestigationinternal += dataList[i].complaints.scrutinyreportcompleteinvestigationinternal;
                    annualData.complaints.scrutinyreportexaminedqtrstatutory += dataList[i].complaints.scrutinyreportexaminedqtrstatutory;
                    annualData.complaints.scrutinyreportidentifiedexamistatutory += dataList[i].complaints.scrutinyreportidentifiedexamistatutory;
                    annualData.complaints.scrutinyreportcompleteinvestigationstatutory += dataList[i].complaints.scrutinyreportcompleteinvestigationstatutory;
                    annualData.complaints.scrutinyreportexaminedqtrothers += dataList[i].complaints.scrutinyreportexaminedqtrothers;
                    annualData.complaints.scrutinyreportidentifiedexamiothers += dataList[i].complaints.scrutinyreportidentifiedexamiothers;
                    annualData.complaints.scrutinyreportcompleteinvestigationothers += dataList[i].complaints.scrutinyreportcompleteinvestigationothers;
                }
                annualData.complaints.scrutinyreportexaminedqtrtotal = annualData.complaints.scrutinyreportexaminedqtr + annualData.complaints.scrutinyreportexaminedqtrconcurrent + annualData.complaints.scrutinyreportexaminedqtrinternal + annualData.complaints.scrutinyreportexaminedqtrstatutory + annualData.complaints.scrutinyreportexaminedqtrothers;
                annualData.complaints.scrutinyreportidentifiedexamitotal = annualData.complaints.scrutinyreportidentifiedexami + annualData.complaints.scrutinyreportidentifiedexamiconcurrent + annualData.complaints.scrutinyreportidentifiedexamiinternal + annualData.complaints.scrutinyreportidentifiedexamiothers + annualData.complaints.scrutinyreportidentifiedexamistatutory;
                annualData.complaints.scrutinyreportcompleteinvestigationtotal = annualData.complaints.scrutinyreportcompleteinvestigation + annualData.complaints.scrutinyreportcompleteinvestigationconcurrent + annualData.complaints.scrutinyreportcompleteinvestigationinternal + annualData.complaints.scrutinyreportcompleteinvestigationothers + annualData.complaints.scrutinyreportcompleteinvestigationstatutory;

                annualData.complaints.scrutinyreportpendinginvestigation = dataList[3].complaints.scrutinyreportpendinginvestigation;
                annualData.complaints.scrutinyreportpendinginvestigationconcurrent = dataList[3].complaints.scrutinyreportpendinginvestigationconcurrent;
                annualData.complaints.scrutinyreportpendinginvestigationinternal = dataList[3].complaints.scrutinyreportpendinginvestigationinternal;
                annualData.complaints.scrutinyreportpendinginvestigationstatutory = dataList[3].complaints.scrutinyreportpendinginvestigationstatutory;
                annualData.complaints.scrutinyreportpendinginvestigationothers = dataList[3].complaints.scrutinyreportpendinginvestigationothers;
                annualData.complaints.scrutinyreportpendinginvestigationtotal = annualData.complaints.scrutinyreportpendinginvestigation + annualData.complaints.scrutinyreportpendinginvestigationconcurrent + annualData.complaints.scrutinyreportpendinginvestigationinternal + annualData.complaints.scrutinyreportpendinginvestigationstatutory + annualData.complaints.scrutinyreportpendinginvestigationothers;

                #endregion
                // complaints ends

                // vigilance investigation starts
                #region vigilance investigation 

                annualData.vigInvestigation.viginvescvcopeningbalance = dataList[0].vigInvestigation.viginvescvcopeningbalance;
                annualData.vigInvestigation.viginvescvoopeningbalance = dataList[0].vigInvestigation.viginvescvoopeningbalance;
                annualData.vigInvestigation.viginvestotalopeningbalance = annualData.vigInvestigation.viginvescvcopeningbalance + annualData.vigInvestigation.viginvescvoopeningbalance;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.vigInvestigation.viginvescvctakeninvesqtr += dataList[i].vigInvestigation.viginvescvctakeninvesqtr;
                    annualData.vigInvestigation.viginvescvotakeninvesqtr += dataList[i].vigInvestigation.viginvescvotakeninvesqtr;

                }
                annualData.vigInvestigation.viginvestotaltakeninvesqtr = annualData.vigInvestigation.viginvescvctakeninvesqtr + annualData.vigInvestigation.viginvescvotakeninvesqtr;

                annualData.vigInvestigation.viginvescvctotal = annualData.vigInvestigation.viginvescvcopeningbalance + annualData.vigInvestigation.viginvescvctakeninvesqtr;
                annualData.vigInvestigation.viginvescvototal = annualData.vigInvestigation.viginvescvoopeningbalance + annualData.vigInvestigation.viginvescvotakeninvesqtr;
                annualData.vigInvestigation.viginvestotal = annualData.vigInvestigation.viginvescvctotal + annualData.vigInvestigation.viginvescvototal;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.vigInvestigation.viginvescvcreportsrecivqtr += dataList[i].vigInvestigation.viginvescvcreportsrecivqtr;
                    annualData.vigInvestigation.viginvescvoreportsrecivqtr += dataList[i].vigInvestigation.viginvescvoreportsrecivqtr;
                }

                annualData.vigInvestigation.viginvestotalreportsrecivqtr = annualData.vigInvestigation.viginvescvcreportsrecivqtr + annualData.vigInvestigation.viginvescvoreportsrecivqtr;

                annualData.vigInvestigation.viginvescvcbalancepending = dataList[3].vigInvestigation.viginvescvcbalancepending;
                annualData.vigInvestigation.viginvescvobalancepending = dataList[3].vigInvestigation.viginvescvobalancepending;
                annualData.vigInvestigation.viginvestotalbalancepending = annualData.vigInvestigation.viginvescvcbalancepending + annualData.vigInvestigation.viginvescvobalancepending;

                annualData.vigInvestigation.viginvescvcgreaterthree = dataList[3].vigInvestigation.viginvescvcgreaterthree;
                annualData.vigInvestigation.viginvescvcthreetosix = dataList[3].vigInvestigation.viginvescvcthreetosix;
                annualData.vigInvestigation.viginvescvcsixtotwelve = dataList[3].vigInvestigation.viginvescvcsixtotwelve;
                annualData.vigInvestigation.viginvescvclesstwelve = dataList[3].vigInvestigation.viginvescvclesstwelve;

                annualData.vigInvestigation.viginvescvogreaterthree = dataList[3].vigInvestigation.viginvescvogreaterthree;
                annualData.vigInvestigation.viginvescvothreetosix = dataList[3].vigInvestigation.viginvescvothreetosix;
                annualData.vigInvestigation.viginvescvosixtotwelve = dataList[3].vigInvestigation.viginvescvosixtotwelve;
                annualData.vigInvestigation.viginvescvolesstwelve = dataList[3].vigInvestigation.viginvescvolesstwelve;

                annualData.vigInvestigation.viginvestotalgreaterthree = dataList[3].vigInvestigation.viginvestotalgreaterthree;
                annualData.vigInvestigation.viginvestotalthreetosix = dataList[3].vigInvestigation.viginvestotalthreetosix;
                annualData.vigInvestigation.viginvestotalsixtotwelve = dataList[3].vigInvestigation.viginvestotalsixtotwelve;
                annualData.vigInvestigation.viginvestotallesstwelve = dataList[3].vigInvestigation.viginvestotallesstwelve;

                annualData.vigInvestigation.viginvespendingopeningpending = dataList[0].vigInvestigation.viginvespendingopeningpending;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.vigInvestigation.viginvespendinginvestqtr += dataList[0].vigInvestigation.viginvespendinginvestqtr;
                    annualData.vigInvestigation.viginvespendingreportio += dataList[0].vigInvestigation.viginvespendingreportio;
                }

                annualData.vigInvestigation.viginvespendingtotal = annualData.vigInvestigation.viginvespendingopeningpending + annualData.vigInvestigation.viginvespendinginvestqtr;
                annualData.vigInvestigation.viginvespendingbalancepending = dataList[3].vigInvestigation.viginvespendingbalancepending;
                annualData.vigInvestigation.viginvespendinggreaterthree = dataList[3].vigInvestigation.viginvespendinggreaterthree;
                annualData.vigInvestigation.viginvespendingthreetosix = dataList[3].vigInvestigation.viginvespendingthreetosix;
                annualData.vigInvestigation.viginvespendingsixtotwelve = dataList[3].vigInvestigation.viginvespendingsixtotwelve;
                annualData.vigInvestigation.viginvespendinglesstwelve = dataList[3].vigInvestigation.viginvespendinglesstwelve;

                annualData.vigInvestigation.viginvestacbireportqtr = dataList[0].vigInvestigation.viginvestacbireportqtr;
                annualData.vigInvestigation.viginvestacvoreportqtr = dataList[0].vigInvestigation.viginvestacvoreportqtr;
                annualData.vigInvestigation.viginvestatotalreportqtr = annualData.vigInvestigation.viginvestacbireportqtr + annualData.vigInvestigation.viginvestacvoreportqtr;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.vigInvestigation.viginvestacbireceiveqtr += dataList[0].vigInvestigation.viginvestacbireceiveqtr;
                    annualData.vigInvestigation.viginvestacvoreceiveqtr += dataList[0].vigInvestigation.viginvestacvoreceiveqtr;
                }

                annualData.vigInvestigation.viginvestatotalreceiveqtr = annualData.vigInvestigation.viginvestacbireceiveqtr + annualData.vigInvestigation.viginvestacvoreceiveqtr;

                annualData.vigInvestigation.viginvestacbitotal = annualData.vigInvestigation.viginvestacbireportqtr + annualData.vigInvestigation.viginvestacbireceiveqtr;
                annualData.vigInvestigation.viginvestacvototal = annualData.vigInvestigation.viginvestacvoreportqtr + annualData.vigInvestigation.viginvestacvoreceiveqtr;
                annualData.vigInvestigation.viginvestatotal = annualData.vigInvestigation.viginvestacbitotal + annualData.vigInvestigation.viginvestacvototal;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.vigInvestigation.viginvestacbidisposedqtr += dataList[i].vigInvestigation.viginvestacbidisposedqtr;
                    annualData.vigInvestigation.viginvestacvodisposedqtr += dataList[i].vigInvestigation.viginvestacvodisposedqtr;
                }
                annualData.vigInvestigation.viginvestatotaldisposedqtr = annualData.vigInvestigation.viginvestacbidisposedqtr + annualData.vigInvestigation.viginvestacvodisposedqtr;

                annualData.vigInvestigation.viginvestacbibalancepending = dataList[3].vigInvestigation.viginvestacbibalancepending;
                annualData.vigInvestigation.viginvestacbigreaterthree = dataList[3].vigInvestigation.viginvestacbigreaterthree;
                annualData.vigInvestigation.viginvestacbithreetosix = dataList[3].vigInvestigation.viginvestacbithreetosix;
                annualData.vigInvestigation.viginvestacbisixtotwelve = dataList[3].vigInvestigation.viginvestacbisixtotwelve;
                annualData.vigInvestigation.viginvestacbilesstwelve = dataList[3].vigInvestigation.viginvestacbilesstwelve;

                annualData.vigInvestigation.viginvestacvobalancepending = dataList[3].vigInvestigation.viginvestacvobalancepending;
                annualData.vigInvestigation.viginvestacvogreaterthree = dataList[3].vigInvestigation.viginvestacvogreaterthree;
                annualData.vigInvestigation.viginvestacvothreetosix = dataList[3].vigInvestigation.viginvestacvothreetosix;
                annualData.vigInvestigation.viginvestacvosixtotwelve = dataList[3].vigInvestigation.viginvestacvosixtotwelve;
                annualData.vigInvestigation.viginvestacvolesstwelve = dataList[3].vigInvestigation.viginvestacvolesstwelve;

                annualData.vigInvestigation.viginvestatotalbalancepending = dataList[3].vigInvestigation.viginvestatotalbalancepending;
                annualData.vigInvestigation.viginvestatotalgreaterthree = dataList[3].vigInvestigation.viginvestatotalgreaterthree;
                annualData.vigInvestigation.viginvestatotalthreetosix = dataList[3].vigInvestigation.viginvestatotalthreetosix;
                annualData.vigInvestigation.viginvestatotalsixtotwelve = dataList[3].vigInvestigation.viginvestatotalsixtotwelve;
                annualData.vigInvestigation.viginvestatotallesstwelve = dataList[3].vigInvestigation.viginvestatotallesstwelve;


                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.vigInvestigation.viginvestardamajorpenalty += dataList[i].vigInvestigation.viginvestardamajorpenalty;
                    annualData.vigInvestigation.viginvestardaminorpenalty += dataList[i].vigInvestigation.viginvestardaminorpenalty;
                    annualData.vigInvestigation.viginvestadecisionadminaction += dataList[i].vigInvestigation.viginvestadecisionadminaction;
                    annualData.vigInvestigation.viginvestadecisionclosurecase += dataList[i].vigInvestigation.viginvestadecisionclosurecase;
                }
                annualData.vigInvestigation.viginvestbcbireportbfqtr = dataList[0].vigInvestigation.viginvestbcbireportbfqtr;
                annualData.vigInvestigation.viginvestbcvoreportbfqtr = dataList[0].vigInvestigation.viginvestbcvoreportbfqtr;
                annualData.vigInvestigation.viginvestbtotalreportbfqtr = annualData.vigInvestigation.viginvestbcbireportbfqtr + annualData.vigInvestigation.viginvestbcvoreportbfqtr;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.vigInvestigation.viginvestbcbireceiveqtr += dataList[i].vigInvestigation.viginvestbcbireceiveqtr;
                    annualData.vigInvestigation.viginvestbcvoreceiveqtr += dataList[i].vigInvestigation.viginvestbcvoreceiveqtr;
                }
                annualData.vigInvestigation.viginvestbtotalreceiveqtr = annualData.vigInvestigation.viginvestbcbireceiveqtr + annualData.vigInvestigation.viginvestbcvoreceiveqtr;


                annualData.vigInvestigation.viginvestbcbitotal = annualData.vigInvestigation.viginvestbcbireportbfqtr + annualData.vigInvestigation.viginvestbcbireceiveqtr;
                annualData.vigInvestigation.viginvestbcvototal = annualData.vigInvestigation.viginvestbcvoreportbfqtr + annualData.vigInvestigation.viginvestbcvoreceiveqtr;
                annualData.vigInvestigation.viginvestbtotal = annualData.vigInvestigation.viginvestbcbitotal + annualData.vigInvestigation.viginvestbcvototal;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.vigInvestigation.viginvestbcbisentdaaction += dataList[i].vigInvestigation.viginvestbcbisentdaaction;
                    annualData.vigInvestigation.viginvestbcvosentdaaction += dataList[i].vigInvestigation.viginvestbcvosentdaaction;
                    annualData.vigInvestigation.viginvestbcbidisposedqtr += dataList[i].vigInvestigation.viginvestbcbidisposedqtr;
                    annualData.vigInvestigation.viginvestbcvodisposedqtr += dataList[i].vigInvestigation.viginvestbcvodisposedqtr;
                }

                annualData.vigInvestigation.viginvestbtotalsentdaaction = annualData.vigInvestigation.viginvestbcbisentdaaction + annualData.vigInvestigation.viginvestbcvosentdaaction;
                annualData.vigInvestigation.viginvestbtotaldisposedqtr = annualData.vigInvestigation.viginvestbcbidisposedqtr + annualData.vigInvestigation.viginvestbcvodisposedqtr;

                annualData.vigInvestigation.viginvestbcbibalancepending = dataList[3].vigInvestigation.viginvestbcbibalancepending;
                annualData.vigInvestigation.viginvestbcbigreaterthree = dataList[3].vigInvestigation.viginvestbcbigreaterthree;
                annualData.vigInvestigation.viginvestbcbithreetosix = dataList[3].vigInvestigation.viginvestbcbithreetosix;
                annualData.vigInvestigation.viginvestbcbisixtotwelve = dataList[3].vigInvestigation.viginvestbcbisixtotwelve;
                annualData.vigInvestigation.viginvestbcbilesstwelve = dataList[3].vigInvestigation.viginvestbcbilesstwelve;

                annualData.vigInvestigation.viginvestbcvobalancepending = dataList[3].vigInvestigation.viginvestbcvobalancepending; ;
                annualData.vigInvestigation.viginvestbcvogreaterthree = dataList[3].vigInvestigation.viginvestbcvogreaterthree;
                annualData.vigInvestigation.viginvestbcvothreetosix = dataList[3].vigInvestigation.viginvestbcvothreetosix;
                annualData.vigInvestigation.viginvestbcvosixtotwelve = dataList[3].vigInvestigation.viginvestbcvosixtotwelve;
                annualData.vigInvestigation.viginvestbcvolesstwelve = dataList[3].vigInvestigation.viginvestbcvolesstwelve;

                annualData.vigInvestigation.viginvestbtotalbalancepending = annualData.vigInvestigation.viginvestbcbibalancepending + annualData.vigInvestigation.viginvestbcvobalancepending;
                annualData.vigInvestigation.viginvestbtotalgreaterthree = annualData.vigInvestigation.viginvestbcbigreaterthree + annualData.vigInvestigation.viginvestbcvogreaterthree;
                annualData.vigInvestigation.viginvestbtotalthreetosix = annualData.vigInvestigation.viginvestbcbithreetosix + annualData.vigInvestigation.viginvestbcvothreetosix;
                annualData.vigInvestigation.viginvestbtotalsixtotwelve = annualData.vigInvestigation.viginvestbcbisixtotwelve + annualData.vigInvestigation.viginvestbcvosixtotwelve;
                annualData.vigInvestigation.viginvestbtotallesstwelve = annualData.vigInvestigation.viginvestbcbilesstwelve + annualData.vigInvestigation.viginvestbcvolesstwelve;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.vigInvestigation.viginvestbrdamajorpenalty += dataList[i].vigInvestigation.viginvestbrdamajorpenalty;
                    annualData.vigInvestigation.viginvestbrdaminorpenalty += dataList[i].vigInvestigation.viginvestbrdaminorpenalty;
                    annualData.vigInvestigation.viginvestbdecisionadminaction += dataList[i].vigInvestigation.viginvestbdecisionadminaction;
                    annualData.vigInvestigation.viginvestbdecisionclosurecase += dataList[i].vigInvestigation.viginvestbdecisionclosurecase;

                    annualData.vigInvestigation.viginvestreportbycvoqtr += dataList[i].vigInvestigation.viginvestreportbycvoqtr;
                    annualData.vigInvestigation.viginvestmajorpp += dataList[i].vigInvestigation.viginvestmajorpp;
                    annualData.vigInvestigation.viginvestminorpp += dataList[i].vigInvestigation.viginvestminorpp;
                    annualData.vigInvestigation.viginvestotheradmnaction += dataList[i].vigInvestigation.viginvestotheradmnaction;
                    annualData.vigInvestigation.viginvestclosure += dataList[i].vigInvestigation.viginvestclosure;

                    annualData.vigInvestigation.viginvestreskgroupcmajorpp += dataList[i].vigInvestigation.viginvestreskgroupcmajorpp;
                    annualData.vigInvestigation.viginvestreskgroupcminorpp += dataList[i].vigInvestigation.viginvestreskgroupcminorpp;
                    annualData.vigInvestigation.viginvestreskgroupcotheraction += dataList[i].vigInvestigation.viginvestreskgroupcotheraction;
                    annualData.vigInvestigation.viginvestreskgroupcclosure += dataList[i].vigInvestigation.viginvestreskgroupcclosure;

                    annualData.vigInvestigation.viginvestreskgroupbmajorpp += dataList[i].vigInvestigation.viginvestreskgroupbmajorpp;
                    annualData.vigInvestigation.viginvestreskgroupbminorpp += dataList[i].vigInvestigation.viginvestreskgroupbminorpp;
                    annualData.vigInvestigation.viginvestreskgroupbotheraction += dataList[i].vigInvestigation.viginvestreskgroupbotheraction;
                    annualData.vigInvestigation.viginvestreskgroupbclosure += dataList[i].vigInvestigation.viginvestreskgroupbclosure;

                    annualData.vigInvestigation.viginvestreskgroupamajorpp += dataList[i].vigInvestigation.viginvestreskgroupamajorpp;
                    annualData.vigInvestigation.viginvestreskgroupaminorpp += dataList[i].vigInvestigation.viginvestreskgroupaminorpp;
                    annualData.vigInvestigation.viginvestreskgroupaotheraction += dataList[i].vigInvestigation.viginvestreskgroupaotheraction;
                    annualData.vigInvestigation.viginvestreskgroupaclosure += dataList[i].vigInvestigation.viginvestreskgroupaclosure;

                    annualData.vigInvestigation.viginvestreskgroupjsmajorpp += dataList[i].vigInvestigation.viginvestreskgroupjsmajorpp;
                    annualData.vigInvestigation.viginvestreskgroupjsminorpp += dataList[i].vigInvestigation.viginvestreskgroupjsminorpp;
                    annualData.vigInvestigation.viginvestreskgroupjsotheraction += dataList[i].vigInvestigation.viginvestreskgroupjsotheraction;
                    annualData.vigInvestigation.viginvestreskgroupjsclosure += dataList[i].vigInvestigation.viginvestreskgroupjsclosure;
                }

                annualData.vigInvestigation.viginvestreskgroupctotal = annualData.vigInvestigation.viginvestreskgroupcmajorpp + annualData.vigInvestigation.viginvestreskgroupcminorpp + annualData.vigInvestigation.viginvestreskgroupcotheraction + annualData.vigInvestigation.viginvestreskgroupcclosure;
                annualData.vigInvestigation.viginvestreskgroupbtotal = annualData.vigInvestigation.viginvestreskgroupbmajorpp + annualData.vigInvestigation.viginvestreskgroupbminorpp + annualData.vigInvestigation.viginvestreskgroupbotheraction + annualData.vigInvestigation.viginvestreskgroupbclosure;
                annualData.vigInvestigation.viginvestreskgroupatotal = annualData.vigInvestigation.viginvestreskgroupamajorpp + annualData.vigInvestigation.viginvestreskgroupaminorpp + annualData.vigInvestigation.viginvestreskgroupaotheraction + annualData.vigInvestigation.viginvestreskgroupaclosure;
                annualData.vigInvestigation.viginvestreskgroupjstotal = annualData.vigInvestigation.viginvestreskgroupjsmajorpp + annualData.vigInvestigation.viginvestreskgroupjsminorpp + annualData.vigInvestigation.viginvestreskgroupjsotheraction + annualData.vigInvestigation.viginvestreskgroupjsclosure;

                annualData.vigInvestigation.viginvestresktotalmajorpp = annualData.vigInvestigation.viginvestreskgroupcmajorpp + annualData.vigInvestigation.viginvestreskgroupbmajorpp + annualData.vigInvestigation.viginvestreskgroupamajorpp + annualData.vigInvestigation.viginvestreskgroupjsmajorpp;
                annualData.vigInvestigation.viginvestresktotalminorpp = annualData.vigInvestigation.viginvestreskgroupcminorpp + annualData.vigInvestigation.viginvestreskgroupbminorpp + annualData.vigInvestigation.viginvestreskgroupaminorpp + annualData.vigInvestigation.viginvestreskgroupjsminorpp;
                annualData.vigInvestigation.viginvestresktotalotheraction = annualData.vigInvestigation.viginvestreskgroupcotheraction + annualData.vigInvestigation.viginvestreskgroupbotheraction + annualData.vigInvestigation.viginvestreskgroupaotheraction + annualData.vigInvestigation.viginvestreskgroupjsotheraction;
                annualData.vigInvestigation.viginvestresktotalclosure = annualData.vigInvestigation.viginvestreskgroupcclosure + annualData.vigInvestigation.viginvestreskgroupbclosure + annualData.vigInvestigation.viginvestreskgroupaclosure + annualData.vigInvestigation.viginvestreskgroupjsclosure;
                annualData.vigInvestigation.viginvestresktotal = annualData.vigInvestigation.viginvestreskgroupctotal + annualData.vigInvestigation.viginvestreskgroupbtotal + annualData.vigInvestigation.viginvestreskgroupatotal + annualData.vigInvestigation.viginvestreskgroupjstotal;
                #endregion
                // vigilance investigation ends

                // Prosecution sanctions starts
                #region prosecution sanctions

                prosecutionsanctionsqrs _prosec = new prosecutionsanctionsqrs();

                _prosec.prosesanctgroupcopeningbalance = dataList[0].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupcopeningbalance;
                _prosec.prosesanctgroupbopeningbalance = dataList[0].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupbopeningbalance;
                _prosec.prosesanctgroupaopeningbalance = dataList[0].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupaopeningbalance;
                _prosec.prosesanctjsopeningbalance = dataList[0].prosecVM.Prosecutionsanctionsqrs.prosesanctjsopeningbalance;
                _prosec.prosesancttotalopeningbalance = _prosec.prosesanctgroupcopeningbalance + _prosec.prosesanctgroupbopeningbalance + _prosec.prosesanctgroupaopeningbalance + _prosec.prosesanctjsopeningbalance;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _prosec.prosesanctgroupcreciveqtr += dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupcreciveqtr;
                    _prosec.prosesanctgroupbreciveqtr += dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupbreciveqtr;
                    _prosec.prosesanctgroupareciveqtr += dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupareciveqtr;
                    _prosec.prosesanctjsreciveqtr += dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctjsreciveqtr;
                    _prosec.prosesancttotalreciveqtr += dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesancttotalreciveqtr;
                }

                _prosec.prosesanctgroupctotal = _prosec.prosesanctgroupcopeningbalance + _prosec.prosesanctgroupcreciveqtr;
                _prosec.prosesanctgroupbtotal = _prosec.prosesanctgroupbopeningbalance + _prosec.prosesanctgroupbreciveqtr;
                _prosec.prosesanctgroupatotal = _prosec.prosesanctgroupaopeningbalance + _prosec.prosesanctgroupareciveqtr;
                _prosec.prosesanctjstotal = _prosec.prosesanctjsopeningbalance + _prosec.prosesanctjsreciveqtr;
                _prosec.prosesancttotal = _prosec.prosesancttotalopeningbalance + _prosec.prosesancttotalreciveqtr;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _prosec.prosesanctgroupcsanctiongranted += dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupcsanctiongranted;
                    _prosec.prosesanctgroupbsanctiongranted += dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupbsanctiongranted;
                    _prosec.prosesanctgroupasanctiongranted += dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupasanctiongranted;
                    _prosec.prosesanctjssanctiongranted += dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctjssanctiongranted;

                    _prosec.prosesanctgroupcsanctionrefused = dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupcsanctionrefused;
                    _prosec.prosesanctgroupbsanctionrefused = dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupbsanctionrefused;
                    _prosec.prosesanctgroupasanctionrefused = dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupasanctionrefused;
                    _prosec.prosesanctjssanctionrefused = dataList[i].prosecVM.Prosecutionsanctionsqrs.prosesanctjssanctionrefused;

                }

                _prosec.prosesancttotalsanctiongranted = _prosec.prosesanctgroupcsanctiongranted + _prosec.prosesanctgroupbsanctiongranted + _prosec.prosesanctgroupasanctiongranted + _prosec.prosesanctjssanctiongranted;
                _prosec.prosesancttotalsanctionrefused = _prosec.prosesanctgroupcsanctionrefused + _prosec.prosesanctgroupbsanctionrefused + _prosec.prosesanctgroupasanctionrefused + _prosec.prosesanctjssanctionrefused;

                _prosec.prosesanctgroupcbalancepending = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupcbalancepending;
                _prosec.prosesanctgroupcgreaterthree = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupcgreaterthree;
                _prosec.prosesanctgroupcthreetosix = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupcthreetosix;
                _prosec.prosesanctgroupclesssix = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupclesssix;
                _prosec.prosesanctgroupbbalancepending = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupbbalancepending;
                _prosec.prosesanctgroupbgreaterthree = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupbgreaterthree;
                _prosec.prosesanctgroupbthreetosix = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupbthreetosix;
                _prosec.prosesanctgroupblesssix = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupblesssix;
                _prosec.prosesanctgroupabalancepending = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupabalancepending;
                _prosec.prosesanctgroupagreaterthree = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupagreaterthree;
                _prosec.prosesanctgroupathreetosix = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupathreetosix;
                _prosec.prosesanctgroupalesssix = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctgroupalesssix;

                _prosec.prosesanctjsbalancepending = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctjsbalancepending;
                _prosec.prosesanctjsgreaterthree = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctjsgreaterthree;
                _prosec.prosesanctjsthreetosix = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctjsthreetosix;
                _prosec.prosesanctjslesssix = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosesanctjslesssix;

                _prosec.prosesancttotalbalancepending = _prosec.prosesanctgroupcbalancepending + _prosec.prosesanctgroupbbalancepending + _prosec.prosesanctgroupabalancepending + _prosec.prosesanctjsbalancepending;
                _prosec.prosesancttotalgreaterthree = _prosec.prosesanctgroupcgreaterthree + _prosec.prosesanctgroupbgreaterthree + _prosec.prosesanctgroupagreaterthree + _prosec.prosesanctjsgreaterthree;
                _prosec.prosesancttotalthreetosix = _prosec.prosesanctgroupcthreetosix + _prosec.prosesanctgroupbthreetosix + _prosec.prosesanctgroupathreetosix + _prosec.prosesanctjsthreetosix;
                _prosec.prosesancttotallesssix = _prosec.prosesanctgroupclesssix + _prosec.prosesanctgroupblesssix + _prosec.prosesanctgroupalesssix + _prosec.prosesanctjslesssix;

                annualData.prosecVM.Agewisependency = dataList[3].prosecVM.Agewisependency;

                _prosec.prosevigiofficersuspension = dataList[0].prosecVM.Prosecutionsanctionsqrs.prosevigiofficersuspension;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _prosec.prosevigisuspensionordered = dataList[i].prosecVM.Prosecutionsanctionsqrs.prosevigisuspensionordered;
                    _prosec.prosevigisuspensionqtr = dataList[i].prosecVM.Prosecutionsanctionsqrs.prosevigisuspensionqtr;
                }

                _prosec.prosevigitotal = _prosec.prosevigiofficersuspension + _prosec.prosevigisuspensionordered;
                _prosec.prosevigisuspensionendqtr = dataList[3].prosecVM.Prosecutionsanctionsqrs.prosevigisuspensionendqtr;

                annualData.prosecVM.Prosecutionsanctionsqrs = _prosec;

                #endregion
                // Prosecution sanctions ends

                // Departmental Proceedings starts
                #region departmental proceedings

                departmentalproceedingsqrs _dept = new departmentalproceedingsqrs();

                _dept.departproceedingsmajor_cvc_lastqtr = dataList[0].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_cvc_lastqtr;
                _dept.departproceedingsmajor_other_lastqtr = dataList[0].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_other_lastqtr;
                _dept.departproceedingsmajor_total_lastqtr = dataList[0].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_total_lastqtr;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _dept.departproceedingsmajor_cvc_inquiries += dataList[i].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_cvc_inquiries;
                    _dept.departproceedingsmajor_other_inquiries += dataList[i].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_other_inquiries;
                }
                _dept.departproceedingsmajor_total_inquiries = _dept.departproceedingsmajor_cvc_inquiries + _dept.departproceedingsmajor_other_inquiries;

                _dept.departproceedingsmajor_cvc_total = _dept.departproceedingsmajor_cvc_lastqtr + _dept.departproceedingsmajor_cvc_inquiries;
                _dept.departproceedingsmajor_other_total = _dept.departproceedingsmajor_other_lastqtr + _dept.departproceedingsmajor_other_inquiries;
                _dept.departproceedingsmajor_total_total = _dept.departproceedingsmajor_cvc_total + _dept.departproceedingsmajor_other_total;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _dept.departproceedingsmajor_cvc_reportsio += dataList[i].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_cvc_reportsio;
                    _dept.departproceedingsmajor_other_reportsio += dataList[i].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_other_reportsio;
                }
                _dept.departproceedingsmajor_total_reportsio = _dept.departproceedingsmajor_cvc_reportsio + _dept.departproceedingsmajor_other_reportsio;

                _dept.departproceedingsmajor_cvc_enquiries = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_cvc_enquiries;
                _dept.departproceedingsmajor_other_enquiries = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_other_enquiries;
                _dept.departproceedingsmajor_total_enquiries = _dept.departproceedingsmajor_cvc_enquiries + _dept.departproceedingsmajor_other_enquiries;

                _dept.departproceedingsmajor_cvc_greatersix = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_cvc_greatersix;
                _dept.departproceedingsmajor_cvc_sixtotwelve = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_cvc_sixtotwelve;
                _dept.departproceedingsmajor_cvc_twelvetoeighteen = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_cvc_twelvetoeighteen;
                _dept.departproceedingsmajor_cvc_lesseighteen = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_cvc_lesseighteen;

                _dept.departproceedingsmajor_other_greatersix = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_other_greatersix;
                _dept.departproceedingsmajor_other_sixtotwelve = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_other_sixtotwelve;
                _dept.departproceedingsmajor_other_twelvetoeighteen = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_other_twelvetoeighteen;
                _dept.departproceedingsmajor_other_lesseighteen = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedingsmajor_other_lesseighteen;

                _dept.departproceedingsmajor_total_greatersix = _dept.departproceedingsmajor_cvc_greatersix + _dept.departproceedingsmajor_other_greatersix;
                _dept.departproceedingsmajor_total_sixtotwelve = _dept.departproceedingsmajor_cvc_sixtotwelve + _dept.departproceedingsmajor_other_sixtotwelve;
                _dept.departproceedingsmajor_total_twelvetoeighteen = _dept.departproceedingsmajor_cvc_twelvetoeighteen + _dept.departproceedingsmajor_other_twelvetoeighteen;
                _dept.departproceedingsmajor_total_lesseighteen = _dept.departproceedingsmajor_cvc_lesseighteen + _dept.departproceedingsmajor_other_lesseighteen;

                _dept.departproceedings_minor_cvc_lastqtr = dataList[0].deptVM.Departmentalproceedingsqrs.departproceedings_minor_cvc_lastqtr;
                _dept.departproceedings_minor_other_lastqtr = dataList[0].deptVM.Departmentalproceedingsqrs.departproceedings_minor_other_lastqtr;
                _dept.departproceedings_minor_total_lastqtr = _dept.departproceedings_minor_cvc_lastqtr + _dept.departproceedings_minor_other_lastqtr;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _dept.departproceedings_minor_cvc_inquiries += dataList[i].deptVM.Departmentalproceedingsqrs.departproceedings_minor_cvc_inquiries;
                    _dept.departproceedings_minor_other_inquiries += dataList[i].deptVM.Departmentalproceedingsqrs.departproceedings_minor_other_inquiries;
                }
                _dept.departproceedings_minor_total_inquiries = _dept.departproceedings_minor_cvc_inquiries + _dept.departproceedings_minor_other_inquiries;

                _dept.departproceedings_minor_cvc_total = _dept.departproceedings_minor_cvc_lastqtr + _dept.departproceedings_minor_cvc_inquiries;
                _dept.departproceedings_minor_other_total = _dept.departproceedings_minor_other_lastqtr + _dept.departproceedings_minor_other_inquiries;
                _dept.departproceedings_minor_total = _dept.departproceedings_minor_cvc_total + _dept.departproceedings_minor_other_total;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _dept.departproceedings_minor_cvc_reportsio = dataList[i].deptVM.Departmentalproceedingsqrs.departproceedings_minor_cvc_reportsio;
                    _dept.departproceedings_minor_other_reportsio = dataList[i].deptVM.Departmentalproceedingsqrs.departproceedings_minor_other_reportsio;
                }
                _dept.departproceedings_minor_total_reportsio = _dept.departproceedings_minor_cvc_reportsio + _dept.departproceedings_minor_other_reportsio;

                _dept.departproceedings_minor_cvc_enquiries = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_cvc_enquiries;
                _dept.departproceedings_minor_cvc_greatersix = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_cvc_greatersix;
                _dept.departproceedings_minor_cvc_sixtotwelve = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_cvc_sixtotwelve;
                _dept.departproceedings_minor_cvc_twelvetoeighteen = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_cvc_twelvetoeighteen;
                _dept.departproceedings_minor_cvc_lesseighteen = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_cvc_lesseighteen;

                _dept.departproceedings_minor_other_enquiries = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_other_enquiries;
                _dept.departproceedings_minor_other_greatersix = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_other_greatersix;
                _dept.departproceedings_minor_other_sixtotwelve = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_other_sixtotwelve;
                _dept.departproceedings_minor_other_twelvetoeighteen = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_other_twelvetoeighteen;
                _dept.departproceedings_minor_other_lesseighteen = dataList[3].deptVM.Departmentalproceedingsqrs.departproceedings_minor_other_lesseighteen;

                _dept.departproceedings_minor_total_enquiries = _dept.departproceedings_minor_cvc_enquiries + _dept.departproceedings_minor_other_enquiries;
                _dept.departproceedings_minor_total_greatersix = _dept.departproceedings_minor_cvc_greatersix + _dept.departproceedings_minor_other_greatersix;
                _dept.departproceedings_minor_total_sixtotwelve = _dept.departproceedings_minor_cvc_sixtotwelve + _dept.departproceedings_minor_other_sixtotwelve;
                _dept.departproceedings_minor_total_twelvetoeighteen = _dept.departproceedings_minor_cvc_twelvetoeighteen + _dept.departproceedings_minor_other_twelvetoeighteen;
                _dept.departproceedings_minor_total_lesseighteen = _dept.departproceedings_minor_cvc_lesseighteen + _dept.departproceedings_minor_other_lesseighteen;
                //_dept.AgainstChargedDto(AgainstCharged4);

                //for (int i = 0; i < dataList.Count; i++)
                //{
                //    annualData.deptVM.AgainstChargedTables.AddRange(dataList[i].deptVM.AgainstChargedTables);
                //}
                annualData.deptVM.AgainstChargedTables = dataList[3].deptVM.AgainstChargedTables;

                annualData.deptVM.Departmentalproceedingsqrs = _dept;

                #endregion
                // Departmental Proceedings ends


                // Advice of CVC starts
                #region advice of cvc
                adviceofcvcqrs _advice = new adviceofcvcqrs();

                _advice.advice_cvc_first_casespreviousqtr = dataList[0].adviceViewModel.AdviceOfCvc.advice_cvc_first_casespreviousqtr;
                _advice.advice_cvc_second_casespreviousqtr = dataList[0].adviceViewModel.AdviceOfCvc.advice_cvc_second_casespreviousqtr;
                _advice.advice_cvc_firstreconsider_casespreviousqtr = dataList[0].adviceViewModel.AdviceOfCvc.advice_cvc_firstreconsider_casespreviousqtr;
                _advice.advice_cvc_secondreconsider_casespreviousqtr = dataList[0].adviceViewModel.AdviceOfCvc.advice_cvc_secondreconsider_casespreviousqtr;
                _advice.advice_cvc_total_casespreviousqtr = _advice.advice_cvc_secondreconsider_casespreviousqtr + _advice.advice_cvc_firstreconsider_casespreviousqtr + _advice.advice_cvc_second_casespreviousqtr + _advice.advice_cvc_first_casespreviousqtr;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _advice.advice_cvc_first_casesduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.advice_cvc_first_casesduringqtr;
                    _advice.advice_cvc_second_casesduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.advice_cvc_second_casesduringqtr;
                    _advice.advice_cvc_firstreconsider_casesduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.advice_cvc_firstreconsider_casesduringqtr;
                    _advice.advice_cvc_secondreconsider_casesduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.advice_cvc_secondreconsider_casesduringqtr;

                    _advice.advice_cvc_first_adviceduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.advice_cvc_first_adviceduringqtr;
                    _advice.advice_cvc_second_adviceduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.advice_cvc_second_adviceduringqtr;
                    _advice.advice_cvc_firstreconsider_adviceduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.advice_cvc_firstreconsider_adviceduringqtr;
                    _advice.advice_cvc_secondreconsider_adviceduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.advice_cvc_secondreconsider_adviceduringqtr;

                }
                _advice.advice_cvc_total_casesduringqtr = _advice.advice_cvc_secondreconsider_casesduringqtr + _advice.advice_cvc_firstreconsider_casesduringqtr + _advice.advice_cvc_second_casesduringqtr + _advice.advice_cvc_first_casesduringqtr;
                _advice.advice_cvc_total_adviceduringqtr = _advice.advice_cvc_secondreconsider_adviceduringqtr + _advice.advice_cvc_firstreconsider_adviceduringqtr + _advice.advice_cvc_second_adviceduringqtr + _advice.advice_cvc_first_adviceduringqtr;

                _advice.advice_cvc_first_adviceawaitedcvc = dataList[3].adviceViewModel.AdviceOfCvc.advice_cvc_first_adviceawaitedcvc;
                _advice.advice_cvc_second_adviceawaitedcvc = dataList[3].adviceViewModel.AdviceOfCvc.advice_cvc_second_adviceawaitedcvc;
                _advice.advice_cvc_firstreconsider_adviceawaitedcvc = dataList[3].adviceViewModel.AdviceOfCvc.advice_cvc_firstreconsider_adviceawaitedcvc;
                _advice.advice_cvc_secondreconsider_adviceawaitedcvc = dataList[3].adviceViewModel.AdviceOfCvc.advice_cvc_secondreconsider_adviceawaitedcvc;

                _advice.advice_cvc_total_adviceawaitedcvc = _advice.advice_cvc_first_adviceawaitedcvc + _advice.advice_cvc_second_adviceawaitedcvc + _advice.advice_cvc_firstreconsider_adviceawaitedcvc + _advice.advice_cvc_secondreconsider_adviceawaitedcvc;

                _advice.action_cvc_firstmajor_openingbalance = dataList[0].adviceViewModel.AdviceOfCvc.action_cvc_firstmajor_openingbalance;
                _advice.action_cvc_firstminor_openingbalance = dataList[0].adviceViewModel.AdviceOfCvc.action_cvc_firstminor_openingbalance;
                _advice.action_cvc_secondmajor_openingbalance = dataList[0].adviceViewModel.AdviceOfCvc.action_cvc_secondmajor_openingbalance;
                _advice.action_cvc_secondminor_openingbalance = dataList[0].adviceViewModel.AdviceOfCvc.action_cvc_secondminor_openingbalance;

                _advice.action_cvc_total_openingbalance = _advice.action_cvc_firstmajor_openingbalance + _advice.action_cvc_firstminor_openingbalance + _advice.action_cvc_secondmajor_openingbalance + _advice.action_cvc_secondminor_openingbalance;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _advice.action_cvc_firstmajor_adviceduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.action_cvc_firstmajor_adviceduringqtr;
                    _advice.action_cvc_firstminor_adviceduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.action_cvc_firstminor_adviceduringqtr;
                    _advice.action_cvc_secondmajor_adviceduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.action_cvc_secondmajor_adviceduringqtr;
                    _advice.action_cvc_secondminor_adviceduringqtr += dataList[i].adviceViewModel.AdviceOfCvc.action_cvc_secondminor_adviceduringqtr;

                    _advice.action_cvc_firstmajor_disposed += dataList[i].adviceViewModel.AdviceOfCvc.action_cvc_firstmajor_disposed;
                    _advice.action_cvc_firstminor_disposed += dataList[i].adviceViewModel.AdviceOfCvc.action_cvc_firstminor_disposed;
                    _advice.action_cvc_secondmajor_disposed += dataList[i].adviceViewModel.AdviceOfCvc.action_cvc_secondmajor_disposed;
                    _advice.action_cvc_secondminor_disposed += dataList[i].adviceViewModel.AdviceOfCvc.action_cvc_secondminor_disposed;
                }

                _advice.action_cvc_total_adviceduringqtr = _advice.action_cvc_firstmajor_adviceduringqtr + _advice.action_cvc_firstminor_adviceduringqtr + _advice.action_cvc_secondmajor_adviceduringqtr + _advice.action_cvc_secondminor_adviceduringqtr;
                _advice.action_cvc_total_disposed = _advice.action_cvc_secondminor_disposed + _advice.action_cvc_secondmajor_disposed + _advice.action_cvc_firstminor_disposed + _advice.action_cvc_firstmajor_disposed;

                _advice.action_cvc_firstmajor_balancepending = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstmajor_balancepending;
                _advice.action_cvc_firstmajor_greaterone = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstmajor_greaterone;
                _advice.action_cvc_firstmajor_onetothree = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstmajor_onetothree;
                _advice.action_cvc_firstmajor_threetosix = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstmajor_threetosix;
                _advice.action_cvc_firstmajor_lesssix = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstmajor_lesssix;
                _advice.action_cvc_firstminor_balancepending = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstminor_balancepending;
                _advice.action_cvc_firstminor_greaterone = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstminor_greaterone;
                _advice.action_cvc_firstminor_onetothree = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstminor_onetothree;
                _advice.action_cvc_firstminor_threetosix = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstminor_threetosix;
                _advice.action_cvc_firstminor_lesssix = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_firstminor_lesssix;
                _advice.action_cvc_secondmajor_balancepending = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondmajor_balancepending;
                _advice.action_cvc_secondmajor_greaterone = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondmajor_greaterone;
                _advice.action_cvc_secondmajor_onetothree = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondmajor_onetothree;
                _advice.action_cvc_secondmajor_threetosix = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondmajor_threetosix;
                _advice.action_cvc_secondmajor_lesssix = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondmajor_lesssix;
                _advice.action_cvc_secondminor_balancepending = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondminor_balancepending;
                _advice.action_cvc_secondminor_greaterone = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondminor_greaterone;
                _advice.action_cvc_secondminor_onetothree = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondminor_onetothree;
                _advice.action_cvc_secondminor_threetosix = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondminor_threetosix;
                _advice.action_cvc_secondminor_lesssix = dataList[3].adviceViewModel.AdviceOfCvc.action_cvc_secondminor_lesssix;

                _advice.action_cvc_total_balancepending = _advice.action_cvc_firstmajor_balancepending + _advice.action_cvc_firstminor_balancepending + _advice.action_cvc_secondmajor_balancepending + _advice.action_cvc_secondminor_balancepending;
                _advice.action_cvc_total_greaterone = _advice.action_cvc_firstmajor_greaterone + _advice.action_cvc_firstminor_greaterone + _advice.action_cvc_secondmajor_greaterone + _advice.action_cvc_secondminor_greaterone;
                _advice.action_cvc_total_onetothree = _advice.action_cvc_firstmajor_onetothree + _advice.action_cvc_firstminor_onetothree + _advice.action_cvc_secondmajor_onetothree + _advice.action_cvc_secondminor_onetothree;
                _advice.action_cvc_total_threetosix = _advice.action_cvc_firstmajor_threetosix + _advice.action_cvc_firstminor_threetosix + _advice.action_cvc_secondmajor_threetosix + _advice.action_cvc_secondminor_threetosix;
                _advice.action_cvc_total_lesssix = _advice.action_cvc_firstmajor_lesssix + _advice.action_cvc_firstminor_lesssix + _advice.action_cvc_secondmajor_lesssix + _advice.action_cvc_secondminor_lesssix;

                annualData.adviceViewModel.AdviceOfCvc = _advice;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.adviceViewModel.CvcAdvices.AddRange(dataList[i].adviceViewModel.CvcAdvices);
                    annualData.adviceViewModel.AppeleateAuthorities.AddRange(dataList[i].adviceViewModel.AppeleateAuthorities);
                }
                #endregion
                // Advice of CVC ends

                // Status Of Pendency starts
                #region Status Of Pendency

                statusofpendencyqrs _statusPend = new statusofpendencyqrs();

                _statusPend.pendency_status_fi_previousqtr = dataList[0].statusVM.StatusOfPendency.pendency_status_fi_previousqtr;
                for (int i = 0; i < dataList.Count; i++)
                {
                    _statusPend.pendency_status_fi_addduringqtr += dataList[i].statusVM.StatusOfPendency.pendency_status_fi_addduringqtr;
                    _statusPend.pendency_status_fi_reply_commission += dataList[i].statusVM.StatusOfPendency.pendency_status_fi_reply_commission;
                    _statusPend.pendency_status_ca_addduringqtr += dataList[i].statusVM.StatusOfPendency.pendency_status_ca_addduringqtr;
                    _statusPend.pendency_status_ca_comments_commission += dataList[i].statusVM.StatusOfPendency.pendency_status_ca_comments_commission;
                }
                _statusPend.pendency_status_fi_total = _statusPend.pendency_status_fi_previousqtr + _statusPend.pendency_status_fi_addduringqtr;

                _statusPend.pendency_status_fi_reply_pending = dataList[3].statusVM.StatusOfPendency.pendency_status_fi_reply_pending;

                _statusPend.pendency_status_fi_greaterthree = dataList[3].statusVM.StatusOfPendency.pendency_status_fi_greaterthree;
                _statusPend.pendency_status_fi_threetosix = dataList[3].statusVM.StatusOfPendency.pendency_status_fi_threetosix;
                _statusPend.pendency_status_fi_sixtotwelve = dataList[3].statusVM.StatusOfPendency.pendency_status_fi_sixtotwelve;
                _statusPend.pendency_status_fi_lessone = dataList[3].statusVM.StatusOfPendency.pendency_status_fi_lessone;

                _statusPend.pendency_status_ca_previousqtr = dataList[0].statusVM.StatusOfPendency.pendency_status_ca_previousqtr;

                _statusPend.pendency_status_ca_total = _statusPend.pendency_status_ca_previousqtr + _statusPend.pendency_status_ca_addduringqtr;

                _statusPend.pendency_status_ca_comments_pending = dataList[3].statusVM.StatusOfPendency.pendency_status_ca_comments_pending;
                _statusPend.pendency_status_ca_greaterone = dataList[3].statusVM.StatusOfPendency.pendency_status_ca_greaterone;
                _statusPend.pendency_status_ca_onetotwo = dataList[3].statusVM.StatusOfPendency.pendency_status_ca_onetotwo;
                _statusPend.pendency_status_ca_twotothree = dataList[3].statusVM.StatusOfPendency.pendency_status_ca_twotothree;
                _statusPend.pendency_status_ca_lessthree = dataList[3].statusVM.StatusOfPendency.pendency_status_ca_lessthree;

                annualData.statusVM.StatusOfPendency = _statusPend;

                annualData.statusVM.FiCasesQPRs = dataList[3].statusVM.FiCasesQPRs;
                annualData.statusVM.CaCasesQPRs = dataList[3].statusVM.CaCasesQPRs;

                #endregion
                // Status Of Pendency ends

                // Punitive Vigilance starts
                #region Punitive Vigilance

                punitivevigilanceqrs _punVig = new punitivevigilanceqrs();

                for (int i = 0; i < dataList.Count; i++)
                {
                    _punVig.punitive_vigilance_finaldisposal_major_threetosix_months += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_major_threetosix_months;
                    _punVig.punitive_vigilance_finaldisposal_major_sixtotwelve_months += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_major_sixtotwelve_months;
                    _punVig.punitive_vigilance_finaldisposal_major_onetotwo_year += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_major_onetotwo_year;
                    _punVig.punitive_vigilance_finaldisposal_major_overtwo_year += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_major_overtwo_year;
                    _punVig.punitive_vigilance_finaldisposal_major_overthree_year += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_major_overthree_year;
                    _punVig.punitive_vigilance_finaldisposal_minor_threetosix_months += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_minor_threetosix_months;
                    _punVig.punitive_vigilance_finaldisposal_minor_sixtotwelve_months += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_minor_sixtotwelve_months;
                    _punVig.punitive_vigilance_finaldisposal_minor_onetotwo_year += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_minor_onetotwo_year;
                    _punVig.punitive_vigilance_finaldisposal_minor_overtwo_year += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_minor_overtwo_year;
                    _punVig.punitive_vigilance_finaldisposal_minor_overthree_year += dataList[i].punitiveVig.punitive_vigilance_finaldisposal_minor_overthree_year;

                    _punVig.punitive_vigilance_majorpenalty_numberofcase += dataList[i].punitiveVig.punitive_vigilance_majorpenalty_numberofcase;
                    _punVig.punitive_vigilance_majorpenalty_numberofofficer_against += dataList[i].punitiveVig.punitive_vigilance_majorpenalty_numberofofficer_against;
                    _punVig.punitive_vigilance_majorpenalty_cutinpension += dataList[i].punitiveVig.punitive_vigilance_majorpenalty_cutinpension;
                    _punVig.punitive_vigilance_majorpenalty_dismissal += dataList[i].punitiveVig.punitive_vigilance_majorpenalty_dismissal;
                    _punVig.punitive_vigilance_majorpenalty_reduction_scale += dataList[i].punitiveVig.punitive_vigilance_majorpenalty_reduction_scale;
                    _punVig.punitive_vigilance_majorpenalty_other_majorpenalties += dataList[i].punitiveVig.punitive_vigilance_majorpenalty_other_majorpenalties;
                    _punVig.punitive_vigilance_majorpenalty_minorpenalties += dataList[i].punitiveVig.punitive_vigilance_majorpenalty_minorpenalties;
                    _punVig.punitive_vigilance_majorpenalty_censure_warning += dataList[i].punitiveVig.punitive_vigilance_majorpenalty_censure_warning;
                    _punVig.punitive_vigilance_majorpenalty_noaction += dataList[i].punitiveVig.punitive_vigilance_majorpenalty_noaction;

                    _punVig.punitive_vigilance_minorpenalty_numberofcase += dataList[i].punitiveVig.punitive_vigilance_minorpenalty_numberofcase;
                    _punVig.punitive_vigilance_minorpenalty_numberofofficer_against += dataList[i].punitiveVig.punitive_vigilance_minorpenalty_numberofofficer_against;
                    _punVig.punitive_vigilance_minorpenalty_reduction_lowerstage += dataList[i].punitiveVig.punitive_vigilance_minorpenalty_reduction_lowerstage;
                    _punVig.punitive_vigilance_minorpenalty_postponement += dataList[i].punitiveVig.punitive_vigilance_minorpenalty_postponement;
                    _punVig.punitive_vigilance_minorpenalty_recovery_pay += dataList[i].punitiveVig.punitive_vigilance_minorpenalty_recovery_pay;
                    _punVig.punitive_vigilance_minorpenalty_holding_promotion += dataList[i].punitiveVig.punitive_vigilance_minorpenalty_holding_promotion;
                    _punVig.punitive_vigilance_minorpenalty_censure += dataList[i].punitiveVig.punitive_vigilance_minorpenalty_censure;
                    _punVig.punitive_vigilance_minorpenalty_exoneration += dataList[i].punitiveVig.punitive_vigilance_minorpenalty_exoneration;

                    _punVig.punitivevigilance_riskwisebreakup_groupc_numberofofficer += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupc_numberofofficer;
                    _punVig.punitivevigilance_riskwisebreakup_groupc_officeragainst += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupc_officeragainst;
                    _punVig.punitivevigilance_riskwisebreakup_groupc_pensioncut += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupc_pensioncut;
                    _punVig.punitivevigilance_riskwisebreakup_groupc_dismissal += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupc_dismissal;
                    _punVig.punitivevigilance_riskwisebreakup_groupc_reductionscale += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupc_reductionscale;
                    _punVig.punitivevigilance_riskwisebreakup_groupc_majorpenalties += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupc_majorpenalties;
                    _punVig.punitivevigilance_riskwisebreakup_groupc_minorpenalties += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupc_minorpenalties;
                    _punVig.punitivevigilance_riskwisebreakup_groupc_censure += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupc_censure;
                    _punVig.punitivevigilance_riskwisebreakup_groupc_noaction += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupc_noaction;
                    _punVig.punitivevigilance_riskwisebreakup_groupb_numberofofficer += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupb_numberofofficer;
                    _punVig.punitivevigilance_riskwisebreakup_groupb_officeragainst += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupb_officeragainst;
                    _punVig.punitivevigilance_riskwisebreakup_groupb_pensioncut += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupb_pensioncut;
                    _punVig.punitivevigilance_riskwisebreakup_groupb_dismissal += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupb_dismissal;
                    _punVig.punitivevigilance_riskwisebreakup_groupb_reductionscale += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupb_reductionscale;
                    _punVig.punitivevigilance_riskwisebreakup_groupb_majorpenalties += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupb_majorpenalties;
                    _punVig.punitivevigilance_riskwisebreakup_groupb_minorpenalties += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupb_minorpenalties;
                    _punVig.punitivevigilance_riskwisebreakup_groupb_censure += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupb_censure;
                    _punVig.punitivevigilance_riskwisebreakup_groupb_noaction += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupb_noaction;
                    _punVig.punitivevigilance_riskwisebreakup_groupa_numberofofficer += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupa_numberofofficer;
                    _punVig.punitivevigilance_riskwisebreakup_groupa_officeragainst += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupa_officeragainst;
                    _punVig.punitivevigilance_riskwisebreakup_groupa_pensioncut += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupa_pensioncut;
                    _punVig.punitivevigilance_riskwisebreakup_groupa_dismissal += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupa_dismissal;
                    _punVig.punitivevigilance_riskwisebreakup_groupa_reductionscale += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupa_reductionscale;
                    _punVig.punitivevigilance_riskwisebreakup_groupa_majorpenalties += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupa_majorpenalties;
                    _punVig.punitivevigilance_riskwisebreakup_groupa_minorpenalties += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupa_minorpenalties;
                    _punVig.punitivevigilance_riskwisebreakup_groupa_censure += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupa_censure;
                    _punVig.punitivevigilance_riskwisebreakup_groupa_noaction += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_groupa_noaction;
                    _punVig.punitivevigilance_riskwisebreakup_js_numberofofficer += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_js_numberofofficer;
                    _punVig.punitivevigilance_riskwisebreakup_js_officeragainst += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_js_officeragainst;
                    _punVig.punitivevigilance_riskwisebreakup_js_pensioncut += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_js_pensioncut;
                    _punVig.punitivevigilance_riskwisebreakup_js_dismissal += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_js_dismissal;
                    _punVig.punitivevigilance_riskwisebreakup_js_reductionscale += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_js_reductionscale;
                    _punVig.punitivevigilance_riskwisebreakup_js_majorpenalties += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_js_majorpenalties;
                    _punVig.punitivevigilance_riskwisebreakup_js_minorpenalties += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_js_minorpenalties;
                    _punVig.punitivevigilance_riskwisebreakup_js_censure += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_js_censure;
                    _punVig.punitivevigilance_riskwisebreakup_js_noaction += dataList[i].punitiveVig.punitivevigilance_riskwisebreakup_js_noaction;

                }

                annualData.punitiveVig = _punVig;
                #endregion
                // Punitive Vigilance ends

                //Preventive Vigilance starts
                #region Preventive Vigilance
                preventivevigilanceqrs _prevVig = new preventivevigilanceqrs();

                _prevVig.preventivevig_bycvo_periodic_end_previous_qtr = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_periodic_end_previous_qtr;
                _prevVig.preventivevig_bycvo_surprise_end_previous_qtr = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_surprise_end_previous_qtr;
                _prevVig.preventivevig_bycvo_majorwork_end_previous_qtr = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_majorwork_end_previous_qtr;
                _prevVig.preventivevig_bycvo_scrutiny_file_end_previous_qtr = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_scrutiny_file_end_previous_qtr;
                _prevVig.preventivevig_bycvo_scrutiny_property_end_previous_qtr = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_scrutiny_property_end_previous_qtr;
                _prevVig.preventivevig_bycvo_audit_reports_end_previous_qtr = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_audit_reports_end_previous_qtr;
                _prevVig.preventivevig_bycvo_training_programs_end_previous_qtr = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_training_programs_end_previous_qtr;
                _prevVig.preventivevigi_bycvo_system_improvements_end_previous_qtr = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_bycvo_system_improvements_end_previous_qtr;

                for (int i = 0; i < dataList.Count; i++)
                {                    
                    _prevVig.preventivevig_bycvo_periodic_during_qtr += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_periodic_during_qtr;
                    _prevVig.preventivevig_bycvo_periodic_system_improvement += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_periodic_system_improvement;
                    _prevVig.preventivevig_bycvo_periodic_recovery_effected += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_periodic_recovery_effected;

                    _prevVig.preventivevig_bycvo_surprise_during_qtr += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_surprise_during_qtr;
                    _prevVig.preventivevig_bycvo_surprise_system_improvement += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_surprise_system_improvement;
                    _prevVig.preventivevig_bycvo_surprise_recovery_effected += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_surprise_recovery_effected;

                    _prevVig.preventivevig_bycvo_majorwork_during_qtr += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_majorwork_during_qtr;
                    _prevVig.preventivevig_bycvo_majorwork_system_improvement += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_majorwork_system_improvement;
                    _prevVig.preventivevig_bycvo_majorwork_recovery_effected += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_majorwork_recovery_effected;

                    _prevVig.preventivevig_bycvo_scrutiny_file_during_qtr += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_scrutiny_file_during_qtr;
                    _prevVig.preventivevig_bycvo_scrutiny_file_system_improvement += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_scrutiny_file_system_improvement;
                    _prevVig.preventivevig_bycvo_scrutiny_file_recovery_effected += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_scrutiny_file_recovery_effected;

                    _prevVig.preventivevig_bycvo_scrutiny_property_during_qtr += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_scrutiny_property_during_qtr;
                    _prevVig.preventivevig_bycvo_scrutiny_property_system_improvement += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_scrutiny_property_system_improvement;
                    _prevVig.preventivevig_bycvo_scrutiny_property_recovery_effected += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_scrutiny_property_recovery_effected;

                    _prevVig.preventivevig_bycvo_audit_reports_during_qtr += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_audit_reports_during_qtr;
                    _prevVig.preventivevig_cvc_audit_reports_system_improvement += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_cvc_audit_reports_system_improvement;
                    _prevVig.preventivevig_bycvo_audit_reports_recovery_effected += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_audit_reports_recovery_effected;


                    _prevVig.preventivevig_bycvo_training_programs_during_qtr += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevig_bycvo_training_programs_during_qtr;
                    _prevVig.preventivevigi_bycvo_training_programs_system_improvement += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_bycvo_training_programs_system_improvement;
                    _prevVig.preventivevigi_bycvo_training_programs_recovery_effected += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_bycvo_training_programs_recovery_effected;

                    _prevVig.preventivevigi_bycvo_system_improvements_during_qtr += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_bycvo_system_improvements_during_qtr;
                    _prevVig.preventivevigi_bycvo_system_improvements_system_improvement += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_bycvo_system_improvements_system_improvement;
                    _prevVig.preventivevigi_bycvo_system_improvements_recovery_effected += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_bycvo_system_improvements_recovery_effected;

                    _prevVig.preventivevigi_management_job_rotation_postduerotation += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_management_job_rotation_postduerotation;

                    _prevVig.preventivevigi_management_job_rotation_reasons += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_management_job_rotation_reasons;

                }
                _prevVig.preventivevigi_management_job_rotation_sensitivenumberpost = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_management_job_rotation_sensitivenumberpost;
                _prevVig.preventivevigi_management_job_rotation_postduerotation = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_management_job_rotation_postduerotation;

                _prevVig.preventivevigi_management_job_rotation_postnotrotated = dataList[3].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_management_job_rotation_postnotrotated;

                _prevVig.preventivevigi_management_frj_numberofficer_covered = dataList[0].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_management_job_rotation_postnotrotated;

                for (int i = 0; i < dataList.Count; i++)
                {
                    _prevVig.preventivevigi_management_frj_reviews_undertaken += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_management_frj_reviews_undertaken;
                    _prevVig.preventivevigi_management_frj_case_under_fr += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_management_frj_case_under_fr;
                    _prevVig.preventivevigi_management_frj_action_taken += dataList[i].preventiveViewModel.PreventiveVigilanceQRS.preventivevigi_management_frj_action_taken;
                }

                annualData.preventiveViewModel.PreventiveVigilanceQRS = _prevVig;

                annualData.preventiveViewModel.PrevVigiA = dataList[3].preventiveViewModel.PrevVigiA;
                annualData.preventiveViewModel.PrevVigiB = dataList[3].preventiveViewModel.PrevVigiB;

                #endregion
                //Preventive Vigilance ends

                //Preventive Vigilance Activities ends
                #region Preventive Vigilance Activities
                vigilanceactivitiescvcqrs _vigActivities = new vigilanceactivitiescvcqrs();

                _vigActivities.vigilance_activites_upload_doc = dataList[3].preventiveActivitiesVM.vigilance_activites_upload_doc;
                _vigActivities.vigilance_activites_any_remark = dataList[3].preventiveActivitiesVM.vigilance_activites_any_remark;
                _vigActivities.vigilance_activites_place = dataList[3].preventiveActivitiesVM.vigilance_activites_place;
                _vigActivities.vigilance_activites_date = dataList[3].preventiveActivitiesVM.vigilance_activites_date;

                annualData.preventiveActivitiesVM = _vigActivities;

                #endregion
                //Preventive Vigilance Activities ends
            }
            return annualData;
        }
    }
}
