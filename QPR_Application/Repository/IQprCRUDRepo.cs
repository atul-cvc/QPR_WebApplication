﻿using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.Entities;
using QPR_Application.Models.ViewModels;

namespace QPR_Application.Repository
{
    public interface IQprCRUDRepo
    {
        public Task<List<Years>> GetYears();
        public Task<List<Years>> GetYearsFinalSubmit(string userId);
        public Task<List<Years>> GetQPRRequestYear();
        public Task<qpr> GetQPRDetails(string refNum);
        public Task UpdateQPRFinalSubmit(string refNum);
        public string GetReferenceNumber(GetQPR qprDetails, string UserId);
        public string GenerateReferenceNumber(GetQPR qprDetails, string UserId, string ip);
        public string GetPreviousReferenceNumber(string UserId, string qtrYear, string qtrReport);
        public Task<string> UpdateQPR(GetQPR qprLoginDetails, string refNum);
        public Task CreateComplaints(complaintsqrs complaint);
        public Task SaveComplaints(complaintsqrs complaint);
        public Task CreateVigilanceInvestigation(viginvestigationqrs vigInv);
        public Task SaveVigilanceInvestigation(viginvestigationqrs vigInv);
        public Task CreateProsecutionSanctionsViewModel(ProsecutionSanctionsViewModel prosecViewModel);
        public Task SaveProsecutionSanctionsViewModel(ProsecutionSanctionsViewModel prosecViewModel);
        public Task CreateDepartmentalProceedings(DepartmentalProceedingsViewModel deptViewModel);
        public Task SaveDepartmentalProceedings(DepartmentalProceedingsViewModel deptViewModel, departmentalproceedingsqrs prevData);
        public Task CreateAdviceCVC(AdviceOfCvcViewModel adviceVM);
        public Task SaveAdviceCVC(AdviceOfCvcViewModel adviceVM);
        public Task CreateStatusPendency(StatusOfPendencyViewModel statusVM);
        public Task SaveStatusPendency(StatusOfPendencyViewModel statusVM);
        public Task CreatePunitiveVigilance(punitivevigilanceqrs pVig);
        public Task SavePunitiveVigilance(punitivevigilanceqrs pVig);
        public Task CreatePreventiveVigilance(PreventiveVigilanceViewModel pVig);
        public Task SavePreventiveVigilance(PreventiveVigilanceViewModel pVig);
        public Task AddNewPrevVigA(preventivevigi_a_qpr prevVigA, int preventive_vigilance_id);
        public Task AddNewPrevVigB(preventivevigi_b_qpr prevVigB, int preventive_vigilance_id);
        public Task CreatePreventiveVigilanceActivities(vigilanceactivitiescvcqrs vigilanceactivities);
        public Task SavePreventiveVigilanceActivities(vigilanceactivitiescvcqrs vigilanceactivities);
        public Task SaveCVO_Training(Training_CVO training);
        public Task DeleteAgeWisePendency(int pend_id);
        public Task DeleteAgainstChargedOfficers(int pend_id);
        public Task DeleteCvcAdvice(int pend_id);
        public Task DeleteAppeleateAuthority(int pend_id);
        public Task DeleteFiCaseRow(int id);
        public Task DeleteCaCaseRow(int id);
        public Task DeletePrevVigi(int id, string tableName);
    }
}
