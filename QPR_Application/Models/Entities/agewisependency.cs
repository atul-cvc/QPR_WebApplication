﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QPR_Application.Models.Entities
{
    public partial class agewisependency
    {
        [Key]
        public int pend_id { get; set; }
        public long? qpr_id { get; set; }
        public string prosependingnamedesig { get; set; }
        public DateOnly? prosependingdaterecommend { get; set; }
        public DateOnly prosependingdatereceipt { get; set; }
        public string prosependingstatusrequest { get; set; }
        public string? prosependingnameauthority { get; set; }
        public string used_ip { get; set; }
        public string? prosependingcbifirno { get; set; }
        public string? prosependingsanctionpc { get; set; }

        //public agewisependency()
        //{
        //    pend_id = 0;
        //    qpr_id = 0;
        //    prosependingnamedesig = String.Empty;
        //    prosependingdaterecommend = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        //    prosependingdatereceipt = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        //    prosependingstatusrequest = String.Empty;
        //    prosependingnameauthority = String.Empty;
        //    used_ip = String.Empty;
        //    prosependingcbifirno = String.Empty;
        //    prosependingsanctionpc = String.Empty;
        //}
    }
}