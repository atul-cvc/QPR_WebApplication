using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;

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
            //return new List<QPRRequestSubjects>();
        }

        public async Task SaveUserRequest(UserRequests _userRequest)
        {
            try
            {
                if (_userRequest == null)
                {
                    _userRequest.ip = _httpContext.HttpContext?.Session?.GetString("ipAddress");
                    _userRequest.userid = _httpContext.HttpContext?.Session?.GetString("UserName");
                    _userRequest.created_date = DateTime.Now;
                    var offsetTime = DateTimeOffset.Now;
                    _userRequest.isActive = true;
                    _userRequest.isResolved = false;

                    await _dbContext.UserRequests.AddAsync(_userRequest);
                    await _dbContext.SaveChangesAsync();
                } else
                {
                    throw new Exception("Request Data empty. No new request created");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
