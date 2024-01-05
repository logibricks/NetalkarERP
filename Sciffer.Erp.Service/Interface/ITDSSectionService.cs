using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Service.Interface
{
    public interface ITDSSectionService:IDisposable
    {
        List<REF_TDS_SECTION> GetAll();
        REF_TDS_SECTION Get(int id);
        bool Add(REF_TDS_SECTION tds);
        bool Update(REF_TDS_SECTION tds);
        bool Delete(int id);
    }
}
