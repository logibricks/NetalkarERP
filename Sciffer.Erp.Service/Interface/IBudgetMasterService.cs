using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

 namespace Sciffer.Erp.Service.Interface
{
    public interface IBudgetMasterService:IDisposable
    {
        List<ref_budget_mastervm> GetAll();
        ref_budget_master Get(int id);
        ref_budget_mastervm Add(ref_budget_mastervm BudgetMaster);
        ref_budget_mastervm Update(ref_budget_mastervm BudgetMaster);
        bool Delete(int id);
        bool AddExcel(List<ref_budget_mastervm> ref_budget_mastervm );
    }
}
