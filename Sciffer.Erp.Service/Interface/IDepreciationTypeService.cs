using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IDepreciationTypeService
    {
        string Add(ref_dep_type_vm add_depre);
        List<ref_dep_type_vm> getall();
        ref_dep_type_vm Get(int id);
    }
}
