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
    public class ItemAccountingService : IItemAccountingService
    {
        private readonly ScifferContext _scifferContext;

        public ItemAccountingService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(REF_ITEM_ACCOUNTING itemvaluation)
        {
            try
            {
                _scifferContext.REF_ITEM_ACCOUNTING.Add(itemvaluation);
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public REF_ITEM_ACCOUNTING Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            try
            {
                var itemvaluation = _scifferContext.REF_ITEM_ACCOUNTING.FirstOrDefault(c => c.ITEM_ACCOUNTING_ID == id);
                _scifferContext.Entry(itemvaluation).State = EntityState.Deleted;
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

        public REF_ITEM_ACCOUNTING Get(int id)
        {
            var itemvaluation = _scifferContext.REF_ITEM_ACCOUNTING.FirstOrDefault(c => c.ITEM_ACCOUNTING_ID == id);
            return itemvaluation;
        }

        public List<REF_ITEM_ACCOUNTING> GetAll()
        {
            return _scifferContext.REF_ITEM_ACCOUNTING.ToList();
        }

        public bool Update(REF_ITEM_ACCOUNTING itemvaluation)
        {
            try
            {
                _scifferContext.Entry(itemvaluation).State = EntityState.Modified;
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
