using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IInventoryBalanceService
    {
        List<ref_inventory_balanceVM> GetAll();
        ref_inventory_balance_VM Get(int id);
        string Add(ref_inventory_balance_VM vm);
        bool Update(ref_inventory_balance_VM vm);
        bool Delete(int id);
        bool AddExcel(List<inventory_balance_VM> inventory_balance_VM1, List<inventory_balance_detail_VM> bldetails);
        ref_inventory_balance_VM GetDetails(int id);
    }
}
