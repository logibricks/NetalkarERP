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
    public class QualityControlParameterService : IQualityControlParameterService
    {
        private readonly ScifferContext _scifferContext;

        public QualityControlParameterService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public List<mfg_qc_vm> GetAll()
        {
            var list = new List<mfg_qc_vm>();
            try
            {
                //list = (from qc in _scifferContext.mfg_qc
                //        where qc.is_active == true
                //        select new
                //        {
                //            mfg_qc_id = qc.mfg_qc_id,
                //            machine_id = qc.machine_id,
                //            item_id = qc.item_id,
                //            frequency = qc.frequency,
                //            item_name = qc.REF_ITEM.ITEM_NAME,
                //            item_code = qc.REF_ITEM.ITEM_CODE,
                //            machine_name = qc.ref_machine.machine_name,
                //            machine_code = qc.ref_machine.machine_code,

                //        }).ToList()
                //        .Select(x => new mfg_qc_vm
                //        {
                //            mfg_qc_id = x.mfg_qc_id,
                //            machine_id = x.machine_id,
                //            item_id = x.item_id,
                //            frequency = x.frequency,
                //            item_code = x.item_code + " - " + x.item_name,
                //            machine_code = x.machine_code + " - " + x.machine_name
                //        }).ToList();
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

        public quality_parameter_vm Get(int? id)
        {
            var list = new quality_parameter_vm();
            var list1 = new List<mfg_qc_vm>();
            try
            {
                list1 = (from p in _scifferContext.mfg_qc_parameter
                         where p.mfg_qc_id == id
                         select new
                         {
                             mfg_qc_id = p.mfg_qc_id,
                             mfg_qc_parameter_id = p.mfg_qc_parameter_id,
                             parameter_name = p.parameter_name,
                             parameter_uom = p.parameter_uom,
                             std_range_end = p.std_range_end,
                             std_range_start = p.std_range_start,

                         }).ToList()
                        .Select(x => new mfg_qc_vm
                        {
                            mfg_qc_id = x.mfg_qc_id,
                            mfg_qc_parameter_id = x.mfg_qc_parameter_id,
                            parameter_name = x.parameter_name,
                            parameter_uom = x.parameter_uom,
                            std_range_end = x.std_range_end,
                            std_range_start = x.std_range_start,
                        }).ToList();

            }
            catch (Exception ex)
            {

            }
            try
            {
                //list = (from qc in _scifferContext.mfg_qc
                //        where qc.mfg_qc_id == id
                //        select new
                //        {
                //            mfg_qc_id = qc.mfg_qc_id,
                //            //item_id = qc.item_id,
                //            machine_id = qc.machine_id,
                //            //frequency = qc.frequency,

                //        }).ToList()
                //        .Select(x => new quality_parameter_vm
                //        {
                //            mfg_qc_id = x.mfg_qc_id,
                //            item_code = x.item_id,
                //            machine_code = x.machine_id,
                //            frequency = x.frequency,
                //            mfg_qc_vm = list1,
                //        }).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return list;

        }

        public bool SaveQCParameter(quality_parameter_vm vm)
        {
            try
            {
                //mfg_qc qc = new mfg_qc();
                //qc.item_id = vm.item_code;
                //qc.machine_id = vm.machine_code;
                //qc.frequency = vm.frequency;
                //qc.is_active = true;
                //_scifferContext.mfg_qc.Add(qc);

                //for (var i = 0; i < vm.para_name.Count; i++)
                //{
                //    mfg_qc_parameter qcp = new mfg_qc_parameter();
                //    qcp.parameter_name = vm.para_name[i];
                //    qcp.parameter_uom = vm.para_uom[i];
                //    qcp.std_range_start = vm.para_from[i];
                //    qcp.std_range_end = vm.para_to[i];
                //    int a;
                //    if (int.TryParse(qcp.std_range_start, out a))
                //    {
                //        qcp.is_numeric = true;
                //    }
                //    else
                //    {
                //        qcp.is_numeric = false;
                //    }

                //    _scifferContext.mfg_qc_parameter.Add(qcp);
                //}

                //_scifferContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateQCParameter(quality_parameter_vm vm)
        {
            try
            {
                //mfg_qc qc = _scifferContext.mfg_qc.Where(x => x.mfg_qc_id == vm.mfg_qc_id).FirstOrDefault();

                //qc.item_id = vm.item_code;
                //qc.machine_id = vm.machine_code;
                //qc.frequency = vm.frequency;
                //_scifferContext.Entry(qc).State = System.Data.Entity.EntityState.Modified;

                //List<mfg_qc_parameter> qp = _scifferContext.mfg_qc_parameter.Where(x => x.mfg_qc_id == vm.mfg_qc_id).ToList();
                //foreach(var item in qp)
                //{
                //    _scifferContext.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                //}

                //for (var i = 0; i < vm.para_name.Count; i++)
                //{
                //    mfg_qc_parameter qcp = new mfg_qc_parameter();
                //    qcp.parameter_name = vm.para_name[i];
                //    qcp.parameter_uom = vm.para_uom[i];
                //    qcp.std_range_start = vm.para_from[i];
                //    qcp.std_range_end = vm.para_to[i];
                //    qcp.mfg_qc_id = vm.mfg_qc_id;
                //    int a;
                //    if (int.TryParse(qcp.std_range_start, out a))
                //    {
                //        qcp.is_numeric = true;
                //    }
                //    else
                //    {
                //        qcp.is_numeric = false;
                //    }

                //    _scifferContext.mfg_qc_parameter.Add(qcp);
                //}

                //_scifferContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
