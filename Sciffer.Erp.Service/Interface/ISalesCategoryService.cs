using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISalesCategoryService:IDisposable
    {
        List<REF_SALES_CATEGORY> GetAll();
        REF_SALES_CATEGORY Get(int id);
        bool Add(REF_SALES_CATEGORY category);
        bool Update(REF_SALES_CATEGORY category);
        bool Delete(int id);
    }
}
