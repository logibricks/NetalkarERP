using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICostCenterService : IDisposable
    {

        List<ref_cost_center> GetAll();
        ref_cost_center Get(int id);
        bool Add(ref_cost_center_vm CostCenter);
        bool Update(ref_cost_center_vm CostCenter);
        bool Delete(int id);
        int getLevel(int id);
        List<ref_cost_center_vm> GetCostCenter();
        TreeViewNodeCostCenter GetTreeVeiwList(string code);
        string GetParent();
        int GetLevel(string code);
        List<ref_cost_center_vm> GetDropdownParent();
        ref_cost_center_vm GetChild(int id);
        List<exportdataCostCenter> GetExport();
    }
}
