using System.ComponentModel.DataAnnotations;

namespace QPR_Application.Models.Entities
{
    public class tbl_MasterMinistryNew
    {
        [Key]
        public long Id { get; set; }
        public string MinName { get; set; }
        public string OrgName { get; set; }
        public string? BranchOffice { get; set; }
        public string OrgCode { get; set; }
        public string? MinFileHead { get; set; }
        public string? OrgFileHead { get; set; }
        public int? Section { get; set; }
        public string? Category { get; set; }
        public string? BOCode { get; set; }
        public bool? IsDeleted { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; }
        public string? createdbyIP { get; set; }
        public string? updatedby { get; set; }
        public DateTime? updatedon { get; set; }
        public string? updatedbyIP { get; set; }
        public string? SessionID { get; set; }
        public int? PrevId { get; set; }
        public int? Sno { get; set; }
        public string? AS_CODE { get; set; }
        public string? Remarks { get; set; }
        public string? OrgCode_Old { get; set; }
        public string? BOCODE_27022024 { get; set; }
    }
}
