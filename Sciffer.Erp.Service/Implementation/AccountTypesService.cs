using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class AccountTypesService : IAccountTypesService
    {
        private readonly ScifferContext _scifferContext;

        public AccountTypesService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }

        public bool Add(REF_ACCOUNT_TYPE BANK)
        {
            try
            {
                _scifferContext.REF_ACCOUNT_TYPE.Add(BANK);
                _scifferContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {

            try
            {
                var account = _scifferContext.REF_ACCOUNT_TYPE.FirstOrDefault(c => c.ACCOUNT_TYPE_ID == id);
                _scifferContext.Entry(account).State = EntityState.Deleted;
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

        public REF_ACCOUNT_TYPE Get(int id)
        {
            return _scifferContext.REF_ACCOUNT_TYPE.FirstOrDefault(x => x.ACCOUNT_TYPE_ID == id);
        }

        public List<REF_ACCOUNT_TYPE> GetAll()
        {
           return  _scifferContext.REF_ACCOUNT_TYPE.ToList();
        }

        public bool Update(REF_ACCOUNT_TYPE BANK)
        {
            try
            {
                _scifferContext.Entry(BANK).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
