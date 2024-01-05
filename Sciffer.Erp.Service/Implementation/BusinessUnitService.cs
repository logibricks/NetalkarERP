using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class BusinessUnitService : IBusinessUnitService
    {
        private readonly ScifferContext _scifferContext;

        public BusinessUnitService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public REF_BUSINESS_UNIT Add(REF_BUSINESS_UNIT business)
        {
            try
            {
                business.is_active = true;
                _scifferContext.REF_BUSINESS_UNIT.Add(business);
                _scifferContext.SaveChanges();
                business.BUSINESS_UNIT_ID = _scifferContext.REF_BUSINESS_UNIT.Max(x => x.BUSINESS_UNIT_ID);
            }
            catch (Exception)
            {
                return business;
            }
            return business;
        }

        public bool Delete(int id)
        {
            try
            {
                var itemvaluation = _scifferContext.REF_BUSINESS_UNIT.FirstOrDefault(c => c.BUSINESS_UNIT_ID == id);
                itemvaluation.is_active = false;
                _scifferContext.Entry(itemvaluation).State = EntityState.Modified;
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

        public REF_BUSINESS_UNIT Get(int id)
        {
            var business = _scifferContext.REF_BUSINESS_UNIT.FirstOrDefault(c => c.BUSINESS_UNIT_ID == id);
            return business;
        }

        public List<REF_BUSINESS_UNIT> GetAll()
        {
            return _scifferContext.REF_BUSINESS_UNIT.ToList().Where(x => x.is_active == true).OrderByDescending(a => a.BUSINESS_UNIT_ID).ToList();
        }

        public REF_BUSINESS_UNIT Update(REF_BUSINESS_UNIT business)
        {
            try
            {
                business.is_active = true;
                _scifferContext.Entry(business).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return business;
            }
            return business;
        }
    }
}
