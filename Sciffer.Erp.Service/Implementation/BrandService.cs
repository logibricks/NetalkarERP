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
    public class BrandService : IBrandService
    {
        private readonly ScifferContext _scifferContext;

        public BrandService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public REF_BRAND Add(REF_BRAND itemvaluation)
        {
            try
            {
                itemvaluation.is_active = true;
                _scifferContext.REF_BRAND.Add(itemvaluation);
                _scifferContext.SaveChanges();
                itemvaluation.BRAND_ID = _scifferContext.REF_BRAND.Max(x => x.BRAND_ID);
            }
            catch (Exception)
            {
                return itemvaluation;
            }
            return itemvaluation;
        }

        public REF_BRAND Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            try
            {
                var itemvaluation = _scifferContext.REF_BRAND.FirstOrDefault(c => c.BRAND_ID == id);
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

        public REF_BRAND Get(int id)
        {
            var itemvaluation = _scifferContext.REF_BRAND.FirstOrDefault(c => c.BRAND_ID == id);
            return itemvaluation;
        }

        public List<REF_BRAND> GetAll()
        {
            return _scifferContext.REF_BRAND.Where(x=>x.is_active==true).ToList();
        }

        public REF_BRAND Update(REF_BRAND itemvaluation)
        {
            try
            {
                itemvaluation.is_active = true;
                _scifferContext.Entry(itemvaluation).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return itemvaluation;
            }
            return itemvaluation;
        }
    }
}
