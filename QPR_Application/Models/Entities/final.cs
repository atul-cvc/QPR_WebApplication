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
    public partial class final
    {
        public int? section { get; set; }
        public string file_no { get; set; }
        public int? sno { get; set; }
        public int? disp_order_issd { get; set; }
        public string disp_order_dt { get; set; }
        public string disp_order_penalty { get; set; }
        public bool? fin_ord_issd { get; set; }
        public string fin_ord_dt { get; set; }
        public string fin_penal { get; set; }
        public string remark { get; set; }
        public string major_penalty_recd { get; set; }
        public string disp_order_ma_dt { get; set; }
        public string disp_order_ma_penalty { get; set; }
        public string fin_ord_ma_dt { get; set; }
        public string remark_ma { get; set; }
        public string last_updated { get; set; }
    }
}