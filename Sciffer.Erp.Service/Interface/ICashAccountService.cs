using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICashAccountService:IDisposable
    {
        List<ref_cash_account_VM> getall();
        string Add(ref_cash_account cashaccount);
        ref_cash_account Get(int id);
        bool Delete(int id);
    }
}
