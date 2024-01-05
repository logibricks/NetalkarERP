using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IBankAccountService:IDisposable
    {
        List<ref_bank_accountvm> GetBankAccount();
        List<ref_bank_account_vm> GetAll();
        ref_bank_account_vm Get(int id);
        bool Add(ref_bank_account_vm BANK);
        bool Update(ref_bank_account_vm BANK);
        bool Delete(int id);
    }
}
