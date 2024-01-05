using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Service.Implementation
{
    public class QualityParameterService : IQualityParameterService
    {

        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericservice;
        public QualityParameterService(ScifferContext scifferContext, IGenericService genericservice)
        {
            _scifferContext = scifferContext;
            _genericservice = genericservice;
        }

        public string Add(mfg_qc_qc_parameter_VM vm)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                //Detail Table
                dt.Columns.Add("mfg_qc_qc_parameter_list_id ", typeof(int));
                dt.Columns.Add("mfg_qc_qc_parameter_id ", typeof(int));
                dt.Columns.Add("parameter_name ", typeof(string));
                dt.Columns.Add("parameter_uom ", typeof(string));
                dt.Columns.Add("std_range_start ", typeof(string));
                dt.Columns.Add("std_range_end ", typeof(string));
                dt.Columns.Add("is_numeric ", typeof(bool));


                if (vm.mfg_qc_qc_parameter_list_id != null)
                {
                    for (int i = 0; i < vm.mfg_qc_qc_parameter_list_id.Count; i++) //Detail Table
                    {

                        dt.Rows.Add(vm.mfg_qc_qc_parameter_list_id[i] == "" ? 0 : int.Parse(vm.mfg_qc_qc_parameter_list_id[i]),
                            vm.mfg_qc_qc_parameter_id == null ? 0 : vm.mfg_qc_qc_parameter_id,
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
                var machinelist = vm.machine_list_id.Split(',');
                if (machinelist.Length > 0)
                {
                    for (int i = 0; i < machinelist.Length; i++) //Detail Table
                    {
                        if(machinelist[i] != "")
                        {
                            dt1.Rows.Add(machinelist[i]);
                        }

                    }
                }
                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                //Header Table
                var created_by = new SqlParameter("@created_by", createdby);
                var mfg_qc_qc_parameter_id = new SqlParameter("@mfg_qc_qc_parameter_id", vm.mfg_qc_qc_parameter_id == null ? -1 : vm.mfg_qc_qc_parameter_id);
                var item_id = new SqlParameter("@item_id", vm.item_id);
                var machine_id = new SqlParameter("@machine_id", vm.machine_id);
                var process_id = new SqlParameter("@process_id", vm.process_id);

                var deleteids = new SqlParameter("@deleteids", vm.deleteids==null?"": vm.deleteids);
                //created new User Defind Table of Detail Table
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_mfg_qc_qc_parameter_list";
                t1.Value = dt;
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_machine_id_list";
                t2.Value = dt1;
                //Pushing Parameter in Procedure 
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec Save_QualityParameter @mfg_qc_qc_parameter_id,@item_id,@created_by,@deleteids,@t1,@t2,@process_id ",
                    mfg_qc_qc_parameter_id, item_id, created_by, deleteids, t1,t2, process_id).FirstOrDefault();

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

        public mfg_qc_qc_parameter_VM Get(int? id,int? id1)
        {
            //mfg_qc_qc_parameter JR = _scifferContext.mfg_qc_qc_parameter.FirstOrDefault(c => c.process_id == id);
            var ent = new SqlParameter("@entity", "getqualityparameterHeader");
            var ids = new SqlParameter("@id", id==null?0: id);
            var item_id = new SqlParameter("@item_id", id1==null?0: id1);
            mfg_qc_qc_parameter_VM JR = _scifferContext.Database.SqlQuery<mfg_qc_qc_parameter_VM>("exec get_edit_detail @entity,@id,@item_id", ent, ids, item_id).FirstOrDefault();
            Mapper.CreateMap<mfg_qc_qc_parameter, mfg_qc_qc_parameter_VM>();
            mfg_qc_qc_parameter_VM JRVM = Mapper.Map<mfg_qc_qc_parameter_VM, mfg_qc_qc_parameter_VM>(JR);
            //JRVM.mfg_qc_qc_parameter_list = JRVM.mfg_qc_qc_parameter_list.Where(x => x.is_active == true).ToList();
            var ent1 = new SqlParameter("@entity", "getqualityparameterDetail");
            var ids1 = new SqlParameter("@id", id == null ? 0 : id);
            var item_ids = new SqlParameter("@item_id", id1 == null ? 0 : id1);
            JRVM.mfg_qc_qc_parameter_list = _scifferContext.Database.SqlQuery<mfg_qc_qc_parameter_list>("exec get_edit_detail @entity,@id,@item_id", ent1, ids1, item_ids).ToList();
            return JRVM;
        }

        public List<mfg_qc_qc_parameter_VM> GetAll()
        {
            //var query = (from mqqc in _scifferContext.mfg_qc_qc_parameter
            //             join ri in _scifferContext.REF_ITEM on mqqc.item_id equals ri.ITEM_ID
            //             join rm in _scifferContext.ref_machine on mqqc.machine_id equals rm.machine_id
            //             select new
            //             {
            //                 mfg_qc_qc_parameter_id = mqqc.mfg_qc_qc_parameter_id,
            //                 machine_id = mqqc.machine_id,
            //                 item_id = mqqc.item_id,
            //                 machine_name = rm.machine_name,
            //                 item_name = ri.ITEM_NAME,
            //             }).ToList()
            //             .Select(x => new mfg_qc_qc_parameter_VM
            //             {
            //                 mfg_qc_qc_parameter_id = x.mfg_qc_qc_parameter_id,
            //                 machine_id = x.machine_id,
            //                 item_id = x.item_id,
            //                 machine_name = x.machine_name,
            //                 item_name = x.item_name,
            //             }).OrderByDescending(z => z.mfg_qc_qc_parameter_id).ToList();
            //return query;
            var ent = new SqlParameter("@entity", "getqualityparameter");
            var query = _scifferContext.Database.SqlQuery<mfg_qc_qc_parameter_VM>(
            "exec get_index_detail @entity", ent).ToList();
            return query;
        }

        public string Update(mfg_qc_qc_parameter_VM vm)
        {
            throw new NotImplementedException();
        }
    }
}
