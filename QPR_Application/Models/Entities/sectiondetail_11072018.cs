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
    public partial class sectiondetail_11072018
    {
        public long? section { get; set; }
        [StringLength(50)]
        public string orgcod { get; set; }
        [StringLength(50)]
        public string depcod { get; set; }
        [StringLength(50)]
        public string mincod { get; set; }
        [StringLength(1000)]
        public string organisationname1 { get; set; }
    }
}