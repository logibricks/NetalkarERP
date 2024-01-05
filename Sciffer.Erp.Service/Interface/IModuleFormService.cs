using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
   public interface IModuleFormService:IDisposable
    {
        List<module_form_vm> GetModuleForm();
        List<ref_module_form> GetAll();
        ref_module_form Get(int id);
        bool Add(ref_module_form module);
        bool Update(ref_module_form module);
        bool Delete(int id);
    }
}
