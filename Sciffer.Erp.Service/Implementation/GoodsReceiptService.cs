using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class GoodsReceiptService : IGoodsReceiptService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public GoodsReceiptService(ScifferContext scifferContext, IGenericService genericService)
        {
            _scifferContext = scifferContext;
            _genericService = genericService;
        }

        public string Add(GOODS_RECEIPT_VM item)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("goods_detail_id", typeof(int));
                dt1.Columns.Add("sr_no", typeof(int));
                dt1.Columns.Add("item_id", typeof(int));
                dt1.Columns.Add("storage_location_id", typeof(int));
                dt1.Columns.Add("bucket_id", typeof(int));
                dt1.Columns.Add("batch_yes_no", typeof(bool));
                dt1.Columns.Add("batch_manual", typeof(string));
                dt1.Columns.Add("reason_determination_id", typeof(int));
                dt1.Columns.Add("item_batch_id", typeof(int));
                dt1.Columns.Add("general_ledger_id", typeof(int));
                dt1.Columns.Add("quantity", typeof(double));
                dt1.Columns.Add("uom_id", typeof(int));
                dt1.Columns.Add("rate", typeof(double));
                dt1.Columns.Add("value", typeof(double));
                dt1.Columns.Add("remark", typeof(string));
                dt1.Columns.Add("grn_id", typeof(int));
                for (var i = 0; i < item.item_id.Count; i++)
                {
                    dt1.Rows.Add(item.goods_detail_id[i] == "0" ? -1 : int.Parse(item.goods_detail_id[i]), int.Parse(item.sr_no[i]),
                        int.Parse(item.item_id[i]), int.Parse(item.storage_location_id[i]), int.Parse(item.bucket_id[i]), item.batch_yes_no[i]=="No"?false:true,
                         item.batch_manual[i], int.Parse(item.reason_determination_id[i]), item.item_batch_id[i] == "" ? 0 : int.Parse(item.item_batch_id[i]),
                         int.Parse(item.general_ledger_id[i]), Double.Parse(item.quantity[i]), int.Parse(item.uom_id[i]), Double.Parse(item.rate[i]),
                         Double.Parse(item.value[i]), item.remark[i], item.grn_id[i] == "" ? 0 : int.Parse(item.grn_id[i]));
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_goods_receipt_detail";
                t1.Value = dt1;
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                var goods_receipt_id = new SqlParameter("@goods_receipt_id", item.goods_receipt_id == null ? -1 : item.goods_receipt_id);
                var category_id = new SqlParameter("@category_id", item.category_id);
                var goods_receipt_number = new SqlParameter("@goods_receipt_number", item.goods_receipt_number==null?"":item.goods_receipt_number);
                var posting_date = new SqlParameter("@posting_date", item.posting_date);
                var document_date = new SqlParameter("@document_date", item.document_date);
                var plant_id = new SqlParameter("@plant_id", item.plant_id);
                var remarks = new SqlParameter("@remarks", item.remarks == null ? "" : item.remarks);
                // var attachment = new SqlParameter("@attachment", item.attachment);                
                var is_active = new SqlParameter("@is_active", 1);
                var ref1 = new SqlParameter("@ref1", item.ref1 == null ? "" : item.ref1);
                var user = new SqlParameter("@create_user", create_user);
                var deleteids = new SqlParameter("@deleteids", item.deleteids == null ? "" : item.deleteids);
                if (item.FileUpload != null)
                {
                    item.attachment = _genericService.GetFilePath("ITEM", item.FileUpload);
                }
                else
                {
                    item.attachment = "No File";
                }
                var attachment = new SqlParameter("@attachment", item.attachment);
                var val = _scifferContext.Database.SqlQuery<string>("exec Save_GoodsReceipt @goods_receipt_id,@category_id,@goods_receipt_number,@posting_date, @document_date, @plant_id, @remarks, @attachment, @is_active,@ref1,@create_user,@deleteids,@t1",
                   goods_receipt_id, category_id, goods_receipt_number, posting_date, document_date, plant_id, remarks, attachment, is_active, ref1, user, deleteids, t1).FirstOrDefault();
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
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[GOODS_RECEIPT] set [IS_ACTIVE] = 0 where GOODS_RECEIPT_ID = " + id);
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

        public GOODS_RECEIPT_VM Get(int id)
        {
            goods_receipt GI = _scifferContext.goods_receipt.FirstOrDefault(c => c.goods_receipt_id == id);
            Mapper.CreateMap<goods_receipt, GOODS_RECEIPT_VM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            GOODS_RECEIPT_VM GIV = Mapper.Map<goods_receipt, GOODS_RECEIPT_VM>(GI);
            GIV.goods_receipt_detail = GIV.goods_receipt_detail.Where(c => c.is_active == true).ToList();
            int inv_item_batch = GIV.goods_receipt_detail.FirstOrDefault().item_batch_id;
            //var batch_number;
            if (inv_item_batch != 0 && inv_item_batch != null)
            {
                var batch_number = _scifferContext.inv_item_batch.Where(x => x.item_batch_id == inv_item_batch);
                GIV.batch_number = batch_number == null ? "" : batch_number.FirstOrDefault().batch_number;
            }            
            int? grn_id = GIV.goods_receipt_detail.FirstOrDefault().grn_id;
            if (grn_id != 0 && grn_id != null)
            {
                var grn = _scifferContext.pur_grn.Where(x => x.grn_id == grn_id);
                GIV.grn = grn == null ? "" : grn.FirstOrDefault().grn_number + " - " + grn.FirstOrDefault().posting_date;
            }
            return GIV;
        }

        public List<GOODS_RECEIPT_VM> GetAll()
        {

            Mapper.CreateMap<goods_receipt, GOODS_RECEIPT_VM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            return _scifferContext.goods_receipt.Project().To<GOODS_RECEIPT_VM>().Where(c => c.is_active == true).ToList();

        }

        public List<GOODS_RECEIPT_VM> getall()
        {
            var ent = new SqlParameter("@entity", "getall_receipt");
            var val = _scifferContext.Database.SqlQuery<GOODS_RECEIPT_VM>(
                "exec get_goods_issue_receipt @entity", ent).ToList();
            return val;
            //   var a = _scifferContext.goods_receipt.ToList();
            //var query = (from goods in _scifferContext.goods_receipt
            //             join category in _scifferContext.ref_document_numbring on goods.category_id equals category.document_numbring_id into category1
            //             from category2 in category1.DefaultIfEmpty()
            //             join plantname in _scifferContext.REF_PLANT on goods.plant_id equals plantname.PLANT_ID into plantname1
            //             from plantname2 in plantname1.DefaultIfEmpty()


            //             select new GOODS_RECEIPT_VM()
            //             {
            //                 goods_receipt_id = goods.goods_receipt_id,
            //                 category_id = category2 == null ? 0 : category2.document_numbring_id,
            //                 goods_receipt_number = goods.goods_receipt_number,
            //                 posting_date = goods.posting_date,
            //                 document_date = goods.document_date,
            //                 plant_id = goods.plant_id,
            //                 plant_name = plantname2.PLANT_NAME,
            //                 remarks = goods.remarks,
            //                 category_name = category2.category,
            //                 // attachement = goods.attachment,
            //                 ref1 = goods.ref1

            //             }).OrderByDescending(a => a.goods_receipt_id).ToList();
            //return query;
        }


        public bool Update(GOODS_RECEIPT_VM goods)
        {
            try
            {
                goods_receipt GI = new goods_receipt();
                GI.goods_receipt_id = goods.goods_receipt_id;
                GI.goods_receipt_number = goods.goods_receipt_number;
                GI.posting_date = goods.posting_date;
                GI.document_date = goods.document_date;

                GI.plant_id = goods.plant_id;

                GI.remarks = goods.remarks;
                if (goods.attachment != null)
                    //GI.attachment = goods.attachment.FileName;
                    GI.category_id = goods.category_id;
                GI.is_active = goods.is_active;
                GI.ref1 = goods.ref1;
               
                //HttpPostedFileBase aFile = goods.ATTACHMENT;
                //int contentLength = aFile.ContentLength;
                //byte[] bytePic = new byte[contentLength];
                //aFile.InputStream.Read(bytePic, 0, contentLength);
                //GI.ATTACHMENT = bytePic;
                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = goods.deleteids.Split(new char[] { '~' });
                }
                catch
                {

                }
                int goods_detail_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        goods_detail_id = int.Parse(deleteStringArray[i]);
                        var goods_detail = _scifferContext.goods_receipt_detail.Find(goods_detail_id);
                        _scifferContext.Entry(goods_detail).State = EntityState.Modified;
                        goods_detail.is_active = false;
                    }
                }

                List<goods_receipt_detail> GIS = new List<goods_receipt_detail>();

                foreach (var I in goods.goods_receipt_detail)
                {
                    goods_receipt_detail GD = new goods_receipt_detail();
                    GD.goods_detail_id = I.goods_detail_id;
                    GD.sr_no = I.sr_no;
                    GD.goods_receipt_id = goods.goods_receipt_id;
                    GD.item_id = I.item_id;
                    GD.reason_determination_id = I.reason_determination_id;
                    GD.storage_location_id = I.storage_location_id;
                    GD.item_batch_id = I.item_batch_id;
                    GD.bucket_id = I.bucket_id;//Review this line ************************ 
                    GD.general_ledger_id = I.general_ledger_id;
                    GD.quantity = I.quantity;
                    GD.uom_id = I.uom_id;
                    GD.rate = I.rate;
                    GD.value = I.value;
                    GD.remark = I.remark;
                    GD.is_active = true;
                    GIS.Add(GD);
                }
                GI.goods_receipt_detail = GIS;
                foreach (var i in GI.goods_receipt_detail)
                {
                    if (i.goods_detail_id == 0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }
                _scifferContext.Entry(GI).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public double GetMAP(int item_id, int plant_id)
        {
            double map = _scifferContext.ref_item_plant_valuation.Where(x => x.item_id == item_id && x.plant_id == plant_id).Select(y => y.item_value).FirstOrDefault();
            return map;
        }
        public GOODS_RECEIPT_VM GetDetails(int id)
        {
            goods_receipt GI = _scifferContext.goods_receipt.FirstOrDefault(c => c.goods_receipt_id == id);
            Mapper.CreateMap<goods_receipt, GOODS_RECEIPT_VM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            GOODS_RECEIPT_VM GIV = Mapper.Map<goods_receipt, GOODS_RECEIPT_VM>(GI);
            if (GIV.cancellation_reason_id != null)
            {
                var cancellation_reason = _scifferContext.ref_cancellation_reason.Where(x => x.cancellation_reason_id == GI.cancellation_reason_id).FirstOrDefault();
                GIV.cancellation_reason = cancellation_reason.cancellation_reason;
                GIV.cancelled_date = GI.cancelled_date;
                GIV.cancellation_remarks = GI.cancellation_remarks;
            }
            GIV.goods_receipt_detail = GIV.goods_receipt_detail.Where(c => c.is_active == true).ToList();
            GIV.good_receipt_detail = GIV.goods_receipt_detail.Where(c => c.is_active == true).Select(a => new
            {
                item_code = a.REF_ITEM.ITEM_CODE,
                item_desc = a.REF_ITEM.ITEM_NAME,
                reason = a.REF_REASON_DETERMINATION.reason_determination_code,
                batch = a.inv_item_batch == null ? "" : a.inv_item_batch.batch_number,
                batch_yes_no = a.batch_yes_no == true ? "yes" : "no",
                quantity = a.quantity,
                rate = a.rate,
                value = a.value,
                gl = a.REF_GENERAL_LEDGER.gl_ledger_code,
                sloc = a.REF_STORAGE_LOCATION.description,
                grn = a.pur_grn == null ? "" : a.pur_grn.grn_number,
                remarks = a.remark,
                bucket = a.ref_bucket.bucket_name,
                uom = a.REF_UOM.UOM_NAME,
                batch_manual = a.batch_manual,
            }).ToList().Select(a => new
            good_receipt_detail
            {
                item_code = a.item_code,
                item_desc = a.item_desc,
                reason = a.reason,
                batch = a.batch,
                batch_yes_no = a.batch_yes_no,
                quantity = a.quantity == null ? "0.00" : string.Format("{0:0.00}", a.quantity),
                rate = a.rate == null ? "0.00" : string.Format("{0:0.00}", a.rate),
                value = a.value == null ? "0.00" : string.Format("{0:0.00}", a.value),
                gl = a.gl,
                sloc = a.sloc,
                grn = a.grn,
                remarks = a.remarks,
                bucket = a.bucket,
                uom = a.uom,
                batch_manual = a.batch_manual,
            }).ToList();
            return GIV;
        }


        public string Delete(int id, string cancellation_remarks, int reason_id)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var goods_receipt_id = new SqlParameter("@id", id);
                var remarks = new SqlParameter("@cancellation_remarks", cancellation_remarks == null ? "" : cancellation_remarks);
                var created_by = new SqlParameter("@created_by", create_user);
                var cancellation_reason_id = new SqlParameter("@cancellation_reason_id", reason_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec cancel_goods_receipt @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", goods_receipt_id, remarks, created_by, cancellation_reason_id).FirstOrDefault();
                return val;
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
            }
        }

    }
}
