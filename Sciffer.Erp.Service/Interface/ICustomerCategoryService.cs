using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICustomerCategoryService: IDisposable
    {
        List<REF_CUSTOMER_CATEGORY> GetAll();
        REF_CUSTOMER_CATEGORY Get(int id);
        REF_CUSTOMER_CATEGORY Add(REF_CUSTOMER_CATEGORY category);
        REF_CUSTOMER_CATEGORY Update(REF_CUSTOMER_CATEGORY category);
        bool Delete(int id);
    }
}
