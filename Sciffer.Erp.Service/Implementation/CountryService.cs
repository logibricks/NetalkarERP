using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class CountryService : ICountryService
    {

        private readonly ScifferContext _scifferContext;

        public CountryService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public REF_COUNTRY Add(REF_COUNTRY country)
        {
            try
            {
                 country.is_active = true;
                _scifferContext.REF_COUNTRY.Add(country);
                _scifferContext.SaveChanges();
                country.COUNTRY_ID = _scifferContext.REF_COUNTRY.Max(x => x.COUNTRY_ID);
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
                var country = _scifferContext.REF_COUNTRY.FirstOrDefault(c => c.COUNTRY_ID == id);
                 country.is_active = false;
                _scifferContext.Entry(country).State =EntityState.Modified;
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

        public REF_COUNTRY Get(int id)
        {
            var country = _scifferContext.REF_COUNTRY.FirstOrDefault(c => c.COUNTRY_ID == id);
            return country;
        }

        public List<REF_COUNTRY> GetAll()
        {
            return _scifferContext.REF_COUNTRY.ToList().Where(x => x.is_active == true).OrderByDescending(a => a.COUNTRY_ID).ToList();
        }

        public REF_COUNTRY Update(REF_COUNTRY country)
        {
            try
            {
                country.is_active = true;
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
