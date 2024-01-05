using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class InjobWorkService : IInjobWorkService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public InjobWorkService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string DuplicateChalanNo(string no)
        {
            var dupli = _scifferContext.in_jobwork_in.Where(x => x.customer_chalan_no == no).FirstOrDefault();
            return dupli == null ? "" : dupli.customer_chalan_no ;
        }
        public string Add(in_jobwork_in_VM JobWorkIn)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                dt.Columns.Add("job_work_detail_in_id ", typeof(int));
                dt.Columns.Add("item_id ", typeof(int));
                dt.Columns.Add("quantity ", typeof(double));
                dt.Columns.Add("uom_id ", typeof(int));
                dt.Columns.Add("batch ", typeof(string));
                dt.Columns.Add("sloc_id ", typeof(int));
                dt.Columns.Add("bucket_id ", typeof(int));
                dt.Columns.Add("rate ", typeof(float));

                if (JobWorkIn.Item_id!=null)
                {
                    for (int i = 0; i < JobWorkIn.Item_id.Count; i++)
                    {
                        if(JobWorkIn.Item_id[i]!="")
                        {
                            dt.Rows.Add(JobWorkIn.job_Work_detail_in_id[i] == "" ? 0 : int.Parse(JobWorkIn.job_Work_detail_in_id[i]),
                                JobWorkIn.Item_id[i] == "" ? 0 : int.Parse(JobWorkIn.Item_id[i]),
                                JobWorkIn.Quantity[i] == "" ? 0 : Double.Parse(JobWorkIn.Quantity[i]),
                                JobWorkIn.uom_id[i] == "" ? 0 : int.Parse(JobWorkIn.uom_id[i]),
                                JobWorkIn.batch[i], JobWorkIn.sloc_id[i] == "" ? 0 : int.Parse(JobWorkIn.sloc_id[i]),
                                JobWorkIn.bucket_id[i] == "" ? 0 : int.Parse(JobWorkIn.bucket_id[i]),
                                JobWorkIn.rate[i] == "" ? 0 : Double.Parse(JobWorkIn.rate[i]));
                }  
                    }
                }
                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var job_work_in_id = new SqlParameter("@job_work_in_id", JobWorkIn.job_work_in_id);
                var category_id = new SqlParameter("@category_id", JobWorkIn.category_id);
                var document_no = new SqlParameter("@document_no", JobWorkIn.document_no==null?string.Empty:JobWorkIn.document_no);
                var posting_date = new SqlParameter("@posting_date", JobWorkIn.posting_date);
                var customer_id = new SqlParameter("@customer_id", JobWorkIn.customer_id);
                var business_unit_id = new SqlParameter("@business_unit_id", JobWorkIn.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", JobWorkIn.plant_id);
                var customer_chalan_no = new SqlParameter("@customer_chalan_no", JobWorkIn.customer_chalan_no);
                var customer_chalan_date = new SqlParameter("@customer_chalan_date", JobWorkIn.customer_chalan_date==null?dte:JobWorkIn.customer_chalan_date);
                var get_entry_no = new SqlParameter("@get_entry_no", JobWorkIn.gate_entry_no==null?string.Empty:JobWorkIn.gate_entry_no);
                var get_entry_date = new SqlParameter("@get_entry_date", JobWorkIn.gate_entry_date==null?dte:JobWorkIn.gate_entry_date);
                var state_id = new SqlParameter("@state_id", JobWorkIn.state_id);
                var billing_address = new SqlParameter("@billing_address", JobWorkIn.billing_address==null?string.Empty:JobWorkIn.billing_address);
                var billing_city = new SqlParameter("@billing_city", JobWorkIn.billing_city==null?string.Empty:JobWorkIn.billing_city);
                var pin_code = new SqlParameter("@pin_code", JobWorkIn.pin_code==null?string.Empty:JobWorkIn.pin_code);
                var email = new SqlParameter("@email", JobWorkIn.email==null?"":JobWorkIn.email);
                var created_by = new SqlParameter("@created_by", createdby);             
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_job_work_detail_in";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_job_work_in @job_work_in_id ,@category_id ,@document_no ,@posting_date ,@customer_id ,@business_unit_id ,@plant_id ,@customer_chalan_no ,@customer_chalan_date ,@get_entry_no ,@get_entry_date ,@state_id ,@billing_address ,@billing_city ,@pin_code ,@email ,@created_by ,@t1 ", 
                    job_work_in_id, category_id, document_no, posting_date, customer_id, business_unit_id, plant_id, customer_chalan_no, 
                    customer_chalan_date, get_entry_no, get_entry_date, state_id, billing_address, billing_city, pin_code, email, 
                    created_by, t1).FirstOrDefault();
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
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return "error";
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

        public in_jobwork_in_VM Get(int id)
        {
            in_jobwork_in injobwork = _scifferContext.in_jobwork_in.FirstOrDefault(X => X.job_work_in_id == id);
            Mapper.CreateMap<in_jobwork_in, in_jobwork_in_VM>();
            in_jobwork_in_VM injobwork_vm= Mapper.Map<in_jobwork_in, in_jobwork_in_VM>(injobwork);
            injobwork_vm.in_jobwork_detail = injobwork_vm.in_jobwork_detail.ToList();
            injobwork_vm.Country_id = injobwork.REF_STATE.COUNTRY_ID;
            return injobwork_vm;
        }

        public List<in_jobwork_in_VM> getall()
        {
            var ent = new SqlParameter("@entity", "getall");
            var getDetails = _scifferContext.Database.SqlQuery<in_jobwork_in_VM>(
                "exec get_JobWork @entity", ent).ToList();
            //var getDetails = (from jin in _scifferContext.in_jobwork_in
            //                  join cat in _scifferContext.ref_document_numbring on jin.category_id equals cat.document_numbring_id into subpet1
            //                  from ct in subpet1.DefaultIfEmpty()
            //                  join cust in _scifferContext.REF_CUSTOMER on jin.customer_id equals cust.CUSTOMER_ID
            //                  join buss in _scifferContext.REF_BUSINESS_UNIT on jin.business_unit_id equals buss.BUSINESS_UNIT_ID
            //                  join plant in _scifferContext.REF_PLANT on jin.plant_id equals plant.PLANT_ID
            //                  join st in _scifferContext.REF_STATE on jin.state_id equals st.STATE_ID
            //                  select new
            //                  {
            //                      job_work_in_id = jin.job_work_in_id,
            //                      category_id = jin.category_id,
            //                      categoryName = ct.category,
            //                      posting_date = jin.posting_date,
            //                      customer_id = jin.customer_id,
            //                      CustomerName = cust.CUSTOMER_NAME,
            //                      business_unit_id = jin.business_unit_id,
            //                      business_UnitName = buss.BUSINESS_UNIT_NAME,
            //                      plant_id = jin.plant_id,
            //                      plant_name = plant.PLANT_NAME,
            //                      customer_chalan_no = jin.customer_chalan_no,
            //                      customer_chalan_date = jin.customer_chalan_date,
            //                      state_id = jin.state_id,
            //                      state_name = st.STATE_NAME,
            //                      billing_address = jin.billing_address,
            //                      billing_city = jin.billing_city,
            //                      pin_code = jin.pin_code,
            //                      email = jin.email,
            //                      document_no=jin.document_no,
            //                      gate_entry_date = jin.gate_entry_date,
            //                      gate_entry_no=jin.gate_entry_no,
            //                      Country_name=st.REF_COUNTRY.COUNTRY_NAME,
            //                  }).ToList()
            //                  .Select(x => new in_jobwork_in_VM
            //                  {
            //                      job_work_in_id = x.job_work_in_id,
            //                      category_id = x.category_id,
            //                      categoryName = x.categoryName,
            //                      posting_date = x.posting_date,
            //                      customer_id = x.customer_id,
            //                      CustomerName = x.CustomerName,
            //                      business_unit_id = x.business_unit_id,
            //                      business_UnitName = x.business_UnitName,
            //                      plant_id = x.plant_id,
            //                      plant_name = x.plant_name,
            //                      customer_chalan_no = x.customer_chalan_no,
            //                      customer_chalan_date = x.customer_chalan_date,
            //                      state_id = x.state_id,
            //                      state_name = x.state_name,
            //                      billing_address = x.billing_address,
            //                      billing_city = x.billing_city,
            //                      pin_code = x.pin_code,
            //                      email = x.email,
            //                      document_no=x.document_no,
            //                      gate_entry_date = x.gate_entry_date,
            //                      gate_entry_no=x.gate_entry_no,
            //                      Country_name=x.Country_name,
            //                  }).OrderByDescending(a => a.job_work_in_id).ToList();
            return getDetails;
        }

        public string Update(in_jobwork_in_VM JobWorkIn)
        {
            try
            {
                in_jobwork_in ij = _scifferContext.in_jobwork_in.Where(x => x.job_work_in_id == JobWorkIn.job_work_in_id).FirstOrDefault();
                ij.customer_chalan_no = JobWorkIn.customer_chalan_no;
                ij.customer_chalan_date = JobWorkIn.customer_chalan_date;
                _scifferContext.Entry(ij).State = System.Data.Entity.EntityState.Modified;

                List<in_jobwork_in_detail> ijd = _scifferContext.in_jobwork_in_detail.Where(x => x.job_work_in_id == JobWorkIn.job_work_in_id).ToList();
                foreach (var xx in ijd)
                {
                    for (var i = 0; i < JobWorkIn.rate.Count; i++)
                    {
                        xx.batch = JobWorkIn.customer_chalan_no;
                        xx.rate = double.Parse(JobWorkIn.rate[i]);
                    }
                    _scifferContext.Entry(xx).State = System.Data.Entity.EntityState.Modified;
                }

                inv_item_batch iib = _scifferContext.inv_item_batch.Where(x => x.document_id == JobWorkIn.job_work_in_id && x.document_code == "JWI").FirstOrDefault();
                iib.batch_number = JobWorkIn.customer_chalan_no;
                _scifferContext.Entry(iib).State = System.Data.Entity.EntityState.Modified;

                _scifferContext.SaveChanges();
                return "~Saved" + ij.document_no;
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
            }
        }
        public List<in_jobwork_in_VM> getcustomerlistplantwise(int id)
        {
            var ent = new SqlParameter("@entity", "getcustomer");
            var getDetails = _scifferContext.Database.SqlQuery<in_jobwork_in_VM>(
                "exec get_JobWork @entity", ent).Where(a => a.document_numbring_id == id).ToList();
            return getDetails;
        }

        public string Delete(int id, string cancellation_remarks, int cancellation_reason_id)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var pi_id = new SqlParameter("@job_work_in_id", id);
                var remarks = new SqlParameter("@cancellation_remarks", cancellation_remarks == null ? "" : cancellation_remarks);
                var created_by = new SqlParameter("@created_by", create_user);
                var cancellation_reason = new SqlParameter("@cancellation_reason_id", cancellation_reason_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec cancel_jobwork_in @job_work_in_id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", pi_id, remarks, created_by, cancellation_reason).FirstOrDefault();
                return val;
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
            }
        }
    }
}
