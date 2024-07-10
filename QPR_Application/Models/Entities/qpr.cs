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
    public partial class qpr
    {
        [StringLength(255)]
        public string userid { get; set; }
        [StringLength(255)]
        public string orgname { get; set; }
        [StringLength(255)]
        public string orgcode { get; set; }
        public string? qtrreport { get; set; }
        public string? qtryear { get; set; }
        public string? contactnumberoffice { get; set; }
        public string? mobilenumber { get; set; }
        [StringLength(500)]
        public string emailid { get; set; }
        public string fulltime { get; set; }
        public string parttime { get; set; }
        //[Column(TypeName = "datetime")]
        public string? createdate { get; set; }
        [Key]
        public long? referencenumber { get; set; }
        [StringLength(255)]
        public string ip { get; set; }
        public string finalsubmit { get; set; }
        //[Column(TypeName = "datetime")]
        public string? finalsubmitdate { get; set; }
    }
}