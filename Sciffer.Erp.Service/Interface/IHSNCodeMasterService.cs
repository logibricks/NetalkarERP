using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;


namespace Sciffer.Erp.Service.Interface
{
   public interface IHSNCodeMasterService: IDisposable
    {
        List<ref_hsn_code_vm> GetAll();
        ref_hsn_code Get(int id);
        ref_hsn_code Add(ref_hsn_code hsn);
        ref_hsn_code Update(ref_hsn_code hsn);
        bool Delete(int id);


    }
}
