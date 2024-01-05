using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Implementation
{
    public class IncentiveApplicabilityService : IIncentiveApplicabilityService
    {

        private readonly ScifferContext _scifferContext;

        public IncentiveApplicabilityService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool UpdateRecords(ref_mfg_operator_incentive_appl_vm vm)
        {
            try
            {  
                foreach (var item in vm.user_id_name)
                {
                    var user_id = Convert.ToUInt32(item);
                    ref_mfg_operator_incentive_appl list = _scifferContext.ref_mfg_operator_incentive_appl.Where(x => x.user_id == user_id).FirstOrDefault();
                    _scifferContext.Entry(list).State = EntityState.Deleted;
                }

                for (var i = 0; i < vm.user_id_name.Count; i++)
                {
                    {
                        ref_mfg_operator_incentive_appl rmc = new ref_mfg_operator_incentive_appl();
                        rmc.user_id = vm.user_id;
                        rmc.user_id = Convert.ToInt32(vm.user_id_name[i]);
                        rmc.is_incentive_applicable = Convert.ToBoolean(vm.create_rights_name[i]);
                        _scifferContext.ref_mfg_operator_incentive_appl.Add(rmc);
                       
                    }
                }
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;

            }
            return true;
        }
    }
}
