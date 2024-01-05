using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IShelfLifeService
    {
        bool Add(ref_shelf_life life);
        bool Update(ref_shelf_life life);
        bool Delete(int id);
        ref_shelf_life Get(int id);
        List<ref_shelf_life> GetAll();
    }
}
