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
    public partial class fulltimecvo
    {
        [Required]
        public string slno { get; set; }
        [Required]
        public string ministry { get; set; }
        [Required]
        public string department { get; set; }
        [Required]
        public string organisation { get; set; }
        [Required]
        public string cvoname { get; set; }
        [Required]
        public string services { get; set; }
        public string batch { get; set; }
        public string tenurefrom { get; set; }
        public string tenureto { get; set; }
        [Required]
        public string contact { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string asondate { get; set; }
    }
}