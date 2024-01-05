using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICustomerService: IDisposable
    {
        List<REF_CUSTOMER_VM> GetAll();
        List<CustomerVM> GetCustomerList();
        REF_CUSTOMER_VM Get(int id);
        string Add(REF_CUSTOMER_VM cusotmerViewModel);           
        bool Delete(int id);
        bool AddExcel(List<customer_excel> customer, List<contact_excel> contact, List<item_excel> item, List<gl_excel> gl);
        ref_customer_balance_VM GetDetails(int id);
    }
}