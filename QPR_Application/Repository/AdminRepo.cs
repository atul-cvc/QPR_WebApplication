﻿using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;

namespace QPR_Application.Repository
{
    public class AdminRepo : IAdminRepo
    {
        private readonly QPRContext _dbContext;
        public AdminRepo(QPRContext DbContext)
        {
            _dbContext = DbContext;
        }
        
    }
}
