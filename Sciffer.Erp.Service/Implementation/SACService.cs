using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class SACService : ISACService
    {
        private readonly ScifferContext _scifferContext;
        public SACService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public ref_sac Add(ref_sac sac)
        {
            try
            {                
                _scifferContext.ref_sac.Add(sac);
                _scifferContext.SaveChanges();
                sac.sac_id = _scifferContext.ref_sac.Max(x => x.sac_id);
            }
            catch (Exception)
            {
                return sac;
            }
            return sac;
        }

        public bool Delete(int id)
        {
            try
            {
                var sac = _scifferContext.ref_sac.FirstOrDefault(c => c.sac_id == id);               
                _scifferContext.Entry(sac).State = EntityState.Deleted;
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

        public ref_sac Get(int id)
        {
            var sac = _scifferContext.ref_sac.FirstOrDefault(c => c.sac_id == id);
            return sac;
        }

        public List<ref_sac> GetAll()
        {
            return _scifferContext.ref_sac.ToList();
        }

        public ref_sac Update(ref_sac sac)
        {
            try
            {                
                _scifferContext.Entry(sac).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return sac;
            }
            return sac;
        }
    }
}
