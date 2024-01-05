using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IEmployeeBalanceService
    {
        List<ref_employee_balanceVM> GetAll();
        ref_employee_balance_VM Get(int id);
        bool Add(ref_employee_balance_VM vm);
        bool Update(ref_employee_balance_VM vm);
        bool Delete(int id);
        bool AddExcel(List<employee_balance_VM> inventory_balance_VM1, List<employee_balance_details_VM> bldetails);
        ref_employee_balance_VM GetDetails(int id);
    }
}
