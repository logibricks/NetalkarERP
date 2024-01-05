using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
   public interface ISRNService : IDisposable
    {
        List<pur_srn_vm> GetAll();      
        pur_srn_vm Get(int? id);
        string Add(pur_srn_vm SRN, List<pur_srn_detail_vm> detail);      
        string Delete(int id, string cancellation_remarks, int reason_id);
        List<pur_srn_vm> GetGrnList();
    }
}
