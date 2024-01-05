using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IDepreciationAreaService
    {
        ref_dep_area_vm Get(int id);
        string Add(ref_dep_area_vm depdata, List<ref_dep_posting_period_vm> DepParaArr);
        ref_dep_area Update(ref_dep_area dep_area);
        List<ref_dep_area_vm> GetAll();
    }
}
