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
   public class MultiMachiningService : IMultiMachiningService
    {
        private readonly ScifferContext _scifferContext;

        public MultiMachiningService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool UpdateRecords(ref_mfg_multi_machining_vm vm)
        {
            try
            {

                var list = _scifferContext.ref_mfg_multi_machining.ToList(); 

                if(list.Count != 0)
                {
                    foreach (var item in list)
                    {
                        _scifferContext.Entry(item).State = EntityState.Deleted;
                    }
                }
                
             
                for (var i = 0; i < vm.srnos.Count; i++)
                {
                    {
                        ref_mfg_multi_machining rmc = new ref_mfg_multi_machining();

                        rmc.machine_group_id = Convert.ToInt32(vm.srnos[i]);
                        rmc.machine_id = Convert.ToString(vm.mac_id1[i]);

                        _scifferContext.ref_mfg_multi_machining.Add(rmc);

                    }
                }

                for (var i = 0; i < vm.srnos.Count; i++)
                {
                    {
                        ref_mfg_multi_machining rmc = new ref_mfg_multi_machining();

                        rmc.machine_group_id = Convert.ToInt32(vm.srnos[i]);

                        rmc.machine_id = Convert.ToString(vm.mac_id2[i]);
                        _scifferContext.ref_mfg_multi_machining.Add(rmc);

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
        public string DeleteMultiMachine(int group_id)
        {
            try
            {
                var COLLECTION = _scifferContext.ref_mfg_multi_machining.Where(x => x.machine_group_id == group_id).ToList();
                foreach (var VARIABLE in COLLECTION)
                {
                    _scifferContext.Entry(VARIABLE).State = EntityState.Deleted;
                    _scifferContext.SaveChanges();
                }
                return "Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
