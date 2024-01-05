using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IGLAccountTypeService:IDisposable
    {
        List<ref_gl_acount_type> GetAll();
        ref_gl_acount_type Get(int? id);
        bool Add(ref_gl_acount_type gl);
        bool Update(ref_gl_acount_type gl);
        bool Delete(int? id);
        int GetID(string st);
        
    }
}
