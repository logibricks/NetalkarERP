using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class AssetClassService : IAssetClassService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public AssetClassService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }

        public string Add(ref_asset_class_vm fin_map, List<ref_asset_class_gl_vm> fin_detail, List<ref_asset_class_depreciation_vm> dip_detail)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                dt.Columns.Add("asset_class_gl_id", typeof(int));
                dt.Columns.Add("ledger_account_type_id", typeof(int));
                dt.Columns.Add("gl_id", typeof(int));
                foreach (var d in fin_detail)
                {
                    if (d.ledger_account_type_id != null)
                    {
                        dt.Rows.Add(d.asset_class_gl_id == 0 ? 0 : d.asset_class_gl_id,
                            d.ledger_account_type_id == null ? 0 : d.ledger_account_type_id,
                            d.gl_id == 0 ? 0 : d.gl_id);
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_financial_mapping";
                t1.Value = dt;

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("asset_class_dep_id", typeof(int));
                dt1.Columns.Add("dep_area_id", typeof(int));
                dt1.Columns.Add("dep_type_id", typeof(int));
                dt1.Columns.Add("dep_type_frquency_id", typeof(int));
                dt1.Columns.Add("useful_life_months", typeof(int));
                dt1.Columns.Add("useful_life_rate", typeof(int));
                dt1.Columns.Add("is_blocked", typeof(bool));
                foreach (var d in dip_detail)
                {
                    if (d.useful_life_months != null)
                    {
                        dt1.Rows.Add(d.asset_class_dep_id == 0 ? 0 : d.asset_class_dep_id,
                            d.dep_area_id == null ? 0 : d.dep_area_id,
                            d.dep_type_id == 0 ? 0 : d.dep_type_id,
                            d.dep_type_frquency_id == 0 ? 0 : d.dep_type_frquency_id,
                            d.useful_life_months == 0 ? 0 : d.useful_life_months,
                            d.useful_life_rate == 0 ? 0 : d.useful_life_rate,
                            d.is_blocked = false
                            );
                    }
                }
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_depriciation";
                t2.Value = dt1;
                var asset_class_id = new SqlParameter("@asset_class_id", fin_map.asset_class_id == null ? 0 : fin_map.asset_class_id);
                var asset_class_code = new SqlParameter("@asset_class_code", fin_map.asset_class_code == null ? string.Empty : fin_map.asset_class_code);
                var asset_class_des = new SqlParameter("@asset_class_des", fin_map.asset_class_des == null ? string.Empty : fin_map.asset_class_des);
                var asset_type_id = new SqlParameter("@asset_type_id", fin_map.asset_type_id == null ? 0 : fin_map.asset_type_id);
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var created_by = new SqlParameter("@user", user);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_asset_class @asset_class_id ,@asset_class_code,@asset_class_des ,@asset_type_id,@user,@t1,@t2 ",
                    asset_class_id, asset_class_code, asset_class_des, asset_type_id, created_by, t1,t2).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    var mrn_no = val;
                    return mrn_no;
                }
                else
                {
                    return "error";
                }

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "error";
            }
            //return true;

        }
        public List<ref_asset_class_vm> getall()
        {
            var val = (from r in _scifferContext.ref_asset_class
                       select new ref_asset_class_vm
                       {
                           asset_class_id = r.asset_class_id,
                           asset_class_code = r.asset_class_code,
                           asset_class_des = r.asset_class_des,
                           asset_type = r.asset_type_id == 1 ? "TANGIBLE" :r.asset_type_id ==2 ? "INTANGIBLE":"",
                       }).ToList();
            return val;
        }
        public ref_asset_class_vm Get(int id)
        {
            ref_asset_class so = _scifferContext.ref_asset_class.FirstOrDefault(c => c.asset_class_id == id);
            Mapper.CreateMap<ref_asset_class, ref_asset_class_vm>().ForMember(dest => dest.asset_class_id, opt => opt.Ignore());
            ref_asset_class_vm sovm = Mapper.Map<ref_asset_class, ref_asset_class_vm>(so);
            //sovm.ref_asset_class_gl = sovm.ref_asset_class_gl.Where(x => x.asset_class_id == id).ToList();
            //sovm.ref_asset_class_depreciation = sovm.ref_asset_class_depreciation.Where(x => x.asset_class_id == id).ToList();
            var gl =(from r in _scifferContext.ref_asset_class_gl.Where(x => x.asset_class_id == id)
                                       join s in _scifferContext.ref_ledger_account_type on r.ledger_account_type_id equals s.ledger_account_type_id
                                       join g in _scifferContext.ref_general_ledger on r.gl_id equals g.gl_ledger_id
                                       select new ref_asset_class_gl_vm
                                       {
                                                     asset_class_gl_id = r.asset_class_gl_id,
                                                     ledger_account_type_name = s.ledger_account_type_name,
                                                     gl_ledger_code = g.gl_ledger_code +"-"+g.gl_ledger_name,
                                                 }).ToList();
            sovm.ref_asset_class_gl_vm = gl;
            var dep = (from r in _scifferContext.ref_asset_class_depreciation.Where(x => x.asset_class_id == id)
                       join s in _scifferContext.ref_dep_area on r.dep_area_id equals s.dep_area_id
                       join g in _scifferContext.ref_dep_type on r.dep_type_id equals g.dep_type_id
                       select new ref_asset_class_depreciation_vm
                       {
                           asset_class_dep_id = r.asset_class_dep_id,
                           dep_area_code = s.dep_area_code + "-" + s.dep_area_description,
                           dep_type_code = g.dep_type_code + "-" + g.dep_type_description,
                           dep_type_frquency = r.dep_type_frquency_id == 1 ? "MONTHLY" : r.dep_type_frquency_id == 2 ? "abc" : "",
                           useful_life_months = r.useful_life_months,
                           useful_life_rate = r.useful_life_rate,
                      }).ToList();
            sovm.ref_asset_class_depreciation_vm = dep;
            return sovm;
        }
    }
}
