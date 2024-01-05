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
    public class ProcessMachineMappingService : IProcessMachineMappingService
    {
        private readonly ScifferContext _scifferContext;

        public ProcessMachineMappingService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        //public List<process_machine_mapping_VM> GetAll()
        //{
        //    var result = new List<process_machine_mapping_VM>();
        //    try
        //    {
        //        result = (from mp in _scifferContext.ref_mfg_process
        //                  select new
        //                  {
        //                      process_id = mp.process_id,
        //                      process_code = mp.process_code,
        //                      process_description = mp.process_description,
        //                      map_mfg_process_machine = _scifferContext.map_mfg_process_machine.Where(x => x.process_id == mp.process_id).ToList(),
        //                  }).ToList()
        //                  .Select(x => new process_machine_mapping_VM
        //                  {
        //                      process_id_get = x.process_id,
        //                      process_desc = x.process_code + "-" + x.process_description,
        //                      machine_id_get = x.map_mfg_process_machine == null ? "" : string.Join(" ,", x.map_mfg_process_machine.Select(z => z.machine_id).ToArray()),
        //                      machine_desc = x.map_mfg_process_machine == null ? "" : string.Join(" ,", x.map_mfg_process_machine.Select(z => z.ref_machine.machine_code.ToString() + " - " + z.ref_machine.machine_name.ToString()).ToArray()),
        //                  }).ToList();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return result;
        //}

        //public List<ref_machine> GetAllMachines()
        //{
        //    List<ref_machine> list = new List<ref_machine>();
        //    try
        //    {
        //        list = (from ed in _scifferContext.ref_machine

        //                where (ed.is_active == true)
        //                select new
        //                {
        //                    Machine_id = ed.machine_id,
        //                    Machine_name = ed.machine_name,
        //                    Machine_description = ed.machine_code,


        //                }).ToList().Select(x => new ref_machine()
        //                {
        //                    machine_id = x.Machine_id,
        //                    machine_name = x.Machine_description + " - " + x.Machine_name,
        //                }).ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return list;
        //}

        public List<ref_mfg_process> GetAllProcess()
        {
            List<ref_mfg_process> list = new List<ref_mfg_process>();
            try
            {
                list = (from ed in _scifferContext.ref_mfg_process
                        where (ed.is_active == true)
                        select new
                        {
                            Process_id = ed.process_id,
                            Process_code = ed.process_code,
                            Process_description = ed.process_description,

                        }).ToList().Select(x => new ref_mfg_process()
                        {
                            process_id = x.Process_id,
                            process_code = x.Process_code + " - " + x.Process_description,


                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }


        public string UpdateProcessMapping(int processid, string machineids)
        {
            try
            {
                List<map_mfg_process_machine> map_mfg_process_machine = _scifferContext.map_mfg_process_machine.Where(x=>x.process_id == processid).ToList();

                foreach (var item in map_mfg_process_machine)
                {
                    _scifferContext.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                int[] arr = machineids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                for (int j = 0; j < arr.Length; j++)
                {
                    map_mfg_process_machine pm = new map_mfg_process_machine();
                    pm.process_id = Convert.ToInt32(processid);
                    pm.machine_id = arr[j];
                    _scifferContext.map_mfg_process_machine.Add(pm);
                }


                _scifferContext.SaveChanges();
                return "Saved";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //-----------------------Old code------------------------
        public List<ref_mfg_process_VM> GetProcessDetails()
        {
            //var query = _scifferContext.ref_mfg_process.Where(x => x.is_active == true).ToList();

            var query = (from mp in _scifferContext.ref_mfg_process
                         join mpm in _scifferContext.map_mfg_process_machine on mp.process_id equals mpm.process_id into subpet1
                         from sub in subpet1.DefaultIfEmpty()
                         select new
                         {
                             process_id = mp.process_id,
                             process_code = mp.process_code,
                             process_description = mp.process_description,
                             map_mfg_process_machine = _scifferContext.map_mfg_process_machine.Where(x => x.process_id == mp.process_id).ToList(),


                         }).ToList().Select(x => new ref_mfg_process_VM()
                         {
                             process_id = x.process_id,
                             process_code = x.process_code + " - " + x.process_description,
                             machine_id = x.map_mfg_process_machine == null ? "" : string.Join(",", x.map_mfg_process_machine.Select(z => z.machine_id.ToString()).ToArray()),
                             machine_name = x.map_mfg_process_machine == null ? "" : string.Join(",", x.map_mfg_process_machine.Select(z => z.ref_machine.machine_code.ToString() + " - " + z.ref_machine.machine_name.ToString()).ToArray()),

                         }).ToList();

            var list = query.GroupBy(i => new { i.process_id }).Select(x => new ref_mfg_process_VM()
            {
                process_id = x.Key.process_id,
                process_code = x.Select(z => z.process_code).FirstOrDefault(),
                machine_id = x.Select(z => z.machine_id).FirstOrDefault(),
                machine_name = x.Select(z => z.machine_name).FirstOrDefault(),
            }).ToList();


            return list;
        }
        //-----------------------------------------------------------------------
        public List<ref_machine> GetAllMachinesForProcess(int process_id)
        {
            List<ref_machine> list2 = new List<ref_machine>();
            List<ref_machine> list3 = new List<ref_machine>();

            try
            {
                //var query1 = (from m in _scifferContext.map_mfg_process_machine
                //              select (m.machine_id)).ToList();

                var query2 = (from m in _scifferContext.map_mfg_process_machine
                              where (m.process_id == process_id)
                              select (m.machine_id)).ToList();

                //list1 = (from ed in _scifferContext.ref_machine
                //         where (ed.is_active == true && !query1.Contains(ed.machine_id))
                //         select new
                //         {
                //             Machine_id = ed.machine_id,
                //             Machine_name = ed.machine_name,
                //             Machine_description = ed.machine_code,

                //         }).ToList().Select(x => new ref_machine()
                //         {
                //             machine_id = x.Machine_id,
                //             machine_name = x.Machine_description + " - " + x.Machine_name,
                //         }).ToList();


                list2 = (from ed in _scifferContext.ref_machine
                         where (ed.is_active == true && query2.Contains(ed.machine_id))
                         select new
                         {
                             Machine_id = ed.machine_id,
                             Machine_name = ed.machine_name,
                             Machine_description = ed.machine_code,

                         }).ToList().Select(x => new ref_machine()
                         {
                             machine_id = x.Machine_id,
                             machine_name = x.Machine_description + " - " + x.Machine_name,
                         }).ToList();

                list3 = list2.OrderBy(x => x.machine_id).ToList();

            }

            catch (Exception ex)
            {

            }

            return list3;
        }
        //------------------------------End--------------------------------------------------
    }
}
