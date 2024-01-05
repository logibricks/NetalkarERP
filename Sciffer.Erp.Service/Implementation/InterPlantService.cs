using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class InterPlantService : IInterPlantService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;

        public InterPlantService(ScifferContext ScifferContext, IGenericService GenericService)
        {
            _scifferContext = ScifferContext;
            _genericService = GenericService;
        }
        public string Add(InterPlaTransferVM main)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("tag_pla_transfer_detail_tag_id", typeof(int));
                dt.Columns.Add("tag_item_batch_detail_id", typeof(int));
                dt.Columns.Add("tag_item_id", typeof(int));
                dt.Columns.Add("tag_batch_id ", typeof(int));
                dt.Columns.Add("tag_tag_id", typeof(int));
                dt.Columns.Add("tag_quantity", typeof(double));
                dt.Columns.Add("tag_is_active", typeof(bool));
                dt.Columns.Add("tag_uom_id", typeof(int));
                dt.Columns.Add("tag_sr_no", typeof(int));
                dt.Columns.Add("tag_no", typeof(string));
                //foreach (var I in main.pla_transfer_detail_tag)
                //{
                //    dt.Rows.Add(I.pla_transfer_detail_tag_id == 0 ? -1 : I.pla_transfer_detail_tag_id, I.item_batch_detail_id, I.item_id, I.tag_id, I.batch_id, I.quantity, I.is_active);

                //}
                if (main.titem_id != null)
                {
                    for (var i = 0; i < main.pla_transfer_detail_tag_id.Count; i++)
                    {
                        int tag = -1;
                        if (main.pla_transfer_detail_tag_id != null)
                        {
                            tag = main.pla_transfer_detail_tag_id[i] == "" ? -1 : Convert.ToInt32(main.pla_transfer_detail_tag_id[i]);
                        }

                        int result = _genericService.GetCheck_Inventory(Convert.ToInt32(main.titem_id[i]), Convert.ToInt32(main.receiving_plant_id), Convert.ToInt32(main.pla_receive_sloc), Convert.ToInt32(main.pla_receive_bucket), Convert.ToDecimal(main.quantity[i]));
                        if (result == 0)
                        {
                            return "Stock is Not Available";
                        }

                        dt.Rows.Add(tag, main.titem_batch_detail_id[i] == "0" ? 0 : int.Parse(main.titem_batch_detail_id[i]), int.Parse(main.titem_id[i]), int.Parse(main.titem_batch_id[i]), int.Parse(main.tag_id[i]), double.Parse(main.quantity[i]), true, int.Parse(main.tuom_id[i]), int.Parse(main.tsr_no[i]), (main.tag_no[i]));

                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_pla_transfer_detail_tag";
                t1.Value = dt;

                DataTable cp = new DataTable();
                cp.Columns.Add("ntag_pla_transfer_detail_id", typeof(int));
                cp.Columns.Add("ntag_in_item_id", typeof(int));
                cp.Columns.Add("ntag_batch_id", typeof(int));
                cp.Columns.Add("ntag_quantity", typeof(double));
                cp.Columns.Add("ntag_item_batch_detail_id", typeof(int));
                cp.Columns.Add("ntag_sr_no", typeof(int));
                cp.Columns.Add("ntag_uom_id", typeof(int));
                
                if (main.item_id != null)
                {
                    for (var j = 0; j < main.pla_transfer_detail_id.Count; j++)
                    {
                        int bat = -1;
                        if (main.pla_transfer_detail_id != null)
                        {
                            bat = main.pla_transfer_detail_id[j] == "" ? -1 : Convert.ToInt32(main.pla_transfer_detail_id[j]);
                        }

                        int result = _genericService.GetCheck_Inventory(Convert.ToInt32(main.item_id[j]), Convert.ToInt32(main.sending_plant_id), Convert.ToInt32(main.pla_send_sloc), Convert.ToInt32(main.pla_receive_bucket), Convert.ToDecimal(main.pla_qty[j]));
                        if (result == 0)
                        {
                            return "Stock is Not Available";
                        }

                        cp.Rows.Add(bat, main.item_id[j] == "0" ? 0 : int.Parse(main.item_id[j]), int.Parse(main.item_batch_id[j]), double.Parse(main.pla_qty[j]), int.Parse(main.item_batch_detail_id[j]), int.Parse(main.sr_no[j]), int.Parse(main.uom_id[j]));

                    }
                }

                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_pla_transfer_detail";
                t2.Value = cp;
                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var pla_transfer_id = new SqlParameter("@pla_transfer_id", main.pla_transfer_id == 0 ? -1 : main.pla_transfer_id);
                var category_id = new SqlParameter("@category_id", main.@category_id);
                var pla_transfer_number = new SqlParameter("@pla_transfer_number", main.pla_transfer_number);
                var pla_posting_date = new SqlParameter("@pla_posting_date", main.pla_posting_date);
                var pla_document_date = new SqlParameter("@pla_document_date", main.pla_document_date);
                var sending_plant_id = new SqlParameter("@sending_plant_id", main.sending_plant_id);
                var receiving_plant_id = new SqlParameter("@receiving_plant_id", main.receiving_plant_id);
                var pla_send_sloc = new SqlParameter("@pla_send_sloc", main.pla_send_sloc);
                var pla_receive_sloc = new SqlParameter("@pla_receive_sloc", main.pla_receive_sloc);
                var pla_send_bucket = new SqlParameter("@pla_send_bucket", main.pla_send_bucket);
                var pla_receive_bucket = new SqlParameter("@pla_receive_bucket", main.pla_receive_bucket);
                var pla_attachment = new SqlParameter("@pla_attachment", main.pla_attachment == null ? string.Empty : main.pla_attachment);
                var is_active = new SqlParameter("@is_active", 1);
                var created_by = new SqlParameter("@created_by", createdby);
                var remarks_on_document = new SqlParameter("@remarks_on_document", main.remarks_on_document == null ? string.Empty : main.remarks_on_document);

                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec Save_IntraPlantTransfer @pla_transfer_id ,@category_id ,@pla_transfer_number ,@pla_posting_date,@pla_document_date ,@sending_plant_id, @receiving_plant_id ,@pla_send_sloc ,@pla_receive_sloc ,@pla_send_bucket ,@pla_receive_bucket ,@pla_attachment ,@is_active ,@remarks_on_document,@created_by,@t1,@t2 ",
                    pla_transfer_id, category_id, pla_transfer_number, pla_posting_date, pla_document_date, sending_plant_id, receiving_plant_id, pla_send_sloc, pla_receive_sloc, pla_send_bucket,
                    pla_receive_bucket, pla_attachment, is_active, remarks_on_document, created_by, t1, t2).FirstOrDefault();

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

        public List<InterPlaTransferVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<InterPlaTransferVM> getall()
        {
            var query = (from pt in _scifferContext.intra_pla_transfer
                         join tdd in _scifferContext.intra_pla_transfer_detail on pt.pla_transfer_id equals tdd.pla_transfer_id
                         join item in _scifferContext.REF_ITEM on tdd.item_id equals item.ITEM_ID
                         join rp in _scifferContext.REF_PLANT on pt.receiving_plant_id equals rp.PLANT_ID
                         join sp in _scifferContext.REF_PLANT on pt.sending_plant_id equals sp.PLANT_ID
                         join c in _scifferContext.ref_document_numbring on pt.category_id equals c.document_numbring_id
                         join ss in _scifferContext.REF_STORAGE_LOCATION on pt.pla_send_sloc equals ss.storage_location_id
                         join rs in _scifferContext.REF_STORAGE_LOCATION on pt.pla_receive_sloc equals rs.storage_location_id
                         join g in _scifferContext.ref_bucket on pt.pla_send_bucket equals g.bucket_id
                         join e in _scifferContext.ref_bucket on pt.pla_receive_bucket equals e.bucket_id
                         select new
                         {
                             pla_transfer_id = pt.pla_transfer_id,
                             pla_transfer_number = pt.pla_transfer_number,
                             pla_posting_date = pt.pla_posting_date,
                             pla_document_date = pt.pla_document_date,
                             receiving_plant_id = pt.receiving_plant_id,
                             receving_plant_name = rp.PLANT_NAME,
                             sending_plant_id = pt.sending_plant_id,
                             sending_plant_name = sp.PLANT_NAME,
                             pla_send_sloc = pt.pla_send_sloc,
                             send_sloc_name = ss.storage_location_name,
                             pla_receive_sloc = pt.pla_receive_sloc,
                             receive_sloc_name = rs.storage_location_name,
                             pla_send_bucket = pt.pla_send_bucket,
                             send_bucket_name = g.bucket_name,
                             pla_receive_bucket = pt.pla_receive_bucket,
                             rcv_bucket_name = e.bucket_name,
                             category = c.category,
                             pla_attachment = pt.pla_attachment,
                             remarks_on_document = pt.remarks_on_document,
                             item_name = item.ITEM_NAME
                         }).ToList().GroupBy(l => l.pla_transfer_id)
                        .Select(ed => new InterPlaTransferVM
                        {
                            pla_transfer_id = ed.First().pla_transfer_id,
                            pla_transfer_number = ed.First().pla_transfer_number,
                            pla_posting_date = ed.First().pla_posting_date,
                            pla_document_date = ed.First().pla_document_date,
                            receiving_plant_id = ed.First().receiving_plant_id,
                            receving_plant_name = ed.First().sending_plant_name,
                            sending_plant_id = ed.First().sending_plant_id,
                            sending_plant_name = ed.First().sending_plant_name,
                            pla_send_sloc = ed.First().pla_send_sloc,
                            send_sloc_name = ed.First().send_sloc_name,
                            pla_receive_sloc = ed.First().pla_receive_sloc,
                            receive_sloc_name = ed.First().receive_sloc_name,
                            pla_send_bucket = ed.First().pla_send_bucket,
                            send_bucket_name = ed.First().send_bucket_name,
                            pla_receive_bucket = ed.First().pla_receive_bucket,
                            rcv_bucket_name = ed.First().receive_sloc_name,
                            category = ed.First().category,
                            pla_attachment = ed.First().pla_attachment,
                            remarks_on_document = ed.First().remarks_on_document,
                            ITEM_NAME = string.Join(",", ed.Select(c => c.item_name)).ToString(),
                        }).ToList().OrderByDescending(a => a.pla_transfer_id).ToList();

            return query;
        }

        public InterPlaTransferVM Get(int id)
        {
            try
            {
                inter_pla_transfer pla = _scifferContext.intra_pla_transfer.FirstOrDefault(c => c.pla_transfer_id == id);
                Mapper.CreateMap<inter_pla_transfer, InterPlaTransferVM>();
                InterPlaTransferVM plvm = Mapper.Map<inter_pla_transfer, InterPlaTransferVM>(pla);
                plvm.inter_pla_transfer_detail = plvm.inter_pla_transfer_detail.Where(c => c.is_active == true).ToList();
                plvm.inter_pla_transfer_detail_tag = plvm.inter_pla_transfer_detail_tag.Where(c => c.is_active == true).ToList();                
                return plvm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<inter_plant_detail_vm> GetInterPlantTransferDetail(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getInterPlantTransferDetail");
            var val = _scifferContext.Database.SqlQuery<inter_plant_detail_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<InterPlaTransferVM> InterPlantTransfer(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getInterPlantTransfer");
            var val = _scifferContext.Database.SqlQuery<InterPlaTransferVM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
