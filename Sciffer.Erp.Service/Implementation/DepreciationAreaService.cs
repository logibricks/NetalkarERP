using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;
using System.Web;
using Sciffer.Erp.Domain.ViewModel;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class DepreciationAreaService : IDepreciationAreaService
    {
        private readonly ScifferContext _scifferContext;

        public DepreciationAreaService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public string Add(ref_dep_area_vm depdata, List<ref_dep_posting_period_vm> DepParaArr)
        {
            try
            {

                DataTable dtf = new DataTable();
                dtf.Columns.Add("dep_area_posting_period_id", typeof(int));
                dtf.Columns.Add("financial_year_id", typeof(int));
                dtf.Columns.Add("posting_periods_code", typeof(string));
                dtf.Columns.Add("from_date", typeof(DateTime));
                dtf.Columns.Add("to_date", typeof(DateTime));
                dtf.Columns.Add("status_id", typeof(int));

               
              
                if (DepParaArr != null)
                {
                    var finids = depdata.financial_year_id_selected.Split(',');
                    var count = 0;
                    var from = "";
                    var to = "";
                    var fincount = finids.Count();

                    if (depdata.frequency_id == 1)
                    {
                        
                        for (int j = 0; j < fincount; j++)
                        {
                            var fin_id = Convert.ToInt32(finids[j]);
                            var finance = _scifferContext.REF_FINANCIAL_YEAR.Where(a => a.FINANCIAL_YEAR_ID == fin_id).FirstOrDefault();
                            var filt = DepParaArr.Where(a => a.financial_year_id == fin_id).ToList();
                            for (int i = 0; i < filt.Count(); i++)
                            {
                                

                                DateTime dt = finance.FINANCIAL_YEAR_FROM;
                                DateTime dt1 = finance.FINANCIAL_YEAR_TO;
                                DateTime dt2 = DateTime.Now;
                                DateTime dt3 = DateTime.Now;

                               
                                    from = dt.ToString();
                                    to = dt1.ToString(); ;
                               
                                dtf.Rows.Add(filt[i].dep_area_posting_period_id,
                                          filt[i].financial_year_id,
                                         filt[i].posting_periods_code,
                                         from,
                                         to,
                                         1
                                          );
                            }
                        }

                    }
                    else
                    {
                        count = 11;
                        for (int j = 0; j < fincount; j++)
                        {
                            var fin_id = Convert.ToInt32(finids[j]);
                            var finance = _scifferContext.REF_FINANCIAL_YEAR.Where(a => a.FINANCIAL_YEAR_ID == fin_id).FirstOrDefault();
                            var filt = DepParaArr.Where(a => a.financial_year_id == fin_id).ToList();
                            for (int i = 0; i < filt.Count(); i++)
                            {

                                DateTime dt = finance.FINANCIAL_YEAR_FROM;
                                DateTime dt1 = finance.FINANCIAL_YEAR_TO;
                                DateTime dt2 = DateTime.Now;
                                DateTime dt3 = DateTime.Now;


                                if (i == 0)
                                {
                                    from = dt.ToString(); ;
                                    to = new DateTime(dt.Year, 4, 30).ToString(); ;
                                }
                                else if (i == 1)
                                {
                                    from = new DateTime(dt.Year, 5, 1).ToString(); ;
                                    to = new DateTime(dt.Year, 5, 31).ToString(); ;
                                }
                                else if (i == 2)
                                {
                                    from = new DateTime(dt.Year, 6, 1).ToString(); ;
                                    to = new DateTime(dt.Year, 6, 30).ToString(); ;
                                }
                                else if (i == 3)
                                {
                                    from = new DateTime(dt.Year, 7, 1).ToString(); ;
                                    to = new DateTime(dt.Year, 7, 31).ToString(); ;
                                }
                                else if (i == 4)
                                {
                                    from = new DateTime(dt.Year, 8, 1).ToString(); ;
                                    to = new DateTime(dt.Year, 8, 31).ToString(); ;
                                }
                                else if (i == 5)
                                {
                                    from = new DateTime(dt.Year, 9, 1).ToString(); ;
                                    to = new DateTime(dt.Year, 9, 30).ToString(); ;
                                }
                                else if (i == 6)
                                {
                                    from = new DateTime(dt.Year, 10, 1).ToString(); ;
                                    to = new DateTime(dt.Year, 10, 31).ToString(); ;
                                }
                                else if (i == 7)
                                {
                                    from = new DateTime(dt.Year, 11, 1).ToString(); ;
                                    to = new DateTime(dt.Year, 11, 30).ToString(); ;
                                }
                                else if (i == 8)
                                {
                                    from = new DateTime(dt.Year, 12, 1).ToString(); ;
                                    to = new DateTime(dt.Year, 12, 31).ToString(); ;
                                }
                                else if (i == 9)
                                {
                                    from = new DateTime(dt1.Year, 1, 1).ToString(); ;
                                    to = new DateTime(dt1.Year, 1, 31).ToString(); ;
                                }
                                else if (i == 10)
                                {
                                    int daysInFeb = System.DateTime.DaysInMonth(dt1.Year, 2);
                                    from = new DateTime(dt1.Year, 2, 1).ToString(); ;
                                    to = new DateTime(dt1.Year, 2, daysInFeb).ToString(); ;
                                }
                                else if (i == 11)
                                {
                                    from = new DateTime(dt1.Year, 3, 1).ToString(); ;
                                    to = new DateTime(dt1.Year, 3, 31).ToString(); ;
                                }

                                dtf.Rows.Add(filt[i].dep_area_posting_period_id,
                                   filt[i].financial_year_id,
                                  filt[i].posting_periods_code,
                                  from,
                                  to,
                                  1
                                   );

                            }
                        }
                    }
           
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_dep_posting_period_detail";
                t1.Value = dtf;

                DateTime dte = new DateTime(0001, 01, 01);
                DateTime dte1 = new DateTime(1990, 01, 01);
                var created_by1 = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var dep_area_id = new SqlParameter("@dep_area_id", depdata.dep_area_id == null ? 0 : depdata.dep_area_id);
                var dep_area_code = new SqlParameter("@dep_area_code", depdata.dep_area_code == null ? "" : depdata.dep_area_code);
                var dep_area_description = new SqlParameter("@dep_area_description", depdata.dep_area_description == null? "" : depdata.dep_area_description);
                var dep_type_id = new SqlParameter("@dep_type_id", depdata.dep_type_id == null ? 0 : depdata.dep_type_id);
                var dep_posting_id = new SqlParameter("@dep_posting_id", depdata.dep_posting_id == null ? 0 : depdata.dep_posting_id);
                var dep_type_frquency_id = new SqlParameter("@dep_type_frquency_id", depdata.frequency_id == null ? 0 : depdata.frequency_id);
                var no_of_periods = new SqlParameter("@no_of_periods", depdata.no_of_periods == null ? 0 : depdata.no_of_periods);             
                var created_by = new SqlParameter("@created_by", created_by1);
                var is_blocked = new SqlParameter("@is_blocked", depdata.is_blocked == false ? false : true);
                var financial_year_id = new SqlParameter("@financial_year_id", depdata.financial_year_id_selected == null ? "" : depdata.financial_year_id_selected);
                var val = _scifferContext.Database.SqlQuery<string>("exec save_dep_area @dep_area_id ,@dep_area_code ,@dep_area_description ,@dep_type_id ,@dep_posting_id ,@dep_type_frquency_id ,@no_of_periods,@created_by ,@is_blocked,@financial_year_id, @t1 ", dep_area_id, dep_area_code, dep_area_description, dep_type_id, dep_posting_id, dep_type_frquency_id, no_of_periods, created_by, is_blocked, financial_year_id, t1).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    return val;
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.InnerException.Message;
            }
        }

        public List<ref_dep_area_vm> GetAll()
        {
            var query = (from order in _scifferContext.ref_dep_area.Where(x => x.is_blocked == false).OrderByDescending(x => x.dep_area_id)
                         
                         join fre in _scifferContext.ref_frequency on order.dep_type_frquency_id equals fre.frequency_id into fre1
                         from fre2 in fre1.DefaultIfEmpty()                       
                         //join status in _scifferContext.ref_status on order.status_id equals status.status_id into status1
                         //from status2 in status1.DefaultIfEmpty()
                         select new ref_dep_area_vm()
                         {
                             dep_area_id = order.dep_area_id,
                             dep_area_code = order.dep_area_code,
                             dep_area_description = order.dep_area_description,
                             dep_area_frequency = fre2.frequency_name,
                             dep_type = order.dep_type_id == 1 ? "Posting to GL" : order.dep_type_id == 2 ? "Informative" : "",
                             dep_posting = order.dep_posting_id == 1 ? "Direct to Asset" : order.dep_posting_id == 2 ? "Accumulated Depreciation" : "",
                             //status_name = status2 == null ? "" : status2.status_name
                             no_of_periods = order.no_of_periods,

                         }).OrderByDescending(x => x.dep_area_id).ToList();
            return query;
        }
        public ref_dep_area Update(ref_dep_area dep_area)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                dep_area.is_blocked = dep_area.is_blocked;
                dep_area.created_by = user;
                dep_area.created_ts = DateTime.Now;
                dep_area.modify_by = user;
                dep_area.modify_ts = DateTime.Now;
                _scifferContext.Entry(dep_area).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return dep_area;
            }
            return dep_area;
        }

        public ref_dep_area_vm Get(int id)
        {
            ref_dep_area po = _scifferContext.ref_dep_area.FirstOrDefault(c => c.dep_area_id == id && c.is_blocked == false);
            Mapper.CreateMap<ref_dep_area, ref_dep_area_vm>();
            ref_dep_area_vm mmv = Mapper.Map<ref_dep_area, ref_dep_area_vm>(po);


           var mv = (from detail in _scifferContext.ref_dep_posting_period.Where(a => a.dep_area_id == po.dep_area_id)
                                                       
                                            select new ref_dep_posting_period_vm
                                            {

                                            dep_area_posting_period_id = detail.dep_area_posting_period_id,
                                            financial_year_id = detail.financial_year_id,
                                            posting_periods_code = detail.posting_periods_code,

                                            }).ToList();

            mmv.ref_dep_posting_period_vm = mv;
            return mmv;

        }
    }
}
