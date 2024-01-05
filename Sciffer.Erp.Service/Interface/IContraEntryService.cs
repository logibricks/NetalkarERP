using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IContraEntryService : IDisposable
    {
        List<fin_contra_entry_vm> GetAll();
        fin_contra_entry_vm Get(int id);
        string Add(fin_contra_entry_vm contra);
        string Delete(int id, string cancellation_remarks, int cancellation_reason_id);
        fin_contra_entry Update(fin_contra_entry contra);
    }
}
