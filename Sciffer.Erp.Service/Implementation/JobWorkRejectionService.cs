using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using System.Data;
using System.Data.SqlClient;
using Sciffer.Erp.Data;
using System.Web;
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class JobWorkRejectionService : IJobWorkRejectionService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public JobWorkRejectionService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;

        }
        public string Add(jobwork_rejection_VM main)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("detail_id1", typeof(int));
                dt.Columns.Add("job_work_detail_id", typeof(int));
                dt.Columns.Add("item_id1", typeof(int));
                dt.Columns.Add("uom_id1", typeof(int));
                dt.Columns.Add("batch_id1", typeof(int));
                dt.Columns.Add("bucket_id", typeof(int));
                dt.Columns.Add("batch_bal_quantity", typeof(double));
                dt.Columns.Add("quantity1", typeof(double));
                dt.Columns.Add("sloc_id1", typeof(int));
                dt.Columns.Add("rate",typeof(double));
                dt.Columns.Add("value", typeof(double));
                if (main.detail_id1 != null)
                {
                    for (var i = 0; i < main.detail_id1.Count; i++)
                    {
                        int result = _genericService.GetCheck_Inventory(Convert.ToInt32(main.item_id1[i]), Convert.ToInt32(main.plant_id), Convert.ToInt32(main.sloc_id1[i]), main.bucket_id1[i], Convert.ToDecimal(main.quantity1[i]));
                        if (result == 0)
                        {
                            return "Stock is Not Available";
                        }

                        dt.Rows.Add(main.detail_id1[i] == 0 ? -1 : main.detail_id1[i],
                            main.job_work_detail_id[i],
                            main.item_id1[i],
                            main.uom_id1[i],
                            main.batch_id1[i],
                            main.bucket_id1[i],
                            main.batch_bal_quantity[i],
                            main.quantity1[i],
                            main.sloc_id1[i],
                            main.rate[i],
                            main.value[i]
                            );
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_jobwork_rejection_detail";
                t1.Value = dt;

                DataTable cp = new DataTable();
                cp.Columns.Add("detail_id2", typeof(int));
                cp.Columns.Add("item_id2", typeof(int));
                cp.Columns.Add("tag_id2", typeof(int));
                cp.Columns.Add("quantity2", typeof(double));
                if (main.detail_id2 != null)
                {
                    for (var i = 0; i < main.detail_id2.Count; i++)
                    {
                        cp.Rows.Add(main.detail_id2[i] == 0 ? -1 : main.detail_id2[i],
                            main.item_id2[i],
                            main.tag_id2[i],
                            main.quantity2[i]);
                    }
                }
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_jobwork_rejection_tag_detail";
                t2.Value = cp;

                var createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var jobwork_rejection_id = new SqlParameter("@jobwork_rejection_id", main.jobwork_rejection_id==0 ? -1 : main.jobwork_rejection_id);
                var doc_number = new SqlParameter("@doc_number", main.doc_number==null? "" : main.doc_number);
                var category_id = new SqlParameter("@category_id", main.category_id);
                var posting_date = new SqlParameter("@posting_date", main.posting_date);
                var bill_to_party = new SqlParameter("@bill_to_party", main.bill_to_party);
                var ship_to_party = new SqlParameter("@ship_to_party", main.ship_to_party);
                var place_of_supply = new SqlParameter("@place_of_supply", main.place_of_supply);
                var place_of_delivery = new SqlParameter("@place_of_delivery", main.place_of_delivery);
                var business_unit_id = new SqlParameter("@business_unit_id", main.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", main.plant_id);
                var freight_term_id = new SqlParameter("@freight_term_id", main.freight_term_id == null ? 0 : main.freight_term_id);
                var territory_id = new SqlParameter("@territory_id", main.territory_id == null ? 0 : main.territory_id);
                var sales_rm_id = new SqlParameter("@sales_rm_id", main.sales_rm_id == null ? 0 : main.sales_rm_id);
                var customer_po_date = new SqlParameter("@customer_po_date", main.customer_po_date==null ? DateTime.Parse("01-01-1990"): main.customer_po_date);
                var customer_po_number = new SqlParameter("@customer_po_number", main.customer_po_number==null ? "" : main.customer_po_number);
                var removal_date = new SqlParameter("@removal_date", main.removal_date==null ? DateTime.Parse("01-01-1990") : main.removal_date);
                var removal_time = new SqlParameter("@removal_time", main.removal_time==null ? TimeSpan.Parse("00:00:01"): main.removal_time);
                var sales_against_order = new SqlParameter("@sales_against_order", main.sales_against_order);
                var internal_remarks = new SqlParameter("@internal_remarks", main.internal_remarks == null ? "" : main.internal_remarks);
                var remarks_on_doc = new SqlParameter("@remarks_on_doc", main.remarks_on_doc == null ? "" : main.remarks_on_doc);
                var attachment = new SqlParameter("@attachment", main.attachment == null ? "" : main.attachment);
                var is_active = new SqlParameter("@is_active", 1);
                var created_by = new SqlParameter("@created_by", createdby);
                var deleteids1 = new SqlParameter("@deleteids1", main.deleteids1 == null ? "" : main.deleteids1);
                var deleteids2 = new SqlParameter("@deleteids2", main.deleteids2 == null ? "" : main.deleteids2);
                var mode_of_transport = new SqlParameter("@mode_of_transport", main.mode_of_transport == null ? "" : main.mode_of_transport);
                var vehical_no = new SqlParameter("@vehical_no", main.vehicle_no == null ? "" : main.vehicle_no);
                var reason_id = new SqlParameter("@reason_id", main.reason_id == null ? 0 : main.reason_id);

                var reason_code = _scifferContext.REF_REASON_DETERMINATION.Where(x => x.REASON_DETERMINATION_ID == main.reason_id).FirstOrDefault().reason_determination_code;
                var val = "";
                if (reason_code == "REJECTION AS IT IS" && main.detail_id2 == null)
                {
                    val = _scifferContext.Database.SqlQuery<string>("exec Save_jobwork_rejection_out @jobwork_rejection_id ,@doc_number,@category_id, @posting_date, @bill_to_party, @ship_to_party, @place_of_supply, @place_of_delivery,@business_unit_id, @plant_id, @freight_term_id, @territory_id, @sales_rm_id, @customer_po_date,@customer_po_number, @removal_date, @removal_time, @sales_against_order, @internal_remarks,@remarks_on_doc, @attachment, @is_active,@created_by, @deleteids1, @deleteids2, @t1,@mode_of_transport,@vehical_no,@reason_id",
                                        jobwork_rejection_id, doc_number, category_id, posting_date, bill_to_party, ship_to_party, place_of_supply, place_of_delivery, business_unit_id, plant_id, freight_term_id, territory_id, sales_rm_id, customer_po_date, customer_po_number, removal_date, removal_time, sales_against_order, internal_remarks, remarks_on_doc, attachment, is_active, created_by, deleteids1, deleteids2, t1, mode_of_transport, vehical_no, reason_id).FirstOrDefault();
                }
                else
                {
                    val = _scifferContext.Database.SqlQuery<string>("exec Save_jobwork_rejection @jobwork_rejection_id ,@doc_number,@category_id, @posting_date, @bill_to_party, @ship_to_party, @place_of_supply, @place_of_delivery,@business_unit_id, @plant_id, @freight_term_id, @territory_id, @sales_rm_id, @customer_po_date,@customer_po_number, @removal_date, @removal_time, @sales_against_order, @internal_remarks,@remarks_on_doc, @attachment, @is_active,@created_by, @deleteids1, @deleteids2, @t1,@t2,@mode_of_transport,@vehical_no,@reason_id",
                                        jobwork_rejection_id, doc_number, category_id, posting_date, bill_to_party, ship_to_party, place_of_supply, place_of_delivery, business_unit_id, plant_id, freight_term_id, territory_id, sales_rm_id, customer_po_date, customer_po_number, removal_date, removal_time, sales_against_order, internal_remarks, remarks_on_doc, attachment, is_active, created_by, deleteids1, deleteids2, t1, t2, mode_of_transport, vehical_no, reason_id).FirstOrDefault();
                }                
                return val;
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
            throw new NotImplementedException();
        }

        public jobwork_rejection_VM Get(int id)
        {
            jobwork_rejection invoice = _scifferContext.jobwork_rejection.FirstOrDefault(c => c.jobwork_rejection_id == id);
            Mapper.CreateMap<jobwork_rejection, jobwork_rejection_VM>();
            jobwork_rejection_VM quotationvm = Mapper.Map<jobwork_rejection, jobwork_rejection_VM>(invoice);
            quotationvm.jobwork_rejection_detail = quotationvm.jobwork_rejection_detail.ToList();            
            return quotationvm;
        }

        public List<jobwork_rejection_VM> GetAll()
        { 
            var query = (from ed in _scifferContext.jobwork_rejection.Where(x => x.is_active == true)
                         join jd in _scifferContext.jobwork_rejection_detail on ed.jobwork_rejection_id equals jd.jobwork_rejection_id
                         join item in _scifferContext.REF_ITEM on jd.item_id equals item.ITEM_ID
                         join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id
                         join pos in _scifferContext.REF_STATE on ed.place_of_supply equals pos.STATE_ID
                         join pod in _scifferContext.REF_STATE on ed.place_of_delivery equals pod.STATE_ID
                         join btp in _scifferContext.REF_CUSTOMER on ed.bill_to_party equals btp.CUSTOMER_ID
                         join stp in _scifferContext.REF_CUSTOMER on ed.ship_to_party equals stp.CUSTOMER_ID
                         join bu in _scifferContext.REF_BUSINESS_UNIT on ed.business_unit_id equals bu.BUSINESS_UNIT_ID into bu1
                         from bu2 in bu1.DefaultIfEmpty()
                         join plant in _scifferContext.REF_PLANT on ed.plant_id equals plant.PLANT_ID
                         join ft in _scifferContext.REF_FREIGHT_TERMS on ed.freight_term_id equals ft.FREIGHT_TERMS_ID into ft1
                         from ft2 in ft1.DefaultIfEmpty()
                         join trr in _scifferContext.REF_TERRITORY on ed.territory_id equals trr.TERRITORY_ID into trr1
                         from trr2 in trr1.DefaultIfEmpty()
                         join sal in _scifferContext.ref_sales_rm on ed.sales_rm_id equals sal.sales_rm_id into sal1
                         from sal2 in sal1.DefaultIfEmpty()
                         join reason in _scifferContext.REF_REASON_DETERMINATION on ed.reason_id equals reason.REASON_DETERMINATION_ID into reason1
                         from reason2 in reason1.DefaultIfEmpty()
                         select new jobwork_rejection_VM
                         {
                             jobwork_rejection_id = ed.jobwork_rejection_id,
                             doc_number = ed.doc_number,
                             category_name = cat.category,
                             posting_date = ed.posting_date,
                             place_of_supply_name = pos.STATE_NAME +"-" + pos.state_ut_code,
                             place_of_delivery_name = pod.STATE_NAME + "-" + pod.state_ut_code,
                             bill_to_party_name = btp.CUSTOMER_NAME,
                             ship_to_party_name = stp.CUSTOMER_NAME,
                             business_unit_id_name = bu2.BUSINESS_UNIT_NAME,
                             plant_name = plant.PLANT_NAME,
                             freight_term_id_name = ft2.FREIGHT_TERMS_NAME,
                             territory_id_name = trr2.TERRITORY_NAME,
                             sales_rm_id_name = sal2.REF_EMPLOYEE.employee_name,
                             customer_po_date = ed.customer_po_date,
                             customer_po_number = ed.customer_po_number,
                             removal_date = ed.removal_date,
                             removal_time = ed.removal_time,
                             sales_against_order = ed.sales_against_order,
                             internal_remarks = ed.internal_remarks,
                             remarks_on_doc = ed.remarks_on_doc,
                             attachment = ed.attachment,
                             created_by = ed.created_by,
                             created_ts = ed.created_ts,
                             item_name = item.ITEM_NAME + "/" + item.ITEM_CODE,
                             quantity = jd.quantity,
                             reason = reason2.REASON_DETERMINATION_NAME,
                         }).OrderByDescending(a => a.jobwork_rejection_id).ToList();
            return query;
        }
        public List<jobwork_rejection_VM> jobworkrejection(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getjobworkrejection");
            var val = _scifferContext.Database.SqlQuery<jobwork_rejection_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<jobwork_rejection_detail_VM> jobworkrejectiondetail(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getjobworkrejectiondetail");
            var val = _scifferContext.Database.SqlQuery<jobwork_rejection_detail_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<jobwork_rejection_item_detail_VM> jobworkrejectionitemdetail(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getjobworkrejectionitemdetail");
            var val = _scifferContext.Database.SqlQuery<jobwork_rejection_item_detail_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<jobwork_rejection_VM> GetJobWorkRejectionDetail(string entity, int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var ent = new SqlParameter("@entity", entity);
            var val = _scifferContext.Database.SqlQuery<jobwork_rejection_VM>(
            "exec Get_JobWork_Rejection @entity,@id", ent, quotation_id).ToList();
            return val;
        }
        public string GetItemCategoryByItem(int item_id)
        {
            return _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == item_id).FirstOrDefault().REF_ITEM_CATEGORY.ITEM_CATEGORY_NAME;
        }

        public int GetState(int id)
        {
            return _scifferContext.REF_CUSTOMER.Where(x => x.CUSTOMER_ID == id).FirstOrDefault().BILLING_STATE_ID;
        }

        public int GetdeliveryState(int id)
        {
            return _scifferContext.REF_CUSTOMER.Where(x => x.CUSTOMER_ID == id).FirstOrDefault().BILLING_STATE_ID;
        }
    }
}
