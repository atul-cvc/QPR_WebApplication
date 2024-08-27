using Microsoft.Data.SqlClient;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Repository;
using System.Data;
using QPR_Application.Models.ViewModels;
using QPR_Application.Models.Entities;

namespace QPR_Application.Util
{
    public class QPRUtilility
    {
        private readonly IQprRepo _qprRepo;
        private readonly IHttpContextAccessor _httpContext;
        private string _connString = string.Empty;
        public QPRUtilility(IQprRepo qprRepo, IHttpContextAccessor httpContext, IConfiguration config)
        {
            _qprRepo = qprRepo;
            _httpContext = httpContext;
            _connString = config.GetConnectionString("SQLConnection") ?? String.Empty;
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

        public QPRReportViewModel PopulateAnnualReportData(List<QPRReportViewModel> dataList)
        {
            QPRReportViewModel annualData = new QPRReportViewModel();
            if (dataList != null && dataList.Count > 0)
            {
                
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
                    annualData.complaints.cvcpidpi_receivequarter = dataList[i].complaints.cvcpidpi_receivequarter;
                    annualData.complaints.otherpidpi_receivequarter = dataList[i].complaints.otherpidpi_receivequarter;
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
                annualData.complaints.totalpidpi_report_quarter += annualData.complaints.cvcpidpi_report_quarter + annualData.complaints.otherpidpi_report_quarter;

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

                annualData.complaints.totalpidpi_delay_one_month = dataList[3].complaints.totalpidpi_delay_one_month;

                annualData.complaints.cvcpidpiinvestadvicecvc = dataList[0].complaints.cvcpidpiinvestadvicecvc;
                annualData.complaints.cvopidpiinvestadvicecvc = dataList[0].complaints.cvopidpiinvestadvicecvc;
                annualData.complaints.totalpidpiinvestadvicecvc = annualData.complaints.cvcpidpiinvestadvicecvc + annualData.complaints.cvopidpiinvestadvicecvc;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.cvcpidpiinvesquarterreportbycvo = dataList[i].complaints.cvcpidpiinvesquarterreportbycvo;
                    annualData.complaints.cvopidpiinvesquarterreportbycvo = dataList[i].complaints.cvopidpiinvesquarterreportbycvo;
                    annualData.complaints.cvcpidpiinvesquarteradvicereceive = dataList[i].complaints.cvcpidpiinvesquarteradvicereceive;
                    annualData.complaints.cvopidpiinvesquarteradvicereceive = dataList[i].complaints.cvopidpiinvesquarteradvicereceive;
                }
                annualData.complaints.totalpidpiinvesquarterreportbycvo = annualData.complaints.cvcpidpiinvesquarterreportbycvo + annualData.complaints.cvopidpiinvesquarterreportbycvo;
                annualData.complaints.totalpidpiinvesquarteradvicereceive = annualData.complaints.cvcpidpiinvesquarteradvicereceive + annualData.complaints.cvopidpiinvesquarteradvicereceive;

                annualData.complaints.cvcpidpiinvestotaladvicereceive = annualData.complaints.cvcpidpiinvestadvicecvc + annualData.complaints.cvcpidpiinvesquarteradvicereceive;
                annualData.complaints.cvopidpiinvestotaladvicereceive = annualData.complaints.cvopidpiinvestotaladvicereceive + annualData.complaints.cvopidpiinvestotaladvicereceive;
                annualData.complaints.totalpidpiinvestotaladvicereceive = annualData.complaints.cvcpidpiinvestotaladvicereceive + annualData.complaints.cvopidpiinvestotaladvicereceive;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.cvcpidpiinvesactionduringquarter = dataList[i].complaints.cvcpidpiinvesactionduringquarter;
                    annualData.complaints.cvopidpiinvesactionduringquarter = dataList[i].complaints.cvopidpiinvesactionduringquarter;
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
                    annualData.complaints.napidpireceiveqtr = dataList[i].complaints.napidpireceiveqtr;
                }
                annualData.complaints.napidpitotal = annualData.complaints.napidpibroughtforward + annualData.complaints.napidpireceiveqtr;

                for (int i = 0; i < dataList.Count; i++)
                {
                    annualData.complaints.napidpiclosedwithoutaction = dataList[i].complaints.napidpiclosedwithoutaction;
                    annualData.complaints.napidpifurtherinvestigation = dataList[i].complaints.napidpifurtherinvestigation;
                    annualData.complaints.napidpiadminaction = dataList[i].complaints.napidpiadminaction;
                    annualData.complaints.napidpiimpositionpenalty = dataList[i].complaints.napidpiimpositionpenalty;
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
                    annualData.complaints.scrutinyreportexaminedqtr = dataList[i].complaints.scrutinyreportexaminedqtr;
                    annualData.complaints.scrutinyreportidentifiedexami = dataList[i].complaints.scrutinyreportidentifiedexami;
                    annualData.complaints.scrutinyreportcompleteinvestigation = dataList[i].complaints.scrutinyreportcompleteinvestigation;

                    annualData.complaints.scrutinyreportexaminedqtrconcurrent = dataList[i].complaints.scrutinyreportexaminedqtrconcurrent;
                    annualData.complaints.scrutinyreportidentifiedexamiconcurrent = dataList[i].complaints.scrutinyreportidentifiedexamiconcurrent;
                    annualData.complaints.scrutinyreportcompleteinvestigationconcurrent = dataList[i].complaints.scrutinyreportcompleteinvestigationconcurrent;
                    annualData.complaints.scrutinyreportexaminedqtrinternal = dataList[i].complaints.scrutinyreportexaminedqtrinternal;
                    annualData.complaints.scrutinyreportidentifiedexamiinternal = dataList[i].complaints.scrutinyreportidentifiedexamiinternal;
                    annualData.complaints.scrutinyreportcompleteinvestigationinternal = dataList[i].complaints.scrutinyreportcompleteinvestigationinternal;
                    annualData.complaints.scrutinyreportexaminedqtrstatutory = dataList[i].complaints.scrutinyreportexaminedqtrstatutory;
                    annualData.complaints.scrutinyreportidentifiedexamistatutory = dataList[i].complaints.scrutinyreportidentifiedexamistatutory;
                    annualData.complaints.scrutinyreportcompleteinvestigationstatutory = dataList[i].complaints.scrutinyreportcompleteinvestigationstatutory;
                    annualData.complaints.scrutinyreportexaminedqtrothers = dataList[i].complaints.scrutinyreportexaminedqtrothers;
                    annualData.complaints.scrutinyreportidentifiedexamiothers = dataList[i].complaints.scrutinyreportidentifiedexamiothers;
                    annualData.complaints.scrutinyreportcompleteinvestigationothers = dataList[i].complaints.scrutinyreportcompleteinvestigationothers;
                }
                annualData.complaints.scrutinyreportexaminedqtrtotal = annualData.complaints.scrutinyreportexaminedqtr + annualData.complaints.scrutinyreportexaminedqtrconcurrent + annualData.complaints.scrutinyreportexaminedqtrinternal + annualData.complaints.scrutinyreportexaminedqtrstatutory + annualData.complaints.scrutinyreportexaminedqtrothers;
                annualData.complaints.scrutinyreportidentifiedexamitotal = annualData.complaints.scrutinyreportidentifiedexami + annualData.complaints.scrutinyreportidentifiedexamiconcurrent + annualData.complaints.scrutinyreportidentifiedexamiinternal + annualData.complaints.scrutinyreportidentifiedexamiothers + annualData.complaints.scrutinyreportidentifiedexamistatutory;
                annualData.complaints.scrutinyreportcompleteinvestigationtotal =  annualData.complaints.scrutinyreportcompleteinvestigation +  annualData.complaints.scrutinyreportcompleteinvestigationconcurrent +  annualData.complaints.scrutinyreportcompleteinvestigationinternal +  annualData.complaints.scrutinyreportcompleteinvestigationothers +  annualData.complaints.scrutinyreportcompleteinvestigationstatutory;

                annualData.complaints.scrutinyreportpendinginvestigation = dataList[3].complaints.scrutinyreportpendinginvestigation;
                annualData.complaints.scrutinyreportpendinginvestigationconcurrent = dataList[3].complaints.scrutinyreportpendinginvestigationconcurrent;
                annualData.complaints.scrutinyreportpendinginvestigationinternal = dataList[3].complaints.scrutinyreportpendinginvestigationinternal;
                annualData.complaints.scrutinyreportpendinginvestigationstatutory = dataList[3].complaints.scrutinyreportpendinginvestigationstatutory;
                annualData.complaints.scrutinyreportpendinginvestigationothers = dataList[3].complaints.scrutinyreportpendinginvestigationothers;
                annualData.complaints.scrutinyreportpendinginvestigationtotal = annualData.complaints.scrutinyreportpendinginvestigation + annualData.complaints.scrutinyreportpendinginvestigationconcurrent + annualData.complaints.scrutinyreportpendinginvestigationinternal + annualData.complaints.scrutinyreportpendinginvestigationstatutory + annualData.complaints.scrutinyreportpendinginvestigationothers;
                


            }
            return annualData;
        }
    }
}
