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
    public class AccountAssignmentService : IAccountAssignmentService
    {
        private readonly ScifferContext _scifferContext;
        public AccountAssignmentService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public bool Add(ref_account_assignment Assignment)
        {
            try
            {
                _scifferContext.ref_account_assignment.Add(Assignment);
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
                _scifferContext.ref_account_assignment.Remove(_scifferContext.ref_account_assignment.Find(id));
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

        public ref_account_assignment Get(int? id)
        {
            try
            {
             return    _scifferContext.ref_account_assignment.Find(id);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public List<ref_account_assignment> GetAll()
        {
            try
            {
               return _scifferContext.ref_account_assignment.ToList();
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public bool Update(ref_account_assignment Assignment)
        {
            try
            {
                _scifferContext.Entry(Assignment).State = System.Data.Entity.EntityState.Modified;
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
