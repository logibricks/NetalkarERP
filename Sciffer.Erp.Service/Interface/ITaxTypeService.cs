using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ITaxTypeService:IDisposable
    {
        List<ref_tax_type> GetAll();
        ref_tax_type Get(int? id);
        ref_tax_type Add(ref_tax_type Tax);
        ref_tax_type Update(ref_tax_type Tax);
        bool Delete(int? id);
    }
}
