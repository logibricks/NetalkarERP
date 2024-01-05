using Sciffer.Erp.Data;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class OperatorScreenUpgradationService : IOperatorScreenUpgradationService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name

        public OperatorScreenUpgradationService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public List<ref_machine_master_VM> GetMachineListByProcess(int process_id)
        {
            var list = new List<ref_machine_master_VM>();
            try
            {
                list = (from mmpm in _scifferContext.map_mfg_process_machine
                        join rm in _scifferContext.ref_machine on mmpm.machine_id equals rm.machine_id
                        where mmpm.process_id == process_id
                        select new ref_machine_master_VM
                        {
                            machine_id = rm.machine_id,
                            machine_name = rm.machine_name,
                        }).ToList();

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
            }
            return list;
        }

        public string UploadFile(mfg_machine_item_upgradation mfg_machine_item_upgradation)
        {
            try
            {

                mfg_machine_item_upgradation mmip = new mfg_machine_item_upgradation();
                mmip.item_id = mfg_machine_item_upgradation.item_id;
                mmip.process_id = mfg_machine_item_upgradation.process_id;
                mmip.machine_id = mfg_machine_item_upgradation.machine_id;
                mmip.file_name = mfg_machine_item_upgradation.file_name;
                mmip.file_path = mfg_machine_item_upgradation.file_path;
                _scifferContext.mfg_machine_item_upgradation.Add(mmip);
                _scifferContext.SaveChanges();
                return "Saved";
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return ex.Message;
            }
        }

        public List<mfg_machine_item_upgradation> GetFileByItemMachineId(int machine_id, int item_id)
        {
            return _scifferContext.mfg_machine_item_upgradation.Where(x => x.machine_id == machine_id && x.item_id == item_id).ToList();
        }

        public mfg_machine_item_upgradation Update(mfg_machine_item_upgradation itemvaluation)
        {
            try
            {
                var mfg_machine_item_upgradation = _scifferContext.mfg_machine_item_upgradation.FirstOrDefault(x => x.mfg_upgradation_id == itemvaluation.mfg_upgradation_id);
                mfg_machine_item_upgradation.is_active = false;
                _scifferContext.Entry(mfg_machine_item_upgradation).State = EntityState.Modified;
                _scifferContext.SaveChanges();

                mfg_machine_item_upgradation mmip = new mfg_machine_item_upgradation();
                mmip.item_id = itemvaluation.item_id;
                mmip.process_id = itemvaluation.process_id;
                mmip.machine_id = itemvaluation.machine_id;
                mmip.file_name = itemvaluation.file_name;
                mmip.file_path = itemvaluation.file_path;
                mmip.is_active = true;
                _scifferContext.mfg_machine_item_upgradation.Add(mmip);
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return itemvaluation;
            }
            return itemvaluation;
        }

        public bool Delete(int id)
        {
            try
            {
                var mfg_machine_item_upgradation = _scifferContext.mfg_machine_item_upgradation.FirstOrDefault(x => x.mfg_upgradation_id == id);
                mfg_machine_item_upgradation.is_active = false;
                _scifferContext.Entry(mfg_machine_item_upgradation).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<mfg_machine_item_upgradation_vm> getall()
        {
            var list = (from mmip in _scifferContext.mfg_machine_item_upgradation.Where(x => x.is_active == true)
                        join rm in _scifferContext.ref_machine on mmip.machine_id equals rm.machine_id
                        join rmp in _scifferContext.ref_mfg_process on mmip.process_id equals rmp.process_id
                        join ri in _scifferContext.REF_ITEM on mmip.item_id equals ri.ITEM_ID
                        select new mfg_machine_item_upgradation_vm
                        {
                            mfg_upgradation_id = mmip.mfg_upgradation_id,
                            machine_id = mmip.machine_id,
                            machine_name = rm.machine_name,
                            item_id = mmip.item_id,
                            item_name = ri.ITEM_NAME,
                            process_id = mmip.process_id,
                            process_name = rmp.process_description,
                            file_name = mmip.file_name,
                        }).ToList();

            return list;
        }

        public mfg_machine_item_upgradation_vm get(int id)
        {
            var list = (from mmip in _scifferContext.mfg_machine_item_upgradation.Where(x => x.mfg_upgradation_id == id)
                        join rm in _scifferContext.ref_machine on mmip.machine_id equals rm.machine_id
                        join rmp in _scifferContext.ref_mfg_process on mmip.process_id equals rmp.process_id
                        join ri in _scifferContext.REF_ITEM on mmip.item_id equals ri.ITEM_ID
                        select new mfg_machine_item_upgradation_vm
                        {
                            mfg_upgradation_id = mmip.mfg_upgradation_id,
                            machine_id = mmip.machine_id,
                            machine_name = rm.machine_name,
                            item_id = mmip.item_id,
                            item_name = ri.ITEM_NAME,
                            process_id = mmip.process_id,


                        }).FirstOrDefault();

            return list;
        }
    }
}
