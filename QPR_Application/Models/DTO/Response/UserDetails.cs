using QPR_Application.Models.Entities;

namespace QPR_Application.Models.DTO.Response
{
    public class UserDetails
    {
        public registration User {  get; set; }
        public tbl_MasterMinistryNew OrgDetails {  get; set; }
        public orgadd OrgDetails_ADD {  get; set; }
    }
}
