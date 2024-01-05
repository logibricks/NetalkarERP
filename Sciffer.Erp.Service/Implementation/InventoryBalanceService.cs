using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;

namespace Sciffer.Erp.Service.Implementation
{ 
    public class InventoryBalanceService : IInventoryBalanceService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name

        private readonly ScifferContext _scifferContext;
        public InventoryBalanceService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(ref_inventory_balance_VM item)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("inventory_balance_detail_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("plant_id", typeof(int));
                dt.Columns.Add("sloc_id", typeof(int));
                dt.Columns.Add("bucket_id", typeof(int));
                dt.Columns.Add("batch", typeof(string));
                dt.Columns.Add("qty", typeof(int));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("rate", typeof(double));
                dt.Columns.Add("value", typeof(double));
                dt.Columns.Add("line_remarks", typeof(string));
                dt.Columns.Add("is_active", typeof(bool));
                
                for(int i=0; i<item.item_id.Count;i++)
                {
                    dt.Rows.Add(int.Parse(item.inventory_balance_detail_id[i]) == 0 ? -1 : int.Parse(item.inventory_balance_detail_id[i]), int.Parse(item.item_id[i]),
                        int.Parse(item.plant_id[i]), int.Parse(item.sloc_id[i]), int.Parse(item.bucket_id[i]), item.batch[i], Double.Parse(item.qty[i]),
                        int.Parse(item.uom_id[i]), Double.Parse(item.rate[i]), Double.Parse(item.value[i]),item.line_remarks[i],1);
                }
                
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_inventory_balance_detail";
                t1.Value = dt;
                int create_user = 0;

                var inventory_balance_id = new SqlParameter("@inventory_balance_id", item.inventory_balance_id == null ? -1 : item.inventory_balance_id);
                var offset_account_id = new SqlParameter("@offset_account_id", item.offset_account_id);
                var posting_date = new SqlParameter("@posting_date", item.posting_date);
                var header_remarks = new SqlParameter("@header_remarks", item.header_remarks==null?"":item.header_remarks);
                var is_active = new SqlParameter("@is_active", 1);
                var doc_number = new SqlParameter("@doc_number", item.doc_number);
                var category_id = new SqlParameter("@category_id", item.category_id);
                var user = new SqlParameter("@create_user", create_user);

                var val = _scifferContext.Database.SqlQuery<string>("exec Save_InventoryBalance @inventory_balance_id,@offset_account_id,@posting_date,@header_remarks,@is_active,@doc_number,@category_id,@create_user,@t1",
                    inventory_balance_id, offset_account_id, posting_date, header_remarks, is_active, doc_number, category_id, user, t1).FirstOrDefault();
                if (val == "Saved")
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
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return "error";
            }
        }
        public ref_inventory_balance_VM GetDetails(int id)
        {
            try
            {
                ref_inventory_balance JR = _scifferContext.ref_inventory_balance.FirstOrDefault(c => c.inventory_balance_id == id);
                Mapper.CreateMap<ref_inventory_balance, ref_inventory_balance_VM>();
                ref_inventory_balance_VM JRVM = Mapper.Map<ref_inventory_balance, ref_inventory_balance_VM>(JR);
                JRVM.ref_inventory_balance_details = JRVM.ref_inventory_balance_details.Where(a => a.is_active == true).ToList();
                JRVM.inventory_balance_detail = JRVM.ref_inventory_balance_details.Select(a => new {
                    item_code = a.REF_ITEM.ITEM_CODE,
                    item_desc = a.REF_ITEM.ITEM_NAME,
                    plant_code = a.REF_PLANT.PLANT_CODE,
                    plant_desc = a.REF_PLANT.PLANT_NAME,
                    sloc = a.REF_STORAGE_LOCATION.storage_location_name,
                    bucket = a.ref_bucket.bucket_name,
                    batch = a.batch,
                    qty = a.qty,
                    uom = a.REF_UOM.UOM_NAME,
                    rate = a.rate,
                    value = a.value,
                    remarks = a.line_remarks,
                }).ToList().Select(a => new inventory_balance_detail
                {
                    item_code = a.item_code,
                    item_desc = a.item_desc,
                    plant_code = a.plant_code,
                    plant_desc = a.plant_desc,
                    sloc = a.sloc,
                    bucket = a.bucket,
                    batch = a.batch,
                    qty = string.Format("{0:0.00}", a.qty),
                    uom = a.uom,
                    rate = string.Format("{0:0.00}", a.rate),
                    value = string.Format("{0:0.00}", a.value),
                    line_remarks = a.remarks,
                }).ToList();
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AddExcel(List<inventory_balance_VM> item, List<inventory_balance_detail_VM> bldetails)
        {
            try
            {
                foreach (var data in item)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("inventory_balance_detail_id", typeof(int));
                    dt.Columns.Add("item_id", typeof(int));
                    dt.Columns.Add("plant_id", typeof(int));
                    dt.Columns.Add("sloc_id", typeof(int));
                    dt.Columns.Add("bucket_id", typeof(int));
                    dt.Columns.Add("batch", typeof(string));
                    dt.Columns.Add("qty", typeof(int));
                    dt.Columns.Add("uom_id", typeof(int));
                    dt.Columns.Add("rate", typeof(double));
                    dt.Columns.Add("value", typeof(double));
                    dt.Columns.Add("line_remarks", typeof(string));
                    dt.Columns.Add("is_active", typeof(bool));
                    foreach (var items in bldetails)
                    {
                        if (data.offset_account == items.offset_account)
                        {
                            dt.Rows.Add(items.inventory_balance_detail_id == null ? -1 : items.inventory_balance_detail_id, items.item_id, items.plant_id,
                                items.sloc_id, items.bucket_id, items.batch, items.qty, items.uom_id, items.rate,items.value,items.line_remarks,1);
                        }
                    }
                    var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                    t1.TypeName = "dbo.temp_inventory_balance_detail";
                    t1.Value = dt;
                    int create_user = 0;
                    var inventory_balance_id = new SqlParameter("@inventory_balance_id", data.inventory_balance_id == null ? -1 : data.inventory_balance_id);
                    var offset_account_id = new SqlParameter("@offset_account_id", data.offset_account_id);
                    var posting_date = new SqlParameter("@posting_date", data.posting_date);
                    var header_remarks = new SqlParameter("@header_remarks", data.header_remarks == null ? "" : data.header_remarks);
                    var is_active = new SqlParameter("@is_active", 1);
                    var doc_number = new SqlParameter("@doc_number", data.doc_number);
                    var category_id = new SqlParameter("@category_id", data.category_id);
                    var user = new SqlParameter("@create_user", create_user);

                    var val = _scifferContext.Database.SqlQuery<string>("exec Save_InventoryBalance @inventory_balance_id,@offset_account_id,@posting_date,@header_remarks,@is_active,@doc_number,@category_id,@create_user,@t1",
                        inventory_balance_id, offset_account_id, posting_date, header_remarks, is_active, doc_number, category_id, user, t1).FirstOrDefault();
                    if (val == "Saved")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool Delete(int id)
        {
            try
            {
                var re = _scifferContext.ref_inventory_balance.Where(x => x.inventory_balance_id == id).FirstOrDefault();
                re.is_active = false;
                _scifferContext.Entry(re).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public ref_inventory_balance_VM Get(int id)
        {
            try
            {
                ref_inventory_balance JR = _scifferContext.ref_inventory_balance.FirstOrDefault(c => c.inventory_balance_id == id);
                Mapper.CreateMap<ref_inventory_balance, ref_inventory_balance_VM>();
                ref_inventory_balance_VM JRVM = Mapper.Map<ref_inventory_balance, ref_inventory_balance_VM>(JR);
                JRVM.ref_inventory_balance_details = JRVM.ref_inventory_balance_details.ToList();
                JRVM.gl_ledger_id = JR.ref_general_ledger.gl_ledger_id;

                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_inventory_balanceVM> GetAll()
        {
            var list = (from ed in _scifferContext.ref_inventory_balance.Where(x => x.is_active == true)
                        join r in _scifferContext.ref_general_ledger on ed.offset_account_id equals r.gl_ledger_id
                        join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id
                        select new ref_inventory_balanceVM
                        {
                            hearder_remarks = ed.header_remarks,
                            offset_account_id = ed.offset_account_id,
                            posting_date = ed.posting_date,
                            inventory_balance_id = ed.inventory_balance_id,
                            gl_ledger_code = r.gl_ledger_code,
                            gl_ledger_name = r.gl_ledger_code + "-" + r.gl_ledger_name,
                            category_id = ed.category_id,
                            doc_number = ed.doc_number,
                            category_name = cat.category ,

                        }).OrderByDescending(a => a.inventory_balance_id).ToList();
            return list;
        }

        public bool Update(ref_inventory_balance_VM item)
        {
            try
            {

                ref_inventory_balance re = new ref_inventory_balance();

                re.offset_account_id = item.offset_account_id;
                re.header_remarks = item.header_remarks;
                re.posting_date = item.posting_date;
                re.inventory_balance_id = item.inventory_balance_id;
                re.is_active = true;

                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = item.deleteids.Split(new char[] { '~' });
                }
                catch
                {

                }
                int pt_detail_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        pt_detail_id = int.Parse(deleteStringArray[i]);
                        var pt_detail = _scifferContext.ref_inventory_balance_details.Find(pt_detail_id);
                        pt_detail.is_active = false;
                        _scifferContext.Entry(pt_detail).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                List<ref_inventory_balance_details> allalternate = new List<ref_inventory_balance_details>();
                if (item.ref_inventory_balance_details != null)
                {
                    foreach (var alternates in item.ref_inventory_balance_details)
                    {
                        ref_inventory_balance_details alternate = new ref_inventory_balance_details();
                        alternate.inventory_balance_id = item.inventory_balance_id;
                        if (alternates.inventory_balance_detail_id == null)
                        {
                            alternates.inventory_balance_detail_id = 0;
                        }
                        alternate.inventory_balance_id = alternates.inventory_balance_id;
                        alternate.item_id = alternates.item_id;
                        alternate.line_remarks = alternates.line_remarks;
                        alternate.plant_id = alternates.plant_id;
                        alternate.qty = alternates.qty;
                        alternate.rate = alternates.rate;
                        alternate.sloc_id = alternates.sloc_id;
                        alternate.uom_id = alternates.uom_id;
                        alternate.value = alternates.value;
                        alternate.batch = alternates.batch;
                        alternate.bucket_id = alternates.bucket_id;
                        alternate.is_active = true;
                        allalternate.Add(alternate);
                    }

                }

                foreach (var i in allalternate)
                {
                    if (i.inventory_balance_detail_id == 0)
                    {
                        _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                re.ref_inventory_balance_details = allalternate;
                _scifferContext.Entry(re).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
