using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.SqlClient;
using Sciffer.Erp.Data;
using System.Data;
using AutoMapper;
using Sciffer.Erp.Domain.Model;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class RejectionReceiptService : IRejectionReceiptService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;

        public RejectionReceiptService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;

        }
        public string Add(rejection_receipt_VM item)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("reject_receipt_detail_id", typeof(int));
                dt1.Columns.Add("prod_order_detail_id", typeof(int));
                dt1.Columns.Add("out_item_id", typeof(int));
                dt1.Columns.Add("uom_id", typeof(int));
                dt1.Columns.Add("sloc_id", typeof(int));
                dt1.Columns.Add("batch_id", typeof(int));
                dt1.Columns.Add("tag_id", typeof(int));
                dt1.Columns.Add("po_quantity", typeof(double));
                dt1.Columns.Add("reason_id", typeof(int));
                if (item.out_item_id != null)
                {
                    for (var i = 0; i < item.out_item_id.Count; i++)
                    {

                        int result = _genericService.GetCheck_Inventory(Convert.ToInt32(item.out_item_id[i]), Convert.ToInt32(item.plant_id), Convert.ToInt32(item.storage_location_id), 2, Convert.ToDecimal(item.po_quantity[i]));
                        if (result == 0)
                        {
                            return "Stock is Not Available";
                        }

                        dt1.Rows.Add(item.reject_receipt_detail_id[i] == "0" ? -1 : int.Parse(item.reject_receipt_detail_id[i]),
                            int.Parse(item.prod_order_detail_id[i]),
                            int.Parse(item.out_item_id[i]),
                            int.Parse(item.uom_id[i]),
                            int.Parse(item.sloc_id[i]),
                            int.Parse(item.batch_id[i]),
                            int.Parse(item.tag_id[i]),
                            item.po_quantity[i] == "0" ? 0 : Double.Parse(item.po_quantity[i]),
                            item.reason_id[i]
                            );
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_reject_receipt_detail";
                t1.Value = dt1;

                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var prod_receipt_id = new SqlParameter("@reject_receipt_id", item.reject_receipt_id == null ? -1 : item.reject_receipt_id);
                var category_id = new SqlParameter("@category_id", item.category_id);
                var posting_date = new SqlParameter("@posting_date", item.posting_date);
                var document_date = new SqlParameter("@document_date", item.document_date);
                var plant_id = new SqlParameter("@plant_id", item.plant_id);
                var header_remarks = new SqlParameter("@header_remarks", item.header_remarks == null ? "" : item.header_remarks);
                var is_active = new SqlParameter("@is_active", 1);
                var prod_order_id = new SqlParameter("@prod_order_id", item.prod_order_id);
                var storage_location_id = new SqlParameter("@storage_location_id", item.storage_location_id);
                var deleteids = new SqlParameter("@deleteids", item.deleteids == null ? "" : item.deleteids);
                var user = new SqlParameter("@create_user", create_user);

                var val = _scifferContext.Database.SqlQuery<string>("exec Save_RejectionReceipt @reject_receipt_id,@category_id,@posting_date, @document_date, @plant_id, @header_remarks, @is_active,@prod_order_id,@storage_location_id,@deleteids,@create_user,@t1",
                   prod_receipt_id, category_id, posting_date, document_date, plant_id, header_remarks, is_active, prod_order_id, storage_location_id,
                   deleteids, user, t1).FirstOrDefault();
               
                return val;
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

        public bool Delete(int id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[rejection_receipt] set [is_active] = 0 where reject_receipt_id = " + id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public rejection_receipt_VM Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<rejection_receipt_VM> GetAll()
        {

            var ent = new SqlParameter("@entity", "getall");
            var val = _scifferContext.Database.SqlQuery<rejection_receipt_VM>(
                "exec get_rejection_receipt @entity", ent).ToList();
            return val;
            //var query = (from ed in _scifferContext.rejection_receipt.Where(x => x.is_active == true)
            //             join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id
            //             join plant in _scifferContext.REF_PLANT on ed.plant_id equals plant.PLANT_ID
            //             join prod in _scifferContext.mfg_prod_order on ed.prod_order_id equals prod.prod_order_id
            //             select new rejection_receipt_VM
            //             {
            //                 reject_receipt_id = ed.reject_receipt_id,
            //                 reject_receipt_number = ed.reject_doc_number,
            //                 category_id = ed.category_id,
            //                 category_name = cat.category,
            //                 posting_date = ed.posting_date,
            //                 document_date = ed.document_date,
            //                 plant_id = ed.plant_id,
            //                 plant_name = plant.PLANT_CODE + " - " + plant.PLANT_NAME,
            //                 header_remarks = ed.header_remarks,
            //                 prod_order_id = ed.prod_order_id,
            //                 prod_order_no = prod.prod_order_no + "" + prod.prod_order_date,
            //             }).OrderByDescending(a => a.reject_receipt_id).ToList();
            //return query;
        }

        public rejection_receipt_VM GetDetails(int id)
        {
            rejection_receipt GI = _scifferContext.rejection_receipt.FirstOrDefault(c => c.reject_receipt_id == id);
            Mapper.CreateMap<rejection_receipt, rejection_receipt_VM>();
            rejection_receipt_VM GIV = Mapper.Map<rejection_receipt, rejection_receipt_VM>(GI);
            //GIV.reject_receipt_details = GIV.rejection_receipt_detail.Where(a => a.is_active == true).Select(a => new {
            //    item_code = a.REF_ITEM.ITEM_CODE,
            //    item_desc = a.REF_ITEM.ITEM_NAME,
            //    uom_name = a.REF_UOM.UOM_NAME,
            //    uom_code = a.REF_UOM.UOM_DESCRIPTION,
            //    sloc_code = a.REF_STORAGE_LOCATION.description,
            //    sloc_desc = a.REF_STORAGE_LOCATION.storage_location_name,
            //    batch_no = a.inv_item_batch == null ? "" : a.inv_item_batch.batch_number,
            //    tag_no = a.inv_item_batch_detail_tag.tag_no,
            //    quantity = a.quantity,
            //    reason = a.REF_REASON_DETERMINATION.reason_determination_code,
            //}).ToList().Select(a => new
            //reject_receipt_details
            //{
            //    item_code = a.item_code,
            //    item_desc = a.item_desc,
            //    uom_code = a.uom_code,
            //    uom_name = a.uom_name,
            //    sloc_code = a.sloc_code,
            //    sloc_desc = a.sloc_desc,
            //    batch_no = a.batch_no,
            //    tag_no = a.tag_no,
            //    quantity = string.Format("{0:0.00}", a.quantity),
            //    reason= a.reason,
            //}).ToList();
            var quotation_id = new SqlParameter("@id", id);
            GIV.reject_receipt_details = _scifferContext.Database.SqlQuery<reject_receipt_details>(
            "exec get_rejection_receipt_edit @id",  quotation_id).ToList();
            return GIV;
        }
        public List<ProductionOrderReceiptVM> GetRejectionItems(string id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getrejectproductionorderdetail");
            var val = _scifferContext.Database.SqlQuery<ProductionOrderReceiptVM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
    }
}