using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
   public interface IMaterialOutService
    {
        List<inv_material_out_VM> GetAll();
        inv_material_out_VM Get(int id);
        string Add(inv_material_out_VM Inventory);
        List<inv_material_out_detail_VM> GetPurRequisitionDetailReport(int id);
        List<inv_material_out_VM> MaterialOut(int id);
        string Cancel(int material_out_id, int cancellation_reason_id, string cancellation_remarks);
    }
}
