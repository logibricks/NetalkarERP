using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IExciseCategoryService: IDisposable
    {
        List<REF_EXCISE_CATEGORY> GetAll();
        REF_EXCISE_CATEGORY Get(int id);
        REF_EXCISE_CATEGORY Create();
        bool Add(REF_EXCISE_CATEGORY Excise);
        bool Update(REF_EXCISE_CATEGORY Excise);
        bool Delete(int id);
    }
}
