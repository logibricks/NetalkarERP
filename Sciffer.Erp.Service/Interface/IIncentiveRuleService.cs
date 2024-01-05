using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface IIncentiveRuleService
    {
        List<VM_incentive_rule> GetAll();
        VM_incentive_rule Get(int id);
        VM_incentive_rule Add(VM_incentive_rule rule);
        VM_incentive_rule Update(VM_incentive_rule rule);
        bool Delete(int id);
    }
}
