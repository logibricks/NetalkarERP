using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IEmployeeService:IDisposable
    {
        List<REF_EMPLOYEE_VM> GetEmployeeList();
        List<REF_EMPLOYEE_VM> GetAll();
        REF_EMPLOYEE_VM Get(int id);
        bool Add(REF_EMPLOYEE_VM emp);
        bool Update(REF_EMPLOYEE_VM emp);
        bool Delete(int id);
        List<REF_EMPLOYEE_VM> GetEmployeeCode();
    }
}
