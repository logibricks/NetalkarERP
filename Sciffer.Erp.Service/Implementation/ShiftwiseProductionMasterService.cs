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
    public class ShiftwiseProductionMasterService : IShiftwiseProductionMasterService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericservice;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name

        public ShiftwiseProductionMasterService(ScifferContext scifferContext, IGenericService genericService)
        {
            _scifferContext = scifferContext;
            _genericservice = genericService;
        }

        public List<mfg_shiftwise_production_master_vm> GetAll()
        {
            try
            {
                var shiftwise_production_id1 = 0;
                var shiftwise_production_id = new SqlParameter("@shiftwise_production_id", shiftwise_production_id1 == 0 ? 0 : shiftwise_production_id1);
                var ent = new SqlParameter("@entity", "GetAll");
                var val = _scifferContext.Database.SqlQuery<mfg_shiftwise_production_master_vm>(
                    "exec GetAllShiftwiseProductionMaster @shiftwise_production_id,@entity", shiftwise_production_id, ent).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public mfg_shiftwise_production_master_vm Get(int id)
        {
            List<shiftwiseProductionDetails> shiftwise_production = null;
            mfg_shiftwise_production_master_vm mfg_shiftwise_production_master_vm = null;
            try
            {
                var shiftwise_production_id = new SqlParameter("@shiftwise_production_id", id == 0 ? 0 : id);
                var ent = new SqlParameter("@entity", "GetByProduction_id");
                shiftwise_production = _scifferContext.Database.SqlQuery<shiftwiseProductionDetails>(
                    "exec GetAllShiftwiseProductionMaster @shiftwise_production_id,@entity", shiftwise_production_id, ent).ToList();

                var ent1 = new SqlParameter("@entity", "GetHeaderDataByProduction_id");
                var shiftwise_production_id1 = new SqlParameter("@shiftwise_production_id", id == 0 ? 0 : id);

                mfg_shiftwise_production_master_vm = _scifferContext.Database.SqlQuery<mfg_shiftwise_production_master_vm>(
                    "exec GetAllShiftwiseProductionMaster @shiftwise_production_id,@entity", shiftwise_production_id1, ent1).FirstOrDefault();

                mfg_shiftwise_production_master_vm.shiftwiseProductionDetails = shiftwise_production.ToList();
                mfg_shiftwise_production_master_vm.shiftwise_production_id = id;
                return mfg_shiftwise_production_master_vm;
                //return production;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string Add(mfg_shiftwise_production_master_vm shiftwise_production)
        {
            try
            {
                //if (shiftwise_production.shiftwiseProductionDetails != null)
                //{
                //    if (shiftwise_production.shiftwiseProductionDetails.Count != 0)
                //    {
                //        foreach (var d in shiftwise_production.shiftwiseProductionDetails)
                //        {
                //            var details = CheckDuplicate(d);
                //            if (details != null)
                //            {
                //                return "Duplicate~" + d.rowIndex;
                //            }
                //        }
                //    }
                //}

                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("shiftwise_production_details_id", typeof(int));
                dt1.Columns.Add("shiftwise_production_id", typeof(int));
                dt1.Columns.Add("operation_code", typeof(string));
                dt1.Columns.Add("operation_name", typeof(string));
                dt1.Columns.Add("machine_id", typeof(int));
                dt1.Columns.Add("ITEM_ID", typeof(int));
                dt1.Columns.Add("cycle_time", typeof(TimeSpan));
                dt1.Columns.Add("std_prod_qty", typeof(decimal));
                dt1.Columns.Add("wip_qty", typeof(decimal));
                dt1.Columns.Add("target_qty", typeof(decimal));

                if (shiftwise_production.shiftwiseProductionDetails != null)
                {
                    if (shiftwise_production.shiftwiseProductionDetails.Count != 0)
                    {
                        foreach (var d in shiftwise_production.shiftwiseProductionDetails)
                        {
                            dt1.Rows.Add(d.shiftwise_production_details_id == 0 ? 0 : d.shiftwise_production_details_id,
                                d.shiftwise_production_id == 0 ? 0 : d.shiftwise_production_id,
                                d.operation_code == null ? "" : d.operation_code,
                                d.operation_name == null ? "" : d.operation_name,
                                d.machine_id == 0 ? 0 : d.machine_id,
                                d.ITEM_ID == 0 ? 0 : d.ITEM_ID,
                                d.cycle_time == null ? null : d.cycle_time,
                                d.std_prod_qty == null ? 0 : d.std_prod_qty,
                                d.wip_qty == null ? 0 : d.wip_qty,
                                d.target_qty == null ? 0 : d.target_qty
                                );
                        }
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_mfg_shiftwise_production_master_details";
                t1.Value = dt1;
                DateTime dte1 = new DateTime(1990, 01, 01);
                var shiftwise_production_id = new SqlParameter("@shiftwise_production_id", shiftwise_production.shiftwise_production_id == 0 ? 0 : shiftwise_production.shiftwise_production_id);
                var document_numbering_id = new SqlParameter("@document_numbering_id", shiftwise_production.document_numbring_id == 0 ? 376 : shiftwise_production.document_numbring_id);
                var shift_id = new SqlParameter("@shift_id", shiftwise_production.shift_id == null ? -1 : shiftwise_production.shift_id);
                var plant_id = new SqlParameter("@plant_id", shiftwise_production.plant_id == null ? -1 : shiftwise_production.plant_id);
                var postingDate = new SqlParameter("@posting_date", shiftwise_production.posting_date);
                var created_by = new SqlParameter("@created_by", create_user);
                var created_ts = new SqlParameter("@created_ts", DateTime.Now);
                //var modified_by = new SqlParameter("@modified_by", create_user);
                //var modified_ts = new SqlParameter("@modified_ts", DateTime.Now);
                var val = _scifferContext.Database.SqlQuery<string>("exec Save_shiftwise_production_master @shiftwise_production_id,@document_numbering_id,@posting_date,@shift_id,@plant_id,@created_by,@created_ts,@t1",
                  shiftwise_production_id, document_numbering_id, postingDate, shift_id, plant_id, created_by, created_ts, t1).FirstOrDefault();
                return val;
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return ex.Message.ToString();
            }
        }

        public List<shiftwiseProductionDetails> GetAllShiftwiseProduction(string entity, DateTime? posting_date, int? plant_id, int? shift_id, int? process_id, int? machine_id, string item_id)
        {
            List<shiftwiseProductionDetails> shiftwise_production = null;

            try
            {
                var shiftwise_production_id1 = 0;
                var operator_id = 0;
                var shiftwise_production_id = new SqlParameter("@shiftwise_production_id", shiftwise_production_id1 == 0 ? 0 : shiftwise_production_id1);
                var posting_date1 = new SqlParameter("@posting_date", posting_date == null ? DateTime.Today : posting_date);
                var ent = new SqlParameter("@entity", entity == null ? "default" : entity);
                var plant_id1 = new SqlParameter("@plant_id", plant_id == null ? 0 : plant_id);
                var shift_id1 = new SqlParameter("@shift_id", shift_id == null ? 0 : shift_id);
                var process_id1 = new SqlParameter("@process_id", process_id == null ? 0 : process_id);
                var machine_id1 = new SqlParameter("@machine_id", machine_id == null ? 0 : machine_id);
                var item_id1 = new SqlParameter("@item_id", item_id == null ? "" : item_id);
                var operator_id1 = new SqlParameter("@operator_id", operator_id == 0 ? 0 : operator_id);

                shiftwise_production = _scifferContext.Database.SqlQuery<shiftwiseProductionDetails>("exec GetAllShiftwiseProductionMaster @shiftwise_production_id,@entity,@shift_id,@plant_id,@process_id,@machine_id,@item_id,@operator_id",
                     shiftwise_production_id, ent, shift_id1, plant_id1, process_id1, machine_id1, item_id1, operator_id1).ToList();
            }
            catch (Exception ex)
            {
                return shiftwise_production;
            }

            return shiftwise_production;
        }

        public int GetQty(string entity, int? plant_id, int shift_id, int? process_id, int? machine_id, string item_id, int? operator_id)
        {
            try
            {
                int qty = 0;

                var shiftwise_production_id1 = 0;
                var shiftwise_production_id = new SqlParameter("@shiftwise_production_id", shiftwise_production_id1 == 0 ? 0 : shiftwise_production_id1);
                var ent = new SqlParameter("@entity", entity == null ? "GetCompleteQty" : entity);
                var plant_id1 = new SqlParameter("@plant_id", plant_id == null ? 0 : plant_id);
                var shift_id1 = new SqlParameter("@shift_id", shift_id == 0 ? 0 : Convert.ToInt32(shift_id));
                var process_id1 = new SqlParameter("@process_id", process_id == null ? 0 : process_id);
                var machine_id1 = new SqlParameter("@machine_id", machine_id == null ? 0 : machine_id);
                var item_id1 = new SqlParameter("@item_id", item_id == null ? "" : item_id);
                var operator_id1 = new SqlParameter("@operator_id", operator_id == null ? 0 : operator_id);
                qty = _scifferContext.Database.SqlQuery<int>(
                    "exec GetAllShiftwiseProductionMaster @shiftwise_production_id,@entity,@shift_id,@plant_id,@process_id,@machine_id,@item_id,@operator_id", shiftwise_production_id, ent, shift_id1 , plant_id1, process_id1, machine_id1, item_id1, operator_id1).FirstOrDefault();

                return qty;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //Not in Used This Method 
        public shiftwiseProductionDetails CheckDuplicate(shiftwiseProductionDetails details)
        {
            try
            {
                var shiftwise_production_id1 = 0;
                var operator_id = 0;
                var shiftwise_production_id = new SqlParameter("@shiftwise_production_id", shiftwise_production_id1);
                var posting_date1 = new SqlParameter("@posting_date", DateTime.Today);
                var ent = new SqlParameter("@entity", "GetDuplicate");
                var plant_id1 = new SqlParameter("@plant_id", DBNull.Value);
                var shift_id1 = new SqlParameter("@shift_id", DBNull.Value);
                var process_id1 = new SqlParameter("@process_id", details.operation_code == null ? "0" : details.operation_code);
                var machine_id1 = new SqlParameter("@machine_id", details.machine_id == 0 ? 0 : details.machine_id);
                var item_id1 = new SqlParameter("@item_id", details.ITEM_ID == 0 ? 0 : details.ITEM_ID);
                var operator_id1 = new SqlParameter("@operator_id", operator_id);
                var ent1 = new SqlParameter("@entity", "GetTargetQtySum");

                var shiftwise_production = _scifferContext.Database.SqlQuery<shiftwiseProductionDetails>("exec GetAllShiftwiseProductionMaster @shiftwise_production_id,@entity,@shift_id,@plant_id,@process_id,@machine_id,@item_id,@operator_id",
                      shiftwise_production_id, ent, shift_id1, plant_id1, process_id1, machine_id1, item_id1, operator_id1).FirstOrDefault();

                var shiftwise_production_id2 = 0;
                var operator_id2 = 0;
                var shiftwise_production_id3 = new SqlParameter("@shiftwise_production_id", shiftwise_production_id2);
                var ent2 = new SqlParameter("@entity", "GetTargetQtySum");
                var posting_date2 = new SqlParameter("@posting_date", DateTime.Today);
                var plant_id2 = new SqlParameter("@plant_id", DBNull.Value);
                var shift_id2 = new SqlParameter("@shift_id", DBNull.Value);
                var process_id2 = new SqlParameter("@process_id", details.operation_code == null ? "0" : details.operation_code);
                var machine_id2 = new SqlParameter("@machine_id", details.machine_id == 0 ? 0 : details.machine_id);
                var item_id2 = new SqlParameter("@item_id", details.ITEM_ID == 0 ? 0 : details.ITEM_ID);
                var operator_id3 = new SqlParameter("@operator_id", operator_id2);
                var TargetQty = _scifferContext.Database.SqlQuery<int>("exec GetAllShiftwiseProductionMaster @shiftwise_production_id,@entity,@shift_id,@plant_id,@process_id,@machine_id,@item_id,@operator_id",
                     shiftwise_production_id3, ent1, shift_id2, plant_id2, process_id2, machine_id2, item_id2, operator_id3).FirstOrDefault();

                if (shiftwise_production != null)
                {
                    var wipqty = Convert.ToInt32(shiftwise_production.wip_qty) - Convert.ToInt32(TargetQty);
                    if (wipqty == Convert.ToInt32(details.wip_qty))
                    {
                        return null;
                    }
                }

                return shiftwise_production;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int CheckDuplicate(DateTime postingDate, int plantId, int shiftId)
        {
           // mfg_shiftwise_production_master_vm vm = null;
            try
            {
                var shift_wise_production = 0;
                var shiftId1 = new SqlParameter("@shift_id", shiftId);
                var plantId1 = new SqlParameter("@plant_id", plantId);
                var postingDate1 = new SqlParameter("@posting_date", postingDate);
                var shift_wise_production_detail_id = new SqlParameter("@shift_wise_production_detail_id", shift_wise_production);
                var ent = new SqlParameter("@entity", "GetDuplicateHeader");
                var count = _scifferContext.Database.SqlQuery<int>(
                    "exec GetShiftwiseProductionMaster @entity,@shift_id,@plant_id,@posting_date,@shift_wise_production_detail_id", ent, shiftId1, plantId1, postingDate1, shift_wise_production_detail_id).FirstOrDefault();           

                return count;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public List<shiftwiseProductionDetails> GetDetails(int id)
        {

            List<shiftwiseProductionDetails> shiftwise_production = null;
            try
            {
                var shiftwise_production_id = new SqlParameter("@shiftwise_production_id", id == 0 ? 0 : id);
                var ent = new SqlParameter("@entity", "GetByDetails_id");
                shiftwise_production = _scifferContext.Database.SqlQuery<shiftwiseProductionDetails>(
                    "exec GetAllShiftwiseProductionMaster @shiftwise_production_id,@entity", shiftwise_production_id, ent).ToList();

                return shiftwise_production;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string Update(mfg_shiftwise_production_master_vm shiftwise_production, List<shiftwiseProductionDetails> newItem)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("shiftwise_production_details_id", typeof(int));
                dt1.Columns.Add("shiftwise_production_id", typeof(int));
                dt1.Columns.Add("operation_code", typeof(string));
                dt1.Columns.Add("operation_name", typeof(string));
                dt1.Columns.Add("machine_id", typeof(int));
                dt1.Columns.Add("ITEM_ID", typeof(int));
                dt1.Columns.Add("cycle_time", typeof(TimeSpan));
                dt1.Columns.Add("std_prod_qty", typeof(decimal));
                dt1.Columns.Add("wip_qty", typeof(decimal));
                dt1.Columns.Add("target_qty", typeof(decimal));

                if (newItem != null)
                {
                    if (newItem.Count != 0)
                    {
                        foreach (var d in newItem)
                        {
                            dt1.Rows.Add(d.shiftwise_production_details_id == 0 ? 0 : d.shiftwise_production_details_id,
                                d.shiftwise_production_id == 0 ? 0 : d.shiftwise_production_id,
                                d.operation_code == null ? "" : d.operation_code,
                                d.operation_name == null ? "" : d.operation_name,
                                d.machine_id == 0 ? 0 : d.machine_id,
                                d.ITEM_ID == 0 ? 0 : d.ITEM_ID,
                                d.cycle_time == null ? null : d.cycle_time,
                                d.std_prod_qty == null ? 0 : d.std_prod_qty,
                                d.wip_qty == null ? 0 : d.wip_qty,
                                d.target_qty == null ? 0 : d.target_qty
                                );
                        }
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_mfg_shiftwise_production_master_details";
                t1.Value = dt1;

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("shiftwise_production_details_id", typeof(int));
                dt2.Columns.Add("shiftwise_production_id", typeof(int));
                dt2.Columns.Add("operation_code", typeof(string));
                dt2.Columns.Add("operation_name", typeof(string));
                dt2.Columns.Add("machine_id", typeof(int));
                dt2.Columns.Add("ITEM_ID", typeof(int));
                dt2.Columns.Add("cycle_time", typeof(TimeSpan));
                dt2.Columns.Add("std_prod_qty", typeof(decimal));
                dt2.Columns.Add("wip_qty", typeof(decimal));
                dt2.Columns.Add("target_qty", typeof(decimal));
                dt2.Columns.Add("new_target_qty", typeof(decimal));

                if (shiftwise_production.shiftwiseProductionDetails != null)
                {
                    if (shiftwise_production.shiftwiseProductionDetails.Count != 0)
                    {
                        foreach (var d in shiftwise_production.shiftwiseProductionDetails)
                        {
                            dt2.Rows.Add(d.shiftwise_production_details_id == 0 ? 0 : d.shiftwise_production_details_id,
                                d.shiftwise_production_id == 0 ? 0 : d.shiftwise_production_id,
                                d.operation_code == null ? "" : d.operation_code,
                                d.operation_name == null ? "" : d.operation_name,
                                d.machine_id == 0 ? 0 : d.machine_id,
                                d.ITEM_ID == 0 ? 0 : d.ITEM_ID,
                                d.cycle_time == null ? null : d.cycle_time,
                                d.std_prod_qty == null ? 0 : d.std_prod_qty,
                                d.wip_qty == null ? 0 : d.wip_qty,
                                d.target_qty == null ? 0 : d.target_qty,
                                d.new_target_qty = d.new_target_qty
                                );
                        }
                    }
                }

                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_mfg_shiftwise_production_master_details_new";
                t2.Value = dt2;

                //DateTime dte1 = new DateTime(1990, 01, 01);
                var shiftwise_production_id = new SqlParameter("@shiftwise_production_id", shiftwise_production.shiftwise_production_id == 0 ? 0 : shiftwise_production.shiftwise_production_id);
                var document_numbering_id = new SqlParameter("@document_numbering_id", shiftwise_production.document_numbring_id == 0 ? 0 : shiftwise_production.document_numbring_id);
                //var shift_id = new SqlParameter("@shift_id", shiftwise_production.shift_id == null ? -1 : shiftwise_production.shift_id);
                //var plant_id = new SqlParameter("@plant_id", shiftwise_production.plant_id == null ? -1 : shiftwise_production.plant_id);
                // var postingDate = new SqlParameter("@posting_date", shiftwise_production.posting_date);
                //var created_by = new SqlParameter("@created_by", create_user);
                //var created_ts = new SqlParameter("@created_ts", DateTime.Today);
                var modified_by = new SqlParameter("@modified_by", create_user);
                var val = _scifferContext.Database.SqlQuery<string>("exec Update_shiftwise_production_master @shiftwise_production_id,@document_numbering_id,@modified_by,@t1,@t2", shiftwise_production_id, document_numbering_id,
                  modified_by, t1, t2).FirstOrDefault();

                return val;
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return ex.Message.ToString();
            }
        }
    }
}
