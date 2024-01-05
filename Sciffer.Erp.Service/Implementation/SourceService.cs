using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class SourceService : ISourceService
    {
        private readonly ScifferContext _scifferContext;

        public SourceService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(REF_SOURCE src)
        {
            try
            {
                _scifferContext.REF_SOURCE.Add(src);
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
                var src = _scifferContext.REF_SOURCE.FirstOrDefault(c => c.SOURCE_ID == id);
                _scifferContext.Entry(src).State = EntityState.Deleted;
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

        public REF_SOURCE Get(int id)
        {
            var src = _scifferContext.REF_SOURCE.FirstOrDefault(c => c.SOURCE_ID == id);
            return src;
        }

        public List<REF_SOURCE> GetAll()
        {
            return _scifferContext.REF_SOURCE.ToList();
        }

        public bool Update(REF_SOURCE src)
        {
            try
            {
                _scifferContext.Entry(src).State = EntityState.Modified;
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
