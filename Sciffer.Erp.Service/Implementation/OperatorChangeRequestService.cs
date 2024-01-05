using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
   public class OperatorChangeRequestService : IOperatorChangeRequestService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericservice;
        private readonly ISkillMatrixService _SkillMatrix;
        public OperatorChangeRequestService(ScifferContext scifferContext, IGenericService genericservice, ISkillMatrixService SkillMatrix)
        {
            _scifferContext = scifferContext;
            _genericservice = genericservice;
            _SkillMatrix = SkillMatrix;
        }
        public string Add(operator_change_req_vm vm)
        {

            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                //Detail Table
                dt.Columns.Add("operator_change_request_list_id ", typeof(int));
                dt.Columns.Add("operator_change_request_id ", typeof(int));
                dt.Columns.Add("operator_id ", typeof(int));
                dt.Columns.Add("machine_id ", typeof(int));
                dt.Columns.Add("current_level_id ", typeof(int));
                dt.Columns.Add("new_level_id ", typeof(int));
                
                if (vm.operator_change_request_list_id != null)
                {
                    for (int i = 0; i < vm.operator_change_request_list_id.Count; i++) //Detail Table
                    {

                        dt.Rows.Add(vm.operator_change_request_list_id[i] == "" ? 0 : int.Parse(vm.operator_change_request_list_id[i]),
                            vm.operator_change_request_id == null ? 0 : vm.operator_change_request_id,
                            vm.operator_list_id[i],
                            vm.machine_list_id[i],
                            vm.current_level_list_id[i],
                            vm.new_level_list_id[i]);

                    }
                }

                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                //Header Table
                var created_by = new SqlParameter("@created_by", createdby);
                var operator_change_request_id = new SqlParameter("@operator_change_request_id", vm.operator_change_request_id == null ? -1 : vm.operator_change_request_id);
                var posting_date = new SqlParameter("@posting_date", vm.posting_date);
                var category_id = new SqlParameter("@category_id", vm.category_id == null ? -1 : vm.category_id);

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_operator_change_request_list";
                t1.Value = dt;
                var val = "";
                if (vm.operator_change_request_list_id != null)
                {
                     val = _scifferContext.Database.SqlQuery<string>(
                  "exec save_operator_change_request @operator_change_request_id,@posting_date,@category_id,@created_by,@t1 ",
                  operator_change_request_id, posting_date, category_id, created_by, t1).FirstOrDefault();
                }
                else
                {
                    val = "Detail Table cannot be Empty!!";
                }
                    
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

        public operator_change_req_vm Get(int? id)
        {
            //operator_change_request JR = _scifferContext.operator_change_request.FirstOrDefault(c => c.mfg_op_qc_parameter_id == id);
            var ent = new SqlParameter("@entity", "operator_change_req_header_list");
            var ids = new SqlParameter("@id", id == null ? 0 : id);           
            operator_change_req_vm JR = _scifferContext.Database.SqlQuery<operator_change_req_vm>("exec get_all_data_skill_matrix @entity,@id", ent, ids).FirstOrDefault();
            Mapper.CreateMap<operator_change_request, operator_change_req_vm>();
            operator_change_req_vm JRVM = Mapper.Map<operator_change_req_vm, operator_change_req_vm>(JR);
            //JRVM.mfg_op_qc_parameter_list = JRVM.mfg_op_qc_parameter_list.Where(x => x.is_active == true).ToList();
            var ent1 = new SqlParameter("@entity", "operator_change_req_detail_list");
            var ids1 = new SqlParameter("@id", id == null ? 0 : id);          
            JRVM.operator_change_request_detail_vm = _scifferContext.Database.SqlQuery<operator_change_request_detail_vm>("exec get_all_data_skill_matrix @entity,@id", ent1, ids1).ToList();
            return JRVM;
        }

        public List<operator_change_req_vm> GetAll(string entity, int? id)
        {
            var entity1 = new SqlParameter("@entity", entity);
            var id1 = new SqlParameter("@id", id);
            var val = _scifferContext.Database.SqlQuery<operator_change_req_vm>(
            "exec get_all_data_skill_matrix @entity,@id",
            entity1, id1).ToList();
            return val;
        }

        public List<operator_change_req_vm> ChangeApprovedStatus(string entity, int? detail_id,int approval_status_id,string approval_comment)
        {
            var user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
            var entity1 = new SqlParameter("@entity", entity);
            var detail_id1 = new SqlParameter("@detail_id", detail_id);
            var approval_status_id1 = new SqlParameter("@approval_status_id", approval_status_id);
            var approval_comment1 = new SqlParameter("@approval_comment", approval_comment == null ? "" : approval_comment);
            var approved_by1 = new SqlParameter("@approved_by", user);
            var val = _scifferContext.Database.SqlQuery<operator_change_req_vm>(
            "exec approve_operator_change_request @entity,@detail_id,@approval_status_id,@approval_comment,@approved_by",
            entity1, detail_id1, approval_status_id1, approval_comment1, approved_by1).ToList();
            return val;
        }
    }
}
