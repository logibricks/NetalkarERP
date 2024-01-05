using AutoMapper;
using AutoMapper.QueryableExtensions;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class ProductionReceiptService : IProductionReceiptService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;

        public ProductionReceiptService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;

        }
        public string Add(prod_receipt_VM item)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("prod_receipt_detail_id", typeof(int));
                dt1.Columns.Add("prod_order_detail_id", typeof(int));
                dt1.Columns.Add("out_item_id", typeof(int));
                dt1.Columns.Add("uom_id", typeof(int));
                dt1.Columns.Add("sloc_id", typeof(int));
                dt1.Columns.Add("batch_id", typeof(int));
                dt1.Columns.Add("tag_id", typeof(int));
                dt1.Columns.Add("po_quantity", typeof(double));
                if (item.out_item_id != null)
                {
                    for (var i = 0; i < item.out_item_id.Count; i++)
                    {
                            dt1.Rows.Add(item.prod_receipt_detail_id[i] == "0" ? -1 : int.Parse(item.prod_receipt_detail_id[i]),
                            int.Parse(item.prod_order_detail_id[i]),
                            int.Parse(item.out_item_id[i]),
                            int.Parse(item.uom_id[i]),
                            int.Parse(item.sloc_id[i]),
                            int.Parse(item.batch_id[i]),
                            int.Parse(item.tag_id[i]),
                            item.po_quantity[i] == "0" ? 0 : Double.Parse(item.po_quantity[i])
                            );
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_prod_receipt_detail";
                t1.Value = dt1;

                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var prod_receipt_id = new SqlParameter("@prod_receipt_id", item.prod_receipt_id == null ? -1 : item.prod_receipt_id);
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

                var val = _scifferContext.Database.SqlQuery<string>("exec Save_ProdReceipt @prod_receipt_id,@category_id,@posting_date, @document_date, @plant_id, @header_remarks, @is_active,@prod_order_id,@storage_location_id,@deleteids,@create_user,@t1",
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

        public prod_receipt_VM GetDetails(int id)
        {
            prod_receipt GI = _scifferContext.prod_receipt.FirstOrDefault(c => c.prod_receipt_id == id);
            Mapper.CreateMap<prod_receipt, prod_receipt_VM>();
            prod_receipt_VM GIV = Mapper.Map<prod_receipt, prod_receipt_VM>(GI);
            GIV.prod_receipt_details = GIV.prod_receipt_detail.Where(a => a.is_active == true).Select(a => new {
                item_code = a.REF_ITEM.ITEM_CODE,
                item_desc = a.REF_ITEM.ITEM_NAME,
                uom_name = a.REF_UOM.UOM_NAME,
                uom_code = a.REF_UOM.UOM_DESCRIPTION,
                sloc_code = a.REF_STORAGE_LOCATION.description,
                sloc_desc = a.REF_STORAGE_LOCATION.storage_location_name,
                batch_no = a.inv_item_batch == null ? "" : a.inv_item_batch.batch_number,
                tag_no = a.inv_item_batch_detail_tag.tag_no,
                quantity = a.quantity,
            }).ToList().Select(a => new
            prod_receipt_details
            {
                item_code = a.item_code,
                item_desc = a.item_desc,
                uom_code = a.uom_code,
                uom_name = a.uom_name,
                sloc_code = a.sloc_code,
                sloc_desc = a.sloc_desc,
                batch_no = a.batch_no,
                tag_no = a.tag_no,
                quantity = string.Format("{0:0.00}", a.quantity)
            }).ToList();
            return GIV;
        }
        public bool Delete(int id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[prod_receipt] set [is_active] = 0 where prod_receipt_id = " + id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<prod_receipt_VM> GetAll()
        {
            var ent = new SqlParameter("@entity", "getall");
            var val = _scifferContext.Database.SqlQuery<prod_receipt_VM>(
                "exec get_production_receipt @entity", ent).ToList();
            return val;
            //Mapper.CreateMap<prod_receipt, prod_receipt_VM>();
            //return _scifferContext.prod_receipt.Project().To<prod_receipt_VM>().ToList();
            //var query = (from ed in _scifferContext.prod_receipt.Where(x => x.is_active == true)
            //             join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id
            //             join plant in _scifferContext.REF_PLANT on ed.plant_id equals plant.PLANT_ID
            //             join prod in _scifferContext.mfg_prod_order on ed.prod_order_id equals prod.prod_order_id into prod1
            //             from prod2 in prod1.DefaultIfEmpty()
            //             select new prod_receipt_VM
            //             {
            //                 prod_receipt_id = ed.prod_receipt_id,
            //                 prod_receipt_number = ed.prod_receipt_number,
            //                 category_id = ed.category_id,
            //                 category_name = cat.category,
            //                 posting_date = ed.posting_date,
            //                 document_date = ed.document_date,
            //                 plant_id = ed.plant_id,
            //                 plant_name = plant.PLANT_CODE + " - " + plant.PLANT_NAME,
            //                 header_remarks = ed.header_remarks,
            //                 prod_order_id = ed.prod_order_id,
            //                 prod_order_no = prod2 == null ? "" : prod2.prod_order_no + "-" + prod2.prod_order_date,
            //             }).OrderByDescending(a => a.prod_receipt_id).ToList();
            //return query;
        }
       
        public prod_receipt_VM Get(int id)
        {
            prod_receipt GI = _scifferContext.prod_receipt.FirstOrDefault(c => c.prod_receipt_id == id);
            Mapper.CreateMap<prod_receipt, prod_receipt_VM>();
            prod_receipt_VM GIV = Mapper.Map<prod_receipt, prod_receipt_VM>(GI);
            return GIV;
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

        public List<ProductionOrderReceiptVM> GetProductionOrderItems(string id)
        {
            var production_order_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getproductionorderdetail");
            var val = _scifferContext.Database.SqlQuery<ProductionOrderReceiptVM>(
            "exec GetBalanceQuantity @entity,@id", entity, production_order_id).ToList();
            return val;
        }
    }
}
