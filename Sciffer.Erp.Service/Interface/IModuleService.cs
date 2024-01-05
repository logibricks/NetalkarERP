using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IModuleService:IDisposable
    {
        List<ref_module> GetAll();
        ref_module Get(int id);
        bool Add(ref_module module);
        bool Update(ref_module module);
        bool Delete(int id);
    }
}
