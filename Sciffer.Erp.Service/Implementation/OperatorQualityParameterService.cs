using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class OperatorQualityParameterService : IOperatorQualityParameterService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericservice;
        public OperatorQualityParameterService(ScifferContext scifferContext, IGenericService genericservice)
        {
            _scifferContext = scifferContext;
            _genericservice = genericservice;
        }

        public string Add(mfg_op_qc_parameter_VM vm)
        {

            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                //Detail Table
                dt.Columns.Add("mfg_op_qc_parameter_list_id ", typeof(int));
                dt.Columns.Add("mfg_op_qc_parameter_id ", typeof(int));
                dt.Columns.Add("parameter_name ", typeof(string));
                dt.Columns.Add("parameter_uom ", typeof(string));
                dt.Columns.Add("std_range_start ", typeof(string));
                dt.Columns.Add("std_range_end ", typeof(string));
                dt.Columns.Add("is_numeric ", typeof(bool));


                if (vm.mfg_op_qc_parameter_list_id != null)
                {
                    for (int i = 0; i < vm.mfg_op_qc_parameter_list_id.Count; i++) //Detail Table
                    {

                        dt.Rows.Add(vm.mfg_op_qc_parameter_list_id[i] == "" ? 0 : int.Parse(vm.mfg_op_qc_parameter_list_id[i]),
                            vm.mfg_op_qc_parameter_id == null ? 0 : vm.mfg_op_qc_parameter_id,
                            vm.parameter_name[i],
                            vm.paramter_uom[i],
                            vm.std_range_start[i],
                            vm.std_range_end[i],
                        vm.is_numeric1[i]);
                    }
                }

                DataTable dt1 = new DataTable();
                //Detail Table
                dt1.Columns.Add("machine_id ", typeof(int));
                var machinelist = vm.machine_id_list.Split(',');
                if (machinelist.Length > 0)
                {
                    for (int i = 0; i < machinelist.Length; i++) //Detail Table
                    {
                        if (machinelist[i] != "")
                        {
                            dt1.Rows.Add(machinelist[i]);
                        }

                    }
                }

                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                //Header Table
                var created_by = new SqlParameter("@created_by", createdby);
                var mfg_op_qc_parameter_id = new SqlParameter("@mfg_op_qc_parameter_id", vm.mfg_op_qc_parameter_id == null ? -1 : vm.mfg_op_qc_parameter_id);
                var item_id = new SqlParameter("@item_id", vm.item_id);
                var machine_id = new SqlParameter("@machine_id", vm.machine_id);
                var process_id = new SqlParameter("@process_id", vm.process_id);
                var deleteids = new SqlParameter("@deleteids", vm.deleteids==null?"" : vm.deleteids);
                //created new User Defind Table of Detail Table
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_mfg_op_qc_parameter_list";
                t1.Value = dt;
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_machine_id_list";
                t2.Value = dt1;
                //Pushing Parameter in Procedure 
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_mfg_op_qc_parameter_list @mfg_op_qc_parameter_id,@item_id,@created_by,@deleteids,@t1,@t2,@process_id ",
                    mfg_op_qc_parameter_id, item_id, created_by, deleteids, t1, t2, process_id).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    return val;
                }
                else
                {
                    return val;
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public mfg_op_qc_parameter_VM Get (int? id,int? id1)
        {
            //mfg_op_qc_parameter JR = _scifferContext.mfg_op_qc_parameter.FirstOrDefault(c => c.mfg_op_qc_parameter_id == id);
            var ent = new SqlParameter("@entity", "getopqualityparameterHeader");
            var ids = new SqlParameter("@id", id == null ? 0 : id);
            var item_id = new SqlParameter("@item_id", id1 == null ? 0 : id1);
            mfg_op_qc_parameter_VM JR = _scifferContext.Database.SqlQuery<mfg_op_qc_parameter_VM>("exec get_edit_detail @entity,@id,@item_id", ent, ids, item_id).FirstOrDefault();
            Mapper.CreateMap<mfg_op_qc_parameter, mfg_op_qc_parameter_VM>();
            mfg_op_qc_parameter_VM JRVM = Mapper.Map<mfg_op_qc_parameter_VM, mfg_op_qc_parameter_VM>(JR);
            //JRVM.mfg_op_qc_parameter_list = JRVM.mfg_op_qc_parameter_list.Where(x => x.is_active == true).ToList();
            var ent1 = new SqlParameter("@entity", "getopqualityparameterDetail");
            var ids1 = new SqlParameter("@id", id == null ? 0 : id);
            var item_ids = new SqlParameter("@item_id", id1 == null ? 0 : id1);
            JRVM.mfg_op_qc_parameter_list = _scifferContext.Database.SqlQuery<mfg_op_qc_parameter_list>("exec get_edit_detail @entity,@id,@item_id", ent1, ids1, item_ids).ToList();
            return JRVM;
        }

        public List<mfg_op_qc_parameter_VM> GetAll()
        {
            //var query = (from moqc in _scifferContext.mfg_op_qc_parameter
            //             join ri in _scifferContext.REF_ITEM on moqc.item_id equals ri.ITEM_ID
            //             join rm in _scifferContext.ref_machine on moqc.machine_id equals rm.machine_id
            //             select new mfg_op_qc_parameter_VM
            //             {
            //                 mfg_op_qc_parameter_id = moqc.mfg_op_qc_parameter_id,
            //                 machine_id = moqc.machine_id,
            //                 item_id = moqc.item_id,
            //                 machine_name = rm.machine_name,
            //                 item_name = ri.ITEM_NAME,
            //             }).OrderByDescending(a => a.mfg_op_qc_parameter_id).ToList();
            var ent = new SqlParameter("@entity", "getoperatorqualityparameter");
            var query = _scifferContext.Database.SqlQuery<mfg_op_qc_parameter_VM>(
            "exec get_index_detail @entity", ent).ToList();
            return query;
        }

        public string Update(mfg_op_qc_parameter_VM vm)
        {
            try
            {
                mfg_op_qc_parameter data = _scifferContext.mfg_op_qc_parameter.Where(x => x.mfg_op_qc_parameter_id == vm.mfg_op_qc_parameter_id).FirstOrDefault();
                data.item_id = (int)vm.item_id;
                data.machine_id = (int)vm.machine_id;
                data.modified_by = 1;
                data.modify_ts = DateTime.Now;
                _scifferContext.Entry(data).State = System.Data.Entity.EntityState.Modified;

                List<mfg_op_qc_parameter_list> list = _scifferContext.mfg_op_qc_parameter_list.Where(x => x.mfg_op_qc_parameter_id == vm.mfg_op_qc_parameter_id).ToList();
                foreach (var i in list)
                {
                    _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Deleted;
                }

                for (int i = 0; i < vm.parameter_name.Count(); i++)
                {
                    mfg_op_qc_parameter_list mpl = new mfg_op_qc_parameter_list();
                    mpl.mfg_op_qc_parameter_id = (int)vm.mfg_op_qc_parameter_id;
                    mpl.parameter_name = vm.parameter_name[i];
                    mpl.parameter_uom = vm.paramter_uom[i];
                    mpl.std_range_end = vm.std_range_end[i];
                    mpl.std_range_start = vm.std_range_start[i];
                    int a;
                    if (int.TryParse(mpl.std_range_start, out a))
                    {
                        mpl.is_numeric = true;
                    }
                    else
                    {
                        mpl.is_numeric = false;
                    }
                    _scifferContext.mfg_op_qc_parameter_list.Add(mpl);
                }

                _scifferContext.SaveChanges();

                return "Updated";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
    }
}
