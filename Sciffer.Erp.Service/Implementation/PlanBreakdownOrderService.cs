using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using System.Data;
using Sciffer.Erp.Data;
using System.Data.SqlClient;
using System.Web;
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class PlanBreakdownOrderService : IPlanBreakdownOrderService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public PlanBreakdownOrderService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(ref_plan_breakdown_order_VM main)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("plan_breakdown_parameter_id", typeof(int));
                dt.Columns.Add("parameter_id", typeof(int));
                dt.Columns.Add("parameter_range", typeof(string));
                dt.Columns.Add("actual_result", typeof(string));
                dt.Columns.Add("method_used", typeof(string));
                dt.Columns.Add("self_check", typeof(int));
                dt.Columns.Add("document_reference", typeof(string));
                if (main.plan_breakdown_parameter_id != null)
                {
                    for (var i = 0; i < main.plan_breakdown_parameter_id.Count; i++)
                    {
                        dt.Rows.Add(main.plan_breakdown_parameter_id[i] == "0" ? -1 : int.Parse(main.plan_breakdown_parameter_id[i]),
                            main.parameter_id[i],
                            main.parameter_range[i],
                            main.actual_result[i],
                            main.method_used[i],
                            main.shelf_check[i] == "null" ? 0 : int.Parse(main.shelf_check[i]),
                            main.document_reference[i]
                            );
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_plan_breakdown_parameter";
                t1.Value = dt;

                DataTable cp = new DataTable();
                cp.Columns.Add("plan_breakdown_cost_id", typeof(int));
                cp.Columns.Add("cost_sr_no", typeof(int));
                cp.Columns.Add("item_id", typeof(int));
                cp.Columns.Add("uom_id", typeof(int));
                cp.Columns.Add("cost_parameter_id", typeof(int));
                cp.Columns.Add("required_qty", typeof(double));
                cp.Columns.Add("actual_qty", typeof(double));
                cp.Columns.Add("sloc_id", typeof(int));
                cp.Columns.Add("bucket_id", typeof(int));
                cp.Columns.Add("goods_issue_id", typeof(int));
                cp.Columns.Add("issue_doc_number", typeof(string));
                cp.Columns.Add("posting_date", typeof(DateTime));
                if (main.plan_breakdown_cost_id != null)
                { 
                    for (var i = 0; i < main.plan_breakdown_cost_id.Count; i++)
                    {
                        cp.Rows.Add(main.plan_breakdown_cost_id[i] == "0" ? -1 : int.Parse(main.plan_breakdown_cost_id[i]),
                            int.Parse(main.cost_sr_no[i]), int.Parse(main.item_id[i]), int.Parse(main.uom_id[i]),
                            int.Parse(main.cost_parameter_id[i]), main.required_qty[i],
                            main.actual_qty[i] == ""  ? 0 : Double.Parse(main.actual_qty[i]),
                            main.sloc_id[i] == "" || main.sloc_id[i] == "null" ? 0 : int.Parse(main.sloc_id[i]),
                            main.bucket_id[i] == "" ? 0 : int.Parse(main.bucket_id[i]),
                            main.goods_issue_id[i]=="" ? 0 : int.Parse(main.goods_issue_id[i]),
                            main.issue_doc_number[i],
                            main.posting_date[i]=="null" || main.posting_date[i] == "" ? DateTime.Parse("01-01-1990") : DateTime.Parse(main.posting_date[i])
                            );
                    }
                }
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_plan_breakdown_cost";
                t2.Value = cp;

                var created_by1 = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var created_ts1 = DateTime.Now;
                var creation_date1= DateTime.Now;
                var plan_breakdown_order_id = new SqlParameter("@plan_breakdown_order_id", main.plan_breakdown_order_id == 0 ? -1 : main.plan_breakdown_order_id);
                var category_id = new SqlParameter("@category_id", main.category_id);
                var doc_number = new SqlParameter("@doc_number", main.doc_number==null ? "" : main.doc_number);
                var creation_date = new SqlParameter("@creation_date",creation_date1);
                var maintenance_type_id = new SqlParameter("@maintenance_type_id", main.maintenance_type_id);
                var plant_id = new SqlParameter("@plant_id", main.plant_id);
                //var machine_id = new SqlParameter("@machine_id", main.machine_id);
                var actual_start_date = new SqlParameter("@actual_start_date", main.actual_start_date == null ? DateTime.Parse("01-01-1990") : main.actual_start_date);
                var actual_finish_date = new SqlParameter("@actual_finish_date", main.actual_finish_date == null ? DateTime.Parse("01-01-1990") : main.actual_finish_date);
                var order_executed_by = new SqlParameter("@order_executed_by", main.order_executed_by == null ? 0 : main.order_executed_by);
                var order_approved_by = new SqlParameter("@order_approved_by", main.order_approved_by == null ? 0 : main.order_approved_by);
                var permit_id = new SqlParameter("@permit_id", main.permit_id == null ? 0 : main.permit_id);
                var notification_id = new SqlParameter("@notification_id", main.notification_id == null ? 0 : main.notification_id);
                var remarks = new SqlParameter("@remarks", main.remarks == null ? "" : main.remarks);
                var attachment = new SqlParameter("@attachment", main.attachment == null ? "" : main.attachment);                
                var is_active = new SqlParameter("@is_active", 1);
                var created_by = new SqlParameter("@created_by", created_by1);
                var created_ts = new SqlParameter("@created_ts", created_ts1);
                var employee_id = new SqlParameter("@employee_id", main.employee_id==null ? 0: main.employee_id);
                var deleteids = new SqlParameter("@deleteids", main.deleteids == null ? "" : main.deleteids);
                var deleteids1 = new SqlParameter("@deleteids1", main.deleteids1 == null ? "" : main.deleteids1);
                var machine_id_selected = new SqlParameter("@machine_id_selected", main.machine_id_selected == null ? "" : main.machine_id_selected);
                var actual_start_time = new SqlParameter("@actual_start_time", main.actual_start_time == null ? TimeSpan.Parse("00:00:00.0000000") : main.actual_start_time);
                var actual_end_time = new SqlParameter("@actual_end_time", main.actual_end_time == null ? TimeSpan.Parse("00:00:00.0000000") : main.actual_end_time);
                var order_executedby = new SqlParameter("@order_executedby", main.order_executedby == null ? "" : main.order_executedby);
                var rm_item_id = new SqlParameter("@rm_item_id", main.rm_item_id==null ? 0 : main.rm_item_id);
                var notification_description = new SqlParameter("@notification_description", main.notification_description == null ? "" : main.notification_description);

                var val = _scifferContext.Database.SqlQuery<string>("exec save_PlanBreakdownOrder @plan_breakdown_order_id ,@category_id ,@doc_number ,@creation_date ,@maintenance_type_id ,@plant_id ,@actual_start_date ,@actual_finish_date ,@order_executed_by ,@order_approved_by ,@permit_id ,@notification_id ,@remarks ,@attachment ,@is_active ,@created_by ,@created_ts,@employee_id, @deleteids, @deleteids1, @t1, @t2,@machine_id_selected,@actual_start_time ,@actual_end_time,@order_executedby,@rm_item_id,@notification_description", 
                    plan_breakdown_order_id, category_id, doc_number, creation_date, maintenance_type_id, plant_id, actual_start_date, actual_finish_date, order_executed_by, order_approved_by, permit_id, notification_id, remarks, attachment, is_active, created_by, created_ts,employee_id, deleteids, deleteids1, t1, t2, machine_id_selected, actual_start_time, actual_end_time, order_executedby, rm_item_id, notification_description).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    var sp = val.Split('~')[1];
                    return sp;
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error";
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ref_plan_breakdown_order_VM Get(int id)
        {
            ref_plan_breakdown_order po = _scifferContext.ref_plan_breakdown_order.FirstOrDefault(c => c.plan_breakdown_order_id == id && c.is_active == true);
            Mapper.CreateMap<ref_plan_breakdown_order, ref_plan_breakdown_order_VM>();
            ref_plan_breakdown_order_VM mmv = Mapper.Map<ref_plan_breakdown_order, ref_plan_breakdown_order_VM>(po);
            mmv.ref_plan_breakdown_cost = mmv.ref_plan_breakdown_cost.Where(c => c.is_active == true).ToList();
            mmv.ref_plan_breakdown_parameter = mmv.ref_plan_breakdown_parameter.Where(c => c.is_active == true).ToList();
            var machine_list = _scifferContext.map_machine_breakdown_order.Where(x => x.plan_breakdown_order_id == po.plan_breakdown_order_id && x.is_active == true).ToList();
            mmv.machine_id= string.Join(",", machine_list.Select(x=>x.machine_id));
            mmv.under_taken_by_id = po.employee_id;
            return mmv;
        }

        public List<ref_plan_breakdown_order_VM> GetAll()
        {
            var query = (from order in _scifferContext.ref_plan_breakdown_order.Where(x=>x.is_active==true).OrderByDescending(x => x.plan_breakdown_order_id)
                         join map in _scifferContext.map_machine_breakdown_order.Where(x=>x.is_active==true && x.machine_id != 0) on order.plan_breakdown_order_id equals map.plan_breakdown_order_id
                         join machine in _scifferContext.ref_machine on map.machine_id equals machine.machine_id into mach1
                         from mach2 in mach1.DefaultIfEmpty()                         
                         join plantname in _scifferContext.REF_PLANT on order.plant_id equals plantname.PLANT_ID into plantname1
                         from plantname2 in plantname1.DefaultIfEmpty()
                         join category in _scifferContext.ref_document_numbring on order.category_id equals category.document_numbring_id
                         join rm in _scifferContext.REF_ITEM on order.rm_item_id equals rm.ITEM_ID into rm1
                         from rm2 in rm1.DefaultIfEmpty()

                         select new ref_plan_breakdown_order_VM()
                         {
                             plan_breakdown_order_id = order.plan_breakdown_order_id,
                             doc_number = order.doc_number,
                             machine_name = mach2 == null ? "" : mach2.machine_name,
                             creation_date = order.creation_date,
                             maintenance_type_id = order.maintenance_type_id,
                             plant_id = order.plant_id,
                             category_id = order.category_id,
                             category_name = category.category,
                             actual_start_date = order.actual_start_date,
                             actual_finish_date = order.actual_finish_date,
                             order_executed_by = order.order_executed_by,
                             order_approved_by = order.order_approved_by,
                             permit_id = order.permit_id,
                             notification_id = order.notification_id,
                             remarks = order.remarks,
                             attachment = order.attachment,
                             is_active = true,
                             maintenance_type =order.maintenance_type_id==1 ? "BREAK DOWN ORDER" : "BREAK DOWN ORDER",
                             plant_name = plantname2.PLANT_CODE + " - " + plantname2.PLANT_NAME,
                             item_name = rm2.ITEM_NAME
                         }).OrderByDescending(x => x.plan_breakdown_order_id).ToList();
            return query;
        }

        public bool Update(ref_plan_breakdown_order_VM plan_maintenance_order_VM)
        {
            throw new NotImplementedException();
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
