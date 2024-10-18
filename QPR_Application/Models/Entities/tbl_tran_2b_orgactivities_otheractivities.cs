using QPR_Application.Models.DTO.Utility;

namespace QPR_Application.Models.Entities
{
    public class tbl_tran_2b_orgactivities_otheractivities : CommonModel
    {
          public int VAW_Year { get; set; }
          public string UniqueTransactionId {get;set;}
          public string CvoOrgCode {get;set;}
          public string CvoId {get;set;}
          public DateTime DateOfActivity {get;set;}
          public string DistributionOfPamphletsAndBanners_Details {get;set;}
          public string ConductOfWorkshopAndSensitizationProgram_Details {get;set;}
          public string IssueOfJornalAndNwesletter_Details {get;set;}
          public string AnyOtherActivities_Details {get;set;}
    }
}
