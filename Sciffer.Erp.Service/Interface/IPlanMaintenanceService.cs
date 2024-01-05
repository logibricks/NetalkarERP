using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sciffer.Erp.Service.Interface

{
    public interface IPlanMaintenanceService:IDisposable
    {
        List<ref_plan_maintenance_VM> GetAll();
       
        ref_plan_maintenance_VM Get(int id);
        string Add(ref_plan_maintenance_VM ref_plan_maintenance_VM);
        bool Update(ref_plan_maintenance_VM ref_plan_maintenance_VM);
        bool Delete(int id);
    }
}
