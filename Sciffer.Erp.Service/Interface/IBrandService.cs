using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public  interface IBrandService: IDisposable
    {
        List<REF_BRAND> GetAll();
        REF_BRAND Get(int id);
        REF_BRAND Create();
        REF_BRAND Add(REF_BRAND finance);
        REF_BRAND Update(REF_BRAND finance);
        bool Delete(int id);
    }
}
