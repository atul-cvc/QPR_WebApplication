﻿using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;

namespace QPR_Application.Repository
{
    public class ComplaintsRepo : IComplaintsRepo
    {
        private readonly QPRContext _dbContext;
        public ComplaintsRepo(QPRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<complaintsqrs> GetComplaintsData(string refNum)
        {
            try
            {
                return await _dbContext.complaintsqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<viginvestigationqrs> GetVigilanceInvestigationData(string refNum)
        {
            try
            {
                return await _dbContext.viginvestigationqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<ProsecutionSanctionsViewModel?> GetProsecutionSanctionsViewData(string refNum)        
        {
            try
            {
                prosecutionsanctionsqrs prsSec = await GetProsecutionSanctionsData(refNum);
                List<agewisependency> ageWise = await _dbContext.agewisependency.AsNoTracking().Where(i => i.qpr_id == Convert.ToInt64(refNum)).ToListAsync(); ;
                
                ProsecutionSanctionsViewModel proSecViewModel = new ProsecutionSanctionsViewModel();
                proSecViewModel.Prosecutionsanctionsqrs = prsSec;
                proSecViewModel.Agewisependency = ageWise;

                return proSecViewModel;
                //return await _dbContext.prosecutionsanctionsqrs.FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<prosecutionsanctionsqrs?> GetProsecutionSanctionsData(string refNum)
        {
            try
            {
                return await _dbContext.prosecutionsanctionsqrs.AsNoTracking().FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt64(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<departmentalproceedingsqrs?> GetDepartmentalProceedingsData(string refNum)
        {
            try
            {
                return await _dbContext.departmentalproceedingsqrs.FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt32(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<adviceofcvcqrs?> GetAdviceOfCVCData(string refNum)
        {
            try
            {
                return await _dbContext.adviceofcvcqrs.FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt32(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<statusofpendencyqrs?> GetStatusPendencyData(string refNum)
        {
            try
            {
                return await _dbContext.statusofpendencyqrs.FirstOrDefaultAsync(i => i.qpr_id == refNum);
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<punitivevigilanceqrs?> GetPunitiveVigilanceData(string refNum)
        {
            try
            {
                return await _dbContext.punitivevigilanceqrs.FirstOrDefaultAsync(i => i.qpr_id == refNum);
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<preventivevigilanceqrs?> GetPreventiveVigilanceData(string refNum)
        {
            try
            {
                return await _dbContext.preventivevigilanceqrs.FirstOrDefaultAsync(i => i.qpr_id == Convert.ToInt32(refNum));
            }
            catch (Exception ex) { }
            return null;
        }
        public async Task<vigilanceactivitiescvcqrs?> GetPreventiveVigilanceActiviteiesData(string refNum)
        {
            try
            {
                return await _dbContext.vigilanceactivitiescvcqrs.FirstOrDefaultAsync(i => i.qpr_id == refNum);
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
