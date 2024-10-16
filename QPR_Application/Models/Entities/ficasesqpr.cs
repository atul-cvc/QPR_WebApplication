﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QPR_Application.Models.Entities
{
    [Keyless]
    public partial class ficasesqpr
    {
        public int ficasesqpr_id { get; set; }
        [Required]
        public string user_id { get; set; }
        [Column(TypeName = "date")]
        public DateTime update_date { get; set; }
        [Required]
        public string last_user_id { get; set; }
        public long qpr_id { get; set; }
        [Required]
        public string ip { get; set; }
        public string cvc_case_reg_no { get; set; }
        public string cvc_file_no { get; set; }
        public DateTime? date_since_pend { get; set; }
        public string name_officer { get; set; }
        public string dept_ref_no { get; set; }
        public string present_status { get; set; }
        [Column(TypeName = "date")]
        public DateTime? submission_date { get; set; }
        public string remark { get; set; }
        public bool? status { get; set; }
    }
}