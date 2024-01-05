using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IVendorCategoryService : IDisposable
    {
        List<REF_VENDOR_CATEGORY> GetAll();
        REF_VENDOR_CATEGORY Get(int id);
        REF_VENDOR_CATEGORY Add(REF_VENDOR_CATEGORY category);
        REF_VENDOR_CATEGORY Update(REF_VENDOR_CATEGORY category);
        bool Delete(int id);

        string GetVendorCode();
    }
}
