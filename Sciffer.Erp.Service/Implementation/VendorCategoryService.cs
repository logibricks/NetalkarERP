using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class VendorCategoryService : IVendorCategoryService
    {
        private readonly ScifferContext _scifferContext;

        public VendorCategoryService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public REF_VENDOR_CATEGORY Add(REF_VENDOR_CATEGORY category)
        {
            try
            {
                category.is_active = true;
                _scifferContext.REF_VENDOR_CATEGORY.Add(category);
                _scifferContext.SaveChanges();
                category.VENDOR_CATEGORY_ID = _scifferContext.REF_VENDOR_CATEGORY.Max(x => x.VENDOR_CATEGORY_ID);
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
                var category = _scifferContext.REF_VENDOR_CATEGORY.FirstOrDefault(c => c.VENDOR_CATEGORY_ID == id);
                _scifferContext.Entry(category).State = EntityState.Modified;
                category.is_active = false;
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

        public REF_VENDOR_CATEGORY Get(int id)
        {
            var category = _scifferContext.REF_VENDOR_CATEGORY.FirstOrDefault(c => c.VENDOR_CATEGORY_ID == id);
            return category;
        }

        public List<REF_VENDOR_CATEGORY> GetAll()
        {
            return _scifferContext.REF_VENDOR_CATEGORY.Where(x => x.is_active == true).OrderByDescending(a => a.VENDOR_CATEGORY_ID).ToList();
        }

        public string GetVendorCode()
        {
            var vendorList = _scifferContext.REF_VENDOR.Where(x => !x.IS_BLOCKED).OrderByDescending(x => x.VENDOR_ID).Take(1).ToList();
            if (vendorList.Count > 0)
            {
                var vendorCode = vendorList[0].VENDOR_CODE;

                string subs = vendorCode.Remove(0, 2);
                int code = Convert.ToInt32(subs);
                code = code + 1;
                var len = "VD" + code;
                if (len.Length == 6)
                {
                    return len;
                }
                else
                {
                    return "VD0" + code;
                }
            }
            return "";

        }

        public REF_VENDOR_CATEGORY Update(REF_VENDOR_CATEGORY category)
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
