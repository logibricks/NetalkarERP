using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IMaintenanceTypeService : IDisposable
    {
        List<ref_maintenance_type> GetAll();
        ref_maintenance_type Get(int id);
        bool Add(ref_maintenance_type Main);
        bool Update(ref_maintenance_type Main);
        bool Delete(int id);
    }
}
