using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Sciffer.Erp.Service.Implementation
{
  public  class PlantTransferService : IPlantTransferService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public PlantTransferService(ScifferContext ScifferContext, IGenericService GenericService)
        {
            _scifferContext = ScifferContext;
            _genericService = GenericService;
        }

        public string Add(pla_transferVM plantransfer)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("pla_transfer_detail_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("uom_id", typeof(int));                
                dt.Columns.Add("sloc_id ", typeof(int));
                dt.Columns.Add("bucket_id", typeof(int));              
                dt.Columns.Add("rate", typeof(double));
                dt.Columns.Add("issue_quantity", typeof(double));
                dt.Columns.Add("value", typeof(double));
                if (plantransfer.item_id != null)
                {
                    for (var i = 0; i < plantransfer.item_id.Count; i++)
                    {
                        int result = _genericService.GetCheck_Inventory(Convert.ToInt32(plantransfer.item_id[i]),Convert.ToInt32(plantransfer.plant_id),Convert.ToInt32(plantransfer.sloc_id[i]),Convert.ToInt32(plantransfer.bucket_id[i]), Convert.ToDecimal(plantransfer.issue_quantity[i]));
                        if (result == 0)
                        {
                            return "Stock is Not Available";
                        }


                        dt.Rows.Add(int.Parse(plantransfer.pla_transfer_detail_id[i]), int.Parse(plantransfer.item_id[i]),
                            int.Parse(plantransfer.uom_id[i]), int.Parse(plantransfer.sloc_id[i]), int.Parse(plantransfer.bucket_id[i]),
                            plantransfer.rate[i] == "" ? 0 : double.Parse(plantransfer.rate[i]),
                            plantransfer.issue_quantity[i] == "" ? 0 : double.Parse(plantransfer.issue_quantity[i]),
                            plantransfer.value[i] == "" ? 0 : double.Parse(plantransfer.value[i]));
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_within_pla_transfer_detail";
                t1.Value = dt;


                DataTable batch_dt = new DataTable();
                batch_dt.Columns.Add("pla_transfer_batch_id", typeof(int));
                batch_dt.Columns.Add("batch_item_id", typeof(int));
                batch_dt.Columns.Add("batch_id", typeof(int));
                batch_dt.Columns.Add("batch_number", typeof(string));
                batch_dt.Columns.Add("issue_batch_quantity", typeof(double));
               
                if (plantransfer.batch_item_id != null)
                    {
                    for (var j = 0; j < plantransfer.batch_item_id.Count; j++)
                    {
                        var pla_batch_id = int.Parse(plantransfer.pla_transfer_batch_id[j]);
                        var _item = plantransfer.batch_item_id[j] == "" ? 0 : int.Parse(plantransfer.batch_item_id[j]);
                        var batch = plantransfer.batch_id[j] == "" ? 0 : int.Parse(plantransfer.batch_id[j]);
                        var batch_no = plantransfer.batch_number[j];
                        var qty = plantransfer.issue_batch_quantity[j] == "" ? 0 : double.Parse(plantransfer.issue_batch_quantity[j]);
                        batch_dt.Rows.Add(pla_batch_id,_item, batch, batch_no, qty);
                    }
                }

                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_pla_transfer_batch";
                t2.Value = batch_dt;
                var category_id = new SqlParameter("@category_id", plantransfer.@category_id);
                var pla_transfer_id = new SqlParameter("@pla_transfer_id", plantransfer.pla_transfer_id == 0 ? -1 : plantransfer.pla_transfer_id);
                var pla_transfer_number = new SqlParameter("@pla_transfer_number", plantransfer.pla_transfer_number==null?"": plantransfer.pla_transfer_number);
                var pla_posting_date = new SqlParameter("@pla_posting_date", plantransfer.pla_posting_date);
                var pla_document_date = new SqlParameter("@pla_document_date", plantransfer.pla_document_date);
                var plant_id = new SqlParameter("@plant_id", plantransfer.plant_id);
                var pla_send_sloc = new SqlParameter("@pla_send_sloc", plantransfer.pla_send_sloc);
                var pla_receive_sloc = new SqlParameter("@pla_receive_sloc", plantransfer.pla_receive_sloc);
                var pla_send_bucket = new SqlParameter("@pla_send_bucket", plantransfer.pla_send_bucket);
                var pla_receive_bucket = new SqlParameter("@pla_receive_bucket", plantransfer.pla_receive_bucket);
                var pla_attachment = new SqlParameter("@pla_attachment", plantransfer.pla_attachment == null ? string.Empty : plantransfer.pla_attachment);                
                var is_active = new SqlParameter("@is_active", 1);
               // int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var created_by = new SqlParameter("@created_by", 1);
                var remarks_on_document = new SqlParameter("@remarks_on_document", plantransfer.remarks_on_document == null ? string.Empty : plantransfer.remarks_on_document);
               
                

                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec Save_PlantTransfer @pla_transfer_id ,@category_id ,@pla_transfer_number ,@pla_posting_date,@pla_document_date ,@plant_id ,@pla_send_sloc ,@pla_receive_sloc ,@pla_send_bucket ,@pla_receive_bucket ,@pla_attachment ,@is_active ,@remarks_on_document,@created_by,@t1,@t2",
                    pla_transfer_id, category_id, pla_transfer_number, pla_posting_date, pla_document_date, plant_id, pla_send_sloc, pla_receive_sloc, pla_send_bucket,
                    pla_receive_bucket, pla_attachment, is_active, remarks_on_document, created_by, t1, t2).FirstOrDefault();

                return val;
                //if (val.Contains("Saved"))
                //{
                //    var sp = val.Split('~')[1];
                //    return sp;
                //}
                //else
                //{
                //    return "Error";
                //}  
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
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
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[pla_transfer] set [pla_transfer_is_active] = 0 where pla_transfer_id = " + id);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

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

        public  pla_transferVM Get(int id)
        {
            try
            {
                pla_transfer pla = _scifferContext.pla_transfer.FirstOrDefault(c => c.pla_transfer_id == id);
                Mapper.CreateMap<pla_transfer, pla_transferVM>();
                pla_transferVM plvm = Mapper.Map<pla_transfer, pla_transferVM>(pla);
                plvm.pla_transfer_detail = plvm.pla_transfer_detail.Where(c => c.is_active == true).ToList();
              
                return plvm;
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        public List<pla_transferVM> GetAll()
        {
            try
            {
                Mapper.CreateMap<pla_transfer, pla_transferVM>().ForMember(dest => dest.FileUpload, opt => opt.Ignore());
                return _scifferContext.pla_transfer.Project().To<pla_transferVM>().Where(a=>a.is_active == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public List<PlantTransferVM> getall()
        {
            var query = (from grn in _scifferContext.pla_transfer
                         join p in _scifferContext.REF_PLANT on grn.plant_id equals p.PLANT_ID
                         join c in _scifferContext.ref_document_numbring on grn.category_id equals c.document_numbring_id
                         join s in _scifferContext.REF_STORAGE_LOCATION on grn.pla_send_sloc equals s.storage_location_id
                         join r in _scifferContext.REF_STORAGE_LOCATION on grn.pla_receive_sloc equals r.storage_location_id
                         join g in _scifferContext.ref_bucket on grn.pla_send_bucket equals g.bucket_id
                         join e in _scifferContext.ref_bucket on grn.pla_receive_bucket equals e.bucket_id
                         select new PlantTransferVM()
                         {
                             pla_transfer_id = grn.pla_transfer_id,
                             pla_transfer_number = grn.pla_transfer_number,
                             pla_posting_date = grn.pla_posting_date,
                             pla_document_date = grn.pla_document_date,
                             Plant = p.PLANT_NAME,
                             pla_send_sloc = s.storage_location_name,
                             pla_send_bucket = g.bucket_name,
                             pla_receive_sloc = r.storage_location_name,
                             pla_receive_bucket = e.bucket_name,
                             category = c.category,
                            
                             pla_attachment = grn.pla_attachment,
                             remarks_on_document = grn.remarks_on_document

        }).ToList();
            return query;
        }

        public List<GetTagForPlaTransfer> gettagitemforplanttransfer(int item_id, int plant_id, int sloc_id, int bucket_id)
        {
            var item = new SqlParameter("@item_id", item_id);
            var plant = new SqlParameter("@plant_id", plant_id);
            var sloc = new SqlParameter("@sloc_id", sloc_id);
            var bucket = new SqlParameter("@bucket_id", bucket_id);
            var entity = new SqlParameter("@entity", "gettagitemforplanttransfer");
            var val = _scifferContext.Database.SqlQuery<GetTagForPlaTransfer>(
            "exec GetTagForPlaTransfer @item_id,@plant_id,@sloc_id,@bucket_id,@entity", item, plant, sloc,bucket,entity).ToList();
            return val;
        }
        public List<GetTagForPlaTransfer> getnontagitemforplanttransfer(int item_id, int plant_id, int sloc_id, int bucket_id)
        {
            var item = new SqlParameter("@item_id", item_id);
            var plant = new SqlParameter("@plant_id", plant_id);
            var sloc = new SqlParameter("@sloc_id", sloc_id);
            var bucket = new SqlParameter("@bucket_id", bucket_id);
            var entity = new SqlParameter("@entity", "getnontagitemforplanttransfer");
            var val = _scifferContext.Database.SqlQuery<GetTagForPlaTransfer>(
            "exec GetTagForPlaTransfer @item_id,@plant_id,@sloc_id,@bucket_id,@entity", item, plant, sloc, bucket, entity).ToList();
            return val;
        }
        public pla_transferVM GetDetails(int id)
        {
            pla_transfer plan = _scifferContext.pla_transfer.FirstOrDefault(c => c.pla_transfer_id == id);
            Mapper.CreateMap<pla_transfer, pla_transferVM>();
            pla_transferVM plan_vm = Mapper.Map<pla_transfer, pla_transferVM>(plan);
            plan_vm.inter_pla_transfer_detail_vm = plan_vm.pla_transfer_detail.Where(c => c.is_active == true).Select(a => new
            {
                pla_transfer_detail_id = a.pla_transfer_detail_id,
                item_id = a.REF_ITEM.ITEM_ID,
                item_code = a.REF_ITEM.ITEM_CODE,
                item_desc = a.REF_ITEM.ITEM_NAME,
                uom_name = a.REF_UOM.UOM_NAME,
                sloc_name = a.REF_STORAGE_LOCATION.storage_location_name,
                bucket_name = a.ref_bucket.bucket_name,
                rate = a.rate,
                issue_quantity = a.issue_quantity,
                value = a.value
            }).ToList().Select(pla_detail => new
            inter_pla_transfer_detail_vm
            {
                pla_transfer_detail_id = pla_detail.pla_transfer_detail_id,
                item_id = pla_detail.item_id,
                item_code = pla_detail.item_code,
                item_desc = pla_detail.item_desc,
                uom_name = pla_detail.uom_name,
                sloc_name = pla_detail.sloc_name,
                bucket_name = pla_detail.bucket_name,
                rate = pla_detail.rate == null ? "0.00" : string.Format("{0:0.00}", pla_detail.rate),
                issue_quantity = pla_detail.issue_quantity == null ? "0.00" : string.Format("{0:0.00}", pla_detail.issue_quantity),
                value = pla_detail.value == null ? "0.00" : string.Format("{0:0.00}", pla_detail.value)
            }).ToList();

            return plan_vm;
        }

    }
}
