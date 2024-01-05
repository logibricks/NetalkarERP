using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IBranchService:IDisposable
    {
        List<REF_BRANCH> GetAll();
        REF_BRANCH Get(int id);
        REF_BRANCH Add(REF_BRANCH BRANCH);
        REF_BRANCH Update(REF_BRANCH BRANCH);
        bool Delete(int id);

    }
}
