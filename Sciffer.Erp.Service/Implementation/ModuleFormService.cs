using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Linq;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class ModuleFormService : IModuleFormService
    {
        private readonly ScifferContext _scifferContext;

        public ModuleFormService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(ref_module_form module)
        {

            try
            {
                  module.is_active = true;
                _scifferContext.ref_module_form.Add(module);
                _scifferContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var module = _scifferContext.ref_module_form.FirstOrDefault(c => c.module_form_id == id);
                module.is_active = false;
                _scifferContext.Entry(module).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        #region dispoable methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }
        #endregion

        public ref_module_form Get(int id)
        {
            return _scifferContext.ref_module_form.FirstOrDefault(c => c.module_form_id == id);
        }

        public List<ref_module_form> GetAll()
        {
            return _scifferContext.ref_module_form.Where(m => m.is_active == true).ToList();
        }

        public bool Update(ref_module_form module)
        {
            try
            {
                _scifferContext.Entry(module).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public List<module_form_vm> GetModuleForm()
        {
            var query = (from m in _scifferContext.ref_module_form
                         join mf in _scifferContext.ref_module on m.module_id equals mf.module_id
                         select new module_form_vm {
                             module_form_id=m.module_form_id,
                             module_form_name=m.module_form_name,
                             module_name=mf.module_name,
                         }).OrderByDescending(a => a.module_form_id).ToList();
            return query;
        }
    }
}
