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
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class ProdOrderIssueService : IProdOrderIssueService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericservice;
        public ProdOrderIssueService(ScifferContext scifferContext,IGenericService genericservice)
        {
            _scifferContext = scifferContext;
            _genericservice = genericservice;
        }
        public string Add(ProdIssueVM item)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("ntag_prod_issue_detail_id", typeof(int));
                dt1.Columns.Add("ntag_prod_order_detail_id", typeof(int));
                dt1.Columns.Add("ntag_item_batch_detail_id", typeof(int));
                dt1.Columns.Add("ntag_in_item_id", typeof(int));
                dt1.Columns.Add("ntag_batch_id", typeof(int));
                dt1.Columns.Add("ntag_quantity", typeof(double));
                dt1.Columns.Add("ntag_rate", typeof(double));
                dt1.Columns.Add("ntag_value", typeof(double));
                if(item.ntag_in_item_id != null)
                {
                    for (var i = 0; i < item.ntag_in_item_id.Count; i++)
                    {
                        dt1.Rows.Add(item.ntag_prod_issue_detail_id[i] == "0" ? -1 : int.Parse(item.ntag_prod_issue_detail_id[i]),
                            int.Parse(item.ntag_prod_order_detail_id[i]), 
                            int.Parse(item.ntag_item_batch_detail_id[i]),
                            int.Parse(item.ntag_in_item_id[i]), 
                            int.Parse(item.ntag_batch_id[i]), 
                            item.ntag_quantity[i] == "" ? 0 : Double.Parse(item.ntag_quantity[i]),
                            item.ntag_rate[i] == "" ? 0 : Double.Parse(item.ntag_rate[i]), 
                            item.ntag_value[i] == "" ? 0 : Double.Parse(item.ntag_value[i]));
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_prod_issue_detail";
                t1.Value = dt1;

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("tag_prod_issue_detail_tag_id", typeof(int));
                dt2.Columns.Add("tag_prod_order_detail_id", typeof(int));
                dt2.Columns.Add("tag_item_batch_detail_id", typeof(int));
                dt2.Columns.Add("tag_in_item_id", typeof(int));
                dt2.Columns.Add("tag_tag_id", typeof(int));
                dt2.Columns.Add("tag_batch_id", typeof(int));
                dt2.Columns.Add("tag_quantity", typeof(double));
                dt2.Columns.Add("tag_rate", typeof(double));
                dt2.Columns.Add("tag_value", typeof(double));
                if (item.tag_in_item_id != null)
                {
                    for (var i = 0; i < item.tag_in_item_id.Count; i++)
                    {
                        dt2.Rows.Add(item.tag_prod_issue_detail_tag_id[i] == "0" ? -1 : int.Parse(item.tag_prod_issue_detail_tag_id[i]),
                            int.Parse(item.tag_prod_order_detail_id[i]),
                            int.Parse(item.tag_item_batch_detail_id[i]),
                            int.Parse(item.tag_in_item_id[i]),
                            item.tag_tag_id[i] == "null" ? 0 : int.Parse(item.tag_tag_id[i]),
                            int.Parse(item.tag_batch_id[i]),
                            item.tag_quantity[i] == "0" ? 0 : Double.Parse(item.tag_quantity[i]),
                            item.tag_rate[i] == "0" ? 0 : Double.Parse(item.tag_rate[i]),
                            item.tag_value[i] == "0.00" ? 0 : Double.Parse(item.tag_value[i]));
                    }
                }

                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_prod_issue_detail_tag";
                t2.Value = dt2;

                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var prod_issue_id = new SqlParameter("@prod_issue_id", item.prod_issue_id == null ? -1 : item.prod_issue_id);
                var category_id = new SqlParameter("@category_id", item.category_id);
                var prod_issue_number = new SqlParameter("@prod_issue_number", item.prod_issue_number == null ? "" : item.prod_issue_number);
                var posting_date = new SqlParameter("@posting_date", item.posting_date);
                var document_date = new SqlParameter("@document_date", item.document_date);
                var plant_id = new SqlParameter("@plant_id", item.plant_id);
                var remarks = new SqlParameter("@remarks", item.remarks == null ? "" : item.remarks);
                var is_active = new SqlParameter("@is_active", 1);
                var prod_order_id = new SqlParameter("@prod_order_id", item.prod_order_id);
                var user = new SqlParameter("@create_user", create_user);
                var deleteids = new SqlParameter("@deleteids", item.deleteids == null ? "" : item.deleteids);

                if (item.FileUpload != null)
                {
                    item.attachment = _genericservice.GetFilePath("ITEM", item.FileUpload);
                }
                else
                {
                    item.attachment = "No File";
                }
                var attachment = new SqlParameter("@attachment", item.attachment);
                var val = _scifferContext.Database.SqlQuery<string>("exec Save_ProductionOrderIssue @prod_issue_id,@category_id,@prod_issue_number,@posting_date, @document_date, @plant_id, @remarks, @attachment, @is_active, @prod_order_id, @create_user,@deleteids,@t1,@t2",
                   prod_issue_id, category_id, prod_issue_number, posting_date, document_date, plant_id, remarks, attachment, is_active, prod_order_id,
                   user, deleteids , t1 ,t2).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    var sp = val.Split('~')[1];
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
            throw new NotImplementedException();
        }

        public ProdIssueVM Get(int? id)
        {
            prod_issue GI = _scifferContext.prod_issue.FirstOrDefault(c => c.prod_issue_id == id);
            Mapper.CreateMap<prod_issue, ProdIssueVM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            ProdIssueVM GIV = Mapper.Map<prod_issue, ProdIssueVM>(GI);
            GIV.prod_issue_detail = GIV.prod_issue_detail.Where(c => c.is_active == true).ToList();
            GIV.prod_issue_detail_tag = GIV.prod_issue_detail_tag.ToList();
            var inv_item_batchlist = GIV.prod_issue_detail.FirstOrDefault();
            int inv_item_batch = inv_item_batchlist == null ? 0 : inv_item_batchlist.batch_id;
            //var batch_number;            
            return GIV;
        }

        public List<ProdIssueVM> GetAll()
        {
            var ent = new SqlParameter("@entity", "getall");
            var val = _scifferContext.Database.SqlQuery<ProdIssueVM>(
                "exec get_production_issue @entity", ent).ToList();
            //var query = (from ed in _scifferContext.prod_issue.Where(x => x.is_active == true)
            //             join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id
            //             join plant in _scifferContext.REF_PLANT on ed.plant_id equals plant.PLANT_ID
            //             join prod in _scifferContext.mfg_prod_order on ed.prod_order_id equals prod.prod_order_id
            //             select new ProdIssueVM {
            //                 prod_issue_id = ed.prod_issue_id,
            //                 prod_issue_number = ed.prod_issue_number,
            //                 category_id  = ed.category_id,
            //                 category_name = cat.category,
            //                 posting_date = ed.posting_date,
            //                 document_date = ed.document_date,
            //                 plant_id = ed.plant_id,
            //                 plant_name = plant.PLANT_CODE + " - " + plant.PLANT_NAME,
            //                 remarks = ed.remarks,
            //                 prod_order_id = ed.prod_order_id,
            //                 prod_order_no = prod.prod_order_no + "" + prod.prod_order_date,
            //             }).OrderByDescending(a => a.prod_issue_id).ToList();
            return val;
        }

        public bool Update(ProdIssueVM vm)
        {
            throw new NotImplementedException();
        }
        public List<ProOrderDetailVM> GetTagProductionOrderIssue(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "gettagpoforproductionissue");
            var val = _scifferContext.Database.SqlQuery<ProOrderDetailVM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<ProOrderDetailVM> GetNonTagProductionOrderIssue(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getnontagpoforproductionissue");
            var val = _scifferContext.Database.SqlQuery<ProOrderDetailVM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public ProdIssueVM GetDetails(int? id)
        {
            prod_issue GI = _scifferContext.prod_issue.FirstOrDefault(c => c.prod_issue_id == id);
            Mapper.CreateMap<prod_issue, ProdIssueVM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            ProdIssueVM GIV = Mapper.Map<prod_issue, ProdIssueVM>(GI);
            GIV.Prod_order_issue_detail_tagitems = GIV.prod_issue_detail_tag.Where(c => c.is_active == true).Select(a => new {
                item_code = a.REF_ITEM.ITEM_CODE,
                item_desc = a.REF_ITEM.ITEM_NAME,
                batch_number = a.inv_item_batch.batch_number,
                tag_no = a.inv_item_batch_detail_tag.tag_no,
                required_qty = a.quantity,
                rate = a.rate,
                value = a.value
            }).ToList().Select(a => new
            Prod_order_issue_detail_tagitems
            {
                item_code = a.item_code,
                item_desc = a.item_desc,
                batch_number = a.batch_number,
                tag_no = a.tag_no,
                required_qty = string.Format("{0:0.00}", a.required_qty),
                rate = string.Format("{0:0.00}", a.rate),
                value = string.Format("{0:0.00}", a.value)
            }).ToList();
            GIV.Prod_order_issue_detail_nontagitems = GIV.prod_issue_detail_tag.Where(c => c.is_active == true).Select(a => new {
                item_code = a.REF_ITEM.ITEM_CODE,
                item_desc = a.REF_ITEM.ITEM_NAME,
                batch_number = a.inv_item_batch.batch_number,
                required_qty = a.quantity,
                rate = a.rate,
                value = a.value
            }).ToList().Select(a => new
            Prod_order_issue_detail_nontagitems
            {
                item_code = a.item_code,
                item_desc = a.item_desc,
                batch_number = a.batch_number,
                required_qty = string.Format("{0:0.00}", a.required_qty),
                rate = string.Format("{0:0.00}", a.rate),
                value = string.Format("{0:0.00}", a.value)
            }).ToList();
            return GIV;
        }
        public bom_details getClumpsumBatchQuantity(string sloc_id, string plant_id, string item_id, string bucket_id, string entity_id, int? reason_id)
        {
            var slocid = new SqlParameter("@sloc_id", sloc_id);
            var plantid = new SqlParameter("@plant_id", plant_id);
            var itemid = new SqlParameter("@item_id", item_id);
            var bucketid = new SqlParameter("@bucket_id", bucket_id);
            var entity = new SqlParameter("@entity", entity_id);
            var reasonid = new SqlParameter("@reason_id", reason_id==null ? 0 : reason_id);
            var val = _scifferContext.Database.SqlQuery<SubProdOrderDetailVM>(
            "exec GetBatchList @sloc_id,@plant_id,@item_id,@bucket_id,@entity,@reason_id", slocid, plantid, itemid, bucketid, entity,reasonid).ToList();
            var list = val.Select(a => new SubProdOrderDetailVM
            {
                sub_prod_order_detail_id = a.sub_prod_order_detail_id,
                rowIndex = a.rowIndex,
                item_id = a.item_id,
                item_name = a.item_name,
                batch_id = a.batch_id,
                batch_number = a.batch_number,
                sloc_id = a.sloc_id,
                sloc_name = a.sloc_name,
                bucket_id = a.bucket_id,
                bucket_name = a.bucket_name,
                gl_id = a.gl_id,
                gl_name = a.gl_name,
                sub_po_quantityStr = string.Format("{0:0.00}", a.sub_po_quantity),
                rate = a.rate,
                value = a.value,
                sub_prod_order_id = a.sub_prod_order_id,
                batch_detail_id = a.batch_detail_id,
                uom_id = a.uom_id,
                uom_name = a.uom_name,
                batch_quantityStr = string.Format("{0:0.00}", a.batch_quantity),
                emtpy1 = a.emtpy1,
                emtpy2 = a.emtpy2,
                item_batch_detail_id = a.item_batch_detail_id,
                tag_id = a.tag_id,
                tag_no = a.tag_no,
                document_detail_id = a.document_detail_id,
                document_id = a.document_id,
                rack_no=a.rack_no,
            }).ToList();
            val = list;
            bom_details detail = new bom_details();
            detail.SubProd = list;
            return detail;
        }

        public bom_details GetItemForProdGoodsIssue(string sloc_id, string plant_id, string item_id, string bucket_id, int? reason_id)
        {
            var slocid = new SqlParameter("@sloc_id", sloc_id);
            var plantid = new SqlParameter("@plant_id", plant_id);
            var itemid = new SqlParameter("@item_id", item_id);
            var bucketid = new SqlParameter("@bucket_id", bucket_id);
            var reasonid = new SqlParameter("@reason_id", reason_id == null ? 0 : reason_id);
            var entity = new SqlParameter("@entity", "getitemforprodissue");
            var val = _scifferContext.Database.SqlQuery<SubProdOrderDetailVM>(
            "exec GetBatchList @sloc_id,@plant_id,@item_id,@bucket_id,@entity,@reason_id", slocid, plantid, itemid, bucketid, entity, reasonid).ToList();
            var list = val.Select(a => new SubProdOrderDetailVM
            {
                sub_prod_order_detail_id = a.sub_prod_order_detail_id,
                rowIndex = a.rowIndex,
                item_id = a.item_id,
                item_name = a.item_name,
                batch_id = a.batch_id,
                batch_number = a.batch_number,
                sloc_id = a.sloc_id,
                sloc_name = a.sloc_name,
                bucket_id = a.bucket_id,
                bucket_name = a.bucket_name,
                gl_id = a.gl_id,
                gl_name = a.gl_name,
                sub_po_quantityStr = string.Format("{0:0.00}", a.sub_po_quantity),
                rate = a.rate,
                value = a.value,
                sub_prod_order_id = a.sub_prod_order_id,
                batch_detail_id = a.batch_detail_id,
                uom_id = a.uom_id,
                uom_name = a.uom_name,
                batch_quantityStr = string.Format("{0:0.00}", a.batch_quantity),
                emtpy1 = a.emtpy1,
                emtpy2 = a.emtpy2,
                item_batch_detail_id = a.item_batch_detail_id,
                tag_id = a.tag_id,
                tag_no = a.tag_no,
                document_detail_id = a.document_detail_id,
                document_id = a.document_id,
            }).ToList();
            val = list;
            bom_details detail = new bom_details();
            detail.SubProd = list;
            return detail;
        }
    }
}
