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
    public partial class existing_para_clause
    {
        public string paraid { get; set; }
        public string para { get; set; }
        public string clauseid { get; set; }
        public string clause { get; set; }
        public string clause_description { get; set; }
    }
}