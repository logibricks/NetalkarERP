using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface IVendorService: IDisposable
    {
        List<VendorVM> GetVendorDetail();
        List<REF_VENDOR_VM> GetAll();
        REF_VENDOR_VM Get(int id);
        string Add(REF_VENDOR_VM vendorViewModel);        
        bool Delete(int id);
        string GetContactPerson(int id);       
        List<VendorVM1> GetVendorList();
        bool AddExcel(List<vendor_excel> vendor, List<vendor_contact_excel> contact, List<vendor_item_excel> item, List<vendor_gl_excel> gl);
    }
}
