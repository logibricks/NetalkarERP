using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using Sciffer.Erp.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Service.Implementation
{
    public class BudgetMasterService : IBudgetMasterService
    {
        private readonly ScifferContext _scifferContext;

        public BudgetMasterService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }

        public List<ref_budget_mastervm> GetAll()
        {
            var query = (from b in _scifferContext.ref_budget_master
                         join b1 in _scifferContext.REF_FINANCIAL_YEAR on b.financial_year_id equals b1.FINANCIAL_YEAR_ID
                         join a in _scifferContext.ref_general_ledger on b.general_ledger_id equals a.gl_ledger_id
                         join c in _scifferContext.ref_instruction_type on b.instruction_type_id equals c.instruction_type_id
                         select new ref_budget_mastervm()
                         {
                            budget_id=b.budget_id,
                            financial_year_id=b.financial_year_id,
                            
                             is_blocked = b.is_blocked,
                             is_active = b.is_active,
                             amount=b.amount,
                             general_ledger_id = b.general_ledger_id,
                             instruction_type_id=b.instruction_type_id,
                            financial_year_name=b1.FINANCIAL_YEAR_NAME,
                            general_ledger_name = a.gl_ledger_name,
                            instruction_name = c.instruction_name,
                         }).ToList().Where(x => x.is_active == true).ToList();
            return query;
           

        }


        public ref_budget_master Get(int id)
        {

            ref_budget_master CC = _scifferContext.ref_budget_master.FirstOrDefault(c => c.budget_id == id);
            return CC;
        }

        public ref_budget_mastervm Add(ref_budget_mastervm BudgetMaster)
        {
            try
            {

                ref_budget_master budget = new ref_budget_master();
                budget.budget_id = BudgetMaster.budget_id;
                budget.financial_year_id = BudgetMaster.financial_year_id;
                budget.general_ledger_id = BudgetMaster.general_ledger_id;
                budget.instruction_type_id = BudgetMaster.instruction_type_id;
                budget.is_active = true;
                budget.is_blocked = BudgetMaster.is_blocked;
                budget.amount = BudgetMaster.amount;
                _scifferContext.ref_budget_master.Add(budget);
                _scifferContext.SaveChanges();
                BudgetMaster.budget_id = _scifferContext.ref_budget_master.Max(x => x.budget_id);
                BudgetMaster.financial_year_name = _scifferContext.REF_FINANCIAL_YEAR.Where(x => x.FINANCIAL_YEAR_ID == BudgetMaster.financial_year_id).FirstOrDefault().FINANCIAL_YEAR_NAME;
                BudgetMaster.general_ledger_name = _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_id == BudgetMaster.general_ledger_id).FirstOrDefault().gl_ledger_name;
                BudgetMaster.instruction_name = _scifferContext.ref_instruction_type.Where(x => x.instruction_type_id == BudgetMaster.instruction_type_id).FirstOrDefault().instruction_name;



            }
            catch (Exception ex)
            {
                return BudgetMaster;
            }
            return BudgetMaster;
        }

        public ref_budget_mastervm Update(ref_budget_mastervm BudgetMaster)
        {
            try
            {

                ref_budget_master budget = new ref_budget_master();
                budget.budget_id = BudgetMaster.budget_id;
                budget.financial_year_id = BudgetMaster.financial_year_id;
                budget.general_ledger_id = BudgetMaster.general_ledger_id;
                budget.instruction_type_id = BudgetMaster.instruction_type_id;
                budget.is_active = true;
                budget.is_blocked = BudgetMaster.is_blocked;
                budget.amount = BudgetMaster.amount;                
                _scifferContext.Entry(budget).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                BudgetMaster.financial_year_name = _scifferContext.REF_FINANCIAL_YEAR.Where(x => x.FINANCIAL_YEAR_ID == BudgetMaster.financial_year_id).FirstOrDefault().FINANCIAL_YEAR_NAME;
                BudgetMaster.general_ledger_name = _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_id == BudgetMaster.general_ledger_id).FirstOrDefault().gl_ledger_name;
                BudgetMaster.instruction_name = _scifferContext.ref_instruction_type.Where(x => x.instruction_type_id == BudgetMaster.instruction_type_id).FirstOrDefault().instruction_name;

            }
            catch (Exception ex)
            {
                return BudgetMaster;
            }
            return BudgetMaster;
        }

        public bool Delete(int id)
        {

            try
            {
                var CC = _scifferContext.ref_budget_master.FirstOrDefault(d => d.budget_id == id);
                CC.is_active = false;
                _scifferContext.Entry(CC).State = EntityState.Modified;
                _scifferContext.SaveChanges();
               

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }

        public bool AddExcel(List<ref_budget_mastervm> vm )
        {
            try
            {
                foreach (var data in vm)
                {
                    ref_budget_master ed = new ref_budget_master();
                    ed.financial_year_id = data.financial_year_id;
                    ed.general_ledger_id = data.general_ledger_id;
                    ed.amount = data.amount;
                    ed.instruction_type_id = data.instruction_type_id;
                    ed.is_blocked = data.is_blocked;
                    ed.is_active = true;
                    _scifferContext.ref_budget_master.Add(ed);

                }
                _scifferContext.SaveChanges();    
               
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
