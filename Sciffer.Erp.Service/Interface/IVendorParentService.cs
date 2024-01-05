using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IVendorParentService:IDisposable
    {
        List<VendorParentVM> GetAll();
        VendorParentVM Get(int id);
        bool Add(REF_VENDOR_PARENT paret);
        bool Update(VendorParentVM paret);
        bool Delete(int id);
    }
}
