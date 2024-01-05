using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Service.Interface
{
    public interface IOrgTypeService : IDisposable
    {
        List<REF_ORG_TYPE> GetAll();
        REF_ORG_TYPE Get(int id);
        REF_ORG_TYPE Add(REF_ORG_TYPE country);
        REF_ORG_TYPE Update(REF_ORG_TYPE country);
        bool Delete(int id);
        int Check(string st);
        
    }
}
