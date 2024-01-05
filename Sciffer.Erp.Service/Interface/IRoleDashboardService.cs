using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IRoleDashboardService:IDisposable
    {
        List<ref_role_dashboard_mapping_vm> GetRoleDashboard(int role_id);
        bool UpdateRecords(ref_role_dashboard_mapping_vm vm);
    }
}
