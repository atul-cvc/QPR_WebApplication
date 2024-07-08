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
    public partial class preventivevigilanceqrs
    {
        public int preventive_vigilance_id { get; set; }
        public int preventivevig_bycvo_periodic_end_previous_qtr { get; set; }
        public int preventivevig_bycvo_periodic_during_qtr { get; set; }
        public int preventivevig_bycvo_periodic_system_improvement { get; set; }
        public int preventivevig_bycvo_periodic_recovery_effected { get; set; }
        public int preventivevig_bycvo_surprise_end_previous_qtr { get; set; }
        public int preventivevig_bycvo_surprise_during_qtr { get; set; }
        public int preventivevig_bycvo_surprise_system_improvement { get; set; }
        public int preventivevig_bycvo_surprise_recovery_effected { get; set; }
        public int preventivevig_bycvo_majorwork_end_previous_qtr { get; set; }
        public int preventivevig_bycvo_majorwork_during_qtr { get; set; }
        public int preventivevig_bycvo_majorwork_system_improvement { get; set; }
        public int preventivevig_bycvo_majorwork_recovery_effected { get; set; }
        public int preventivevig_bycvo_scrutiny_file_end_previous_qtr { get; set; }
        public int preventivevig_bycvo_scrutiny_file_during_qtr { get; set; }
        public int preventivevig_bycvo_scrutiny_file_system_improvement { get; set; }
        public int preventivevig_bycvo_scrutiny_file_recovery_effected { get; set; }
        public int preventivevig_bycvo_scrutiny_property_end_previous_qtr { get; set; }
        public int preventivevig_bycvo_scrutiny_property_during_qtr { get; set; }
        public int preventivevig_bycvo_scrutiny_property_system_improvement { get; set; }
        public int preventivevig_bycvo_scrutiny_property_recovery_effected { get; set; }
        public int preventivevig_bycvo_audit_reports_end_previous_qtr { get; set; }
        public int preventivevig_bycvo_audit_reports_during_qtr { get; set; }
        public int preventivevig_cvc_audit_reports_system_improvement { get; set; }
        public int preventivevig_bycvo_audit_reports_recovery_effected { get; set; }
        public int preventivevig_bycvo_training_programs_end_previous_qtr { get; set; }
        public int preventivevig_bycvo_training_programs_during_qtr { get; set; }
        public int preventivevigi_bycvo_training_programs_system_improvement { get; set; }
        public int preventivevigi_bycvo_training_programs_recovery_effected { get; set; }
        public int preventivevigi_bycvo_system_improvements_end_previous_qtr { get; set; }
        public int preventivevigi_bycvo_system_improvements_during_qtr { get; set; }
        public int preventivevigi_bycvo_system_improvements_system_improvement { get; set; }
        public int preventivevigi_bycvo_system_improvements_recovery_effected { get; set; }
        public int preventivevigi_management_job_rotation_sensitivenumberpost { get; set; }
        public int preventivevigi_management_job_rotation_postduerotation { get; set; }
        public int preventivevigi_management_job_rotation_post_qtrrotation { get; set; }
        public int preventivevigi_management_job_rotation_postnotrotated { get; set; }
        public int preventivevigi_management_frj_numberofficer_covered { get; set; }
        public int preventivevigi_management_frj_reviews_undertaken { get; set; }
        public int preventivevigi_management_frj_case_under_fr { get; set; }
        public string preventivevigi_management_detailsvig_a_serial_number { get; set; }
        public string preventivevigi_management_detailsvig_a_nameofsub { get; set; }
        public string preventivevigi_management_detailsvig_a_staff_appointed { get; set; }
        public string preventivevigi_management_detailsvig_a_methodofcontrol { get; set; }
        public string preventivevigi_management_detailsvig_b_serial_number { get; set; }
        public string preventivevigi_management_detailsvig_b_nameofsub { get; set; }
        public string preventivevigi_management_detailsvig_b_contral_method { get; set; }
        public string preventivevigi_management_detailsvig_b_likely_time { get; set; }
        public string preventivevigi_whether_agreed_list { get; set; }
        public string preventivevigi_whether_agreed_list_date { get; set; }
        public string preventivevigi_whether_officer_list { get; set; }
        public string preventivevigi_whether_officer_list_date { get; set; }
        public string preventivevigi_whether_annual_property { get; set; }
        public string preventivevigi_whether_any_information { get; set; }
        public bool? preventivevigi_whether_data_relating { get; set; }
        [Column(TypeName = "date")]
        public DateTime? preventivevigi_whether_data_relating_date { get; set; }
        public byte preventivevigi_other_it_epayment { get; set; }
        public byte preventivevigi_other_it_etendering { get; set; }
        public byte preventivevigi_other_it_contracts { get; set; }
        public byte preventivevigi_other_it_emarketplace { get; set; }
        public bool? preventivevigi_other_qpr_cte { get; set; }
        [Column(TypeName = "date")]
        public DateTime? preventivevigi_other_qpr_cte_date { get; set; }
        public bool? preventivevigi_other_review_vigi { get; set; }
        [Column(TypeName = "date")]
        public DateTime? preventivevigi_other_review_vigi_date { get; set; }
        public bool? preventivevigi_other_structured_meeting { get; set; }
        [Column(TypeName = "date")]
        public DateTime? preventivevigi_other_structured_meeting_date { get; set; }
        public bool? preventivevigi_other_report_implementation { get; set; }
        public bool? preventivevigi_other_application_being { get; set; }
        public bool? preventivevigi_other_change_tech { get; set; }
        public bool? preventivevigi_other_e_learning { get; set; }
        public bool? preventivevigi_other_pending_disciplinary { get; set; }
        public bool? preventivevigi_other_cvo_deputed { get; set; }
        public bool? preventivevigi_other_visit_cvo { get; set; }
        public bool? preventivevigi_other_prior_approval { get; set; }
        public int? preventivevigi_other_expenditure { get; set; }
        public bool? preventivevigi_other_tour_details { get; set; }
        public bool? preventivevigi_other_guidelines_appointment { get; set; }
        public string preventivevigi_other_victimisation_vigilance { get; set; }
        public string preventivevigi_other_secrcy_password { get; set; }
        [StringLength(1)]
        public string create_date { get; set; }
        [StringLength(1)]
        public string user_id { get; set; }
        [StringLength(1)]
        public string update_date { get; set; }
        [StringLength(1)]
        public string last_user_id { get; set; }
        public short qpr_id { get; set; }
        public string ip { get; set; }
        public string preventivevigi_management_job_rotation_reasons { get; set; }
        public string preventivevigi_management_frj_action_taken { get; set; }
        public string file1 { get; set; }
    }
}