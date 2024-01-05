using Sciffer.Erp.Data;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
   public class RoleDashboardService: IRoleDashboardService
    {
        private readonly ScifferContext _scifferContext;

        public RoleDashboardService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<ref_role_dashboard_mapping_vm> GetRoleDashboard(int role_id)
        {
            var query = (from d in _scifferContext.ref_dashboard
                         join m in _scifferContext.ref_module on d.module_id equals m.module_id
                         join rd in _scifferContext.ref_role_dashboard_mapping.Where(x => x.role_id == role_id) on d.dashboard_id equals rd.dashboard_id into j1
                         from rdm in j1.DefaultIfEmpty()
                         select new ref_role_dashboard_mapping_vm
                         {
                                dashboard_id=d.dashboard_id,
                                dashboard_name=d.dashboard_name,
                                module_id=d.module_id,
                                module_name=m.module_name,
                                is_active=rdm==null?false:rdm.is_active,
                         }
                       ).ToList();
            return query;
            
        }

        public bool UpdateRecords(ref_role_dashboard_mapping_vm vm)
        {

            try
            {
                if (vm.dashboard_id1 != null)
                {
                    List<ref_role_dashboard_mapping> list = _scifferContext.ref_role_dashboard_mapping.Where(x => x.role_id == vm.role_id).ToList();
                    foreach (var item in list)
                    {

                        _scifferContext.Entry(item).State = EntityState.Deleted;
                    }
                   
                    for (var i = 0; i < vm.dashboard_id1.Count; i++)
                    {
                        ref_role_dashboard_mapping rmc = new ref_role_dashboard_mapping();
                        rmc.dashboard_id = int.Parse(vm.dashboard_id1[i]);
                        rmc.role_id = vm.role_id;
                        rmc.is_active = vm.is_active1[i] == "0" ? false : true;                       
                        _scifferContext.ref_role_dashboard_mapping.Add(rmc);                       
                    }
                    _scifferContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;

            }
            return true;
        }
    }
}
