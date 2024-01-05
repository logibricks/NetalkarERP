using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Service.Interface
{
    public interface IUOMService:IDisposable
    {
        List<REF_UOM> GetAll();
        REF_UOM Get(int id);
        REF_UOM Add(REF_UOM UOM);
        REF_UOM Update(REF_UOM UOM);
        bool Delete(int id);
        int GetID(string st);
    }
}
