using Humanizer;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using QPR_Application.Models.DTO.Utility;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;
using QPR_Application.Util;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;

namespace QPR_Application.Repository
{
    public class ManageUserRepo : IManageUserRepo
    {
        private readonly ILogger<ManageQprRepo> _logger;
        private readonly QPRContext _dbContext;
        public ManageUserRepo(ILogger<ManageQprRepo> logger, QPRContext DbContext)
        {
            _logger = logger;
            _dbContext = DbContext;
        }

        public async Task<Boolean> CreateUser(CreateUserViewModel createUserVM)
        {
            Boolean status = false;
            try
            {
                // Hash the password
                var HS = new PasswordHashingUtil().CreateHashWithSalt(createUserVM.userid + "@1234", 64, SHA512.Create());

                // Prepare parameters
                var parameters = new[]
                {
                    new SqlParameter("@userid", createUserVM.userid),
                    new SqlParameter("@email", createUserVM.email),
                    new SqlParameter("@name", createUserVM.name),
                    new SqlParameter("@logintype", createUserVM.logintype),
                    new SqlParameter("@loginroll", createUserVM.loginroll),
                    new SqlParameter("@desiganation", createUserVM.desiganation),
                    new SqlParameter("@tenure", createUserVM.tenure),
                    new SqlParameter("@bofficercode", string.IsNullOrEmpty(createUserVM.bofficercode) ? String.Empty : createUserVM.bofficercode),
                    new SqlParameter("@cvocode", createUserVM.OrgCode),
                    new SqlParameter("@mobilenumber", createUserVM.mobilenumber),
                    new SqlParameter("@islocked", createUserVM.islocked),
                    new SqlParameter("@cvocontactnumberoffice", createUserVM.cvocontactnumberoffice),
                    new SqlParameter("@fulltime", createUserVM.EmploymentType == "FullTime" ? "t" : "f"),
                    new SqlParameter("@parttime", createUserVM.EmploymentType == "PartTime" ? "t" : "f"),
                    new SqlParameter("@password", HS.Digest),
                    new SqlParameter("@PasswordSalt", HS.Salt),
                    new SqlParameter("@status", "t"),
                    new SqlParameter("@organisation", await GetOrganisationNameByOrgCode(createUserVM.OrgCode)),
                    new SqlParameter("@createdate", DateTime.Now.ToString()),
                    new SqlParameter("@lastmodified", DateTime.Now.ToString()),
                    new SqlParameter("@firstlogin", "1"),
                };

                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC CreateUser @userid, @email, @name, @logintype, @loginroll, @desiganation, @tenure, @bofficercode, @cvocode, @mobilenumber, @islocked, @cvocontactnumberoffice, @fulltime, @parttime, @password, @PasswordSalt, @status, @organisation, @createdate, @lastmodified, @firstlogin", parameters);

                //return true;
                status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "New user creation failed.");
                status = false;
            }
            return status;
        }
        public async Task EditUser(EditUserViewModel User)
        {
            //throw new NotImplementedException();
            try
            {
                // Prepare parameters
                var parameters = new[]
                {
                    new SqlParameter("@UserCode", User.UserCode),
                    new SqlParameter("@UserId", User.UserId),
                    new SqlParameter("@Name", User.Name),
                    new SqlParameter("@Email", User.Email),
                    new SqlParameter("@Organisation", await GetOrganisationNameByOrgCode(User.OrgCode)),
                    new SqlParameter("@LoginType", User.LoginType),
                    new SqlParameter("@LoginRoll", User.LoginRoll),
                    new SqlParameter("@Designation", (object)User.Designation ?? DBNull.Value),
                    new SqlParameter("@Tenure", (object)User.Tenure ?? DBNull.Value),
                    new SqlParameter("@BoOfficerCode", User.BoOfficerCode),
                    new SqlParameter("@Cvocode", User.OrgCode),
                    new SqlParameter("@MobileNumber", User.MobileNumber),
                    new SqlParameter("@IsLocked", User.IsLocked),
                    new SqlParameter("@PostId", (object)User.PostId ?? DBNull.Value),
                    new SqlParameter("@Status", User.Status),
                    new SqlParameter("@CvocontactNumberOffice", User.CvocontactNumberOffice),
                    new SqlParameter("@fulltime", User.EmploymentType.Trim().ToLower() == "full time" ? "t" : "f"),
                    new SqlParameter("@parttime", User.EmploymentType.Trim().ToLower() == "part time" ? "t" : "f"),

                };

                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.EditUser @UserCode, @UserId, @Name, @Email, @Organisation, " +
                    "@LoginType, @LoginRoll, @Designation, @Tenure, @BoOfficerCode, @Cvocode, @MobileNumber, " +
                    "@IsLocked, @PostId, @Status, @CvocontactNumberOffice, @fulltime, @parttime",
                    parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User edit failed");
            }
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsers()
        {
            try
            {
                List<AllUsersViewModel> usersViewModel = new List<AllUsersViewModel>();
                List<registration> user = await _dbContext.registration.ToListAsync();
                for (int i = 0; i < user.Count(); i++)
                {
                    usersViewModel.Add(new AllUsersViewModel
                    {
                        UserCode = user[i].usercode,
                        UserId = user[i].userid,
                        Name = user[i].name,
                        Email = user[i].email,
                        Organisation = user[i].organisation,
                        Logintype = user[i].logintype,
                        Designation = user[i].desiganation,
                        LastModified = user[i].lastmodified,
                        Status = user[i].status,
                        CvoContactNumberOffice = user[i].cvocontactnumberoffice,
                        EmployeeType = GetEmployeeType(user[i].fulltime, user[i].parttime)
                    });
                }
                return usersViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fetching all users list failed.");
                return Enumerable.Empty<AllUsersViewModel>();
            }
        }

        public async Task<IEnumerable<registration>> GetAllUsersForPasswordHashing()
        {
            try
            {
                return await _dbContext.registration.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fetching all users list failed.");
                return new List<registration>();
            }
        }
        public async Task<AllUsersViewModel> GetUserDetails(string Id)
        {
            try
            {
                registration user = await _dbContext.registration.FirstOrDefaultAsync(u => u.usercode == Convert.ToInt64(Id));

                if (user != null)
                {
                    AllUsersViewModel vm = new AllUsersViewModel
                    {
                        UserCode = user.usercode,
                        UserId = user.userid,
                        Name = user.name,
                        Email = user.email,
                        Organisation = user.organisation,
                        Logintype = user.logintype,
                        Designation = user.desiganation,
                        LastModified = user.lastmodified,
                        Status = user.status == "t" ? "Active" : "Inactive",
                        EmployeeType = GetEmployeeType(user.fulltime, user.parttime),
                        createdate = user.createdate,
                        tenure = user.tenure,
                        bofficercode = user.bofficercode,
                        cvocode = user.cvocode,
                        mobilenumber = user.mobilenumber,
                        islocked = user.islocked,
                        postid = user.postid,
                        firstlogin = user.firstlogin,
                        fulltime = user.fulltime ?? "N/A",
                        parttime = user.parttime ?? "N/A",
                        CvoContactNumberOffice = user.cvocontactnumberoffice
                    };
                    return vm;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fetching user details failed.");
            }
            return null;
        }

        public async Task<EditUserViewModel> GetEditUserDetails(string Id)
        {
            try
            {
                registration User = await _dbContext.registration.FirstOrDefaultAsync(u => u.usercode == Convert.ToInt64(Id));

                EditUserViewModel viewModel = new EditUserViewModel
                {
                    UserCode = User.usercode,
                    UserId = User.userid,
                    Name = User.name,
                    Email = User.email,
                    OrgName = User.organisation,
                    LoginType = User.logintype,
                    LoginRoll = User.loginroll,
                    Designation = User.desiganation,
                    Tenure = User.tenure,
                    BoOfficerCode = User.bofficercode,
                    Cvocode = User.cvocode,
                    MobileNumber = User.mobilenumber,
                    IsLocked = User.islocked,
                    PostId = User.postid,
                    Status = User.status,
                    CvocontactNumberOffice = User.cvocontactnumberoffice,
                    EmploymentType = GetEmployeeType(User.fulltime, User.parttime)
                };
                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fetching user details failed.");
            }
            return null;
        }

        public string GetEmployeeType(string fulltime, string parttime)
        {
            if (!String.IsNullOrEmpty(fulltime) && !String.IsNullOrEmpty(parttime))
            {
                if (fulltime == "t")
                    return "Full time";
                if (parttime == "t")
                    return "Part time";
            }
            return "N/A";
        }

        public async Task<string> GetOrganisationNameByOrgCode(string orgCode)
        {
            string orgName = string.Empty;
            orgadd org = await _dbContext.orgadd.AsNoTracking().FirstOrDefaultAsync(i => i.orgcod == orgCode);
            orgName = org.orgnam1;
            return orgName;
        }
    }
}
