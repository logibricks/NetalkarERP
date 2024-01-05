using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Data;
using System.Linq;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class CustomerParentService : ICustomerParentService
    {
        private readonly ScifferContext _scifferContext;

        public CustomerParentService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }


        public bool Add(REF_CUSTOMER_PARENT parent)
        {
            try
              {
                _scifferContext.REF_CUSTOMER_PARENT.Add(parent);
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
                var parent = _scifferContext.REF_CUSTOMER_PARENT.FirstOrDefault(c => c.CUSTOMER_PARENT_ID == id);
                _scifferContext.Entry(parent).State = EntityState.Deleted;
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

        public CustomerParentVM Get(int id)
        {
            var query = (from ed in _scifferContext.REF_CUSTOMER_PARENT.Where(c => c.CUSTOMER_PARENT_ID == id)
                          join ctry in _scifferContext.REF_STATE on ed.REGD_OFFICE_STATE_ID equals ctry.STATE_ID
                          select new CustomerParentVM()
                          {
                             CUSTOMER_PARENT_ID = ed.CUSTOMER_PARENT_ID,
                             customer_code = ed.customer_code,
                             CUSTOMER_PARENT_NAME = ed.CUSTOMER_PARENT_NAME,
                             PARENT_CREDIT_LIMIT = ed.PARENT_CREDIT_LIMIT,
                             REGD_OFFICE_ADDRESS = ed.REGD_OFFICE_ADDRESS,
                             REGD_OFFICE_CITY = ed.REGD_OFFICE_CITY,
                             REGD_OFFICE_COUNTRY_ID = ctry.COUNTRY_ID,
                             REGD_OFFICE_STATE_ID = ed.REGD_OFFICE_STATE_ID,
                             REGD_OFFICE_PINCODE = ed.REGD_OFFICE_PINCODE,
                             WEBSITE_ADDRESS = ed.WEBSITE_ADDRESS,   
                             blocked = ed.blocked,                          
                          }).FirstOrDefault();                          
            return query;
        }

        public List<CustomerParentVM> GetAll()
        {
            var query = (from ed in _scifferContext.REF_CUSTOMER_PARENT
                         join ctry in _scifferContext.REF_STATE on ed.REGD_OFFICE_STATE_ID equals ctry.STATE_ID
                         select new CustomerParentVM()
                         {
                             CUSTOMER_PARENT_ID = ed.CUSTOMER_PARENT_ID,
                             customer_code = ed.customer_code,
                             CUSTOMER_PARENT_NAME = ed.CUSTOMER_PARENT_NAME,
                             PARENT_CREDIT_LIMIT = ed.PARENT_CREDIT_LIMIT,
                             REGD_OFFICE_ADDRESS = ed.REGD_OFFICE_ADDRESS,
                             REGD_OFFICE_CITY = ed.REGD_OFFICE_CITY,
                             REGD_OFFICE_COUNTRY_ID = ctry.COUNTRY_ID,
                             REGD_OFFICE_STATE_ID = ed.REGD_OFFICE_STATE_ID,
                             REGD_OFFICE_PINCODE = ed.REGD_OFFICE_PINCODE,
                             WEBSITE_ADDRESS = ed.WEBSITE_ADDRESS,
                             REGD_OFFICE_COUNTRY_NAME = ctry.REF_COUNTRY.COUNTRY_NAME,
                             REGD_OFFICE_STATE_NAME = ctry.STATE_NAME,
                             blocked = ed.blocked,
                             customer_parent_code_name = ed.customer_code + " - " + ed.CUSTOMER_PARENT_NAME,
                         }).OrderByDescending(a => a.CUSTOMER_PARENT_ID).ToList();
            return query;
        }

        public bool Update(CustomerParentVM parent)
        {
            try
            {
                REF_CUSTOMER_PARENT vp = new REF_CUSTOMER_PARENT();
                vp.blocked = parent.blocked;
                vp.REGD_OFFICE_ADDRESS = parent.REGD_OFFICE_ADDRESS;
                vp.REGD_OFFICE_CITY = parent.REGD_OFFICE_CITY;
                vp.REGD_OFFICE_PINCODE = parent.REGD_OFFICE_PINCODE;
                vp.REGD_OFFICE_STATE_ID = parent.REGD_OFFICE_STATE_ID;
                vp.customer_code = parent.customer_code;
                vp.CUSTOMER_PARENT_ID = parent.CUSTOMER_PARENT_ID;
                vp.CUSTOMER_PARENT_NAME = parent.CUSTOMER_PARENT_NAME;
                vp.PARENT_CREDIT_LIMIT = parent.PARENT_CREDIT_LIMIT;
                vp.WEBSITE_ADDRESS = parent.WEBSITE_ADDRESS;
                _scifferContext.Entry(vp).State = EntityState.Modified;
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
