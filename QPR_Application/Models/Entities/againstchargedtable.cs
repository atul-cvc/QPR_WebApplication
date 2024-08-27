using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QPR_Application.Models.Entities
{
    public partial class againstchargedtable
    {
        [Key]
        public int pend_id { get; set; }
        
        [StringLength(2000)]
        public string? departproceedings_detailsinquiry_chargedofficer { get; set; }
        
        public DateOnly? departproceedings_detailsinquiry_chargesheet { get; set; }
        
        public DateOnly? departproceedings_detailsinquiry_ioappointment { get; set; }

        public DateOnly? departproceedings_detailsinquiry_superannuation { get; set; }
        public string? departproceedings_detailsinquiry_remarks { get; set; }
        public string used_ip { get; set; }
        public long qpr_id { get; set; }
    }
}