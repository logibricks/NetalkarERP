using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISourceService: IDisposable
    {
        List<REF_SOURCE> GetAll();
        REF_SOURCE Get(int id);
        bool Add(REF_SOURCE src);
        bool Update(REF_SOURCE src);
        bool Delete(int id);
    }
}
