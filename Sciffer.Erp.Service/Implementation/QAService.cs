using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using AutoMapper;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class QAService : IQAService
    {
        private readonly ScifferContext _scifferContext;
        private readonly GenericService _genericService;
        public QAService(ScifferContext scifferContext, GenericService genericService)
        {
            _scifferContext = scifferContext;
            _genericService = genericService;
        }

        public string Add(pur_qa_VM item)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("qa_detail_id ", typeof(int));
                dt.Columns.Add("parameter_id ", typeof(int));
                dt.Columns.Add("parameter_range ", typeof(string));
                dt.Columns.Add("actual_value ", typeof(string));
                dt.Columns.Add("method_used ", typeof(string));
                dt.Columns.Add("checked_by ", typeof(string));
                dt.Columns.Add("document_reference ", typeof(string));
                dt.Columns.Add("pass_fail ", typeof(int));
                if(item.parameter_id!=null)
                {
                    for (var i = 0; i < item.parameter_id.Count; i++)
                    {
                        if (item.parameter_id[i] != "")
                        {
                            dt.Rows.Add(int.Parse(item.qa_detail_id[i]), int.Parse(item.parameter_id[i]), item.parameter_range[i], item.actual_value[i], item.method_used[i], item.checked_by[i],
                                item.document_reference[i], int.Parse(item.pass_fail[i]));
                        }
                    }
                }
                
                int user = Convert.ToInt32(HttpContext.Current.Session["User_Id"]);
                var qa_id = new SqlParameter("@qa_id", item.qa_id);
                var category_id = new SqlParameter("@category_id", item.category_id);
                var document_no = new SqlParameter("@document_no", item.document_no);
                var posting_date = new SqlParameter("@posting_date", item.posting_date);
                var item_id = new SqlParameter("@item_id", item.item_id);
                var batch_id = new SqlParameter("@batch_id", item.batch_id);
                var batch_no = new SqlParameter("@batch_no", item.batch_no==null?string.Empty:item.batch_no);
                var status_id = new SqlParameter("@status_id", item.status_id);
                var document_type_code = new SqlParameter("@document_type_code", item.document_type_code);
                var document_id = new SqlParameter("@document_id", item.document_id);
                var document_date = new SqlParameter("@document_date", item.document_date);
                var item_number = new SqlParameter("@item_number", item.item_number);
                var document_qty = new SqlParameter("@document_qty", item.document_qty);
                var inspection_lot_qty = new SqlParameter("@inspection_lot_qty", item.inspection_lot_qty);
                var sample_size_checked = new SqlParameter("@sample_size_checked", item.sample_size_checked);
                var sample_size_accepted = new SqlParameter("@sample_size_accepted", item.sample_size_accepted);
                var sample_size_rejected = new SqlParameter("@sample_size_rejected", item.sample_size_rejected);
                var total_accepted_qty = new SqlParameter("@total_accepted_qty", item.total_accepted_qty);
                var total_rejected_qty = new SqlParameter("@total_rejected_qty", item.total_rejected_qty);
                var total_wip_qty = new SqlParameter("@total_wip_qty", item.total_wip_qty);
                var shelf_life = new SqlParameter("@shelf_life", item.shelf_life);
                var shelf_life_uom_id = new SqlParameter("@shelf_life_uom_id", item.shelf_life_uom_id);
                var grn_date = new SqlParameter("@grn_date", item.grn_date);
                var date_based_on = new SqlParameter("@date_based_on", item.date_based_on);
                var start_date = new SqlParameter("@start_date", item.start_date);
                var end_date = new SqlParameter("@end_date", item.end_date);
                var internal_remarks = new SqlParameter("@internal_remarks", item.internal_remarks==null?string.Empty:item.internal_remarks);
                var remrarks_on_doc = new SqlParameter("@remrarks_on_doc", item.remrarks_on_doc==null?string.Empty:item.remrarks_on_doc);
                var attachment = new SqlParameter("@attachment", item.attachment==null?string.Empty:item.attachment);
                var plant_id = new SqlParameter("@plant_id", item.plant_id);
                var sloc_id = new SqlParameter("@sloc_id", item.sloc_id);
                var created_by = new SqlParameter("@created_by", user);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_qa_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_pur_qa @qa_id ,@category_id ,@document_no ,@posting_date ,@item_id ,@batch_id ,@batch_no ,@status_id ,@document_type_code ,@document_id ,@document_date ,@item_number ,@document_qty ,@inspection_lot_qty ,@sample_size_checked ,@sample_size_accepted ,@sample_size_rejected ,@total_accepted_qty ,@total_rejected_qty ,@total_wip_qty ,@shelf_life ,@shelf_life_uom_id ,@grn_date ,@date_based_on ,@start_date ,@end_date ,@internal_remarks ,@remrarks_on_doc ,@attachment ,@plant_id ,@sloc_id ,@created_by,@t1 ", 
                    qa_id, category_id, document_no, posting_date, item_id, batch_id, batch_no, status_id, document_type_code, document_id, document_date, item_number, document_qty, 
                    inspection_lot_qty, sample_size_checked, sample_size_accepted, sample_size_rejected, total_accepted_qty, total_rejected_qty, total_wip_qty, shelf_life, 
                    shelf_life_uom_id, grn_date, date_based_on, start_date, end_date, internal_remarks, remrarks_on_doc, attachment, plant_id, sloc_id,created_by, t1).FirstOrDefault();
                if(val.Contains("Saved"))
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
                return ex.Message;
            }
                    
        }

    

        public pur_qa_VM Get(int? id)
        {
            try
            {
                pur_qa JR = _scifferContext.pur_qa.FirstOrDefault(c => c.qa_id == id);
                Mapper.CreateMap<pur_qa, pur_qa_VM>();
                pur_qa_VM JRVM = Mapper.Map<pur_qa, pur_qa_VM>(JR);
                JRVM.pur_qa_detail = JRVM.pur_qa_detail.ToList();            
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<pur_qa_VM> GetAll()
        {
            var query = (from ed in _scifferContext.pur_qa.Where(x => x.is_active == true)
                         join i in _scifferContext.REF_ITEM on ed.item_id equals i.ITEM_ID
                         join s in _scifferContext.ref_status on ed.status_id equals s.status_id
                         join d in _scifferContext.ref_document_numbring on ed.category_id equals d.document_numbring_id
                         join gr in _scifferContext.pur_grn on ed.document_id equals gr.grn_id into j1
                         from grn in j1.DefaultIfEmpty()
                         select new pur_qa_VM
                         {
                             batch_id = ed.batch_id,
                             batch_no = ed.batch_no,
                             category_id = ed.category_id,
                             category_name = d.category,
                             date_based_on = ed.date_based_on,
                             document_date = ed.document_date,
                             document_id = ed.document_id,
                             document_qty = ed.document_qty,
                             document_no = ed.document_no,
                             document_type_code = ed.document_type_code,
                             document_number = grn == null ? string.Empty : grn.grn_number,
                             // document_type_id = ed.document_type_id,
                             end_date = ed.end_date,
                             grn_date = ed.grn_date,
                             inspection_lot_qty = ed.inspection_lot_qty,
                             internal_remarks = ed.internal_remarks,
                             item_id = ed.item_id,
                             item_name = i.ITEM_NAME,
                             item_number = ed.item_number==null?0: ed.item_number,
                             posting_date = ed.posting_date,
                             qa_id = ed.qa_id,
                             remrarks_on_doc = ed.remrarks_on_doc,
                             sample_size_accepted = ed.sample_size_accepted,
                             sample_size_checked = ed.sample_size_checked,
                             sample_size_rejected = ed.sample_size_rejected,
                             shelf_life = ed.shelf_life,
                           
                             shelf_life_uom_name = "Days",
                             start_date =ed.start_date,
                             status_id =ed.status_id,
                             status_name=s.status_name,
                             total_accepted_qty = ed.total_accepted_qty,
                             total_rejected_qty =ed.total_rejected_qty,
                             total_wip_qty =ed.total_wip_qty,
                         }).OrderByDescending(a => a.qa_id).ToList();
            return query;
        }
        public List<pur_qa_VM> GetSourceDocument()
        {
            var query = (from qa in _scifferContext.pur_qa
                         select new pur_qa_VM {
                              document_id=qa.document_id,
                              document_number=qa.source_document_no,
                         }).ToList();
            return query;
        }       
    }
}
