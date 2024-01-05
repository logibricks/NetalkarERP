using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using System.Data.Entity;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Data.SqlClient;
using System.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class TdsCodeService : ITdsCodeService
    {
        private readonly ScifferContext _scifferContext;

        public TdsCodeService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public bool Add(tds_codeVM TDSCode)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("tds_code_detail_id", typeof(int));
                dt.Columns.Add("effective_from", typeof(DateTime));
                dt.Columns.Add("rate", typeof(double));
                if (TDSCode.ref_tds_code_detail!=null)
                {
                    foreach (var i in TDSCode.ref_tds_code_detail)
                    {
                        dt.Rows.Add(i.tds_code_detail_id, i.effective_from, i.rate);
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_tds_code_detail";
                t1.Value = dt;
                int modifyuser = 0;
                var entity = new SqlParameter("@entity", "save");
                var tds_code_id = new SqlParameter("@tds_code_id", TDSCode.tds_code_id);
                var tds_code = new SqlParameter("@tds_code", TDSCode.tds_code);
                var tds_code_description = new SqlParameter("@tds_code_description", TDSCode.tds_code_description);
                var creditor_gl = new SqlParameter("@creditor_gl", TDSCode.creditor_gl);
                var debtor_gl = new SqlParameter("@debtor_gl", TDSCode.debtor_gl);
                var is_blocked = new SqlParameter("@is_blocked", TDSCode.is_blocked);
                var modify_user = new SqlParameter("@modify_user", modifyuser);
                var val = _scifferContext.Database.SqlQuery<string>
                ("exec save_tds_code @entity,@tds_code_id ,@tds_code ,@tds_code_description ,@creditor_gl ,@debtor_gl ,@is_blocked,@modify_user,@t1 ",
                entity,tds_code_id, tds_code, tds_code_description, creditor_gl, debtor_gl, is_blocked, modify_user, t1).FirstOrDefault();
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
            //return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var pt_detail = _scifferContext.ref_tds_code.Find(id);
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

        public tds_codeVM Get(int id)
        {
            try
            {
                ref_tds_code JR = _scifferContext.ref_tds_code.FirstOrDefault(c => c.tds_code_id == id);
                Mapper.CreateMap<ref_tds_code, tds_codeVM>();
                tds_codeVM JRVM = Mapper.Map<ref_tds_code, tds_codeVM>(JR);
                JRVM.ref_tds_code_detail = JRVM.ref_tds_code_detail.Where(c => c.is_active == true).ToList();
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tds_codeVM> getall()
        {
            var query = (from tx in _scifferContext.ref_tds_code
                         join cr in _scifferContext.ref_general_ledger on tx.creditor_gl equals cr.gl_ledger_id
                         join dr in _scifferContext.ref_general_ledger on tx.debtor_gl equals dr.gl_ledger_id
                         select new tds_codeVM()
                         {
                             tds_code_id = tx.tds_code_id,
                             tds_code = tx.tds_code,
                             tds_code_description = tx.tds_code_description,
                             creditor_gl_name = cr.gl_ledger_code +"-"+cr.gl_ledger_name,
                             debtor_gl_name = dr.gl_ledger_code + "-" + dr.gl_ledger_name,
                             is_blocked = tx.is_blocked,
                         }).OrderByDescending(a => a.tds_code_id).ToList();
            return query;
        }

        public List<tds_codeVM> GetAll()
        {
            Mapper.CreateMap<ref_tds_code, tds_codeVM>();
            return _scifferContext.ref_tds_code.Project().To<tds_codeVM>().Where(a => a.is_active == true).ToList();
        }
    }
}
