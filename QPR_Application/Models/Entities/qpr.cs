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
        public double? qtrreport { get; set; }
        public double? qtryear { get; set; }
        public double? contactnumberoffice { get; set; }
        public double? mobilenumber { get; set; }
        [StringLength(255)]
        public string emailid { get; set; }
        public bool fulltime { get; set; }
        public bool parttime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? createdate { get; set; }
        public double? referencenumber { get; set; }
        [StringLength(255)]
        public string ip { get; set; }
        public bool finalsubmit { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? finalsubmitdate { get; set; }
    }
}