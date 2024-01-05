using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sciffer.Erp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService _login;
        private readonly IMachineEntryService _machineService;
        public HomeController(ILoginService login, IMachineEntryService MachineEntry)
        {
            _login = login;
            _machineService = MachineEntry;
        }

        [CustomAuthorizeAttribute("Home")]
        public ActionResult Index()
        {
            int user_id = int.Parse(Session["User_Id"].ToString());
            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            if (_machineService.GetOperatorPhoto(user_id) != null)
            {
                Session["OperatorPhoto"] = _machineService.GetOperatorPhoto(user_id).Trim();
            }
            else
            {
                Session["OperatorPhoto"] = "";
            }
            if (checkoperator == true)
            {
                return RedirectToAction("Index", "MachineEntry");
            }
           
            return View();
        }
        public string GetComputerName(string clientIP)
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(clientIP);
                return hostEntry.HostName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetMACAddress()
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            Console.WriteLine("Interface information for {0}.{1}     ",
                    computerProperties.HostName, computerProperties.DomainName);
            if (nics == null || nics.Length < 1)
            {
                return "";
            }
            String sMacAddress = string.Empty;

            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                PhysicalAddress address = adapter.GetPhysicalAddress();
                byte[] bytes = address.GetAddressBytes();

                for (int i = 0; i < bytes.Length; i++)
                {
                    // Display the physical address in hexadecimal.
                    sMacAddress = sMacAddress + bytes[i].ToString("X2");
                    // Insert a hyphen after each byte, unless we are at the end of the 
                    // address.
                    if (i != bytes.Length - 1)
                    {
                        sMacAddress = sMacAddress + "-";
                    }
                }
                return sMacAddress;

            }
            return sMacAddress;
        }

        public ActionResult Log_Out()
        {
            Session.Abandon();

            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Login" })); ;
        }

        public ActionResult Change_Password()
        {

            ViewBag.Message = "Reset Your Password";

            return View();
        }


        public ActionResult UpdatePassword(string Uname, string Password)
        {

            var issaved = _login.UpdatePassword(Uname, Password);
            if (issaved == true)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }


        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public JsonResult KeepSessionAlive()
        {
            return new JsonResult { Data = "Success" };
        }
    }
}