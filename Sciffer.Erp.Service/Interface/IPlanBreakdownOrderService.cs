using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPlanBreakdownOrderService
    {
        List<ref_plan_breakdown_order_VM> GetAll();
        // List<plan_maintenance_order_VM> getall();
        ref_plan_breakdown_order_VM Get(int id);
        string Add(ref_plan_breakdown_order_VM plan_maintenance_order_VM);
        bool Update(ref_plan_breakdown_order_VM plan_maintenance_order_VM);
        bool Delete(int id);
        //bool Dispose(int id);
    }
}
