using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISalesReturnService
    {
        string Add(sal_si_return_vm sal_si_return_vm);
        sal_si_return_batch_lsit_vm getBuyer();
        List<sal_si_return_detail_vm> getItems(int? plant_id, int? buyer_id, int? item_id, string batch_number, int? si_id, string entity,int consignee_id);
        List<sal_si_return_vm> getall();
        sal_si_return_vm Detail(int id);
    }
}
