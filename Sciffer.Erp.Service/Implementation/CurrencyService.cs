using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Linq;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ScifferContext _scifferContext;

        public REF_CURRENCYVM Add(REF_CURRENCYVM currency)
        {
            try
            {
                REF_CURRENCY rc = new REF_CURRENCY();
                rc.CURRENCY_COUNTRY_ID = currency.CURRENCY_COUNTRY_ID;
                rc.CURRENCY_DESCRIPTION = currency.CURRENCY_DESCRIPTION;
               
                rc.CURRENCY_NAME = currency.CURRENCY_NAME;
                rc.IS_ACTIVE = true;
                rc.is_blocked = currency.is_blocked;
                            
                _scifferContext.REF_CURRENCY.Add(rc);
                _scifferContext.SaveChanges();
                currency.CURRENCY_ID = _scifferContext.REF_CURRENCY.Max(x=>x.CURRENCY_ID);
                currency.CountryName = _scifferContext.REF_COUNTRY.Where(x => x.COUNTRY_ID == currency.CURRENCY_COUNTRY_ID).FirstOrDefault().COUNTRY_NAME;
            }
            catch (Exception)
            {
                return currency;
            }
            return currency;
        }

        public bool Delete(int id)
        {
            try
            {
                var currency = _scifferContext.REF_CURRENCY.FirstOrDefault(c => c.CURRENCY_ID == id);
                var country = currency.CURRENCY_COUNTRY_ID;
                if ( country != 0)
                {
                     currency.IS_ACTIVE = false;
                    _scifferContext.Entry(currency).State = EntityState.Modified;
                    _scifferContext.SaveChanges();
                }
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

        public REF_CURRENCY Get(int id)
        {
            var currency = _scifferContext.REF_CURRENCY.FirstOrDefault(c => c.CURRENCY_ID == id);
            return currency;
        }

        public List<REF_CURRENCYVM> GetAll()
        {
            var query = (from c in _scifferContext.REF_CURRENCY.Where(x => x.IS_ACTIVE == true)
                         join co in _scifferContext.REF_COUNTRY on c.CURRENCY_COUNTRY_ID equals co.COUNTRY_ID
                         select new REF_CURRENCYVM
                         {
                             CURRENCY_COUNTRY_ID = c.CURRENCY_COUNTRY_ID,
                             CountryName = co.COUNTRY_NAME,
                             CURRENCY_DESCRIPTION = c.CURRENCY_DESCRIPTION,
                             CURRENCY_NAME = c.CURRENCY_NAME,
                             is_blocked = c.is_blocked,                           
                             CURRENCY_ID = c.CURRENCY_ID,
                         }).ToList();
            return query;
        }

        public CurrencyService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public REF_CURRENCYVM Update(REF_CURRENCYVM currency)
        {
            try
            {
                REF_CURRENCY rc = new REF_CURRENCY();
                rc.CURRENCY_COUNTRY_ID = currency.CURRENCY_COUNTRY_ID;
                rc.CURRENCY_DESCRIPTION = currency.CURRENCY_DESCRIPTION;
                rc.CURRENCY_ID = currency.CURRENCY_ID;
                rc.CURRENCY_NAME = currency.CURRENCY_NAME;
                rc.IS_ACTIVE = true;
                rc.is_blocked = currency.is_blocked;
              
                _scifferContext.Entry(rc).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                currency.CountryName = _scifferContext.REF_COUNTRY.Where(x => x.COUNTRY_ID == currency.CURRENCY_COUNTRY_ID).FirstOrDefault().COUNTRY_NAME;
            }
            catch (Exception)
            {
                return currency;
            }
            return currency;
        }

        public List<REF_CURRENCYVM> GetCurrency1()
        {
            int curr = _scifferContext.REF_COMPANY.FirstOrDefault().CURRENCY_ID;
            var query = (from c in _scifferContext.REF_CURRENCY.Where(x => x.CURRENCY_ID != curr && x.IS_ACTIVE == true)
                         select new REF_CURRENCYVM
                         {
                             CURRENCY_ID = c.CURRENCY_ID,
                             CURRENCY_NAME=c.CURRENCY_NAME,
                         }).ToList();
            return query;
        }

        public List<REF_CURRENCYVM> GetCurrency2()
        {
            var query = (from c in _scifferContext.REF_CURRENCY.Where(x=>x.IS_ACTIVE == true)
                         join c1 in _scifferContext.REF_COMPANY on c.CURRENCY_ID equals c1.CURRENCY_ID
                         select new REF_CURRENCYVM
                         {
                             CURRENCY_ID = c.CURRENCY_ID,
                             CURRENCY_NAME = c.CURRENCY_NAME,
                         }).ToList();
            return query;
        }
    }
}
