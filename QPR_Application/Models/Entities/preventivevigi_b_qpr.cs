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
    public partial class preventivevigi_b_qpr
    {
        public int? preventivevigi_b_id { get; set; }
        [StringLength(1000)]
        public string preventive_vigilance_id { get; set; }
        [StringLength(1000)]
        public string preventivevigi_b_detailsvigi_subsidiaries_serial_number { get; set; }
        [StringLength(1000)]
        public string preventivevigi_management_detailsvig_b_nameofsub { get; set; }
        [StringLength(1000)]
        public string preventivevigi_management_detailsvig_b_contral_method { get; set; }
        public string preventivevigi_management_detailsvig_b_likely_time { get; set; }
        [StringLength(1000)]
        public string qpr_id { get; set; }
        [StringLength(500)]
        public string ip { get; set; }
    }
}