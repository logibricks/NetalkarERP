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


namespace Sciffer.Erp.Service.Implementation
{
    public class PlanMaintenanceOrderService : IPlanMaintenanceOrderService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public PlanMaintenanceOrderService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(plan_maintenance_order_VM order)
        {
            try
            {
                ref_plan_maintenance_order mo = new ref_plan_maintenance_order();

                mo.order_no = order.order_no;
                mo.machine_id = order.machine_id;
                mo.creation_date = order.creation_date;
                mo.maintenance_type_id = order.maintenance_type_id;
                mo.order_description = order.order_desc;
                mo.plant_id = order.plant_id;
                mo.manufacturer = order.manufacturer;
                mo.model_no = order.model_no;
                mo.manufacturer_part_no = order.manufacturer_part_no;
                mo.manufacturer_serial_no = order.manufacturer_serial_no;
                mo.asset_code_id = order.asset_code_id;
                mo.asset_tag_no = order.asset_tag_no;
                mo.schedule_date = order.schedule_date;
                mo.finish_date = order.finish_date;
                mo.actual_start_date = order.actual_start_date;
                mo.actual_finish_date = order.actual_finish_date;
                mo.order_executed_by = order.order_executed_by;
                mo.order_approved_by = order.order_approved_by;
                mo.permit_no = order.permit_no;
                mo.notification_no = order.notification_no;
                mo.remarks = order.remarks;
                mo.attachment = order.attachement;
                mo.machine_category_id = order.machine_category_id;
                mo.is_active = true;


                List<ref_plan_maintenance_order_parameter> mop = new List<ref_plan_maintenance_order_parameter>();

                foreach (var I in order.ref_plan_maintenance_order_parameter)
                {
                    ref_plan_maintenance_order_parameter op = new ref_plan_maintenance_order_parameter();

                    op.plan_maintenance_order_id = I.plan_maintenance_order_id;
                    op.parameter_id = I.parameter_id;
                    op.range = I.range;
                    op.actual_result = I.actual_result;
                    op.method_used = I.method_used;
                    op.self_check = I.self_check;
                    op.document_reference = I.document_reference;
                    op.sr_no = I.sr_no;
                    //  op.plan_maintenance_order_id = I.plan_maintenance_order_id;
                    op.maintenance_detail_id = I.maintenance_detail_id;
                    op.is_active = true;
                    mop.Add(op);
                }
                mo.ref_plan_maintenance_order_parameter = mop;

                List<ref_plan_maintenance_order_cost> moc = new List<ref_plan_maintenance_order_cost>();

                foreach (var J in order.ref_plan_maintenance_order_cost)
                {
                    ref_plan_maintenance_order_cost oc = new ref_plan_maintenance_order_cost();

                    oc.plan_maintenance_order_id = J.plan_maintenance_order_id;
                    oc.item_id = J.item_id;
                    oc.quantity = J.quantity;
                    oc.actual_quantity = J.actual_quantity;
                    oc.sloc_id = J.sloc_id;
                    oc.bucket_id = J.bucket_id;
                    oc.sr_no = J.sr_no;
                    oc.plan_maintenance_component_id = J.plan_maintenance_component_id;
                    oc.posting_date = J.posting_date;
                    oc.is_active = true;
                    moc.Add(oc);
                }
                mo.ref_plan_maintenance_order_cost = moc;
                _scifferContext.ref_plan_maintenance_order.Add(mo);

                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public string Update(plan_maintenance_order_VM main)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("maintenance_parameter_detail_id", typeof(int));
                dt.Columns.Add("parameter_id", typeof(int));
                dt.Columns.Add("parameter_range", typeof(string));
                dt.Columns.Add("actual_result", typeof(string));
                dt.Columns.Add("method_used", typeof(string));
                dt.Columns.Add("self_check", typeof(int));
                dt.Columns.Add("document_reference", typeof(string));
                dt.Columns.Add("maintenance_detail_id", typeof(int));
                dt.Columns.Add("parameter_sr_no", typeof(int));
                //adde_12_06
                dt.Columns.Add("attended_by", typeof(int));
                if (main.maintenance_parameter_id != null)
                {
                    for (var i = 0; i < main.maintenance_parameter_id.Count; i++)
                    {
                        var maintenance_parameter_id = main.maintenance_parameter_id[i] == "0" ? -1 : int.Parse(main.maintenance_parameter_id[i]);
                        var parameter_code_id = main.parameter_code_id[i] == "" ? 0 : int.Parse(main.parameter_code_id[i]);
                        var range = main.range[i];
                        var actual_result = main.actual_result[i] == "null" ? "" : main.actual_result[i];
                        var method_used = main.method_used[i] == "null" ? "" : main.method_used[i] == "" ? "" : main.method_used[i];
                        var shelf_check = main.shelf_check[i] == "" ? 0 : int.Parse(main.shelf_check[i]);
                        var document_reference = main.document_reference[i] == "" ? "" : main.document_reference[i];
                        var maintenannce_detail_id = main.maintenannce_detail_id[i] == "" ? 0 : int.Parse(main.maintenannce_detail_id[i]);
                        var attended_by = main.attended_bys[i] == null ? 0 : main.attended_bys[i];
                        var parameter_sr_no = main.parameter_sr_no[i] == "" ? 0 : int.Parse(main.parameter_sr_no[i]);
                        dt.Rows.Add(maintenance_parameter_id, parameter_code_id, range, actual_result, method_used, shelf_check, document_reference,
                            maintenannce_detail_id, parameter_sr_no, attended_by);
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_maintenance_parameter_detail";
                t1.Value = dt;


                DataTable cp = new DataTable();
                cp.Columns.Add("component_detail_id", typeof(int));
                cp.Columns.Add("component_sr_no", typeof(int));
                cp.Columns.Add("plan_type", typeof(string));
                cp.Columns.Add("item_id", typeof(int));
                cp.Columns.Add("uom_id", typeof(int));
                cp.Columns.Add("component_parameter_id", typeof(int));
                cp.Columns.Add("req_quantity", typeof(double));
                cp.Columns.Add("actual_quantity", typeof(double));
                cp.Columns.Add("sloc_id", typeof(int));
                cp.Columns.Add("bucket_id", typeof(int));
                cp.Columns.Add("doc_number", typeof(string));
                cp.Columns.Add("posting_date", typeof(DateTime));
                cp.Columns.Add("plan_maintenance_component_id", typeof(int));
                if (main.component_detail_id != null)
                {
                    for (var i = 0; i < main.component_detail_id.Count; i++)
                    {
                        cp.Rows.Add(main.component_detail_id[i] == "0" || main.component_detail_id[i] == "" ? -1 : int.Parse(main.component_detail_id[i]),
                            main.component_sr_no[i] == "" ? 0 : int.Parse(main.component_sr_no[i]), main.plan_type[i],
                            main.item_id[i] == "" ? 0 : int.Parse(main.item_id[i]), main.uom_id[i] == "" ? 0 : int.Parse(main.uom_id[i]),
                            main.component_parameter_id[i] == null || main.component_parameter_id[i] == "" ? "0" : main.component_parameter_id[i],
                            main.req_quantity[i], main.actual_quantity[i] == "null" || main.actual_quantity[i] == "" ? 0 : Double.Parse(main.actual_quantity[i]),
                            main.sloc_id[i] == "null" || main.sloc_id[i] == "" ? 0 : int.Parse(main.sloc_id[i]),
                            main.bucket_id[i] == "null" || main.bucket_id[i] == "" ? 0 : int.Parse(main.bucket_id[i]),
                            main.doc_number[i] == "null" ? "" : main.doc_number[i],
                            main.posting_date[i] == "null" || main.posting_date[i] == "" ? DateTime.Parse("01-01-1990") : DateTime.Parse(main.posting_date[i]),
                            main.plan_maintenance_component_id[i] == "" ? 0 : int.Parse(main.plan_maintenance_component_id[i])
                            );
                    }
                }

                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_plan_maintenance_order_component";
                t2.Value = cp;

                var maintenance_order_id = new SqlParameter("@maintenance_order_id", main.maintenance_order_id);
                var order_no = new SqlParameter("@order_no", main.order_no);
                var machine_id = new SqlParameter("@machine_id", main.machine_id);
                var creation_date = new SqlParameter("@creation_date", main.creation_date);
                var maintenance_type_id = new SqlParameter("@maintenance_type_id", main.maintenance_type_id);
                var order_description = new SqlParameter("@order_description", main.order_desc == null ? "" : main.order_desc);
                var plant_id = new SqlParameter("@plant_id", main.plant_id);
                var category_id = new SqlParameter("@category_id", main.category_id);
                var manufacturer = new SqlParameter("@manufacturer", main.manufacturer == null ? "" : main.manufacturer);
                var model_no = new SqlParameter("@model_no", main.model_no == null ? "" : main.model_no);
                var manufacturer_part_no = new SqlParameter("@manufacturer_part_no", main.manufacturer_part_no == null ? "" : main.manufacturer_part_no);
                var manufacturer_serial_no = new SqlParameter("@manufacturer_serial_no", main.manufacturer_serial_no == null ? "" : main.manufacturer_serial_no);
                var asset_code_id = new SqlParameter("@asset_code_id", main.asset_code_id == null ? "" : main.asset_code_id);
                var asset_tag_no = new SqlParameter("@asset_tag_no", main.asset_tag_no == null ? "" : main.asset_tag_no);
                var schedule_date = new SqlParameter("@schedule_date", main.schedule_date);
                var finish_date = new SqlParameter("@finish_date", main.finish_date);
                var actual_start_date = new SqlParameter("@actual_start_date", main.actual_start_date == null ? DateTime.Parse("01-01-1990") : main.actual_start_date);
                var actual_finish_date = new SqlParameter("@actual_finish_date", main.actual_finish_date == null ? DateTime.Parse("01-01-1990") : main.actual_finish_date);
                var order_executed_by = new SqlParameter("@order_executed_by", main.order_executed_by == null ? 0 : main.order_executed_by);
                var order_approved_by = new SqlParameter("@order_approved_by", main.order_approved_by == null ? 0 : main.order_approved_by);
                var permit_no = new SqlParameter("@permit_no", main.permit_no == null ? "" : main.permit_no);
                var notification_no = new SqlParameter("@notification_no", main.notification_no == null ? "" : main.notification_no);
                var remarks = new SqlParameter("@remarks", main.remarks == null ? "" : main.remarks);
                var attachment = new SqlParameter("@attachment", main.attachement == null ? "" : main.attachement);
                var machine_category_id = new SqlParameter("@machine_category_id", main.machine_category_id);
                var is_active = new SqlParameter("@is_active", 1);
                var deleteids = new SqlParameter("@deleteids", main.deleteids == null ? "" : main.deleteids);
                var deleteids1 = new SqlParameter("@deleteids1", main.deleteids1 == null ? "" : main.deleteids1);
                var actual_start_time = new SqlParameter("@actual_start_time", main.actual_start_time == null ? TimeSpan.Parse("00:00") : main.actual_start_time);
                var actual_finish_time = new SqlParameter("@actual_finish_time", main.actual_finish_time == null ? TimeSpan.Parse("00:00") : main.actual_finish_time);
                var val = _scifferContext.Database.SqlQuery<string>("exec save_PlanMaintenanceOrder @maintenance_order_id ,@order_no ,@machine_id ,@creation_date ,@maintenance_type_id ,@order_description ,@plant_id ,@category_id ,@manufacturer ,@model_no ,@manufacturer_part_no ,@manufacturer_serial_no ,@asset_code_id ,@asset_tag_no ,@schedule_date ,@finish_date ,@actual_start_date ,@actual_finish_date ,@order_executed_by ,@order_approved_by ,@permit_no ,@notification_no ,@remarks ,@attachment ,@machine_category_id ,@is_active,@t1,@t2,@deleteids,@deleteids1,@actual_start_time,@actual_finish_time ",
                    maintenance_order_id, order_no, machine_id, creation_date, maintenance_type_id, order_description, plant_id, category_id,
                    manufacturer, model_no, manufacturer_part_no, manufacturer_serial_no, asset_code_id, asset_tag_no, schedule_date,
                    finish_date, actual_start_date, actual_finish_date, order_executed_by, order_approved_by, permit_no, notification_no,
                    remarks, attachment, machine_category_id, is_active, t1, t2, deleteids, deleteids1, actual_start_time, actual_finish_time).FirstOrDefault();

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
        public string UpdatePlanMaintaince(int? maintenance_order_id, List<plan_maintenance_order_parameter_vm> main)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("plan_order_parameter_id", typeof(int));
                dt.Columns.Add("actual_result", typeof(string));
                dt.Columns.Add("method_used", typeof(string));
                dt.Columns.Add("self_check", typeof(string));
                dt.Columns.Add("document_reference", typeof(string));
                dt.Columns.Add("attended_by", typeof(string));
                if (main != null)
                {
                    for (var i = 0; i < main.Count; i++)
                    {
                        var parameter_code_id = main[i].plan_order_parameter_id;
                        var actual_result = main[i].actual_result;
                        var method_used = main[i].method_used;
                        var shelf_check = main[i].self_check;
                        var document_reference = main[i].document_reference;
                        var attended_by = main[i].attended_by;
                        dt.Rows.Add(parameter_code_id, actual_result, method_used, shelf_check, document_reference, attended_by);
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_plan_maintenance_order_parameter";
                t1.Value = dt;


                //SqlParameter[] param = new SqlParameter[1];
                var maintenance_order_id1 = new SqlParameter("@maintenance_order_id", maintenance_order_id);
                // param[0] = new SqlParameter("@grn_id", item.grn_id == null ? 0 : item.grn_id);
                var val = _scifferContext.Database.SqlQuery<string>("exec Update_PlanMaintenanceOrder @maintenance_order_id , @t1",
                    maintenance_order_id1, t1).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    var sp = val.Split('~')[0];
                    return sp.Trim();
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
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[plan_maintenance_order] set [IS_ACTIVE] = 0 where plan_maintenance_order_id = " + id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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

        public plan_maintenance_order_VM Get(int id)
        {
            ref_plan_maintenance_order po = _scifferContext.ref_plan_maintenance_order.FirstOrDefault(c => c.maintenance_order_id == id);
            Mapper.CreateMap<ref_plan_maintenance_order, plan_maintenance_order_VM>();
            plan_maintenance_order_VM mmv = Mapper.Map<ref_plan_maintenance_order, plan_maintenance_order_VM>(po);
            mmv.ref_plan_maintenance_order_parameter = _scifferContext.ref_plan_maintenance_order_parameter.Where(c => c.is_active && c.plan_maintenance_order_id == id).ToList();
            mmv.ref_plan_maintenance_order_cost = _scifferContext.ref_plan_maintenance_order_cost.Where(c => c.is_active && c.plan_maintenance_order_id == id).ToList();
            return mmv;
        }

        public List<plan_maintenance_order_parameter_new_vm> GetMaintenance_order_parameter(int id)
        {
            var paramter = _scifferContext.ref_plan_maintenance_order_parameter.Where(c => c.plan_maintenance_order_id == id).ToList();
            Mapper.CreateMap<ref_plan_maintenance_order_parameter, plan_maintenance_order_parameter_new_vm>();
            List<plan_maintenance_order_parameter_new_vm> mmv = Mapper.Map<List<ref_plan_maintenance_order_parameter>, List<plan_maintenance_order_parameter_new_vm>>(paramter);

            List<REF_EMPLOYEE> employees = _scifferContext.REF_EMPLOYEE.Where(x => !x.is_block).ToList();
            List<ref_parameter_list> parameter_Lists = _scifferContext.ref_parameter_list.Where(x => x.is_active && !x.is_blocked).ToList();

            foreach (var parameter in mmv)
            {
                var param = parameter_Lists.FirstOrDefault(x => x.parameter_id == parameter.parameter_id);
                parameter.paramaterName = param != null ? param.parameter_code + '/' + param.parameter_desc : "";
                var emp = employees.FirstOrDefault(x => x.employee_id == parameter.attended_by);
                parameter.employeeName = emp != null ? emp.employee_code + '/' + emp.employee_name : "";
            }
            return mmv;
        }

        public List<plan_maintenance_order_parameter_cost_new_vm> GetMaintenance_order_parameter_cost(int id)
        {
            var paramter = _scifferContext.ref_plan_maintenance_order_cost.Where(c => c.plan_maintenance_order_id == id).ToList();
            Mapper.CreateMap<ref_plan_maintenance_order_cost, plan_maintenance_order_parameter_cost_new_vm>();
            List<plan_maintenance_order_parameter_cost_new_vm> mmv = Mapper.Map<List<ref_plan_maintenance_order_cost>, List<plan_maintenance_order_parameter_cost_new_vm>>(paramter);

            List<ref_parameter_list> parameter_Lists = _scifferContext.ref_parameter_list.Where(x => x.is_active && !x.is_blocked).ToList();
            List<REF_STORAGE_LOCATION> storage = _scifferContext.REF_STORAGE_LOCATION.Where(x => !x.is_blocked).ToList();
            List<ref_bucket> bucket = _scifferContext.ref_bucket.Where(x => x.is_active).ToList();
            List<REF_ITEM> item = _scifferContext.REF_ITEM.Where(x => x.is_active).ToList();
            List<REF_UOM> UOM = _scifferContext.REF_UOM.Where(x => x.is_active).ToList();

            foreach (var parameter in mmv)
            {
                var param = parameter_Lists.FirstOrDefault(x => x.parameter_id == parameter.parameter_id);
                parameter.parameter_code = param != null ? param.parameter_code + '/' + param.parameter_desc : "";

                var store = storage.FirstOrDefault(x => x.storage_location_id == parameter.sloc_id);
                parameter.storage_code = store != null ? store.storage_location_name : "";

                var buck = bucket.FirstOrDefault(x => x.bucket_id == parameter.bucket_id);
                parameter.bucket = buck != null ? buck.bucket_name : "";

                var itemlist = item.FirstOrDefault(x => x.ITEM_ID == parameter.item_id);
                parameter.ItemName = itemlist != null ? itemlist.ITEM_CODE + '/' + itemlist.ITEM_NAME : "";

                var uomlist = UOM.FirstOrDefault(x => x.UOM_ID == parameter.uom_id);
                parameter.UOM_NAME = uomlist != null ? uomlist.UOM_NAME : "";
            }

            return mmv;
        }
        public ref_plan_maintenance_order_new_VM Getplanmaintenanceorderformachineentry(int id)
        {
            try
            {
                if (id != 0)
                {
                    ref_plan_maintenance_order po = _scifferContext.ref_plan_maintenance_order.OrderByDescending(c => c.maintenance_order_id).FirstOrDefault(c => c.machine_id == id);

                    int maintaince_order_id = po.maintenance_order_id;
                    int plan_maintenance_id = po.plan_maintenance_id;

                    Mapper.CreateMap<ref_plan_maintenance_order, ref_plan_maintenance_order_new_VM>();
                    ref_plan_maintenance_order_new_VM mmv = Mapper.Map<ref_plan_maintenance_order, ref_plan_maintenance_order_new_VM>(po);
                    if (mmv != null)
                    {
                        var oder_parameter = _scifferContext.ref_plan_maintenance_order_parameter.Where(c => c.is_active && c.plan_maintenance_order_id == maintaince_order_id).ToList();


                        Mapper.CreateMap<ref_plan_maintenance_order_parameter, ref_plan_maintenance_order_parameter_vm>();
                        List<ref_plan_maintenance_order_parameter_vm> param = Mapper.Map<List<ref_plan_maintenance_order_parameter>, List<ref_plan_maintenance_order_parameter_vm>>(oder_parameter);

                        mmv.plan_maintenance_order_parameter = param;

                        var machinedetails = _scifferContext.ref_machine.FirstOrDefault(x => x.machine_id == id);
                        mmv.machine_name = machinedetails.machine_code + "/" + machinedetails.machine_name;

                        var machinecategorys = _scifferContext.ref_machine_category.FirstOrDefault(x => x.machine_category_id == machinedetails.machine_category_id);
                        mmv.machine_category_name = machinecategorys.machine_category_code + "/" + machinecategorys.machine_category_description;

                        var maintenanceType = _scifferContext.ref_maintenance_type.FirstOrDefault(x => x.maintenance_type_id == 4);
                        mmv.maintenance_type_name = maintenanceType.maintenance_name;

                        mmv.plan_maintenance_order_no = _scifferContext.ref_plan_maintenance.FirstOrDefault(x => x.plan_maintenance_id == plan_maintenance_id).doc_number;
                    }


                    List<REF_EMPLOYEE> employees = _scifferContext.REF_EMPLOYEE.Where(x => !x.is_block).ToList();
                    List<ref_parameter_list> parameter_Lists = _scifferContext.ref_parameter_list.Where(x => x.is_active && !x.is_blocked).ToList();

                    foreach (var parameter in mmv.plan_maintenance_order_parameter)
                    {
                        var param = parameter_Lists.FirstOrDefault(x => x.parameter_id == parameter.parameter_id);
                        parameter.paramaterName = param != null ? param.parameter_code + '/' + param.parameter_desc : "";
                        var emp = employees.FirstOrDefault(x => x.employee_id == parameter.attended_by);
                        parameter.employeeName = emp != null ? emp.employee_code + '/' + emp.employee_name : "";
                    }


                    return mmv;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool CheckPlanMaintenanceOrder(int id)
        {
            bool isPlanMaintenceOrder = false;
            try
            {
                if (id != 0)
                {
                    ref_plan_maintenance_order po = _scifferContext.ref_plan_maintenance_order.OrderByDescending(c => c.maintenance_order_id).FirstOrDefault(c => c.machine_id == id);
                    Mapper.CreateMap<ref_plan_maintenance_order, plan_maintenance_order_VM>();
                    plan_maintenance_order_VM mmv = Mapper.Map<ref_plan_maintenance_order, plan_maintenance_order_VM>(po);
                    if (mmv != null)
                    {
                        mmv.ref_plan_maintenance_order_parameter = mmv.ref_plan_maintenance_order_parameter.Where(c => c.is_active == true).ToList();
                    }

                    if (mmv != null && mmv.maintenance_type_id == 4)
                    {
                        foreach (var planMaintenance in mmv.ref_plan_maintenance_order_parameter)
                        {
                            if (planMaintenance.actual_result == null && planMaintenance.method_used == null && planMaintenance.document_reference == null)
                            {
                                isPlanMaintenceOrder = true;
                            }
                        }
                    }

                    return isPlanMaintenceOrder;
                }
                return isPlanMaintenceOrder;
            }
            catch (Exception ex)
            {
                return isPlanMaintenceOrder;
            }

        }
        public plan_maintenance_order_VM GetByMachineId(int id)
        {
            plan_maintenance_order_VM vm = null;
            if (id != 0)
            {

                ref_plan_maintenance_order po = _scifferContext.ref_plan_maintenance_order.Where(c => c.machine_id == id && c.is_active).OrderByDescending(x => x.creation_date).FirstOrDefault();
                Mapper.CreateMap<ref_plan_maintenance_order, plan_maintenance_order_VM>();
                plan_maintenance_order_VM mmv = Mapper.Map<ref_plan_maintenance_order, plan_maintenance_order_VM>(po);
                mmv.ref_plan_maintenance_order_parameter = mmv.ref_plan_maintenance_order_parameter.Where(c => c.is_active == true).ToList();
                mmv.ref_plan_maintenance_order_cost = mmv.ref_plan_maintenance_order_cost.Where(c => c.is_active == true).ToList();
                return mmv;
            }
            return vm;
        }

        public List<plan_maintenance_order_VM> GetAll()
        {
            var query = (from order in _scifferContext.ref_plan_maintenance_order.Where(x => x.is_active == true).OrderByDescending(x => x.maintenance_order_id)
                         join mach in _scifferContext.ref_machine on order.machine_id equals mach.machine_id into mach1
                         from mach2 in mach1.DefaultIfEmpty()
                         join mcat in _scifferContext.ref_machine_category on order.machine_category_id equals mcat.machine_category_id into mcat1
                         from mcat2 in mcat1.DefaultIfEmpty()
                         join mtype in _scifferContext.ref_maintenance_type on order.maintenance_type_id equals mtype.maintenance_type_id into mtype1
                         from mtype2 in mtype1.DefaultIfEmpty()
                         join plantname in _scifferContext.REF_PLANT on order.plant_id equals plantname.PLANT_ID into plantname1
                         from plantname2 in plantname1.DefaultIfEmpty()
                         select new plan_maintenance_order_VM()
                         {
                             plan_maintenance_order_id = order.maintenance_order_id,
                             order_no = order.order_no,
                             machine_name = mach2 == null ? "" : mach2.machine_name,
                             machine_id = order.machine_id,
                             creation_date = order.creation_date,
                             maintenance_type_id = order.maintenance_type_id,
                             order_desc = order.order_description,
                             plant_id = order.plant_id,
                             manufacturer = order.manufacturer,
                             model_no = order.model_no,
                             manufacturer_part_no = order.manufacturer_part_no,
                             manufacturer_serial_no = order.manufacturer_serial_no,
                             asset_code_id = order.asset_code_id,
                             asset_tag_no = order.asset_tag_no,
                             schedule_date = order.schedule_date,
                             finish_date = order.finish_date,
                             actual_start_date = order.actual_start_date,
                             actual_finish_date = order.actual_finish_date,
                             order_executed_by = order.order_executed_by,
                             order_approved_by = order.order_approved_by,
                             permit_no = order.permit_no,
                             notification_no = order.notification_no,
                             remarks = order.remarks,
                             attachement = order.attachment,
                             machine_category_id = order.machine_category_id,
                             machine_category_name = mcat2.machine_category_code + " - " + mcat2.machine_category_description,
                             is_active = true,
                             maintenance_type_name = mtype2.maintenance_name,

                         }).OrderByDescending(x => x.plan_maintenance_order_id).ToList();
            return query;
        }

        public bool Update1(plan_maintenance_order_VM order)
        {
            try
            {
                ref_plan_maintenance_order mo = new ref_plan_maintenance_order();


                mo.order_no = order.order_no;

                mo.machine_id = order.machine_id;
                mo.creation_date = order.creation_date;
                mo.maintenance_type_id = order.maintenance_type_id;
                mo.order_description = order.order_desc;
                mo.plant_id = order.plant_id;
                mo.manufacturer = order.manufacturer;
                mo.model_no = order.model_no;
                mo.manufacturer_part_no = order.manufacturer_part_no;
                mo.manufacturer_serial_no = order.manufacturer_serial_no;
                mo.asset_code_id = order.asset_code_id;
                mo.asset_tag_no = order.asset_tag_no;
                mo.schedule_date = order.schedule_date;
                mo.finish_date = order.finish_date;
                mo.actual_start_date = order.actual_start_date;
                mo.actual_finish_date = order.actual_finish_date;
                mo.order_executed_by = order.order_executed_by;
                mo.order_approved_by = order.order_approved_by;
                mo.permit_no = order.permit_no;
                mo.notification_no = order.notification_no;
                mo.remarks = order.remarks;
                mo.attachment = order.attachement;
                mo.machine_category_id = order.machine_category_id;
                mo.is_active = true;

                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = order.deleteids.Split(new char[] { '~' });
                }
                catch
                {

                }
                int planm_order_parameter_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        planm_order_parameter_id = int.Parse(deleteStringArray[i]);
                        var order_parameter = _scifferContext.ref_plan_maintenance_order_parameter.Find(planm_order_parameter_id);

                        _scifferContext.Entry(order_parameter).State = EntityState.Modified;
                        order_parameter.is_active = false;
                    }
                }
                List<ref_plan_maintenance_order_parameter> mop = new List<ref_plan_maintenance_order_parameter>();

                foreach (var I in order.ref_plan_maintenance_order_parameter)
                {
                    ref_plan_maintenance_order_parameter op = new ref_plan_maintenance_order_parameter();


                    op.plan_maintenance_order_id = I.plan_maintenance_order_id;
                    op.parameter_id = I.parameter_id;
                    op.range = I.range;
                    op.actual_result = I.actual_result;
                    op.method_used = I.method_used;
                    op.self_check = I.self_check;
                    op.document_reference = I.document_reference;
                    op.sr_no = I.sr_no;
                    op.plan_maintenance_order_id = I.plan_maintenance_order_id;
                    op.maintenance_detail_id = I.maintenance_detail_id;
                    op.is_active = true;
                    mop.Add(op);

                }
                mo.ref_plan_maintenance_order_parameter = mop;
                foreach (var i in mo.ref_plan_maintenance_order_parameter)
                {
                    if (i.plan_maintenance_order_id == 0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }


                int planm_order_cost_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        planm_order_cost_id = int.Parse(deleteStringArray[i]);
                        var order_cost = _scifferContext.ref_plan_maintenance_order_cost.Find(planm_order_cost_id);

                        _scifferContext.Entry(order_cost).State = EntityState.Modified;
                        order_cost.is_active = false;
                    }
                }
                List<ref_plan_maintenance_order_cost> pmc = new List<ref_plan_maintenance_order_cost>();

                foreach (var J in order.ref_plan_maintenance_order_cost)
                {
                    ref_plan_maintenance_order_cost oc = new ref_plan_maintenance_order_cost();

                    oc.plan_maintenance_order_id = J.plan_maintenance_order_id;
                    oc.item_id = J.item_id;
                    oc.quantity = J.quantity;
                    oc.actual_quantity = J.actual_quantity;
                    oc.sloc_id = J.sloc_id;
                    oc.bucket_id = J.bucket_id;
                    oc.plan_maintenance_order_id = J.plan_maintenance_order_id;
                    oc.sr_no = J.sr_no;
                    oc.plan_maintenance_component_id = J.plan_maintenance_component_id;
                    oc.posting_date = J.posting_date;
                    oc.is_active = true;
                    pmc.Add(oc);
                }
                mo.ref_plan_maintenance_order_cost = pmc;
                foreach (var j in mo.ref_plan_maintenance_order_cost)
                {
                    if (j.plan_maintenance_order_id == 0)
                    {
                        _scifferContext.Entry(j).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(j).State = EntityState.Modified;
                    }
                }


                _scifferContext.Entry(mo).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public List<plan_maintenance_order_detail_VM> PlanMaintenanceOrderDetail(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "PlanMaintenanceOrderDetail");
            var val = _scifferContext.Database.SqlQuery<plan_maintenance_order_detail_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public plan_maintenance_order_VM PlanMaintenanceOrder(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "PlanMaintenanceOrder");
            var val = _scifferContext.Database.SqlQuery<plan_maintenance_order_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).FirstOrDefault();
            return val;
        }

        public List<plan_maintenance_order_detail_VM> PlanMaintenanceOrderComponentDetail(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "PlanMaintenanceOrderComponentDetail");
            var val = _scifferContext.Database.SqlQuery<plan_maintenance_order_detail_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }

    }
}

