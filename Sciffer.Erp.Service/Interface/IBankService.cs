using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IBankService:IDisposable
    {
        List<ref_bank> GetAll();
        ref_bank Get(int id);
        ref_bank Add(ref_bank bank);
        ref_bank Update(ref_bank bank);
        bool Delete(int id);
    }
}
