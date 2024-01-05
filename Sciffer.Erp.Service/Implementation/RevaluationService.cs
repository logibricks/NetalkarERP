using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class RevaluationService : IRevaluationService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        public RevaluationService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public string Add(inv_revaluation_vm revaluation)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("inv_revaluation_detail_id ", typeof(int));
                dt.Columns.Add("inventory_revaluation_id ", typeof(int));
                dt.Columns.Add("item_id ", typeof(int));
                dt.Columns.Add("quantity ", typeof(double));
                dt.Columns.Add("uom_id ", typeof(int));
                dt.Columns.Add("new_rate ", typeof(double));
                dt.Columns.Add("old_rate ", typeof(double));
                dt.Columns.Add("differential_rate ", typeof(double));
                dt.Columns.Add("differential_value ", typeof(double));
                dt.Columns.Add("general_ledger_id ", typeof(int));
                if(revaluation.item_id1!=null)
                {
                    for(var i=0;i<revaluation.item_id1.Count;i++)
                    {
                        if(revaluation.item_id1[i]!="")
                        {
                            dt.Rows.Add(0,0,int.Parse(revaluation.item_id1[i]),double.Parse(revaluation.quantity[i]),int.Parse(revaluation.uom_id[i]),
                                double.Parse(revaluation.new_rate[i]),double.Parse(revaluation.old_rate[i]),double.Parse(revaluation.differential_rate[i]),
                                double.Parse(revaluation.differential_value[i]),int.Parse(revaluation.general_ledger_id[i]));
                        }
                    }
                }
                DateTime dte = new DateTime(1990, 1, 1);
                int createdby= int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var inventory_revaluation_id = new SqlParameter("@inventory_revaluation_id", revaluation.inventory_revaluation_id);
                var category_id = new SqlParameter("@category_id", revaluation.category_id);
                var inventory_revaluation_date = new SqlParameter("@inventory_revaluation_date", revaluation.inventory_revaluation_date);
                var inventory_revaluation_document_date = new SqlParameter("@inventory_revaluation_document_date", revaluation.inventory_revaluation_document_date==null?dte:revaluation.inventory_revaluation_document_date);
                var inventory_revaluation_number = new SqlParameter("@inventory_revaluation_number", revaluation.inventory_revaluation_number==null?string.Empty:revaluation.inventory_revaluation_number);
                var inventory_revaluation_remark = new SqlParameter("@inventory_revaluation_remark", revaluation.inventory_revaluation_remark==null?string.Empty:revaluation.inventory_revaluation_remark);
                var plant_id = new SqlParameter("@plant_id", revaluation.plant_id);
                var created_by = new SqlParameter("@created_by", createdby);
                var attachement = new SqlParameter("@attachement", revaluation.attachement==null?string.Empty:revaluation.attachement);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_inv_revaluation_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_inventory_revaluation @inventory_revaluation_id ,@category_id ,@inventory_revaluation_date ,@inventory_revaluation_document_date ,@inventory_revaluation_number ,@inventory_revaluation_remark ,@plant_id ,@created_by ,@attachement ,@t1 ", 
                    inventory_revaluation_id, category_id, inventory_revaluation_date, inventory_revaluation_document_date, 
                    inventory_revaluation_number, inventory_revaluation_remark, plant_id, created_by, attachement, t1).FirstOrDefault();

                return val;
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString()); ;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return "error";
            }

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

        public inv_revaluation_vm Get(int? id)
        {
            try
            {
               inventory_revaluation pla = _scifferContext.inventory_revaluation.FirstOrDefault(c => c.inventory_revaluation_id == id);
                Mapper.CreateMap<inventory_revaluation, inv_revaluation_vm>().ForMember(dest => dest.FileUpload, opt => opt.Ignore());
                inv_revaluation_vm plvm = Mapper.Map<inventory_revaluation, inv_revaluation_vm>(pla);
                plvm.inventory_revaluation_detail = plvm.inventory_revaluation_detail.Where(c => c.is_active == true).ToList();
                return plvm;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<inv_revaluation_vm> GetAll()
        {
            var query = (from i in _scifferContext.inventory_revaluation
                         join d in _scifferContext.ref_document_numbring on i.category_id equals d.document_numbring_id
                         join p in _scifferContext.REF_PLANT on i.plant_id equals p.PLANT_ID
                         select new inv_revaluation_vm
                         {
                              category_name=d.category,
                              inventory_revaluation_date=i.inventory_revaluation_date,
                              inventory_revaluation_document_date=i.inventory_revaluation_document_date,
                              inventory_revaluation_number=i.inventory_revaluation_number,
                              plant_name=p.PLANT_NAME,
                              inventory_revaluation_id=i.inventory_revaluation_id,
                         }).OrderByDescending(a => a.inventory_revaluation_id).ToList();
            return query;
        }
            
    }
}
