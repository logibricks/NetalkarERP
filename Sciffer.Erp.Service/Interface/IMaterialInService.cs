using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
   public interface IMaterialInService
    {
        List<inv_material_in_VM> GetAll();
        inv_material_in_VM Get(int id);
        string Add(inv_material_in_VM Inventory);
        List<GetMaterialInforVendor> GetMaterialInforVendor(int vendor_id);
        List<GetMOList> GetMOList(int vendor_id);
        inv_material_out_VM GetDocNo(int id);
        List<inv_material_in_detail_VM> GetPurRequisitionDetailReport(int id);
        List<inv_material_in_VM> MaterialIn(int id);
        string Cancel(int material_in_id, int cancellation_reason_id, string cancellation_remarks);

    }
}

