using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
   public interface ICreditDebitNoteTransactionService : IDisposable
    {
        List<fin_credit_debit_note_vm> GetAll(int id);
        fin_credit_debit_note_vm Get(int id);
        string Add(fin_credit_debit_note_vm cd);
        fin_credit_debit_note_vm GetCDNForReport(int id);
        List<fin_credit_debit_note_detail_vm> GetCDNDetailsForReport(int id);
        string Delete(int id, string cancellation_remarks, int reason_id);
    }
}
