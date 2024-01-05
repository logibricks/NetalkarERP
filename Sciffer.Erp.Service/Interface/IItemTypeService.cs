using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IItemTypeService:IDisposable
    {
        List<ref_item_type> GetAll();
        ref_item_type Get(int id);
        bool Add(ref_item_type bank);
        bool Update(ref_item_type bank);
        bool Delete(int id);
    }
}
