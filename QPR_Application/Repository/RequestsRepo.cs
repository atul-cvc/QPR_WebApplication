using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Util;
using System.Text.Json;

namespace QPR_Application.Repository
{
    public class RequestsRepo : IRequestsRepo
    {
        private readonly QPRContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        public RequestsRepo(QPRContext DbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = DbContext;
            _httpContext = httpContext;
        }
        public async Task<List<QPRRequestSubjects>> GetRequestSubjects()
        {
            try
            {
                return await _dbContext.QPRRequestSubjects.AsNoTracking().Where(subject => subject.isActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserRequests>> GetUserRquestsCVO()
        {
            try
            {
                return await GetUserRequests(_httpContext.HttpContext.Session.GetString("UserName"), "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserRequests>> GetUserRquestsSO(string IsActive)
        {
            UserDetails userDetails = new UserDetails();

            var userStr = _httpContext?.HttpContext?.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(userStr))
                throw new InvalidOperationException("Current user details are not available.");
            else
                userDetails.User = JsonSerializer.Deserialize<registration>(userStr);

            List<registration> cvo_users = await _dbContext.registration.AsNoTracking().Where(x => x.organisation == userDetails.User.organisation && x.logintype == "ROLE_CVO").ToListAsync() ?? new List<registration>();

            List<UserRequests> userRequests = new List<UserRequests>();
            for (int i = 0; i < cvo_users.Count; i++)
            {
                List<UserRequests> _userRequests = await GetUserRequests(cvo_users[i].userid, IsActive);
                userRequests.AddRange(_userRequests);
            }
            return userRequests;
        }

        public async Task SaveUserRequest(UserRequests _userRequest)
        {
            try
            {
                if (_userRequest != null)
                {
                    _userRequest.subject = (_dbContext.QPRRequestSubjects.AsNoTracking().FirstOrDefault(item => item.subject_id == _userRequest.subject_id)).subject_name;

                    _userRequest.qtrQuarterName = QPRUtility.quarterItems.FirstOrDefault(q => q.Value == _userRequest.qtrreport.ToString()).Text;
                    _userRequest.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    _userRequest.userid = _httpContext.HttpContext?.Session?.GetString("UserName");
                    _userRequest.created_date = DateTime.Now;
                    _userRequest.isActive = true;
                    _userRequest.isResolved = false;

                    await _dbContext.UserRequests.AddAsync(_userRequest);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Request Data empty. No new request created");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserRequests>> GetUserRequests(string userId, string IsActive)
        {
            List<UserRequests> userRequests = new List<UserRequests>();

            if (!string.IsNullOrEmpty(IsActive))
            {
                userRequests = await _dbContext.UserRequests.AsNoTracking().Where(r => r.userid == userId && r.isActive == Convert.ToBoolean(IsActive)).ToListAsync();
            }
            else
            {
                userRequests = await _dbContext.UserRequests.AsNoTracking().Where(r => r.userid == userId).ToListAsync();
            }

            List<QPRRequestSubjects> subs = await _dbContext.QPRRequestSubjects.AsNoTracking().ToListAsync();
            if (subs.Count > 0)
            {
                foreach (UserRequests userRequest in userRequests)
                {
                    userRequest.subject = subs.FirstOrDefault(item => item.subject_id == userRequest.subject_id).subject_name;
                }
            }
            return userRequests;
        }


        public async Task<UserRequests> GetUserRquestById(int request_id)
        {
            UserRequests _userReq = await _dbContext.UserRequests.AsNoTracking().FirstOrDefaultAsync(r => r.request_id == request_id) ?? new UserRequests();
            return _userReq;
        }
    }
}
