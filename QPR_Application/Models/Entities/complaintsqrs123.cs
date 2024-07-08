﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QPR_Application.Models.Entities
{
    public partial class complaintsqrs123
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
        [Column(TypeName = "date")]
        public DateTime? create_date { get; set; }
        public int? user_id { get; set; }
        [Column(TypeName = "date")]
        public DateTime? update_date { get; set; }
        public string used_ip { get; set; }
        [StringLength(100)]
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
        public int? scrutinyreportpendinginvestigationtotal { get; set; }
    }
}