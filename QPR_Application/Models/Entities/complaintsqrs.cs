﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QPR_Application.Models.Entities
{
    public partial class complaintsqrs
    {
        [Key]
        public int complaints_id { get; set; }
        public int? comcvcopeningbalance { get; set; }
        public int? comcvcreceived_during_quarter { get; set; }
        public int? comcvctotal { get; set; }
        public int? comcvcdisposal_during_quarter { get; set; }
        public int? comcvcbalance_pending { get; set; }
        public int? comcvcage_less_one { get; set; }
        public int? comcvcage_pendency_betweenone_three { get; set; }
        public int? comcvcage_pendency_betweenthree_six { get; set; }
        public int? comotheropeningbalance { get; set; }
        public int? comotherreceived_during_quarter { get; set; }
        public int? comothertotal { get; set; }
        public int? comotherdisposal_during_quarter { get; set; }
        public int? comotherbalance_pending { get; set; }
        public int? comotherage_less_one { get; set; }
        public int? comotherage_pendency_betweenone_three { get; set; }
        public int? comotherage_pendency_betweenthree_six { get; set; }
        public int? comtotalopeningbalance { get; set; }
        public int? comtotalreceived_during_quarter { get; set; }
        public int? comtotal { get; set; }
        public int? comtotaldisposal_during_quarter { get; set; }
        public int? comtotalbalance_pending { get; set; }
        public int? comtotalage_less_one { get; set; }
        public int? comtotalage_pendency_betweenone_three { get; set; }
        public int? comtotalage_pendency_betweenthree_six { get; set; }
        public int? complaints_signed_complaints { get; set; }
        public int? complaints_department { get; set; }
        public int? complaints_cbi { get; set; }
        public int? complaints_other { get; set; }
        public int? complaints_audit { get; set; }
        public int? complaints_detected { get; set; }
        public int? complaints_cbi_action { get; set; }
        public int? complaints_cvoinvestigation { get; set; }
        public int? complaints_signed_filed { get; set; }
        public int? complaints_signed_notconfirmed { get; set; }
        public int? complaints_actionfornovigilance { get; set; }
        public int? cvcpidpi_opening_balance { get; set; }
        public int? cvcpidpi_receivequarter { get; set; }
        public int? cvcpidpi_total { get; set; }
        public int? cvcpidpi_report_quarter { get; set; }
        public int? cvcpidpi_balance_pending { get; set; }
        public int? cvcpidpi_three_month { get; set; }
        public int? cvcpidpi_three_to_six { get; set; }
        public int? cvcpidpi_less_six { get; set; }
        public int? otherpidpi_opening_balance { get; set; }
        public int? otherpidpi_receivequarter { get; set; }
        public int? otherpidpi_total { get; set; }
        public int? otherpidpi_report_quarter { get; set; }
        public int? otherpidpi_balance_pending { get; set; }
        public int? otherpidpi_three_month { get; set; }
        public int? otherpidpi_three_to_six { get; set; }
        public int? otherpidpi_less_six { get; set; }
        public int? totalpidpi_opening_balance { get; set; }
        public int? totalpidpi_receivequarter { get; set; }
        public int? totalpidpi_total { get; set; }
        public int? totalpidpi_report_quarter { get; set; }
        public int? totalpidpi_balance_pending { get; set; }
        public int? toatlpidpi_three_month { get; set; }
        public int? toatlpidpi_three_to_six { get; set; }
        public int? totalpidpi_less_six { get; set; }
        public int? cvcpidpiinvestadvicecvc { get; set; }
        public int? cvcpidpiinvesquarterreportbycvo { get; set; }
        public int? cvcpidpiinvesquarteradvicereceive { get; set; }
        public int? cvcpidpiinvestotaladvicereceive { get; set; }
        public int? cvcpidpiinvesactionduringquarter { get; set; }
        public int? cvcpidpiinvesbalancegreterthreemonth { get; set; }
        public int? cvcpidpiinvesbalancebetweenthreetosix { get; set; }
        public int? cvcpidpiinvesbalancebetweensixtotwelve { get; set; }
        public int? cvcpidpiinvesbalancelesstwelve { get; set; }
        public int? cvopidpiinvestadvicecvc { get; set; }
        public int? cvopidpiinvesquarterreportbycvo { get; set; }
        public int? cvopidpiinvesquarteradvicereceive { get; set; }
        public int? cvopidpiinvestotaladvicereceive { get; set; }
        public int? cvopidpiinvesactionduringquarter { get; set; }
        public int? cvopidpiinvesbalancegreterthreemonth { get; set; }
        public int? cvopidpiinvesbalancebetweenthreetosix { get; set; }
        public int? cvopidpiinvesbalancebetweensixtotwelve { get; set; }
        public int? cvopidpiinvesbalancelesstwelve { get; set; }
        public int? totalpidpiinvestadvicecvc { get; set; }
        public int? totalpidpiinvesquarterreportbycvo { get; set; }
        public int? totalpidpiinvesquarteradvicereceive { get; set; }
        public int? totalpidpiinvestotaladvicereceive { get; set; }
        public int? totalpidpiinvesactionduringquarter { get; set; }
        public int? totalpidpiinvesbalancegreterthreemonth { get; set; }
        public int? totalpidpiinvesbalancebetweenthreetosix { get; set; }
        public int? totalpidpiinvesbalancebetweensixtotwelve { get; set; }
        public int? totalpidpiinvesbalancelesstwelve { get; set; }
        public int? napidpibroughtforward { get; set; }
        public int? napidpireceiveqtr { get; set; }
        public int? napidpitotal { get; set; }
        public int? napidpiclosedwithoutaction { get; set; }
        public int? napidpifurtherinvestigation { get; set; }
        public int? napidpiadminaction { get; set; }
        public int? napidpiimpositionpenalty { get; set; }
        public int? napidpidisposedqtr { get; set; }
        public int? napidpipendingqtr { get; set; }
        public int? scrutinyreportbfpreviousyear { get; set; }
        public int? scrutinyreportexaminedqtr { get; set; }
        public int? scrutinyreportidentifiedexami { get; set; }
        public int? scrutinyreportcompleteinvestigation { get; set; }
        public int? scrutinyreportpendinginvestigation { get; set; }
        [StringLength(50)]
        public string create_date { get; set; }
        [StringLength(50)]
        public string user_id { get; set; }
        [StringLength(50)]
        public string update_date { get; set; }
        [StringLength(50)]
        public string used_ip { get; set; }
        [StringLength(50)]
        public string update_user_id { get; set; }
        public long? qpr_id { get; set; }
        public string cvcpidpi_delay_one_month { get; set; }
        public string otherpidpi_delay_one_month { get; set; }
        public string toatlpidpi_delay_one_month { get; set; }
        public int? scrutinyreportbfpreviousyearconcurrent { get; set; }
        public int? scrutinyreportexaminedqtrconcurrent { get; set; }
        public int? scrutinyreportidentifiedexamiconcurrent { get; set; }
        public int? scrutinyreportcompleteinvestigationconcurrent { get; set; }
        public int? scrutinyreportpendinginvestigationconcurrent { get; set; }
        public int? scrutinyreportbfpreviousyearinternal { get; set; }
        public int? scrutinyreportexaminedqtrinternal { get; set; }
        public int? scrutinyreportidentifiedexamiinternal { get; set; }
        public int? scrutinyreportcompleteinvestigationinternal { get; set; }
        public int? scrutinyreportpendinginvestigationinternal { get; set; }
        public int? scrutinyreportbfpreviousyearstatutory { get; set; }
        public int? scrutinyreportexaminedqtrstatutory { get; set; }
        public int? scrutinyreportidentifiedexamistatutory { get; set; }
        public int? scrutinyreportcompleteinvestigationstatutory { get; set; }
        public int? scrutinyreportpendinginvestigationstatutory { get; set; }
        public int? scrutinyreportbfpreviousyearothers { get; set; }
        public int? scrutinyreportexaminedqtrothers { get; set; }
        public int? scrutinyreportidentifiedexamiothers { get; set; }
        public int? scrutinyreportcompleteinvestigationothers { get; set; }
        public int? scrutinyreportpendinginvestigationothers { get; set; }
        public int? scrutinyreportbfpreviousyeartotal { get; set; }
        public int? scrutinyreportexaminedqtrtotal { get; set; }
        public int? scrutinyreportidentifiedexamitotal { get; set; }
        public int? scrutinyreportcompleteinvestigationtotal { get; set; }
        public int scrutinyreportpendinginvestigationtotal { get; set; }

        // Constructor
        public complaintsqrs()
        {
            // Initialize all integer properties to 0
            //complaints_id = 0;
            comcvcopeningbalance = 0;
            comcvcreceived_during_quarter = 0;
            comcvctotal = 0;
            comcvcdisposal_during_quarter = 0;
            comcvcbalance_pending = 0;
            comcvcage_less_one = 0;
            comcvcage_pendency_betweenone_three = 0;
            comcvcage_pendency_betweenthree_six = 0;
            comotheropeningbalance = 0;
            comotherreceived_during_quarter = 0;
            comothertotal = 0;
            comotherdisposal_during_quarter = 0;
            comotherbalance_pending = 0;
            comotherage_less_one = 0;
            comotherage_pendency_betweenone_three = 0;
            comotherage_pendency_betweenthree_six = 0;
            comtotalopeningbalance = 0;
            comtotalreceived_during_quarter = 0;
            comtotal = 0;
            comtotaldisposal_during_quarter = 0;
            comtotalbalance_pending = 0;
            comtotalage_less_one = 0;
            comtotalage_pendency_betweenone_three = 0;
            comtotalage_pendency_betweenthree_six = 0;
            complaints_signed_complaints = 0;
            complaints_department = 0;
            complaints_cbi = 0;
            complaints_other = 0;
            complaints_audit = 0;
            complaints_detected = 0;
            complaints_cbi_action = 0;
            complaints_cvoinvestigation = 0;
            complaints_signed_filed = 0;
            complaints_signed_notconfirmed = 0;
            complaints_actionfornovigilance = 0;
            cvcpidpi_opening_balance = 0;
            cvcpidpi_receivequarter = 0;
            cvcpidpi_total = 0;
            cvcpidpi_report_quarter = 0;
            cvcpidpi_balance_pending = 0;
            cvcpidpi_three_month = 0;
            cvcpidpi_three_to_six = 0;
            cvcpidpi_less_six = 0;
            otherpidpi_opening_balance = 0;
            otherpidpi_receivequarter = 0;
            otherpidpi_total = 0;
            otherpidpi_report_quarter = 0;
            otherpidpi_balance_pending = 0;
            otherpidpi_three_month = 0;
            otherpidpi_three_to_six = 0;
            otherpidpi_less_six = 0;
            totalpidpi_opening_balance = 0;
            totalpidpi_receivequarter = 0;
            totalpidpi_total = 0;
            totalpidpi_report_quarter = 0;
            totalpidpi_balance_pending = 0;
            toatlpidpi_three_month = 0;
            toatlpidpi_three_to_six = 0;
            totalpidpi_less_six = 0;
            cvcpidpiinvestadvicecvc = 0;
            cvcpidpiinvesquarterreportbycvo = 0;
            cvcpidpiinvesquarteradvicereceive = 0;
            cvcpidpiinvestotaladvicereceive = 0;
            cvcpidpiinvesactionduringquarter = 0;
            cvcpidpiinvesbalancegreterthreemonth = 0;
            cvcpidpiinvesbalancebetweenthreetosix = 0;
            cvcpidpiinvesbalancebetweensixtotwelve = 0;
            cvcpidpiinvesbalancelesstwelve = 0;
            cvopidpiinvestadvicecvc = 0;
            cvopidpiinvesquarterreportbycvo = 0;
            cvopidpiinvesquarteradvicereceive = 0;
            cvopidpiinvestotaladvicereceive = 0;
            cvopidpiinvesactionduringquarter = 0;
            cvopidpiinvesbalancegreterthreemonth = 0;
            cvopidpiinvesbalancebetweenthreetosix = 0;
            cvopidpiinvesbalancebetweensixtotwelve = 0;
            cvopidpiinvesbalancelesstwelve = 0;
            totalpidpiinvestadvicecvc = 0;
            totalpidpiinvesquarterreportbycvo = 0;
            totalpidpiinvesquarteradvicereceive = 0;
            totalpidpiinvestotaladvicereceive = 0;
            totalpidpiinvesactionduringquarter = 0;
            totalpidpiinvesbalancegreterthreemonth = 0;
            totalpidpiinvesbalancebetweenthreetosix = 0;
            totalpidpiinvesbalancebetweensixtotwelve = 0;
            totalpidpiinvesbalancelesstwelve = 0;
            napidpibroughtforward = 0;
            napidpireceiveqtr = 0;
            napidpitotal = 0;
            napidpiclosedwithoutaction = 0;
            napidpifurtherinvestigation = 0;
            napidpiadminaction = 0;
            napidpiimpositionpenalty = 0;
            napidpidisposedqtr = 0;
            napidpipendingqtr = 0;
            scrutinyreportbfpreviousyear = 0;
            scrutinyreportexaminedqtr = 0;
            scrutinyreportidentifiedexami = 0;
            scrutinyreportcompleteinvestigation = 0;
            scrutinyreportpendinginvestigation = 0;
            scrutinyreportbfpreviousyearconcurrent = 0;
            scrutinyreportexaminedqtrconcurrent = 0;
            scrutinyreportidentifiedexamiconcurrent = 0;
            scrutinyreportcompleteinvestigationconcurrent = 0;
            scrutinyreportpendinginvestigationconcurrent = 0;
            scrutinyreportbfpreviousyearinternal = 0;
            scrutinyreportexaminedqtrinternal = 0;
            scrutinyreportidentifiedexamiinternal = 0;
            scrutinyreportcompleteinvestigationinternal = 0;
            scrutinyreportpendinginvestigationinternal = 0;
            scrutinyreportbfpreviousyearstatutory = 0;
            scrutinyreportexaminedqtrstatutory = 0;
            scrutinyreportidentifiedexamistatutory = 0;
            scrutinyreportcompleteinvestigationstatutory = 0;
            scrutinyreportpendinginvestigationstatutory = 0;
            scrutinyreportbfpreviousyearothers = 0;
            scrutinyreportexaminedqtrothers = 0;
            scrutinyreportidentifiedexamiothers = 0;
            scrutinyreportcompleteinvestigationothers = 0;
            scrutinyreportpendinginvestigationothers = 0;
            scrutinyreportbfpreviousyeartotal = 0;
            scrutinyreportexaminedqtrtotal = 0;
            scrutinyreportidentifiedexamitotal = 0;
            scrutinyreportcompleteinvestigationtotal = 0;
            scrutinyreportpendinginvestigationtotal = 0;

            // Initialize string fields to empty
            create_date = string.Empty;
            user_id = string.Empty;
            update_date = string.Empty;
            used_ip = string.Empty;
            update_user_id = string.Empty;
            cvcpidpi_delay_one_month = string.Empty;
            otherpidpi_delay_one_month = string.Empty;
            toatlpidpi_delay_one_month = string.Empty;
        }

    }
}