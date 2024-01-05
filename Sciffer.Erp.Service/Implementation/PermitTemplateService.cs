using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using System.Linq;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class PermitTemplateService : IPermitTemplateService
    {
        private readonly ScifferContext _scifferContext;

        public PermitTemplateService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        //public bool Add(Ref_permit_template_VM country)
        //{
        //    try
        //    {
        //        country.is_blocked = true;
        //        _scifferContext.Ref_permit_template.Add(country);
        //        _scifferContext.SaveChanges();
        //        country.permit_template_id = _scifferContext.Ref_permit_template.Max(x => x.permit_template_id);
        //    }
        //    catch (Exception)
        //    {
        //        return country;
        //    }
        //    return country;
        //}


        public bool Add(Ref_permit_template_VM item)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("checkpoint_id", typeof(int));
                dt.Columns.Add("checkpoints", typeof(string));
                dt.Columns.Add("ideal_scenario", typeof(string));
                if(item.checkpoint_id.Count > 0)
                {
                    for (var i = 0; i < item.checkpoint_id.Count; i++)
                    {
                        dt.Rows.Add(item.checkpoint_id[i]=="0" ? -1 : int.Parse(item.checkpoint_id[i]), item.checkpoints[i], item.ideal_scenario[i]);
                    }
                }
                
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_checkpoints_details1";
                t1.Value = dt;
                
                var permit_template_id = new SqlParameter("@permit_template_id", item.permit_template_id == 0 ? -1 : item.permit_template_id);
                var permit_template_no = new SqlParameter("@permit_template_no", item.permit_template_no);
                var permit_category = new SqlParameter("@permit_category", item.permit_category);
               var deleteids = new SqlParameter("@deleteids", item.deleteids==null? "" : item.deleteids);

                var val = _scifferContext.Database.SqlQuery<string>("exec Save_Permit_Template @permit_template_id,@permit_template_no,@permit_category,@deleteids,@t1",
                     permit_template_id, permit_template_no, permit_category, deleteids, t1).FirstOrDefault();
             
                if (val == "Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Ref_permit_template_VM item)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("checkpoint_id", typeof(int));
                dt.Columns.Add("checkpoints", typeof(string));
                dt.Columns.Add("ideal_scenario", typeof(string));
                if (item.checkpoint_id.Count > 0)
                {
                    for (var i = 0; i < item.checkpoint_id.Count; i++)
                    {
                        dt.Rows.Add(item.checkpoint_id[i] == "0" ? -1 : int.Parse(item.checkpoint_id[i]), item.checkpoints[i], item.ideal_scenario[i]);
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_checkpoints_details1";
                t1.Value = dt;

                var permit_template_id = new SqlParameter("@permit_template_id", item.permit_template_id == 0 ? -1 : item.permit_template_id);
                var permit_template_no = new SqlParameter("@permit_template_no", item.permit_template_no);
                var permit_category = new SqlParameter("@permit_category", item.permit_category);

                 var val = _scifferContext.Database.SqlQuery<string>("exec Save_Permit_Template @permit_template_id,@permit_template_no,@permit_category,@t1",
                     permit_template_id, permit_template_no, permit_category, t1).FirstOrDefault();

               /* var deleteids = new SqlParameter("@deleteids", item.deleteids==null? "" : item.deleteids);

                 var val = _scifferContext.Database.SqlQuery<string>("exec Save_Permit_Template @permit_template_id,@permit_template_no,@permit_category,@deleteids,@t1",
                     permit_template_id, permit_template_no, permit_category, deleteids, t1).FirstOrDefault();*/

                if (val == "Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var country = _scifferContext.Ref_permit_template.FirstOrDefault(c => c.permit_template_id == id);
                country.is_blocked = false;
                _scifferContext.Entry(country).State = EntityState.Modified;
                _scifferContext.SaveChanges();
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

        public Ref_permit_template_VM Get(int id)
        {
            //var country = _scifferContext.Ref_permit_template.FirstOrDefault(c => c.permit_template_id == id);

            try
            {
                Ref_permit_template JR = _scifferContext.Ref_permit_template.FirstOrDefault(c => c.permit_template_id == id);
                Mapper.CreateMap<Ref_permit_template, Ref_permit_template_VM>();
                Ref_permit_template_VM JRVM = Mapper.Map<Ref_permit_template, Ref_permit_template_VM>(JR);
                JRVM.Ref_checkpoints = JRVM.Ref_checkpoints.Where(x=>x.is_blocked==true).ToList();
               // JRVM.gl_ledger_code = JR.ref_general_ledger.gl_ledger_code;
               // JRVM.gl_ledger_name = JR.ref_general_ledger.gl_ledger_name;
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           // return country;
        }

        public List<Ref_permit_template_VM> GetAll()
        {
            //return _scifferContext.Ref_permit_template.ToList().Where(x => x.is_blocked == false).ToList();
            var vax = (from ed in _scifferContext.Ref_permit_template.Where(x => x.is_blocked == true)
                       select new Ref_permit_template_VM
                       {
                           permit_template_id = ed.permit_template_id,
                           permit_template_no = ed.permit_template_no,
                           permit_category = ed.permit_category,
                       }).ToList();
            return vax;
        }

       /* public bool Update(Ref_permit_template_VM country)
        {
            try
            {
                country.is_blocked = true;
                _scifferContext.Entry(country).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }*/
    }
}
