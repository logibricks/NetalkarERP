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
    public class ExciseCategoryService : IExciseCategoryService
    {
        private readonly ScifferContext _scifferContext;

        public ExciseCategoryService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(REF_EXCISE_CATEGORY Excise)
        {
            try
            {
                _scifferContext.REF_EXCISE_CATEGORY.Add(Excise);
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public REF_EXCISE_CATEGORY Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            try
            {
                var Excise = _scifferContext.REF_EXCISE_CATEGORY.FirstOrDefault(c => c.EXCISE_CATEGORY_ID == id);
                _scifferContext.Entry(Excise).State = EntityState.Deleted;
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

        public REF_EXCISE_CATEGORY Get(int id)
        {
            var Excise = _scifferContext.REF_EXCISE_CATEGORY.FirstOrDefault(c => c.EXCISE_CATEGORY_ID == id);
            return Excise;
        }

        public List<REF_EXCISE_CATEGORY> GetAll()
        {
            return _scifferContext.REF_EXCISE_CATEGORY.ToList();
        }

        public bool Update(REF_EXCISE_CATEGORY Excise)
        {
            try
            {
                _scifferContext.Entry(Excise).State = EntityState.Modified;
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
