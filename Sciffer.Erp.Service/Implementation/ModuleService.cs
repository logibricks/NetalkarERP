using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class ModuleService : IModuleService
    {
        private readonly ScifferContext _scifferContext;

        public ModuleService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public bool Add(ref_module module)
        {
            try
            {
                  module.is_active = true;
                _scifferContext.ref_module.Add(module);
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var module = _scifferContext.ref_module.FirstOrDefault(c => c.module_id == id);
                _scifferContext.Entry(module).State = EntityState.Modified;
                 module.is_active = false;
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

        public ref_module Get(int id)
        {
            return _scifferContext.ref_module.FirstOrDefault(c => c.module_id == id);          
        }

        public List<ref_module> GetAll()
        {
            return _scifferContext.ref_module.OrderByDescending(a => a.module_id).ToList();
        }

        public bool Update(ref_module module)
        {
            try
            {
                  module.is_active = true;
                _scifferContext.Entry(module).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
