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
    public class DepreciationTypeService : IDepreciationTypeService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public DepreciationTypeService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }

        public string Add(ref_dep_type_vm add_depre)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                dt.Columns.Add("add_dep_type_id", typeof(int));
                dt.Columns.Add("year_id", typeof(int));
                dt.Columns.Add("add_dep_percentage", typeof(decimal));
                int k = 1;
                if (add_depre.add_dep_percentage != null)
                {
                    for (var i = 0; i < add_depre.add_dep_percentage.Count; i++)
                    {
                        if (add_depre.add_dep_percentage[i] != "")
                        {
                            dt.Rows.Add(add_depre.add_dep_type_id[i] == "" ? 0 : int.Parse(add_depre.add_dep_type_id[i]),
                                add_depre.year_id[i] == "" ? 0 : int.Parse(add_depre.year_id[i]),
                                add_depre.add_dep_percentage[i] == "" ? 0 : double.Parse(add_depre.add_dep_percentage[i]));
                            k = k + 1;
                        }
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_add_depreciation";
                t1.Value = dt;
                var dep_type_id = new SqlParameter("@dep_type_id", add_depre.dep_type_id == null ? 0 : add_depre.dep_type_id);
                var dep_type_code = new SqlParameter("@dep_type_code", add_depre.dep_type_code == null ? string.Empty : add_depre.dep_type_code);
                var dep_type_description = new SqlParameter("@dep_type_description", add_depre.dep_type_description == null ? string.Empty : add_depre.dep_type_description);
                var method_type_id = new SqlParameter("@method_type_id", add_depre.method_type_id == null ? 0 : add_depre.method_type_id);
                var dep_type_frquency_id = new SqlParameter("@dep_type_frquency_id", add_depre.dep_type_frquency_id == null ? 0 : add_depre.dep_type_frquency_id);
                var dep_area_id = new SqlParameter("@dep_area_id", add_depre.dep_area_id == null ? 0 : add_depre.dep_area_id);
                var cal_based_on_id = new SqlParameter("@cal_based_on_id", add_depre.cal_based_on_id == null ? 0 : add_depre.cal_based_on_id);
                var scrap_value_percentage = new SqlParameter("@scrap_value_percentage", add_depre.scrap_value_percentage == null ? 0 : add_depre.scrap_value_percentage);
                var cal_based_value = new SqlParameter("@cal_based_value", add_depre.cal_based_value == null ? 0 : add_depre.cal_based_value);
                var dep_cal_acquistion_id = new SqlParameter("@dep_cal_acquistion_id", add_depre.dep_cal_acquistion_id == null ? 0 : add_depre.dep_cal_acquistion_id);
                var dep_cal_retirement_id = new SqlParameter("@dep_cal_retirement_id", add_depre.dep_cal_retirement_id == null ? 0 : add_depre.dep_cal_retirement_id);               
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var created_by = new SqlParameter("@user", user);

                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_depreciation_type @dep_type_id ,@dep_type_code,@dep_type_description ,@method_type_id ,@dep_type_frquency_id ,@dep_area_id ,@cal_based_on_id ," +
                    "@scrap_value_percentage ,@cal_based_value ,@dep_cal_acquistion_id ,@dep_cal_retirement_id ,@user,@t1 ",
                    dep_type_id, @dep_type_code, dep_type_description, method_type_id, dep_type_frquency_id, dep_area_id,cal_based_on_id,scrap_value_percentage ,cal_based_value ,dep_cal_acquistion_id ,dep_cal_retirement_id , created_by, t1 ).FirstOrDefault();

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
        public List<ref_dep_type_vm> getall()
        {
            var val = _scifferContext.Database.SqlQuery<ref_dep_type_vm>(
                "exec get_all_depreciation_type_data").ToList();
            return val;
        }
        public ref_dep_type_vm Get(int id)
        {
            ref_dep_type so = _scifferContext.ref_dep_type.FirstOrDefault(c => c.dep_type_id == id);
            Mapper.CreateMap<ref_dep_type, ref_dep_type_vm>().ForMember(dest => dest.dep_type_id, opt => opt.Ignore());
            ref_dep_type_vm sovm = Mapper.Map<ref_dep_type, ref_dep_type_vm>(so);
            sovm.ref_additional_dep_type = sovm.ref_additional_dep_type.Where(x => x.dep_type_id == id).ToList();
            return sovm;
        }
    }
}
