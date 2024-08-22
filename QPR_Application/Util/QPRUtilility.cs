using Microsoft.Data.SqlClient;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Repository;
using System.Data;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics;

namespace QPR_Application.Util
{
    public class QPRUtilility 
    {
        private readonly IQprRepo _qprRepo;
        private readonly IHttpContextAccessor _httpContext;
        private string _connString = string.Empty;
        public QPRUtilility(IQprRepo qprRepo, IHttpContextAccessor httpContext, IConfiguration config)
        {
            _qprRepo = qprRepo;
            _httpContext = httpContext;
            _connString = config.GetConnectionString("SQLConnection") ?? String.Empty;
        }
        public string GetPreviousReferenceNumber()
        {
            return _qprRepo.GetPreviousReferenceNumber(
                        _httpContext.HttpContext.Session.GetString("UserName"),
                        _httpContext.HttpContext.Session.GetString("qtryear"),
                        _httpContext.HttpContext.Session.GetString("qtrreport")
                        );
        }

        public List<TypesAdviceCVC> GetStageTypesAdviceCVC()
        {
            List<TypesAdviceCVC> stageList = new List<TypesAdviceCVC>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("GetStageTypesAdviceCVC", conn);
                    cmd.CommandType = CommandType.StoredProcedure;                   

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new TypesAdviceCVC
                            {
                                Name = reader["name"].ToString(), // Cast to string
                                Type = Convert.ToInt32(reader["type"])
                            };
                            stageList.Add(item);
                        }
                    }
                    cmd.Dispose();
                    return stageList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public List<string> GetNatureTypesAdviceCVC(string stageName)
        {
            List<string> natureList = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand("GetNatureOfAdviceCVC", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StageName", stageName);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = reader["name"].ToString();                            
                            natureList.Add(item ?? "");
                        }
                    }
                    cmd.Dispose();
                }
                return natureList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            try
            {
                string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/QPRUploads");

                // Ensure the uploads folder exists
                if (!Directory.Exists(_uploadsFolder))
                {
                    Directory.CreateDirectory(_uploadsFolder);
                }
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("No file provided.");
                }
                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(_uploadsFolder, fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                //return new string("");
                return $"/QPRUploads/{fileName}";
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
