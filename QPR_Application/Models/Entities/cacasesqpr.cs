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
    public partial class cacasesqpr
    {
        public int? cacasesqpr_id { get; set; }
        [StringLength(100)]
        public string user_id { get; set; }
        [Column(TypeName = "date")]
        public DateTime? update_date { get; set; }
        [StringLength(100)]
        public string last_user_id { get; set; }
        public long? qpr_id { get; set; }
        [StringLength(50)]
        public string ip { get; set; }
        [StringLength(100)]
        public string cvc_case_reg_no { get; set; }
        [StringLength(100)]
        public string cvc_file_no { get; set; }
        public DateTime? date_since_pend { get; set; }
        [StringLength(2000)]
        public string name_officer { get; set; }
        public string dept_ref_no { get; set; }
        public string present_status { get; set; }
        [Column(TypeName = "date")]
        public DateTime? submission_date { get; set; }
        public string remark { get; set; }
        public bool? status { get; set; }
    }
}