using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
namespace Sciffer.Erp.Service.Interface
{
    public interface ICompanyService : IDisposable
    {
        List<comapnyvm> GetCompanyDetail();
        List<REF_COMPANY_VM> GetAll();
        REF_COMPANY_VM Get(int id);
        bool Add(REF_COMPANY_VM COMPANY);
        bool Update(REF_COMPANY_VM COMPANY);
        bool Delete(int id);
    }
}