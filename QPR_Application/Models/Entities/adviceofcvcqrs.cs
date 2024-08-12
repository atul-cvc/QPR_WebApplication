﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QPR_Application.Models.Entities
{
    public partial class adviceofcvcqrs
    {
        [Key]
        public int advice_cvc_id { get; set; }
        public int advice_cvc_first_casespreviousqtr { get; set; }
        public int advice_cvc_first_casesduringqtr { get; set; }
        public int advice_cvc_first_adviceduringqtr { get; set; }
        public int advice_cvc_first_adviceawaitedcvc { get; set; }
        public int advice_cvc_second_casespreviousqtr { get; set; }
        public int advice_cvc_second_casesduringqtr { get; set; }
        public int advice_cvc_second_adviceduringqtr { get; set; }
        public int advice_cvc_second_adviceawaitedcvc { get; set; }
        public int advice_cvc_firstreconsider_casespreviousqtr { get; set; }
        public int advice_cvc_firstreconsider_casesduringqtr { get; set; }
        public int advice_cvc_firstreconsider_adviceduringqtr { get; set; }
        public int advice_cvc_firstreconsider_adviceawaitedcvc { get; set; }
        public int advice_cvc_secondreconsider_casespreviousqtr { get; set; }
        public int advice_cvc_secondreconsider_casesduringqtr { get; set; }
        public int advice_cvc_secondreconsider_adviceduringqtr { get; set; }
        public int advice_cvc_secondreconsider_adviceawaitedcvc { get; set; }
        public int advice_cvc_total_casespreviousqtr { get; set; }
        public int advice_cvc_total_casesduringqtr { get; set; }
        public int advice_cvc_total_adviceduringqtr { get; set; }
        public int advice_cvc_total_adviceawaitedcvc { get; set; }
        public int action_cvc_firstmajor_openingbalance { get; set; }
        public int action_cvc_firstmajor_adviceduringqtr { get; set; }
        public int action_cvc_firstmajor_disposed { get; set; }
        public int action_cvc_firstmajor_balancepending { get; set; }
        public int action_cvc_firstmajor_greaterone { get; set; }
        public int action_cvc_firstmajor_onetothree { get; set; }
        public int action_cvc_firstmajor_threetosix { get; set; }
        public int action_cvc_firstmajor_lesssix { get; set; }
        public int action_cvc_firstminor_openingbalance { get; set; }
        public int action_cvc_firstminor_adviceduringqtr { get; set; }
        public int action_cvc_firstminor_disposed { get; set; }
        public int action_cvc_firstminor_balancepending { get; set; }
        public int action_cvc_firstminor_greaterone { get; set; }
        public int action_cvc_firstminor_onetothree { get; set; }
        public int action_cvc_firstminor_threetosix { get; set; }
        public int action_cvc_firstminor_lesssix { get; set; }
        public int action_cvc_secondmajor_openingbalance { get; set; }
        public int action_cvc_secondmajor_adviceduringqtr { get; set; }
        public int action_cvc_secondmajor_disposed { get; set; }
        public int action_cvc_secondmajor_balancepending { get; set; }
        public int action_cvc_secondmajor_greaterone { get; set; }
        public int action_cvc_secondmajor_onetothree { get; set; }
        public int action_cvc_secondmajor_threetosix { get; set; }
        public int action_cvc_secondmajor_lesssix { get; set; }
        public int action_cvc_secondminor_openingbalance { get; set; }
        public int action_cvc_secondminor_adviceduringqtr { get; set; }
        public int action_cvc_secondminor_disposed { get; set; }
        public int action_cvc_secondminor_balancepending { get; set; }
        public int action_cvc_secondminor_greaterone { get; set; }
        public int action_cvc_secondminor_onetothree { get; set; }
        public int action_cvc_secondminor_threetosix { get; set; }
        public int action_cvc_secondminor_lesssix { get; set; }
        public int action_cvc_total_openingbalance { get; set; }
        public int action_cvc_total_adviceduringqtr { get; set; }
        public int action_cvc_total_disposed { get; set; }
        public int action_cvc_total_balancepending { get; set; }
        public int action_cvc_total_greaterone { get; set; }
        public int action_cvc_total_onetothree { get; set; }
        public int action_cvc_total_threetosix { get; set; }
        public int action_cvc_total_lesssix { get; set; }
        public string? deviation_cvc_advice_firststage_typecvcadvice { get; set; }
        public string? deviation_cvc_advice_firststage_cvcfilenumber { get; set; }
        public string? deviation_cvc_advice_firststage_dept_ref_number { get; set; }
        public string? deviation_cvc_advice_firststage_name_designation { get; set; }
        public string? deviation_cvc_advice_firststage_actiontaken_da { get; set; }
        public string? deviation_cvc_advice_secondstage_typecvcadvice { get; set; }
        public string? deviation_cvc_advice_secondstage_cvcfilenumber { get; set; }
        public string? deviation_cvc_advice_secondstage_dept_ref_number { get; set; }
        public string? deviation_cvc_advice_secondstage_name_designation { get; set; }
        public string? deviation_cvc_advice_secondstage_actiontaken_da { get; set; }
        public string? appellate_deviation_firststage_typecvcadvice { get; set; }
        public string? appellate_deviation_firststage_cvcfilenumber { get; set; }
        public string? appellate_deviation_firststage_dept_ref_number { get; set; }
        public string? appellate_deviation_firststage_name_designation { get; set; }
        public string? appellate_deviation_firststage_actiontaken_da { get; set; }
        public string? appellate_deviation_firststage_actiontaken_aa { get; set; }
        public string? appellate_deviation_secondstage_typecvcadvice { get; set; }
        public string? appellate_deviation_secondstage_cvcfilenumber { get; set; }
        public string? appellate_deviation_secondstage_dept_ref_number { get; set; }
        public string? appellate_deviation_secondstage_name_designation { get; set; }
        public string? appellate_deviation_secondstage_actiontaken_da { get; set; }
        public string? appellate_deviation_secondstage_actiontaken_aa { get; set; }
        public DateOnly? create_date { get; set; }
        public string? user_id { get; set; }
        public DateOnly? update_date { get; set; }
        public string? last_user_id { get; set; }
        
        public long qpr_id { get; set; }
        public string ip { get; set; }
        public string? deviation_cvc_advice_firststage_name_designation_da { get; set; }
        public string? deviation_cvc_advice_secondstage_name_designation_da { get; set; }
        public adviceofcvcqrs()
        {
            // Initialize all integer and byte properties to 0
            advice_cvc_id = 0;
            advice_cvc_first_casespreviousqtr = 0;
            advice_cvc_first_casesduringqtr = 0;
            advice_cvc_first_adviceduringqtr = 0;
            advice_cvc_first_adviceawaitedcvc = 0;
            advice_cvc_second_casespreviousqtr = 0;
            advice_cvc_second_casesduringqtr = 0;
            advice_cvc_second_adviceduringqtr = 0;
            advice_cvc_second_adviceawaitedcvc = 0;
            advice_cvc_firstreconsider_casespreviousqtr = 0;
            advice_cvc_firstreconsider_casesduringqtr = 0;
            advice_cvc_firstreconsider_adviceduringqtr = 0;
            advice_cvc_firstreconsider_adviceawaitedcvc = 0;
            advice_cvc_secondreconsider_casespreviousqtr = 0;
            advice_cvc_secondreconsider_casesduringqtr = 0;
            advice_cvc_secondreconsider_adviceduringqtr = 0;
            advice_cvc_secondreconsider_adviceawaitedcvc = 0;
            advice_cvc_total_casespreviousqtr = 0;
            advice_cvc_total_casesduringqtr = 0;
            advice_cvc_total_adviceduringqtr = 0;
            advice_cvc_total_adviceawaitedcvc = 0;
            action_cvc_firstmajor_openingbalance = 0;
            action_cvc_firstmajor_adviceduringqtr = 0;
            action_cvc_firstmajor_disposed = 0;
            action_cvc_firstmajor_balancepending = 0;
            action_cvc_firstmajor_greaterone = 0;
            action_cvc_firstmajor_onetothree = 0;
            action_cvc_firstmajor_threetosix = 0;
            action_cvc_firstmajor_lesssix = 0;
            action_cvc_firstminor_openingbalance = 0;
            action_cvc_firstminor_adviceduringqtr = 0;
            action_cvc_firstminor_disposed = 0;
            action_cvc_firstminor_balancepending = 0;
            action_cvc_firstminor_greaterone = 0;
            action_cvc_firstminor_onetothree = 0;
            action_cvc_firstminor_threetosix = 0;
            action_cvc_firstminor_lesssix = 0;
            action_cvc_secondmajor_openingbalance = 0;
            action_cvc_secondmajor_adviceduringqtr = 0;
            action_cvc_secondmajor_disposed = 0;
            action_cvc_secondmajor_balancepending = 0;
            action_cvc_secondmajor_greaterone = 0;
            action_cvc_secondmajor_onetothree = 0;
            action_cvc_secondmajor_threetosix = 0;
            action_cvc_secondmajor_lesssix = 0;
            action_cvc_secondminor_openingbalance = 0;
            action_cvc_secondminor_adviceduringqtr = 0;
            action_cvc_secondminor_disposed = 0;
            action_cvc_secondminor_balancepending = 0;
            action_cvc_secondminor_greaterone = 0;
            action_cvc_secondminor_onetothree = 0;
            action_cvc_secondminor_threetosix = 0;
            action_cvc_secondminor_lesssix = 0;
            action_cvc_total_openingbalance = 0;
            action_cvc_total_adviceduringqtr = 0;
            action_cvc_total_disposed = 0;
            action_cvc_total_balancepending = 0;
            action_cvc_total_greaterone = 0;
            action_cvc_total_onetothree = 0;
            action_cvc_total_threetosix = 0;
            action_cvc_total_lesssix = 0;

            // Initialize all string properties to null or empty string
            deviation_cvc_advice_firststage_typecvcadvice = string.Empty;
            deviation_cvc_advice_firststage_cvcfilenumber = string.Empty;
            deviation_cvc_advice_firststage_dept_ref_number = string.Empty;
            deviation_cvc_advice_firststage_name_designation = string.Empty;
            deviation_cvc_advice_firststage_actiontaken_da = string.Empty;
            deviation_cvc_advice_secondstage_typecvcadvice = string.Empty;
            deviation_cvc_advice_secondstage_cvcfilenumber = string.Empty;
            deviation_cvc_advice_secondstage_dept_ref_number = string.Empty;
            deviation_cvc_advice_secondstage_name_designation = string.Empty;
            deviation_cvc_advice_secondstage_actiontaken_da = string.Empty;
            appellate_deviation_firststage_typecvcadvice = string.Empty;
            appellate_deviation_firststage_cvcfilenumber = string.Empty;
            appellate_deviation_firststage_dept_ref_number = string.Empty;
            appellate_deviation_firststage_name_designation = string.Empty;
            appellate_deviation_firststage_actiontaken_da = string.Empty;
            appellate_deviation_firststage_actiontaken_aa = string.Empty;
            appellate_deviation_secondstage_typecvcadvice = string.Empty;
            appellate_deviation_secondstage_cvcfilenumber = string.Empty;
            appellate_deviation_secondstage_dept_ref_number = string.Empty;
            appellate_deviation_secondstage_name_designation = string.Empty;
            appellate_deviation_secondstage_actiontaken_da = string.Empty;
            appellate_deviation_secondstage_actiontaken_aa = string.Empty;
            create_date = default;  // Default for DateOnly is 0001-01-01
            user_id = string.Empty;
            update_date = default;  // Default for DateOnly is 0001-01-01
            last_user_id = string.Empty;
            qpr_id = 0;
            ip = string.Empty;
            deviation_cvc_advice_firststage_name_designation_da = string.Empty;
            deviation_cvc_advice_secondstage_name_designation_da = string.Empty;
        }
    }
}