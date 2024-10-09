using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;

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

        public Boolean CreateUser(registration registerUser)
        {
            Boolean status = false;
            try
            {
                _dbContext.registration.Add(registerUser);
                _dbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void EditUser(registration user)
        {
            //throw new NotImplementedException();
            try
            {
                _dbContext.registration.Update(user);
                _dbContext.SaveChanges();
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
                        parttime = user.parttime ?? "N/A"
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

        public async Task<registration> GetEditUserDetails(string Id)
        {
            try
            {
                registration user = await _dbContext.registration.FirstOrDefaultAsync(u => u.usercode == Convert.ToInt64(Id));
                return user;
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

    }
}
