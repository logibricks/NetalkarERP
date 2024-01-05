using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IVendorBalanceService
    {
        List<ref_vendor_balanceVM> GetAll();
        ref_vendor_balance_VM Get(int id);
        bool Add(ref_vendor_balance_VM vm);
        bool Update(ref_vendor_balance_VM vm);
        bool Delete(int id);
        bool AddExcel(List<vendor_balance_VM> inventory_balance_VM1, List<vendor_balance_detail_VM> bldetails);
        ref_vendor_balance_VM GetDetails(int id);
    }
}
