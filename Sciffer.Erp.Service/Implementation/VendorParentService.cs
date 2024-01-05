using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Linq;
using System.Data.Entity;


namespace Sciffer.Erp.Service.Implementation
{
    public class VendorParentService : IVendorParentService
    {
        private readonly ScifferContext _scifferContext;

        public VendorParentService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(REF_VENDOR_PARENT paret)
        {
            try
            {
                _scifferContext.REF_VENDOR_PARENT.Add(paret);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var paret = _scifferContext.REF_VENDOR_PARENT.FirstOrDefault(c => c.VENDOR_PARENT_ID == id);
                _scifferContext.Entry(paret).State = EntityState.Deleted;
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

        public VendorParentVM Get(int id)
        {
            var query = (from ed in _scifferContext.REF_VENDOR_PARENT.Where(c => c.VENDOR_PARENT_ID == id)
                         join ctry in _scifferContext.REF_STATE on ed.REGD_OFFICE_STATE_ID equals ctry.STATE_ID
                         select new VendorParentVM()
                         {
                             VENDOR_PARENT_ID = ed.VENDOR_PARENT_ID,
                             vendor_code = ed.vendor_code,
                             VENDOR_PARENT_NAME = ed.VENDOR_PARENT_NAME,
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

        public List<VendorParentVM> GetAll()
        {
            var query = (from ed in _scifferContext.REF_VENDOR_PARENT
                          join ctry in _scifferContext.REF_STATE on ed.REGD_OFFICE_STATE_ID equals ctry.STATE_ID
                          select new VendorParentVM()
                          {
                              VENDOR_PARENT_ID = ed.VENDOR_PARENT_ID,
                              vendor_code = ed.vendor_code,
                              VENDOR_PARENT_NAME = ed.VENDOR_PARENT_NAME,
                              REGD_OFFICE_ADDRESS = ed.REGD_OFFICE_ADDRESS,
                              REGD_OFFICE_CITY = ed.REGD_OFFICE_CITY,
                              REGD_OFFICE_COUNTRY_ID = ctry.COUNTRY_ID,
                              REGD_OFFICE_STATE_ID = ed.REGD_OFFICE_STATE_ID,
                              REGD_OFFICE_PINCODE = ed.REGD_OFFICE_PINCODE,
                              WEBSITE_ADDRESS = ed.WEBSITE_ADDRESS,
                              REGD_OFFICE_COUNTRY_NAME = ctry.REF_COUNTRY.COUNTRY_NAME,
                              REGD_OFFICE_STATE_NAME = ctry.STATE_NAME,
                              blocked = ed.blocked,
                              vendor_parent_code = ed.vendor_code + " - " + ed.VENDOR_PARENT_NAME, 
                          }).ToList();
            return query;
        }

        public bool Update(VendorParentVM paret)
        {
            try
            {

                REF_VENDOR_PARENT p = new REF_VENDOR_PARENT();
                p.blocked = paret.blocked;
                p.vendor_code = paret.vendor_code;
                p.VENDOR_PARENT_ID = paret.VENDOR_PARENT_ID;
                p.VENDOR_PARENT_NAME = paret.VENDOR_PARENT_NAME;
                p.WEBSITE_ADDRESS = paret.WEBSITE_ADDRESS;
                p.REGD_OFFICE_ADDRESS = paret.REGD_OFFICE_ADDRESS;
                p.REGD_OFFICE_CITY = paret.REGD_OFFICE_CITY;
                p.REGD_OFFICE_PINCODE = paret.REGD_OFFICE_PINCODE;
                p.REGD_OFFICE_STATE_ID = paret.REGD_OFFICE_STATE_ID;
                _scifferContext.Entry(p).State = EntityState.Modified;
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
