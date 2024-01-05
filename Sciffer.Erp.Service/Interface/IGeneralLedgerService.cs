using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Service.Interface
{
    public interface IGeneralLedgerService:IDisposable
    {
        List<ref_ledger_vm> GetAll();
        ref_general_ledger Get(int id);
        bool Add(ref_general_ledger ledger);
        bool Update(ref_general_ledger ledger);
        bool Delete(int id);
        int GetID(string st);
        int GenID(string st);
        string ParentCode(string parentId);
        TreeViewNodeVM GetTreeVeiwList(int id);
        List<ref_ledger_vm> Get_Parent_Ledger(int id);
        string AddExcel(List<ref_ledger_vm> vm);
        List<exportdata> GetExport();
        ref_ledger_vm GetChild(int id);
        List<ref_general_ledger> GetAccountGeneralLedger();
        List<ref_ledger_vm> GetAccountGeneral();
    }
}
