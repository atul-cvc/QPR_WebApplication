﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QPR_Application.Models.Entities
{
    public partial class cmptable
    {
        [Key]
        public long complaintnumber { get; set; }
        [StringLength(50)]
        public string dstatus { get; set; }
        [StringLength(50)]
        public string sendername { get; set; }
        [StringLength(100)]
        public string complaindate { get; set; }
        [StringLength(50)]
        public string address { get; set; }
        [StringLength(50)]
        public string address2 { get; set; }
        [StringLength(50)]
        public string state { get; set; }
        [StringLength(50)]
        public string city { get; set; }
        public int? pinno { get; set; }
        [StringLength(50)]
        public string email { get; set; }
        public long? mobileno { get; set; }
        [StringLength(50)]
        public string complaintagainstname { get; set; }
        [StringLength(50)]
        public string designation { get; set; }
        [StringLength(50)]
        public string organization { get; set; }
        public byte? section { get; set; }
        [StringLength(50)]
        public string whetherfasttrack { get; set; }
        [StringLength(50)]
        public string complainttype { get; set; }
        [StringLength(50)]
        public string actiontaken { get; set; }
        [StringLength(50)]
        public string uploadscandocument { get; set; }
        [StringLength(50)]
        public string fdeg { get; set; }
        [StringLength(50)]
        public string diarylogin { get; set; }
        [StringLength(50)]
        public string documentstatus { get; set; }
        [StringLength(50)]
        public string sectionlogin { get; set; }
        [StringLength(50)]
        public string status { get; set; }
        [StringLength(50)]
        public string search { get; set; }
        [StringLength(50)]
        public string gistallegation { get; set; }
        [StringLength(50)]
        public string boscomptype { get; set; }
        [StringLength(50)]
        public string bosreasonfill { get; set; }
        [StringLength(50)]
        public string bodecision { get; set; }
        [StringLength(50)]
        public string boircondate { get; set; }
        [StringLength(50)]
        public string boirremidate { get; set; }
        [StringLength(50)]
        public string boirconrecdate { get; set; }
        [StringLength(50)]
        public string boirfinaldec { get; set; }
        [StringLength(50)]
        public string boirfasttrack { get; set; }
        [StringLength(50)]
        public string boirfinalno { get; set; }
        [StringLength(50)]
        public string boiracedate { get; set; }
        [StringLength(50)]
        public string confirm_cvo { get; set; }
        [StringLength(50)]
        public string bonaps { get; set; }
        [StringLength(100)]
        public string bonaackdate { get; set; }
        [StringLength(50)]
        public string bofacks { get; set; }
        [StringLength(50)]
        public string finaldecision1 { get; set; }
        [StringLength(50)]
        public string boothacksentdate { get; set; }
        [StringLength(50)]
        public string boothconcvo { get; set; }
        [StringLength(50)]
        public string boothconsendate { get; set; }
        [StringLength(50)]
        public string boothremsendate { get; set; }
        [StringLength(50)]
        public string boothconrecdat { get; set; }
        [StringLength(50)]
        public string whetherfastrack { get; set; }
        [StringLength(50)]
        public string filenumber { get; set; }
        [StringLength(50)]
        public string boothacksntdat { get; set; }
        [StringLength(50)]
        public string creationdate { get; set; }
        [StringLength(100)]
        public string complaint { get; set; }
        [StringLength(50)]
        public string current { get; set; }
        [StringLength(50)]
        public string previouscurrent { get; set; }
        [StringLength(50)]
        public string rdate { get; set; }
        [StringLength(50)]
        public string willingdepose { get; set; }
        [StringLength(50)]
        public string detailalligation { get; set; }
    }
}