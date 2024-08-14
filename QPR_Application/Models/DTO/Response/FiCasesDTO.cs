﻿namespace QPR_Application.Models.DTO.Response
{
    public class FiCasesDTO
    {
        public int? ficasesqpr_id { get; set; }
        public string? user_id { get; set; }
        public DateOnly? update_date { get; set; }
        public string? last_user_id { get; set; }
        public long? qpr_id { get; set; }
        public string? ip { get; set; }
        public string? cvc_case_reg_no { get; set; }
        public string? cvc_file_no { get; set; }
        public DateOnly? date_since_pend { get; set; }
        public string? name_officer { get; set; }
        public string? dept_ref_no { get; set; }
        public string? present_status { get; set; }
        public DateOnly? submission_date { get; set; }
        public string? remark { get; set; }
        public bool? status { get; set; }
    }
}
