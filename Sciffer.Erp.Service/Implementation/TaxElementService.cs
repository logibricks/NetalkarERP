using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using System.Data.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data.SqlClient;
using System.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class TaxElementService : ITaxElementService
    {
        private readonly ScifferContext _scifferContext;

        public TaxElementService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public bool Add(tax_elementVM Tax)
        {
            try
            {
                int modifyuser = 0;
                var modify_user = new SqlParameter("@modify_user", modifyuser);
                var entity = new SqlParameter("@entity", "save");
                var tax_element_id = new SqlParameter("@tax_element_id", Tax.tax_element_id);
                var tax_element_code = new SqlParameter("@tax_element_code", Tax.tax_element_code);
                var tax_element_description = new SqlParameter("@tax_element_description", Tax.tax_element_description);
                var purchase_gl = new SqlParameter("@purchase_gl", Tax.purchase_gl);
                var sales_gl = new SqlParameter("@sales_gl", Tax.sales_gl);
                var tax_type_id = new SqlParameter("@tax_type_id", Tax.tax_type_id);
                var is_blocked = new SqlParameter("@is_blocked", Tax.is_blocked);
                var no_setof_gl = new SqlParameter("@no_setoff_gl", Tax.no_setoff_gl==null?0:Tax.no_setoff_gl);
                var onhold_gl = new SqlParameter("@on_hold_gl", Tax.on_hold_gl==null?0:Tax.on_hold_gl);
                var is_rcm = new SqlParameter("@is_rcm",Tax.is_rcm);
                var rcm_asset_gl = new SqlParameter("@rcm_asset_gl", Tax.rcm_asset_gl == null ? 0 : Tax.rcm_asset_gl);
                var rcm_liability_gl = new SqlParameter("@rcm_liability_gl", Tax.rcm_liability_gl == null ? 0 : Tax.rcm_liability_gl);
                DataTable t11 = new DataTable();
                t11.Columns.Add("tax_element_detail_id", typeof(int));
                t11.Columns.Add("effective_from", typeof(DateTime));
                t11.Columns.Add("rate", typeof(double));
                t11.Columns.Add("no_setoff", typeof(double));
                t11.Columns.Add("on_hold", typeof(double));              
                if (Tax.ref_tax_element_detail != null)
                {
                    foreach (var i in Tax.ref_tax_element_detail)
                    {
                        t11.Rows.Add(i.tax_element_detail_id==0?-1: i.tax_element_detail_id, i.effective_from, i.rate,i.no_setoff,i.on_hold);
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_tax_element_detail";
                t1.Value = t11;
                var val = _scifferContext.Database.SqlQuery<string>(
               "exec save_tax_element @entity,@tax_element_id ,@tax_element_code ,@tax_element_description ,@purchase_gl ,@sales_gl ,@tax_type_id ,@is_blocked,@modify_user,@no_setoff_gl,@on_hold_gl,@is_rcm,@rcm_asset_gl,@rcm_liability_gl,@t1 ", entity, 
               tax_element_id, tax_element_code, tax_element_description, purchase_gl, sales_gl, tax_type_id, is_blocked, modify_user, no_setof_gl, onhold_gl, is_rcm, rcm_asset_gl, rcm_liability_gl, t1).FirstOrDefault();
               
                if(val=="Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
           // return true;
        }

        public bool Delete(int id)
        {
            try
            {
                 var pt_detail = _scifferContext.ref_tax_element.Find(id);
                 pt_detail.is_active = false;
                _scifferContext.Entry(pt_detail).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
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

        public tax_elementVM Get(int id)
        {
            try
            {
                ref_tax_element JR = _scifferContext.ref_tax_element.FirstOrDefault(c => c.tax_element_id == id);
                Mapper.CreateMap<ref_tax_element, tax_elementVM>();
                tax_elementVM JRVM = Mapper.Map<ref_tax_element, tax_elementVM>(JR);
                JRVM.ref_tax_element_detail = JRVM.ref_tax_element_detail.Where(c => c.is_active == true).ToList();
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tax_elementVM> getall()
        {
            var query = (from tx in _scifferContext.ref_tax_element
                         join gl in _scifferContext.ref_general_ledger on tx.purchase_gl equals gl.gl_ledger_id
                         join gl1 in _scifferContext.ref_general_ledger on tx.sales_gl equals gl1.gl_ledger_id
                         join t in _scifferContext.ref_tax_type on tx.tax_type_id equals t.tax_type_id
                         into temp from j in temp.DefaultIfEmpty()
                         join gl2 in _scifferContext.ref_general_ledger on tx.no_setoff_gl equals gl2.gl_ledger_id into j1
                         from g2 in j1.DefaultIfEmpty()
                         join gl3 in _scifferContext.ref_general_ledger on tx.on_hold_gl equals gl3.gl_ledger_id into j2
                         from g3 in j2.DefaultIfEmpty()
                         join gl4 in _scifferContext.ref_general_ledger on tx.rcm_asset_gl equals gl4.gl_ledger_id into j3
                         from g4 in j3.DefaultIfEmpty()
                         join gl5 in _scifferContext.ref_general_ledger on tx.rcm_liability_gl equals gl5.gl_ledger_id into j4
                         from g5 in j4.DefaultIfEmpty()
                         select new tax_elementVM()
                         {
                             tax_element_id = tx.tax_element_id,
                             tax_element_code = tx.tax_element_code,
                             tax_element_description = tx.tax_element_description,                            
                             tax_type = j.tax_type_name,
                             is_blocked = tx.is_blocked,
                             sales_gl_name = gl1.gl_ledger_code + "/" + gl1.gl_ledger_name,
                             purchase_gl_name= gl.gl_ledger_code + "/" + gl.gl_ledger_name,
                             is_rcm=tx.is_rcm,
                             no_setoff_gl_name=g2==null?string.Empty:g2.gl_ledger_code + "/" + g2.gl_ledger_name,
                             on_hold_gl_name = g3 == null ? string.Empty : g3.gl_ledger_code + "/" + g3.gl_ledger_name,
                             rcm_asset_gl_name = g4 == null ? string.Empty : g4.gl_ledger_code + "/" + g4.gl_ledger_name,
                             rcm_liability_gl_name = g5 == null ? string.Empty : g5.gl_ledger_code + "/" + g5.gl_ledger_name,
                         }).ToList();
            return query;
        }

        public List<tax_elementVM> GetAll()
        {
            Mapper.CreateMap<ref_tax_element, tax_elementVM>();
            return _scifferContext.ref_tax_element.Project().To<tax_elementVM>().Where(a => a.is_active == true).ToList();
        }
       
    }
}
