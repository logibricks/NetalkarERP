using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICapitalizationService
    {
        string Add(fin_ledger_capitalization_vm Capdata, List<fin_ledger_capitalization_detail_vm> DepParaArr);
        fin_ledger_capitalization_vm Get(int id);
        List<fin_ledger_capitalization_vm> GetAll();
        string Delete(int id, string cancellation_remarks);
    }
}
