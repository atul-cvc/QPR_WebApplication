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
    public partial class forward
    {
        public int? fwdid { get; set; }
        public int? complaintnumber { get; set; }
        public string fromuser { get; set; }
        public string touser { get; set; }
        public string currentstatus { get; set; }
        public string forwarddate { get; set; }
        public string lastmovement { get; set; }
        public string forwardno { get; set; }
        public string forwardserialno { get; set; }
        public string compliant { get; set; }
        public string fsno { get; set; }
        public string forwardedcount { get; set; }
        public string fromusercode { get; set; }
        public string tousercode { get; set; }
        public string status { get; set; }
        public string fdate { get; set; }
        public string currentsection { get; set; }
        public string ipcapture { get; set; }
        public string byuserid { get; set; }
        public string ondate { get; set; }
    }
}