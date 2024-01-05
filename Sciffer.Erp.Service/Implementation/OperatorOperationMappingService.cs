using Sciffer.Erp.Data;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Implementation
{
    public class OperatorOperationMappingService : IOperatorOperationMappingService
    {
        private readonly ScifferContext _scifferContext;

        public OperatorOperationMappingService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        //Get operator list
        public List<ref_user_management_VM> GetOperatorList()
        {
            var result = (from rum in _scifferContext.ref_user_management
                          join rurm in _scifferContext.ref_user_role_mapping on rum.user_id equals rurm.user_id
                          join rumr in _scifferContext.ref_user_management_role on rurm.role_id equals rumr.role_id
                          join re in _scifferContext.REF_EMPLOYEE on rum.employee_id equals re.employee_id
                          where (rumr.role_code == "PROD_OP") && (re.is_block == false) && rum.is_block == false
                          && re.is_block == false
                          select new ref_user_management_VM
                          {
                              user_id = rum.user_id,
                              user_name = rum.user_name + " - " + re.employee_name,
                          }).ToList();

            result = result.GroupBy(x => new { x.user_id }).Select(y => new ref_user_management_VM
            {
                user_id = y.FirstOrDefault().user_id,
                user_name = y.FirstOrDefault().user_name
            }).ToList();
            return result;
        }

        //get process list
        public List<ref_mfg_process> GetProcessList()
        {
            return _scifferContext.ref_mfg_process.Where(x=> x.is_blocked == false).ToList();
        }

        //get mapped data of operator and operation
        public List<map_operator_operation_vm> GetOperatorOperationMapList()
        {
            try
            {
                var query = (from rum in _scifferContext.ref_user_management.Where(x=> !x.is_block)
                             join rurm in _scifferContext.ref_user_role_mapping.Where(x=> !x.is_block && x.is_active) on rum.user_id equals rurm.user_id
                             join moo in _scifferContext.map_operation_operator on rum.user_id equals moo.operator_id into subpet1
                             from sub in subpet1.DefaultIfEmpty()
                             where rurm.ref_user_management_role.role_code == "PROD_OP"
                             select new
                             {
                                 operator_id = rum.user_id,
                                 operator_name = rum.user_name + " - " + rum.REF_EMPLOYEE.employee_name,
                                 map_operation_operator = _scifferContext.map_operation_operator.Where(x => x.operator_id == rum.user_id).ToList(),

                             }).ToList().Select(x => new map_operator_operation_vm()
                             {
                                 operator_id = x.operator_id,
                                 operator_name = x.operator_name,
                                 operation_id = x.map_operation_operator == null ? "" : string.Join(",", x.map_operation_operator.Select(z => z.operation_id.ToString()).ToArray()),
                                 operation_name = x.map_operation_operator == null ? "" : string.Join(",", x.map_operation_operator.Select(z => z.ref_mfg_process.process_code.ToString() + " - " + z.ref_mfg_process.process_description.ToString()).ToArray()),

                             }).ToList();

                var list = query.GroupBy(i => new { i.operator_id }).Select(x => new map_operator_operation_vm()
                {
                    operator_id = x.Key.operator_id,
                    operator_name = x.Select(z => z.operator_name).FirstOrDefault(),
                    operation_id = x.Select(z => z.operation_id).FirstOrDefault(),
                    operation_name = x.Select(z => z.operation_name).FirstOrDefault(),
                }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string UpdateOperatorOperationMapping(int operator_id, string operation_id)
        {
            try
            {
                List<map_operation_operator> map_operation_operator = _scifferContext.map_operation_operator.Where(x => x.operator_id == operator_id).ToList();
                foreach (var item in map_operation_operator)
                {
                    _scifferContext.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                int[] arr = operation_id.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

                for (var i = 0; i < arr.Length; i++)
                {
                    map_operation_operator moo = new map_operation_operator();
                    moo.operation_id = Convert.ToInt32(arr[i]);
                    moo.operator_id = operator_id;
                    _scifferContext.map_operation_operator.Add(moo);
                }
                _scifferContext.SaveChanges();
                return "Saved";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetMachineRole(int user_id)
        {
            var process = _scifferContext.map_operation_operator.Where(x => x.operator_id == user_id).ToList();
            return string.Join(",", process.Select(x => x.operation_id));
        }
    }
}
