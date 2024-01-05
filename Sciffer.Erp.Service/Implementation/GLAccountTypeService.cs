using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class GLAccountTypeService : IGLAccountTypeService
    {
        private readonly ScifferContext _scifferContext;
        public GLAccountTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(ref_gl_acount_type gl)
        {
            try
            {
                _scifferContext.ref_gl_acount_type.Add(gl);
                _scifferContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int? id)
        {
            try
            {
                var gl = _scifferContext.ref_gl_acount_type.FirstOrDefault(c => c.gl_account_type_id == id);
                _scifferContext.Entry(gl).State = EntityState.Deleted;
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

        public ref_gl_acount_type Get(int? id)
        {
            var gl = _scifferContext.ref_gl_acount_type.FirstOrDefault(c => c.gl_account_type_id == id);
            return gl;
        }

        public List<ref_gl_acount_type> GetAll()
        {
            return _scifferContext.ref_gl_acount_type.ToList();
        }

        public bool Update(ref_gl_acount_type gl)
        {
            try
            {
                _scifferContext.Entry(gl).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public int GetID(string st)
        {
            var Y = _scifferContext.ref_gl_acount_type.Where(x => x.gl_account_type_description.ToLower() == st).FirstOrDefault();
            var id = Y == null ? 0 : Y.gl_account_type_id;
            return id;
        }
    }
}
