using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IJournalEntryService : IDisposable
    {
        List<journal_entryVM> GetAll();
        List<journal_entryVM> getall();
        journal_entryVM Get(int? id);
        bool Add(journal_entryVM party);
        bool Update(journal_entryVM party);
        bool Delete(int? id);
    }
}
