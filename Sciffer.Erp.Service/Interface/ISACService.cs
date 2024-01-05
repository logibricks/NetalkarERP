using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISACService:IDisposable
    {
        List<ref_sac> GetAll();
        ref_sac Get(int id);
        ref_sac Add(ref_sac sac);
        ref_sac Update(ref_sac sac);
        bool Delete(int id);
    }
}
