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
    public partial class vijclearancedetail
    {
        public string vijid { get; set; }
        public string fileno { get; set; }
        public string nameofdepart { get; set; }
        public string departrefno { get; set; }
        public string departrefdate1 { get; set; }
        public string departrefrecvdate { get; set; }
        public string subject { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string numberofofficer { get; set; }
        public string lettertocbidate { get; set; }
        public string lettertosectiondate { get; set; }
        public string lettertoconcerned { get; set; }
        public string completeprofile { get; set; }
        public string returntodepart { get; set; }
        public string feedbackreceivedfromcbidate { get; set; }
        public string feedbackreceivedfromsection { get; set; }
        public string feedbackreceivedfromorganization { get; set; }
        public string filesubmittedbydhdate { get; set; }
        public string dateonfile { get; set; }
        public string letterissuedate { get; set; }
        public string clearancetype { get; set; }
        public string uploadscandocument { get; set; }
    }
}