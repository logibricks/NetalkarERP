using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ScifferContext _scifferContext;

        public CategoryService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public bool Add(REF_CATEGORY CATEGORY)
        {
            try
            {
                _scifferContext.REF_CATEGORY.Add(CATEGORY);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int? id)
        {
            try
            {
                _scifferContext.REF_CATEGORY.Remove(_scifferContext.REF_CATEGORY.Find(id));
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

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

        public REF_CATEGORY Get(int? id)
        {
            try
            {
                return _scifferContext.REF_CATEGORY.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<REF_CATEGORY> GetAll()
        {
            try
            {
                return _scifferContext.REF_CATEGORY.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(REF_CATEGORY CATEGORY)
        {
            try
            {
                _scifferContext.Entry(CATEGORY).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
