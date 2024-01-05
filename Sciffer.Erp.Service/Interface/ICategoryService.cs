using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICategoryService:IDisposable
    {
        List<REF_CATEGORY> GetAll();
        REF_CATEGORY Get(int? id);
        bool Add(REF_CATEGORY CATEGORY);
        bool Update(REF_CATEGORY CATEGORY);
        bool Delete(int? id);
    }
}
