using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Implementation
{
    public class ProcessSequence : IProcessSequence
    {
        private readonly ScifferContext _scifferContext;

        public ProcessSequence(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public List<process_sequence_vm> GetAll()
        {
            var list = new List<process_sequence_vm>();
            try
            {
                list = (from qc in _scifferContext.mfg_process_sequence
                        select new
                        {
                            process_sequence_id = qc.process_sequence_id,
                            process_sequence_name = qc.process_sequence_name,
                            ITEM_ID = qc.ITEM_ID,
                            ITEM_NAME = qc.REF_ITEM.ITEM_NAME,
                            ITEM_CODE = qc.REF_ITEM.ITEM_CODE,
                            is_blocked = qc.is_blocked,
                        }).ToList()
                        .Select(x => new process_sequence_vm
                        {
                            process_sequence_id = x.process_sequence_id,
                            process_sequence_name = x.process_sequence_name,
                            ITEM_ID = x.ITEM_ID,
                            ITEM_CODE = x.ITEM_CODE + " - " + x.ITEM_NAME,
                            is_blocked = x.is_blocked,
                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public process_sequence_vm Get(int? id)
        {
            var list = new process_sequence_vm();
            var list1 = new List<process_sequence_detail_vm>();
            try
            {
                list1 = (from ed in _scifferContext.mfg_process_sequence_detail

                         join m in _scifferContext.ref_machine on ed.machine_id equals m.machine_id into m1
                         from m2 in m1.DefaultIfEmpty()

                         join p in _scifferContext.ref_mfg_process on ed.process_id equals p.process_id into p1
                         from p2 in p1.DefaultIfEmpty()

                         where ed.process_sequence_id == id
                         select new
                         {
                             map_process_sequence_id = ed.map_process_sequence_id,
                             machine_id = ed.machine_id,
                             process_id = ed.process_id,
                             process_sequence_id = ed.process_sequence_id,
                             machine_name = m2.machine_name,
                             machine_code = m2.machine_code,
                             process_description = p2.process_description,
                             process_code = p2.process_code,
                             item_cost = ed.item_cost
                         }).ToList()
                        .Select(x => new process_sequence_detail_vm
                        {
                            map_process_sequence_id = x.map_process_sequence_id,
                            process_sequence_id = x.process_sequence_id,
                            machine_id = x.machine_id,
                            process_id = x.process_id,
                            process_code = x.process_code + " - " + x.process_description,
                            machine_code = x.machine_code + " - " + x.machine_name,
                            item_cost = x.item_cost,
                        }).ToList();

            }
            catch (Exception ex)
            {

            }
            try
            {
                list = (from qc in _scifferContext.mfg_process_sequence
                        where qc.process_sequence_id == id
                        select new
                        {
                            process_sequence_id = qc.process_sequence_id,
                            process_sequence_name = qc.process_sequence_name,
                            ITEM_ID = qc.ITEM_ID,
                            is_blocked = qc.is_blocked,

                        }).ToList()
                        .Select(x => new process_sequence_vm
                        {
                            process_sequence_id = x.process_sequence_id,
                            process_sequence_name = x.process_sequence_name,
                            ITEM_ID = x.ITEM_ID,
                            is_blocked = x.is_blocked,
                            process_sequence_detail_vm = list1,
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<REF_ITEM> GetItemCode()
        {
            List<REF_ITEM> list = new List<REF_ITEM>();
            try
            {
                list = (from ri in _scifferContext.REF_ITEM
                        where ri.is_active == true
                        select new
                        {
                            ITEM_ID = ri.ITEM_ID,
                            ITEM_NAME = ri.ITEM_NAME,
                            ITEM_CODE = ri.ITEM_CODE,
                        }).ToList()
                        .Select(x => new REF_ITEM
                        {
                            ITEM_ID = x.ITEM_ID,
                            ITEM_CODE = x.ITEM_CODE + " - " + x.ITEM_NAME,
                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<ref_machine> GetMachineCode()
        {
            List<ref_machine> list = new List<ref_machine>();
            try
            {
                list = (from ri in _scifferContext.ref_machine
                        where ri.is_active == true
                        select new
                        {
                            machine_id = ri.machine_id,
                            machine_name = ri.machine_name,
                            machine_code = ri.machine_code,
                        }).ToList()
                        .Select(x => new ref_machine
                        {
                            machine_id = x.machine_id,
                            machine_code = x.machine_code + " - " + x.machine_name,
                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<ref_mfg_process> GetProcessCode()
        {
            List<ref_mfg_process> list = new List<ref_mfg_process>();
            try
            {
                list = (from ri in _scifferContext.ref_mfg_process
                        where ri.is_active == true
                        select new
                        {
                            process_id = ri.process_id,
                            process_name = ri.process_description,
                            process_code = ri.process_code,
                        }).ToList()
                        .Select(x => new ref_mfg_process
                        {
                            process_id = x.process_id,
                            process_code = x.process_code + " - " + x.process_name,
                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<ref_machine> GetMachinebyProcess(int process_id)
        {
            var data = new List<ref_machine>();
            try
            {
                data = (from ed in _scifferContext.ref_mfg_process

                        join mp in _scifferContext.map_mfg_process_machine on ed.process_id equals mp.process_id into mp1
                        from mp2 in mp1.DefaultIfEmpty()

                        join m in _scifferContext.ref_machine on mp2.machine_id equals m.machine_id into m1
                        from m2 in m1.DefaultIfEmpty()

                        where (mp2.process_id == process_id)
                        select new
                        {
                            machine_id = m2.machine_id,
                            machine_name = m2.machine_name,
                            machine_code = m2.machine_code,

                        }).ToList().Select(x => new ref_machine()
                        {
                            machine_id = x.machine_id,
                            machine_name = x.machine_code + " - " + x.machine_name,
                        }).ToList();

            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public bool SaveProcessSequence(process_sequence_vm vm)
        {
            try
            {
                mfg_process_sequence ps = new mfg_process_sequence();
                ps.process_sequence_name = vm.process_sequence_name;
                ps.ITEM_ID = vm.ITEM_ID;
                ps.is_blocked = false;
                _scifferContext.mfg_process_sequence.Add(ps);

                for (var i = 0; i < vm.process_id.Count; i++)
                {
                    mfg_process_sequence_detail qcp = new mfg_process_sequence_detail();
                    qcp.machine_id = Convert.ToInt32(vm.machine_id[i]);
                    qcp.process_id = Convert.ToInt32(vm.process_id[i]);
                    qcp.item_cost = Convert.ToDouble(vm.item_cost[i]);
                    _scifferContext.mfg_process_sequence_detail.Add(qcp);
                }

                _scifferContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateProcessSequence(process_sequence_vm vm)
        {
            try
            {
                mfg_process_sequence qc = _scifferContext.mfg_process_sequence.Where(x => x.process_sequence_id == vm.process_sequence_id).FirstOrDefault();

                qc.process_sequence_name = vm.process_sequence_name;
                qc.ITEM_ID = vm.ITEM_ID;
                qc.is_blocked = vm.is_blocked;
                _scifferContext.Entry(qc).State = System.Data.Entity.EntityState.Modified;

                List<mfg_process_sequence_detail> qp = _scifferContext.mfg_process_sequence_detail.Where(x => x.process_sequence_id == vm.process_sequence_id).ToList();
                foreach (var item in qp)
                {
                    _scifferContext.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                try
                {
                    for (var i = 0; i < vm.process_id.Count; i++)
                    {
                        mfg_process_sequence_detail qcp = new mfg_process_sequence_detail();
                        qcp.machine_id = Convert.ToInt32(vm.machine_id[i]);
                        qcp.process_id = Convert.ToInt32(vm.process_id[i]);
                        qcp.item_cost = Convert.ToDouble(vm.item_cost[i]);
                        qcp.process_sequence_id = vm.process_sequence_id;
                        _scifferContext.mfg_process_sequence_detail.Add(qcp);
                    }

                }
                catch (Exception ex)
                {

                }

                _scifferContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
