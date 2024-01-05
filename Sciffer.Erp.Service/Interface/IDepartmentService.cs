using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IDepartmentService:IDisposable
    {
        List<REF_DEPARTMENT> GetAll();
        REF_DEPARTMENT Get(int id);
        bool Add(REF_DEPARTMENT dept);
        bool Update(REF_DEPARTMENT dept);
        bool Delete(int id);
    }
}
