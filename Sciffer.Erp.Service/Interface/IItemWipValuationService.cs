using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface IItemWipValuationService
    {
        List<VM_ref_item_wip_valuation> GetAll();
        VM_ref_item_wip_valuation Get(int id);
        VM_ref_item_wip_valuation Add(VM_ref_item_wip_valuation item);
        VM_ref_item_wip_valuation Update(VM_ref_item_wip_valuation item);
        bool Delete(int id);
    }
}
