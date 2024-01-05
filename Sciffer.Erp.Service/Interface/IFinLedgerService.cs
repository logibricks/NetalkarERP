using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IFinLedgerService : IDisposable
    {
        List<fin_ledgerVM> GetAll();
        List<fin_ledgerVM> getall();
        fin_ledgerVM Get(int? id);
        string Add(fin_ledgerVM party);
        bool Update(fin_ledgerVM party);
        string Delete(int id, string cancellation_remarks, int cancellation_reason_id);
    }
}
