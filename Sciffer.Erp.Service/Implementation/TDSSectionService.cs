using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class TDSSectionService : ITDSSectionService
    {
        private readonly ScifferContext _scifferContext;

        public TDSSectionService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }


        public bool Add(REF_TDS_SECTION tds)
        {
            try
            {
                _scifferContext.REF_TDS_SECTION.Add(tds);
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
                var tds = _scifferContext.REF_TDS_SECTION.FirstOrDefault(c => c.TDS_SECTION_ID == id);
                _scifferContext.Entry(tds).State = EntityState.Deleted;
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

        public REF_TDS_SECTION Get(int id)
        {
            var tds = _scifferContext.REF_TDS_SECTION.FirstOrDefault(c => c.TDS_SECTION_ID == id);
            return tds;
        }

        public List<REF_TDS_SECTION> GetAll()
        {
            return _scifferContext.REF_TDS_SECTION.ToList();
        }

        public bool Update(REF_TDS_SECTION tds)
        {
            try
            {
                _scifferContext.Entry(tds).State = EntityState.Modified;
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
