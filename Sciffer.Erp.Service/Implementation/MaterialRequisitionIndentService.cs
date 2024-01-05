using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class MaterialRequisitionIndentService : IMaterialRequisitionIndentService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name

        public MaterialRequisitionIndentService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        //Tool categories
        public List<ref_tool_category> GetToolCategoryList()
        {
            return _scifferContext.ref_tool_category.ToList();
        }
        //Tool usage types
        public List<ref_tool_usage_type> GetToolUsageTypeList()
        {
            return _scifferContext.ref_tool_usage_type.ToList();
        }
        //Operation list
        public List<ref_mfg_process> GetOperationList(int operator_id)
        {
            var role_code = _scifferContext.ref_user_role_mapping.Where(x => x.user_id == operator_id).Select(z => z.ref_user_management_role.role_code).FirstOrDefault();
            if(role_code == "PROD_OP")
            {

                var list = (from rmp in _scifferContext.ref_mfg_process
                            join moo in _scifferContext.map_operation_operator on rmp.process_id equals moo.operation_id
                            where moo.operator_id == operator_id
                            select new
                            {
                                process_id = rmp.process_id,
                                process_description = rmp.process_code + " - " + rmp.process_description,
                            }).ToList()
                            .Select(x => new ref_mfg_process
                            {
                                process_id = x.process_id,
                                process_description = x.process_description,
                            }).ToList();
                return list;
            }
            else
            {
                var list = (from rmp in _scifferContext.ref_mfg_process
                            select new
                            {
                                process_id = rmp.process_id,
                                process_description = rmp.process_code + " - " + rmp.process_description,
                            }).ToList()
                           .Select(x => new ref_mfg_process
                           {
                               process_id = x.process_id,
                               process_description = x.process_description,
                           }).ToList();
                return list;
            }

        }

        //Tool list for requisition
        public List<ref_tool_operation_map_vm> GetToolOperationMappedList(int crankshaft_id, int tool_usage_type_id, int tool_category_id, int process_id)
        {
            var list = (from tom in _scifferContext.ref_tool_operation_map
                        join ri in _scifferContext.REF_ITEM on tom.item_id equals ri.ITEM_ID
                        join ri1 in _scifferContext.REF_ITEM on tom.tool_id equals ri1.ITEM_ID
                        join rpm in _scifferContext.ref_mfg_process on tom.process_id equals rpm.process_id
                        join rtc in _scifferContext.ref_tool_category on tom.tool_category_id equals rtc.tool_category_id
                        join rtus in _scifferContext.ref_tool_usage_type on tom.tool_usage_type_id equals rtus.tool_usage_type_id
                        where 
                        (tom.process_id == process_id || process_id == 0)
                        && (tom.item_id == crankshaft_id || crankshaft_id == 0)
                        && (tom.tool_category_id == tool_category_id || tool_category_id == 0)
                        && (tom.tool_usage_type_id == tool_usage_type_id || tool_usage_type_id == 0)
                        select new
                        {
                            item_id = tom.tool_id,
                            item_name = ri1.ITEM_NAME,
                        }).ToList()
                        .Select(x => new ref_tool_operation_map_vm
                        {
                            item_id = (int)x.item_id,
                            item_name = x.item_name,
                        }).ToList();
            return list;
        }

        //Add MRI
        public string Add(material_requision_note_vm material)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("material_requision_note_detail_id", typeof(int));
                dt.Columns.Add("material_requision_note_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("required_qty", typeof(double));
                dt.Columns.Add("cost_center_id", typeof(int));
                dt.Columns.Add("machine", typeof(int));
                dt.Columns.Add("reason", typeof(int));
                dt.Columns.Add("order_type", typeof(int));
                dt.Columns.Add("order_number", typeof(string));
                dt.Columns.Add("line_remarks", typeof(string));
                dt.Columns.Add("is_active", typeof(bool));
                dt.Columns.Add("crankshaft_id", typeof(int));
                dt.Columns.Add("tool_category_id", typeof(int));
                dt.Columns.Add("tool_usage_type_id", typeof(int));
                dt.Columns.Add("process_id", typeof(int));
                
                for (var i = 0; i <= material.item_id1.Count - 1; i++)
                {
                    if (material.item_id1 != null)
                    {
                        var id = int.Parse(material.material_requision_note_detail_id1[i] == "" ? "0" : material.material_requision_note_detail_id1[i]);
                        var notid = material.material_requision_note_id == 0 ? 0 : material.material_requision_note_id;
                        var item = int.Parse(material.item_id1[i] == "" ? "0" : material.item_id1[i]);
                        var uom = int.Parse(material.uom_id1[i] == "" ? "0" : material.uom_id1[i]);
                        var required = double.Parse(material.required_qty1[i] == "" ? "0" : material.required_qty1[i]);
                        var cost = int.Parse(material.cost_center_id1[i] == "" ? "0" : material.cost_center_id1[i]);
                        var machine = int.Parse(material.machine1[i] == "" ? "0" : material.machine1[i]);
                        var reason = int.Parse(material.reason1[i] == "" ? "0" : material.reason1[i]);
                        var ordertype = int.Parse(material.order_type1[i] == "" ? "0" : material.order_type1[i]);
                        var ordernum = material.order_number1[i] == "" ? "" : material.order_number1[i];
                        var line = material.line_remarks1[i] == "" ? "" : material.line_remarks1[i];
                        
                        var cranskhaft_id = int.Parse(material.crankshaft_id1[i] == "" ? "0" : material.crankshaft_id1[i]);
                        var tool_category_id = int.Parse(material.tool_category_id1[i] == "" ? "0" : material.tool_category_id1[i]);
                        var tool_usage_type_id = int.Parse(material.tool_usage_type_id1[i] == "" ? "0" : material.tool_usage_type_id1[i]);
                        var process_id = int.Parse(material.process_id1[i] == "" ? "0" : material.process_id1[i]);

                        dt.Rows.Add(id, notid, item, uom, required, cost, machine, reason, ordertype, ordernum, line, 1, cranskhaft_id, tool_category_id, tool_usage_type_id, process_id);
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_material_requision_note_indent";
                t1.Value = dt;
                
                DateTime dte = new DateTime(1990, 1, 1);
                var material_requision_note_id = new SqlParameter("@material_requision_note_id", material.material_requision_note_id);
                var category_id = new SqlParameter("@category_id", material.category_id);
                var number = new SqlParameter("@number", material.number == null ? "" : material.number);
                var document_date = new SqlParameter("@document_date", material.document_date == null ? dte : material.document_date);
                var requirement_by = new SqlParameter("@requirement_by", material.requirement_by == null ? 0 : material.requirement_by);
                var plant_id = new SqlParameter("@plant_id", material.plant_id);
                var remarks = new SqlParameter("@remarks", material.remarks == null ? "" : material.remarks);
                var receiving_sloc = new SqlParameter("@receiving_sloc", material.receiving_sloc);
                var sending_sloc = new SqlParameter("@sending_sloc", material.sending_sloc);
                var type = new SqlParameter("@type", material.type == null ? 0 : material.type);
                var posting_date = new SqlParameter("@posting_date", material.posting_date);
                var status_id = new SqlParameter("@status_id", material.status_id);
                var is_active = new SqlParameter("@is_active", material.is_active);
                var deletes = new SqlParameter("@deleteids", material.deleteids == null ? "" : material.deleteids);

                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_material_requision_indent @material_requision_note_id ,@posting_date,@category_id ,@number ,@document_date ,@requirement_by ,@plant_id ,@remarks ,@receiving_sloc ,@sending_sloc ,@type ,@status_id ,@is_active,@deleteids,@t1",
                    material_requision_note_id, posting_date, category_id, number, document_date, requirement_by, plant_id, remarks, receiving_sloc, sending_sloc, type,
                    status_id, is_active, deletes, t1).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    var str = val.Split('~');
                    var mrn_no = str[0];
                    return mrn_no;
                }
                else
                {
                    return "error";
                }

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "error";
            }
        }
        
        //Get MRI
        public material_requision_note_vm Get(int id)
        {
            material_requision_note invoice = _scifferContext.material_requision_note.FirstOrDefault(c => c.material_requision_note_id == id);
            Mapper.CreateMap<material_requision_note, material_requision_note_vm>();
            material_requision_note_vm quotationvm = Mapper.Map<material_requision_note, material_requision_note_vm>(invoice);
            quotationvm.material_requision_note_detail = quotationvm.material_requision_note_detail.Where(c => c.is_active == true).ToList();
            return quotationvm;
        }

        //Get all MRI List
        public List<material_requision_note_vm> GetAll()
        {
            var query = (from material in _scifferContext.material_requision_note.Where(x => x.is_active == true)
                         join catetgory in _scifferContext.ref_document_numbring on material.category_id equals catetgory.document_numbring_id
                         join palnt in _scifferContext.REF_PLANT on material.plant_id equals palnt.PLANT_ID
                         join requirement in _scifferContext.ref_user_management on material.requirement_by equals requirement.user_id into requirement1
                         from requirement2 in requirement1.DefaultIfEmpty()
                         join rsloc in _scifferContext.REF_STORAGE_LOCATION on material.receiving_sloc equals rsloc.storage_location_id
                         join ssloc in _scifferContext.REF_STORAGE_LOCATION on material.sending_sloc equals ssloc.storage_location_id
                         join status in _scifferContext.ref_status on material.status_id equals status.status_id
                         where catetgory.ref_module_form.module_form_code == "MRI"
                         select new material_requision_note_vm
                         {
                             material_requision_note_id = material.material_requision_note_id,
                             posting_date = material.posting_date,
                             category_name = catetgory.category,
                             number = material.number,
                             document_date = material.document_date,
                             requirement_by_name = requirement2.REF_EMPLOYEE.employee_name,
                             plant_name = palnt.PLANT_NAME,
                             remarks = material.remarks,
                             receiving_sloc_name = rsloc.storage_location_name,
                             sending_sloc_name = ssloc.storage_location_name,
                             type_name = material.type == null ? "" : material.type == 1 ? "Goods Issue" : "Goods Issue",
                             status_name = status.status_name,
                             is_active = material.is_active,
                         }).OrderByDescending(a => a.material_requision_note_id).ToList();
            return query;
        }


        //-----------------------Not implemented-------------------
        public string Update(material_requision_note_vm material)
        {
            throw new NotImplementedException();
        }

        //Delete
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }



        public int GetApprovedMRICount(int user)
        {
            try
            {
                var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "MRN").FirstOrDefault();
                //var cnt = _scifferContext.material_requision_note.Where(v => v.approval_status == 0).Count();
                var cnt = _scifferContext.material_requision_note.Where(v => v.status_id == status.status_id && v.is_seen == false).Count();

                return cnt;
            }


            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                GC.Collect();
            }

        }

        public string materialRequisionIndentupdatestatusseen()
        {
            try
            {
                int user_id = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var op = _scifferContext.ref_user_role_mapping.Where(x => x.user_id == user_id).FirstOrDefault();
                var role = _scifferContext.ref_user_management_role.Where(x => x.role_id == op.role_id).FirstOrDefault();
                //if(op.role_id == role.role_id)
                if (role.role_code == "STO_EXEC" || role.role_code == "IT_ADMIN")

                {
                    var purchaseReqDetail = _scifferContext.material_requision_note.Where(x => x.is_seen == false).ToList();
                    foreach (var item in purchaseReqDetail)
                    {
                        item.is_seen = true;
                        _scifferContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }

                    _scifferContext.SaveChanges();
                }

                return "true";
            }

            catch (Exception)
            {
                return "false";
            }
        }



        #region dispoable methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }
        #endregion

    }
}
