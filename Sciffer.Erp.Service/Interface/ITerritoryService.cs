using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Service.Interface
{
    public interface ITerritoryService:IDisposable
    {
        List<REF_TERRITORY> GetAll();
        REF_TERRITORY Get(int id);
        REF_TERRITORY Add(REF_TERRITORY territory);
        REF_TERRITORY Update(REF_TERRITORY territory);
        bool Delete(int id);
    }
}
