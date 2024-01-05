using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICustomerBalanceService
    {
        List<ref_customer_balanceVM> GetAll();
        ref_customer_balance_VM Get(int id);
        bool Add(ref_customer_balance_VM vm);
        bool Update(ref_customer_balance_VM vm);
        bool Delete(int id);
        bool AddExcel(List<customer_balance_VM> inventory_balance_VM1, List<customer_balance_detail_VM> bldetails);
        ref_customer_balance_VM GetDetails(int id);
    }
}
