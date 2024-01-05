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
    public class GoodsIssueService : IGoodsIssueService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericservice;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public GoodsIssueService(ScifferContext scifferContext, IGenericService genericService)
        {
            _scifferContext = scifferContext;
            _genericservice = genericService;
        }
        public string Add(GOODS_ISSUE_VM item)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("goods_issue_detail_id", typeof(int));
                dt1.Columns.Add("document_detail_id", typeof(int));
                dt1.Columns.Add("item_id", typeof(int));
                dt1.Columns.Add("uom_id", typeof(int));
                dt1.Columns.Add("sloc_id", typeof(int));
                dt1.Columns.Add("bucket_id", typeof(int));
                dt1.Columns.Add("issue_quantity", typeof(double));
                dt1.Columns.Add("rate", typeof(double));
                dt1.Columns.Add("value", typeof(double));
                dt1.Columns.Add("reason_id", typeof(int));
                dt1.Columns.Add("machine_id", typeof(int));
                if (item.item_id1 != null)
                {
                    for (var i = 0; i < item.item_id1.Count; i++)
                    {

                        int result = _genericservice.GetCheck_Inventory(Convert.ToInt32(item.item_id1[i]), Convert.ToInt32(item.plant_id), Convert.ToInt32(item.sloc_id1[i]), item.bucket_id1[i], Convert.ToDecimal(item.issue_quantity1[i]));
                        if (result == 0)
                        {
                            return "Stock is Not Available";
                        }

                        dt1.Rows.Add(-1,
                            item.document_detail_id1[i],
                            item.item_id1[i],
                            item.uom_id1[i],
                            item.sloc_id1[i],
                            item.bucket_id1[i],
                            item.issue_quantity1[i],
                            item.rate1[i],
                            item.value1[i],
                            item.reason_id[i],
                            item.machine_id[i]);
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_std_prod_issue_detail";
                t1.Value = dt1;

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("batch_item_id", typeof(int));
                dt2.Columns.Add("batch_batch_id", typeof(int));
                dt2.Columns.Add("batch_issue_quantity", typeof(float));
                dt2.Columns.Add("batch_number", typeof(string));
                if (item.batch_item_id != null)
                {
                    for (var i = 0; i < item.batch_item_id.Count; i++)
                    {
                        dt2.Rows.Add(item.batch_item_id[i],
                            item.batch_batch_id[i],
                            item.batch_issue_quantity[i],
                            item.batch_number[i]);
                    }
                }
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_std_batch_issue_detail";
                t2.Value = dt2;

                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var goods_issue_id = new SqlParameter("@goods_issue_id", item.goods_issue_id == null ? -1 : item.goods_issue_id);
                var document_numbring_id = new SqlParameter("@document_numbring_id", item.document_numbring_id);
                var goods_issue_number = new SqlParameter("@goods_issue_number", item.goods_issue_number == null ? "" : item.goods_issue_number);
                var posting_date = new SqlParameter("@posting_date", item.posting_date);
                var document_date = new SqlParameter("@document_date", item.document_date);
                var plant_id = new SqlParameter("@plant_id", item.plant_id);
                var remarks = new SqlParameter("@remarks", item.remarks == null ? "" : item.remarks);
                var is_active = new SqlParameter("@is_active", 1);
                var ref1 = new SqlParameter("@ref1", item.ref1 == null ? "" : item.ref1);
                var user = new SqlParameter("@create_user", create_user);
                var deleteids = new SqlParameter("@deleteids", item.deleteids == null ? "" : item.deleteids);
                var document_code = new SqlParameter("@document_code", item.document_code == null ? "" : item.document_code);
                var document_id = new SqlParameter("@document_id", item.document_id == null ? 0 : item.document_id);
                if (item.FileUpload != null)
                {
                    item.attachment = _genericservice.GetFilePath("ITEM", item.FileUpload);
                }
                else
                {
                    item.attachment = "No File";
                }
                var attachment = new SqlParameter("@attachment", item.attachment);
                var val = _scifferContext.Database.SqlQuery<string>("exec Save_GoodsIssue @goods_issue_id,@goods_issue_number,@posting_date, @document_date, @plant_id, @remarks, @attachment, @is_active, @ref1, @document_numbring_id, @create_user, @deleteids, @document_code, @document_id,@t1,@t2",
                   goods_issue_id, goods_issue_number, posting_date, document_date, plant_id, remarks, attachment, is_active, ref1, document_numbring_id,
                   user, deleteids, document_code, document_id, t1, t2).FirstOrDefault();
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

        public bool Delete(int id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[GOODS_ISSUE] set [IS_ACTIVE] = 0 where GOODS_ISSUE_ID = " + id);
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

        public GOODS_ISSUE_VM Get(int id)
        {
            goods_issue GI = _scifferContext.goods_issue.FirstOrDefault(c => c.goods_issue_id == id);
            Mapper.CreateMap<goods_issue, GOODS_ISSUE_VM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            GOODS_ISSUE_VM GIV = Mapper.Map<goods_issue, GOODS_ISSUE_VM>(GI);
            GIV.goods_issue_detail = GIV.goods_issue_detail.Where(c => c.is_active == true).ToList();
            //GIV.goods_issue_detail
            return GIV;
        }
        public GOODS_ISSUE_VM GetDetails(int id)
        {
            goods_issue GI = _scifferContext.goods_issue.FirstOrDefault(c => c.goods_issue_id == id);
            Mapper.CreateMap<goods_issue, GOODS_ISSUE_VM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            GOODS_ISSUE_VM GIV = Mapper.Map<goods_issue, GOODS_ISSUE_VM>(GI);
            GIV.goods_issue_detail_nontagitems = GIV.goods_issue_detail.Where(c => c.is_active == true).Select(a => new {
                item_code = a.REF_ITEM.ITEM_CODE,
                item_desc = a.REF_ITEM.ITEM_NAME,
                //reason = a.REF_REASON_DETERMINATION.reason_determination_code,
                //batch_number = a.inv_item_batch.batch_number,
                quantity = a.quantity,
                rate = a.rate,
                value = a.value,
                bucket = a.ref_bucket.bucket_name,
                sloc = a.REF_STORAGE_LOCATION.description,
                //mrn_qty = a.material_requision_note_detail.required_qty,
                //machine_code = a.ref_machine.machine_code,
                // batch_bal_qty = a.inv_item_batch_detail.qty
            }).ToList().Select(a => new
            goods_issue_detail_nontagitems
            {
                item_code = a.item_code,
                item_desc = a.item_desc,
                //reason =a.reason,
                //batch_number = a.batch_number,
                quantity = a.quantity == null ? "0.00" : string.Format("{0:0.00}", a.quantity),
                rate = a.rate == null ? "0.00" : string.Format("{0:0.00}", a.rate),
                value = a.value == null ? "0.00" : string.Format("{0:0.00}", a.value),
                bucket = a.bucket,
                sloc = a.sloc,
                //mrn_qty = a.mrn_qty == null ? "0.00" : string.Format("{0:0.00}", a.mrn_qty),
                //machine_code = a.machine_code,
                //batch_bal_qty = a.batch_bal_qty == null ? "0.00" : string.Format("{0:0.00}", a.batch_bal_qty)
            }).ToList();
            return GIV;
        }

        public List<GOODS_ISSUE_VM> GetAll()
        {
            Mapper.CreateMap<goods_issue, GOODS_ISSUE_VM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            return _scifferContext.goods_issue.Project().To<GOODS_ISSUE_VM>().Where(c => c.is_active == true).ToList();
        }

        public List<GOODS_ISSUE_VM> getall()
        {
            var ent = new SqlParameter("@entity", "getall");
            var val = _scifferContext.Database.SqlQuery<GOODS_ISSUE_VM>(
                "exec get_goods_issue_receipt @entity", ent).ToList();
            return val;
            //var query = (from goods in _scifferContext.goods_issue.Where(x => x.is_active == true).OrderByDescending(x => x.goods_issue_id)
            //             join category in _scifferContext.ref_document_numbring on goods.document_numbring_id equals category.document_numbring_id into category1
            //             from category2 in category1.DefaultIfEmpty()
            //             join plantname in _scifferContext.REF_PLANT on goods.plant_id equals plantname.PLANT_ID into plantname1
            //             from plantname2 in plantname1.DefaultIfEmpty()
            //             join mrn in _scifferContext.material_requision_note on goods.document_id equals mrn.material_requision_note_id into mrn1
            //             from mrn2 in mrn1.DefaultIfEmpty()
            //             join pmo in _scifferContext.ref_plan_maintenance_order on goods.document_id equals pmo.maintenance_order_id into pmo1
            //             from pmo2 in pmo1.DefaultIfEmpty()

            //             select new GOODS_ISSUE_VM()
            //             {
            //                 goods_issue_id = goods.goods_issue_id,
            //                 document_numbring_id = category2 == null ? 0 : category2.document_numbring_id,
            //                 goods_issue_number = goods.goods_issue_number,
            //                 posting_date = goods.posting_date,
            //                 document_date = goods.document_date,
            //                 plant_id = goods.plant_id,
            //                 plant_name = plantname2.PLANT_NAME,
            //                 remarks = goods.remarks,
            //                 attachment = goods.attachment,
            //                 ref1 = goods.ref1,
            //                 category_name = category2.category,
            //                 document_code = goods.document_code,
            //                 document_id = goods.document_id,
            //                 mrn_number = goods.document_code == "MRN" ? mrn2 == null ? string.Empty : mrn2.number : goods.document_code == "PMO" ? pmo2 == null ? string.Empty : pmo2.order_no : string.Empty,

            //             }).OrderByDescending(x => x.goods_issue_id).ToList();
            //return query;
        }

        public List<MRNDetailListVM> GetNonTagMRNProductForGoodsIssue(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getnontagmrnforgoodsissue");
            var val = _scifferContext.Database.SqlQuery<MRNDetailListVM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<MRNDetailListVM> GetTagMRNProductForGoodsIssue(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "gettagmrnitemforgoodsissue");
            var val = _scifferContext.Database.SqlQuery<MRNDetailListVM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<MRNDetailListVM> GetTagItemsForGoodsIssue(int sloc_id, int plant_id, int item_id)
        {
            var slocid = new SqlParameter("@sloc_id", sloc_id);
            var plantid = new SqlParameter("@plant_id", plant_id);
            var itemid = new SqlParameter("@item_id", item_id);
            var entity = new SqlParameter("@entity", "gettagitemforgoodsissue");
            var val = _scifferContext.Database.SqlQuery<MRNDetailListVM>(
            "exec GetBatchForGoodsIssue @sloc_id,@plant_id,@item_id,@entity", slocid, plantid, itemid, entity).ToList();
            return val;
        }
        public List<MRNDetailListVM> GetNonTagItemsForGoodsIssue(int sloc_id, int plant_id, int item_id)
        {
            var slocid = new SqlParameter("@sloc_id", sloc_id);
            var plantid = new SqlParameter("@plant_id", plant_id);
            var itemid = new SqlParameter("@item_id", item_id);
            var entity = new SqlParameter("@entity", "getnontagitemforgoodsissue");
            var val = _scifferContext.Database.SqlQuery<MRNDetailListVM>(
            "exec GetBatchForGoodsIssue @sloc_id,@plant_id,@item_id,@entity", slocid, plantid, itemid, entity).ToList();
            return val;
        }
        public bool Update(GOODS_ISSUE_VM goodsIssue)
        {
            throw new NotImplementedException();
        }
        public string AddExcel(List<GOODS_ISSUE_VM> header, List<goods_issue_detail_VM> detail, List<goods_issue_batch_VM> batch)
        {
            try
            {
                var val = "";
                foreach (var item in header)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("goods_issue_detail_id", typeof(int));
                    dt1.Columns.Add("document_detail_id", typeof(int));
                    dt1.Columns.Add("item_id", typeof(int));
                    dt1.Columns.Add("uom_id", typeof(int));
                    dt1.Columns.Add("sloc_id", typeof(int));
                    dt1.Columns.Add("bucket_id", typeof(int));
                    dt1.Columns.Add("issue_quantity", typeof(double));
                    dt1.Columns.Add("rate", typeof(double));
                    dt1.Columns.Add("value", typeof(double));
                    dt1.Columns.Add("reason_id", typeof(int));

                    if (detail != null)
                    {
                        foreach (var gls in detail)
                        {
                            if (item.header_reference == gls.header_referenec)
                            {
                                dt1.Rows.Add(gls.goods_issue_detail_id == 0 ? -1 : gls.goods_issue_detail_id,
                                   gls.document_detail_id, gls.item_id, gls.uom_id, gls.sloc_id, gls.bucket_id, gls.quantity,
                                   gls.rate, gls.value, gls.reason_id);

                            }
                        }
                    }

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("batch_item_id", typeof(int));
                    dt2.Columns.Add("batch_batch_id", typeof(int));
                    dt2.Columns.Add("batch_issue_quantity", typeof(float));
                    dt2.Columns.Add("batch_number", typeof(string));
                    if (batch != null)
                    {
                        foreach (var bat in batch)
                        {
                            dt2.Rows.Add(bat.item_id, bat.batch_id, bat.quantity, bat.batch_number);
                        }
                    }

                    int create_user = 0;
                    var goods_issue_id = new SqlParameter("@goods_issue_id", item.goods_issue_id == null ? -1 : item.goods_issue_id);
                    var document_numbring_id = new SqlParameter("@document_numbring_id", item.document_numbring_id);
                    var goods_issue_number = new SqlParameter("@goods_issue_number", item.goods_issue_number == null ? "" : item.goods_issue_number);
                    var posting_date = new SqlParameter("@posting_date", item.posting_date);
                    var document_date = new SqlParameter("@document_date", item.document_date);
                    var plant_id = new SqlParameter("@plant_id", item.plant_id);
                    var remarks = new SqlParameter("@remarks", item.remarks == null ? "" : item.remarks);
                    var is_active = new SqlParameter("@is_active", 1);
                    var ref1 = new SqlParameter("@ref1", item.ref1 == null ? "" : item.ref1);
                    var user = new SqlParameter("@create_user", create_user);
                    var deleteids = new SqlParameter("@deleteids", item.deleteids == null ? "" : item.deleteids);
                    var document_code = new SqlParameter("@document_code", item.document_code == null ? "" : item.document_code);
                    var document_id = new SqlParameter("@document_id", item.document_id == null ? 0 : item.document_id);

                    if (item.FileUpload != null)
                    {
                        item.attachment = _genericservice.GetFilePath("ITEM", item.FileUpload);
                    }
                    else
                    {
                        item.attachment = "No File";
                    }
                    var attachment = new SqlParameter("@attachment", item.attachment);

                    var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                    t1.TypeName = "dbo.temp_std_prod_issue_detail";
                    t1.Value = dt1;

                    var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                    t2.TypeName = "dbo.temp_std_batch_issue_detail";
                    t2.Value = dt2;

                    val = _scifferContext.Database.SqlQuery<string>("exec Save_GoodsIssue @goods_issue_id,@goods_issue_number,@posting_date, @document_date, @plant_id, @remarks, @attachment, @is_active, @ref1, @document_numbring_id, @create_user, @deleteids, @document_code, @document_id,@t1,@t2",
                       goods_issue_id, goods_issue_number, posting_date, document_date, plant_id, remarks, attachment, is_active, ref1, document_numbring_id,
                       user, deleteids, document_code, document_id, t1, t2).FirstOrDefault();

                }
                if (val.Contains("Saved"))
                {
                    return "Saved";
                }
                else
                {
                    return "Error";
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

        public string GetChilItem(string entity, int item_id)
        {
            var ent = new SqlParameter("@entity", entity);
            var itemid = new SqlParameter("@item_id", item_id);
            
            var val = _scifferContext.Database.SqlQuery<string>(
            "exec get_child_item_for_GI @entity,@item_id", ent, itemid).FirstOrDefault();
            return val;

        }
    }
}
