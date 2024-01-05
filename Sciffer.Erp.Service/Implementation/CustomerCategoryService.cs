using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Data;
using System.Linq;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class CustomerCategoryService : ICustomerCategoryService
    {
        private readonly ScifferContext _scifferContext;

        public CustomerCategoryService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public REF_CUSTOMER_CATEGORY Add(REF_CUSTOMER_CATEGORY category)
        {
            try
            {
                category.is_active = true;
                _scifferContext.REF_CUSTOMER_CATEGORY.Add(category);
                _scifferContext.SaveChanges();
                category.CUSTOMER_CATEGORY_ID = _scifferContext.REF_CUSTOMER_CATEGORY.Max(x => x.CUSTOMER_CATEGORY_ID);
            }
            catch (Exception)
            {
                return category;
            }
            return category;
        }

        public bool Delete(int id)
        {
            try
            {
                var category = _scifferContext.REF_CUSTOMER_CATEGORY.FirstOrDefault(c => c.CUSTOMER_CATEGORY_ID == id);
                category.is_active = false;

                _scifferContext.Entry(category).State = EntityState.Modified;
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

        public REF_CUSTOMER_CATEGORY Get(int id)
        {
            var category = _scifferContext.REF_CUSTOMER_CATEGORY.FirstOrDefault(c => c.CUSTOMER_CATEGORY_ID == id);
            return category;
        }

        public List<REF_CUSTOMER_CATEGORY> GetAll()
        {
            return _scifferContext.REF_CUSTOMER_CATEGORY.Where(x=>x.is_active==true).OrderByDescending(a => a.CUSTOMER_CATEGORY_ID).ToList();
        }

        public REF_CUSTOMER_CATEGORY Update(REF_CUSTOMER_CATEGORY category)
        {
            try
            {
                category.is_active = true;
                _scifferContext.Entry(category).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return category;
            }
            return category;
        }
    }
}
