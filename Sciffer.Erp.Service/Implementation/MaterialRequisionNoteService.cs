using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using System.Data;
using System.Data.SqlClient;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using System.Web;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class MaterialRequisionNoteService : IMaterialRequisionNoteService

    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name

        public MaterialRequisionNoteService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(material_requision_note_vm material)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("material_requision_note_detail_id", typeof(int));
                dt.Columns.Add("material_requision_note_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("required_qty", typeof(double));
                dt.Columns.Add("rate", typeof(double));
                dt.Columns.Add("cost_center_id", typeof(int));
                dt.Columns.Add("machine", typeof(int));
                dt.Columns.Add("reason", typeof(int));
                dt.Columns.Add("order_type", typeof(int));
                dt.Columns.Add("order_number", typeof(string));
                dt.Columns.Add("line_remarks", typeof(string));
                dt.Columns.Add("is_active", typeof(bool));

                for (var i = 0; i <= material.item_id1.Count-1; i++)
                {
                if (material.item_id1 != null)
                    {
                        var id = int.Parse(material.material_requision_note_detail_id1[i] == "" ? "0" : material.material_requision_note_detail_id1[i]);
                        var notid = material.material_requision_note_id==0?0: material.material_requision_note_id;
                        var item = int.Parse(material.item_id1[i] == "" ? "0" : material.item_id1[i]);
                        var uom = int.Parse(material.uom_id1[i] == "" ? "0" : material.uom_id1[i]);
                        var required = double.Parse(material.required_qty1[i] == "" ? "0" : material.required_qty1[i]);
                        var rate = material.rate[i];
                        var cost = int.Parse(material.cost_center_id1[i] == "" ? "0" : material.cost_center_id1[i]);
                        var machine = int.Parse(material.machine1[i] == "" ? "0" : material.machine1[i]);
                        var reason = int.Parse(material.reason1[i] == "" ? "0" : material.reason1[i]);
                        var ordertype = int.Parse(material.order_type1[i] == "" ? "0" : material.order_type1[i]);
                        var ordernum = material.order_number1[i] == "" ? "" : material.order_number1[i];
                        var line = material.line_remarks1[i] == "" ? "" : material.line_remarks1[i];
                        dt.Rows.Add(id,notid, item, uom, required, rate, cost, machine, reason, ordertype, ordernum, line,1);
                    }
                }
               
                    
                
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_material_requision_note_detail";
                t1.Value = dt;
                DateTime dte = new DateTime(1990, 1, 1);
                var material_requision_note_id = new SqlParameter("@material_requision_note_id", material.material_requision_note_id);
                var category_id = new SqlParameter("@category_id", material.category_id);
                var number = new SqlParameter("@number", material.number==null?"" : material.number);
                var document_date = new SqlParameter("@document_date", material.document_date==null? dte: material.document_date);
                var requirement_by = new SqlParameter("@requirement_by", material.requirement_by==null?0:material.requirement_by);
                var plant_id = new SqlParameter("@plant_id", material.plant_id);
                var remarks = new SqlParameter("@remarks", material.remarks==null?"": material.remarks);
                var receiving_sloc = new SqlParameter("@receiving_sloc", material.receiving_sloc);
                var sending_sloc = new SqlParameter("@sending_sloc", material.sending_sloc);
                var type = new SqlParameter("@type", material.type==null?0: material.type);
                var posting_date = new SqlParameter("@posting_date", material.posting_date);
                var status_id = new SqlParameter("@status_id", material.status_id);
                var is_active = new SqlParameter("@is_active", material.is_active);
                var deletes = new SqlParameter("@deleteids", material.deleteids==null?"": material.deleteids);

                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_material_requision_note @material_requision_note_id ,@posting_date,@category_id ,@number ,@document_date ,@requirement_by ,@plant_id ,@remarks ,@receiving_sloc ,@sending_sloc ,@type ,@status_id ,@is_active,@deleteids,@t1 ",
                    material_requision_note_id, posting_date, category_id,  number, document_date, requirement_by, plant_id, remarks, receiving_sloc, sending_sloc, type,
                    status_id, is_active, deletes, t1).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    var str = val.Split('~');
                    var mrn_no = str[0];
                    return mrn_no;
                }
                else
                {
                    return "error";
                }

            }
            catch (Exception ex)
             {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "error";
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
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

        public material_requision_note_vm Get(int id)
        {
            material_requision_note invoice = _scifferContext.material_requision_note.FirstOrDefault(c => c.material_requision_note_id == id);
            Mapper.CreateMap<material_requision_note, material_requision_note_vm>();
            material_requision_note_vm quotationvm = Mapper.Map<material_requision_note, material_requision_note_vm>(invoice);
            quotationvm.material_requision_note_detail = quotationvm.material_requision_note_detail.Where(c => c.is_active == true).ToList();
            
            return quotationvm;
        }
        public List<material_requision_note_vm> GetPendigApprovedList()
        {
            int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
            var query = (from material in _scifferContext.material_requision_note.Where(x => x.is_active == true && x.approval_status!=1)
                         join catetgory in _scifferContext.ref_document_numbring on material.category_id equals catetgory.document_numbring_id
                         join palnt in _scifferContext.REF_PLANT on material.plant_id equals palnt.PLANT_ID
                         join requirement in _scifferContext.REF_EMPLOYEE on material.requirement_by equals requirement.employee_id into requirement1
                         from requirement2 in requirement1.DefaultIfEmpty()
                         join rsloc in _scifferContext.REF_STORAGE_LOCATION on material.receiving_sloc equals rsloc.storage_location_id
                         join ssloc in _scifferContext.REF_STORAGE_LOCATION on material.sending_sloc equals ssloc.storage_location_id
                         join status in _scifferContext.ref_status on material.status_id equals status.status_id
                         join mu in _scifferContext.material_requision_note_user_approval.Where(x=>x.user_id==createdby) on material.material_requision_note_id equals mu.material_requision_note_id
                         select new material_requision_note_vm
                         {
                             material_requision_note_id = material.material_requision_note_id,
                             posting_date = material.posting_date,
                             category_name = catetgory.category,
                             number = material.number,
                             document_date = material.document_date,
                             requirement_by_name = requirement2.employee_name,
                             plant_name = palnt.PLANT_NAME,
                             remarks = material.remarks,
                             receiving_sloc_name = rsloc.storage_location_name,
                             sending_sloc_name = ssloc.storage_location_name,
                             type_name = material.type == null ? "" : material.type == 1 ? "Goods Issue" : "Goods Issue",
                             status_name = status.status_name,
                             is_active = material.is_active,
                             approval_status_name = material.approval_status == 0 ? "Pending" : material.approval_status == 1 ? "Approved" : "Rejected",
                         }).ToList();

            return query;
                
        }
        public List<material_requision_note_vm> GetAll()
        {
            var query = (from material in _scifferContext.material_requision_note.Where(x => x.is_active == true)
                         join catetgory in _scifferContext.ref_document_numbring on material.category_id equals catetgory.document_numbring_id
                         join palnt in _scifferContext.REF_PLANT on material.plant_id equals palnt.PLANT_ID
                         join requirement in _scifferContext.ref_user_management on material.requirement_by equals requirement.user_id into requirement1
                         from requirement2 in requirement1.DefaultIfEmpty()
                         join emp in _scifferContext.REF_EMPLOYEE on requirement2.employee_id equals emp.employee_id into emp1
                         from emp2 in emp1.DefaultIfEmpty()
                         join rsloc in _scifferContext.REF_STORAGE_LOCATION on material.receiving_sloc equals rsloc.storage_location_id
                         join ssloc in _scifferContext.REF_STORAGE_LOCATION on material.sending_sloc equals ssloc.storage_location_id
                         join status in _scifferContext.ref_status on material.status_id equals status.status_id
                         join rum in _scifferContext.ref_user_management on material.approved_by equals rum.user_id into user1
                         from user2 in user1.DefaultIfEmpty()
                         select new material_requision_note_vm
                         {
                             material_requision_note_id = material.material_requision_note_id,
                             posting_date = material.posting_date,
                             category_name = catetgory.category,
                             number = material.number,
                             document_date = material.document_date,
                             requirement_by_name = emp2.employee_name,
                             plant_name = palnt.PLANT_NAME,
                             remarks = material.remarks,
                             receiving_sloc_name = rsloc.storage_location_name,
                             sending_sloc_name = ssloc.storage_location_name,
                             type_name = material.type == null ? "" : material.type == 1 ? "Goods Issue" : "Goods Issue",
                             status_name = status.status_name,
                             is_active = material.is_active,
                             approval_status_name = material.approval_status == 0 ? "Pending" : material.approval_status == 1 ? "Approved" : "Rejected",
                             approval_comments = material.approval_comments,
                             approval_user = user2.user_name,
                         }).OrderByDescending(a => a.material_requision_note_id).ToList();
            return query;
        }

        public string Update(material_requision_note_vm material)
        {
            throw new NotImplementedException();
        }
        public List<material_requision_note_vm> GetMRnList()
        {
            var query = (from ed in _scifferContext.material_requision_note.Where(x => x.is_active == true)
                         select new
                         {
                             material_requision_note_id = ed.material_requision_note_id,
                             number = ed.number,
                             posting_date = ed.posting_date,
                         }).Select(a => new material_requision_note_vm
                         {
                             material_requision_note_id = a.material_requision_note_id,
                             mrn_date_number = a.number + " - " + a.posting_date,
                         }).ToList();
            return query;
        }
        public bool ChangeApprovedStatus(material_requision_note_vm vm)
        {
            try
            {
                var data = _scifferContext.material_requision_note.Where(x => x.material_requision_note_id == vm.material_requision_note_id).FirstOrDefault();
                data.approval_status = vm.approval_status;
                data.approval_comments = vm.approval_comments;
                data.approved_by= int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                _scifferContext.Entry(data).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public double GetMaterialRate(int plant_id, int item_id)
        {
            try
            {
                var q = _scifferContext.ref_item_plant_valuation.Where(x => x.item_id == item_id && x.plant_id == plant_id).FirstOrDefault();
                return q == null ? 0 : q.item_value;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public string materialRequisionNoteupdatestatusseen()
        {
            try
            {
                int user_id = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var op = _scifferContext.ref_user_role_mapping.Where(x => x.user_id == user_id).FirstOrDefault();
                var role = _scifferContext.ref_user_management_role.Where(x => x.role_id == op.role_id).FirstOrDefault();
                //if(op.role_id == role.role_id)
                if (role.role_code == "STO_EXEC" || role.role_code == "IT_ADMIN")

                {
                    var purchaseReqDetail = _scifferContext.material_requision_note.Where(x => x.is_seen == false).ToList();
                    foreach (var item in purchaseReqDetail)
                    {
                        item.is_seen = true;
                        _scifferContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }

                    _scifferContext.SaveChanges();
                }

                return "true";
            }

            catch (Exception)
            {
                return "false";
            }
        }

    }
}
