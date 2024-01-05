using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ITaxService:IDisposable
    {
        List<ref_tax_vm> GetAll();
        ref_tax_vm Get(int id);
        bool Add(ref_tax_vm tax);
        bool Update(ref_tax_vm tax);
        bool Delete(int id);
    }
}
