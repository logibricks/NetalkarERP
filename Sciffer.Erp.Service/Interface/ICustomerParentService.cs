using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public  interface ICustomerParentService: IDisposable
    {
        List<CustomerParentVM> GetAll();
        CustomerParentVM Get(int id);
        bool Add(REF_CUSTOMER_PARENT parent);
        bool Update(CustomerParentVM parent);
        bool Delete(int id);
    }
}
