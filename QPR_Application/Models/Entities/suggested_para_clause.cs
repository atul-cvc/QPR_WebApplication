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
    public partial class suggested_para_clause
    {
        public int id { get; set; }
        public int? refno { get; set; }
        public string paraid { get; set; }
        public string para { get; set; }
        public string clauseid { get; set; }
        public string clause { get; set; }
        public string existingclause { get; set; }
        public string proposedclause { get; set; }
    }
}