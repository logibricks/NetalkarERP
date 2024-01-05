using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IMachineCategoryService : IDisposable
    {
        List<ref_machine_category> GetAll();
        ref_machine_category Get(int id);
        ref_machine_category Add(ref_machine_category ref_machine_category);
        ref_machine_category Update(ref_machine_category ref_machine_category);
        bool Delete(int id);

    }
}

