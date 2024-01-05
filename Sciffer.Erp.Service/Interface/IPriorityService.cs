using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPriorityService: IDisposable
    {
        List<ref_priority_vm> GetAll();
        REF_PRIORITY Get(int id);
        ref_priority_vm Add(ref_priority_vm state);
        ref_priority_vm Update(ref_priority_vm state);
        bool Delete(int id);       
    }
}
