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
    public class SalesCategoryService : ISalesCategoryService
    {
        private readonly ScifferContext _scifferContext;

        public SalesCategoryService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(REF_SALES_CATEGORY category)
        {
            try
            {
                _scifferContext.REF_SALES_CATEGORY.Add(category);
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
                var category = _scifferContext.REF_SALES_CATEGORY.FirstOrDefault(c => c.SALES_CATEGORY_ID == id);
                _scifferContext.Entry(category).State = EntityState.Deleted;
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

        public REF_SALES_CATEGORY Get(int id)
        {
            var category = _scifferContext.REF_SALES_CATEGORY.FirstOrDefault(c => c.SALES_CATEGORY_ID == id);
            return category;
        }

        public List<REF_SALES_CATEGORY> GetAll()
        {
           return _scifferContext.REF_SALES_CATEGORY.ToList();
        }

        public bool Update(REF_SALES_CATEGORY category)
        {
            try
            {
                _scifferContext.Entry(category).State = EntityState.Modified;
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
