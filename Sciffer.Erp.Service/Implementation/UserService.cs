using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Data;
using System.Linq;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly ScifferContext _scifferContext;

        public UserService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(REF_USER cusotmerViewModel)
        {
            try
            {
                _scifferContext.REF_USER.Add(cusotmerViewModel);
                _scifferContext.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var user = _scifferContext.REF_USER.FirstOrDefault(c => c.USER_ID == id);
                _scifferContext.Entry(user).State = EntityState.Deleted;
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
        #endregion}

        public REF_USER Get(int id)
        {
            var user = _scifferContext.REF_USER.FirstOrDefault(c => c.USER_ID == id);
            return user;
        }

        public List<REF_USER> GetAll()
        {
            return _scifferContext.REF_USER.ToList();
        }

        public bool Update(REF_USER cusotmerViewModel)
        {
            try
            {
                _scifferContext.Entry(cusotmerViewModel).State = EntityState.Modified;
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
