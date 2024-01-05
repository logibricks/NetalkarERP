using Quartz;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Sciffer.Erp.Web.ScheduleTask
{
    public class jobOrder : IJob
    {

        ScifferContext _scifferContext = new ScifferContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public void Execute(IJobExecutionContext context)
        {
            var val = "";
            try
            {
                var day = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var today = year + "-" + month + "-" + day;
                var posting_date = new SqlParameter("@posting_date", DateTime.Now.Date);
                val = _scifferContext.Database.SqlQuery<string>(
                "exec save_WebPlanMaintenanceOrdder @posting_date", posting_date).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 1;
                log.Error("Exception Occured " + val, ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
            }
        }
    }
    public class TaskSchedule : IJob
    {
        //private readonly IGenericService _genericService;
        //public TaskSchedule(IGenericService genericService)
        //{
        //    _genericService = genericService;
        //}
        ScifferContext _scifferContext = new ScifferContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public void Execute(IJobExecutionContext context)
        {
            var val = "";
            try
            {
                ref_send_mail sm = new ref_send_mail();
                sm = _scifferContext.ref_send_mail.FirstOrDefault();
                var save_entity = new SqlParameter("@entity", "save");
                try
                {
                    val = _scifferContext.Database.SqlQuery<string>(
                          "exec task_schedule @entity", save_entity).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
                if (sm != null)
                {
                    var get_entity = new SqlParameter("@entity", "get");
                    var data = _scifferContext.Database.SqlQuery<ref_send_mail_vm>(
                    "exec task_schedule @entity", get_entity).ToList();
                    foreach (var i in data)
                    {
                        if (i.send_to != null)
                        {
                            if (i.send_to != "")
                            {
                                try
                                {
                                    MailMessage mail = new MailMessage();
                                    mail.To.Add(i.send_to);

                                    if (i.send_cc != null && i.send_cc != "")
                                    {
                                        string[] CCId = i.send_cc.Split(',');
                                        foreach (string CCEmail in CCId)
                                        {
                                            if (CCEmail != "")
                                            {
                                                mail.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                            }

                                        }
                                    }

                                    if (i.send_bcc != null && i.send_bcc != "")
                                    {
                                        string[] BCCId = i.send_bcc.Split(',');
                                        foreach (string BCCEmail in BCCId)
                                        {
                                            if (BCCEmail != "")
                                            {
                                                mail.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple CC email Id
                                            }

                                        }
                                    }
                                    mail.From = new MailAddress(sm.send_from);
                                    mail.Subject = i.subject;
                                    mail.Body = i.body;
                                    mail.IsBodyHtml = true;
                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = sm.host_name;
                                    smtp.Port = sm.port_name;
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = new System.Net.NetworkCredential(sm.send_from, sm.send_password); // Enter seders User name and password  
                                    smtp.EnableSsl = sm.EnableSsl;
                                    smtp.Send(mail);
                                }
                                catch (Exception ex)
                                {

                                }
                                finally
                                {
                                    GC.Collect();
                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 1;
                log.Error("Exception Occured " + val, ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}