using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICreditDebitNoteService:IDisposable
    {
        List<fin_credit_debit_note_vm> GetAll(int id);
        fin_credit_debit_note_vm Get(int id);
        string Add(fin_credit_debit_note_vm cd);
    }
}
