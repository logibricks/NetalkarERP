using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;

namespace Sciffer.Erp.Service.Implementation
{
    public class SMSService : ISMSService
    {
        public string sendSMS(string number, string message)
        {
            try
            {
                string no= "";string numbers = "";
                var mobile = number.Split(',');
                foreach(var i in mobile)
                {
                    no = i.Substring(i.Length - 10);
                    if (no.Length != 10)
                    {
                        return "Check mobile no.!";
                    }
                    numbers = numbers + "91" + no;
                }
                
                var apikey = ConfigurationManager.AppSettings["smskey"];
                using (var wb = new WebClient())
                {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , apikey},
                {"numbers" , numbers}, 
                {"message" , message},
                { "sender" , "NPTCOM"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
                }
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                GC.Collect();
            }
            
        }
    }
}
