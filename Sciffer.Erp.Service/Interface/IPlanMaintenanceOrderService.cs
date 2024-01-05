using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPlanMaintenanceOrderService : IDisposable
    {
        List<plan_maintenance_order_VM> GetAll();
        // List<plan_maintenance_order_VM> getall();
        plan_maintenance_order_VM Get(int id);
        ref_plan_maintenance_order_new_VM Getplanmaintenanceorderformachineentry(int id);
        bool Add(plan_maintenance_order_VM plan_maintenance_order_VM);
        string Update(plan_maintenance_order_VM plan_maintenance_order_VM);
        bool Delete(int id);
        plan_maintenance_order_VM PlanMaintenanceOrder(int id);
        //List<plan_maintenance_order_VM> PlanMaintenanceOrderDetail(int id);
        List<plan_maintenance_order_detail_VM> PlanMaintenanceOrderDetail(int id);
        List<plan_maintenance_order_detail_VM> PlanMaintenanceOrderComponentDetail(int id);
        plan_maintenance_order_VM GetByMachineId(int id);
        string UpdatePlanMaintaince(int? maintenance_order_id, List<plan_maintenance_order_parameter_vm> plan_maintenance_order_parameter);
        bool CheckPlanMaintenanceOrder(int id);
        List<plan_maintenance_order_parameter_new_vm> GetMaintenance_order_parameter(int id);
        List<plan_maintenance_order_parameter_cost_new_vm> GetMaintenance_order_parameter_cost(int id);

    }
}
