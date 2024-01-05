using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;


namespace Sciffer.Erp.Service.Implementation
{
    public class FreightTermsService : IFreightTermsService
    {
        private readonly ScifferContext _scifferContext;

        public FreightTermsService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public REF_FREIGHT_TERMS Add(REF_FREIGHT_TERMS country)
        {
            try
            {
                country.Is_active = true;
                _scifferContext.REF_FREIGHT_TERMS.Add(country);
                _scifferContext.SaveChanges();
                country.FREIGHT_TERMS_ID = _scifferContext.REF_FREIGHT_TERMS.Max(x => x.FREIGHT_TERMS_ID);
            }
            catch (Exception)
            {
                return country;
            }
            return country;
        }

        public bool Delete(int id)
        {
            try
            {
                var org = _scifferContext.REF_FREIGHT_TERMS.FirstOrDefault(c => c.FREIGHT_TERMS_ID == id);
                org.Is_active = false;
                _scifferContext.Entry(org).State = EntityState.Modified;
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

        public REF_FREIGHT_TERMS Get(int id)
        {
            var freight = _scifferContext.REF_FREIGHT_TERMS.FirstOrDefault(c => c.FREIGHT_TERMS_ID == id);
            return freight;
        }

        public List<REF_FREIGHT_TERMS> GetAll()
        {
            return _scifferContext.REF_FREIGHT_TERMS.ToList().Where(x=>x.Is_active==true).OrderByDescending(a => a.FREIGHT_TERMS_ID).ToList();
        }

        public REF_FREIGHT_TERMS Update(REF_FREIGHT_TERMS country)
        {
            try
            {
                country.Is_active = true;
                _scifferContext.Entry(country).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return country;
            }
            return country;
        }
    }
}
