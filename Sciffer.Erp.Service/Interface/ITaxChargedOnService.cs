using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ITaxChargedOnService:IDisposable
    {
        List<ref_tax_charged_on> GetAll();
        ref_tax_charged_on Get(int id);
        ref_tax_charged_on Add(ref_tax_charged_on tax);
        ref_tax_charged_on Update(ref_tax_charged_on tax);
        bool Delete(int id);
    }
}
