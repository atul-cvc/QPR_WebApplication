﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QPR_Application.Models.Entities
{
    public partial class Login
    {
        //[Key]
        //public int Id { get; set; }
        //public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}