using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Service.Implementation;
using Sciffer.Erp.Data;

namespace Sciffer.Erp.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _login;
        private readonly IMachineEntryService _machineService;
        private readonly IPurRequisitionService _pur;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IGenericService _Generic;



        public LoginController(LoginService login, IMachineEntryService MachineEntry, IPurRequisitionService pur, IPurchaseOrderService purchaseOrderService, IGenericService generic)
        {
            _login = login;
            _machineService = MachineEntry;
            _pur = pur;
            _purchaseOrderService = purchaseOrderService;
            _Generic = generic;
        }
        public ActionResult Index(string msg = "")
        {
            ViewBag.msg = msg;
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            var username = fc["user"];
            var password = fc["password"];

            ScifferContext context = new ScifferContext();
            if (ModelState.IsValid)
            {
                var user = _login.CheckLoginParameters(username, password);
                if (user != null)
                {
                    Session["User_Id"] = user.user_id;
                    Session["User_Name"] = user.user_name;
                    int open_cnt = _login.GetOpentaskcount(user.user_id);
                    int overdue_cnt = _login.GetOverduetask(user.user_id);
                    int preq1 = 0;
                    int purcount = 0;
                    int open_cnt12 = 0;
                    int open_cnt2 = 0;
                    int open_cnt123 = 0;
                    int reorderCount = 0;
                    Session["overdue_count"] = overdue_cnt;
                    Session["open_count"] = open_cnt;

                    var checkoperator1 = _login.CheckOperatorLogin(user.user_id, "IT_ADMIN");

                    var checkoperator3 = _login.CheckOperatorLogin(user.user_id, "TOP_MGMT");
                    if (checkoperator3 == true || checkoperator1 == true)
                    {
                        preq1 = _pur.GetPendigApprovedList().Count;
                        Session["preq_count1"] = preq1;
                    }
                    var checkoperator4 = _login.CheckOperatorLogin(user.user_id, "STO_EXEC");
                    var checkoperator5 = _login.CheckOperatorLogin(user.user_id, "PUR_EXEC");


                    if (checkoperator4 == true || checkoperator5 == true || checkoperator1 == true)
                    {
                        open_cnt12 = _login.GetPurchaseRequisitionAllRejectAndApprovedcount(user.user_id);
                        Session["open_count12"] = open_cnt12;
                        open_cnt123 = _login.GetPurchaseRequisitionRejectedcount(user.user_id);
                        Session["open_cnt123"] = open_cnt123;
                    }


                    if (checkoperator3 == true || checkoperator1 == true)
                    {
                        //var open_cnt2 = _login.GetApprovedPurchaseOrderCount(user.user_id);
                        //Session["open_count2"] = open_cnt2;
                        purcount = _purchaseOrderService.GetPendigApprovedList(0).Count();
                        Session["open_count2"] = purcount;
                    }
                    if (checkoperator5 == true || checkoperator1 == true)
                    {
                        open_cnt12 = _login.GetPurchaseOrderAllRejectAndApprovedcount(user.user_id);
                        Session["open_count21"] = open_cnt12;
                        open_cnt123 = _login.GetPurchaseOrderAllRejectdcount(user.user_id);
                        Session["open_count213"] = open_cnt123;
                    }
                    //if (checkoperator4 == true)

                    if (checkoperator4 == true)
                    {
                        int create_user = int.Parse(Session["User_Id"].ToString());
                        //var open_cnt2 = _login.GetApprovedMRNCount(create_user);
                        //Session["open_count3"] = open_cnt2;
                        open_cnt2 = _login.GetApprovedMRICount(create_user);
                        Session["open_count3"] = open_cnt2;
                    }

                    if (checkoperator3 == true)
                    {                       
                        reorderCount = _Generic.GetReOrderCount();
                        Session["reorderCount"] = reorderCount;
                    }

                    var checkoperator = _login.CheckOperatorLogin(user.user_id, "PROD_OP");
                    var checkoperator12 = _login.CheckOperatorLogin(user.user_id, "QA_OP");

                    if (_machineService.GetOperatorPhoto(user.user_id) != null)
                    {
                        Session["OperatorPhoto"] = _machineService.GetOperatorPhoto(user.user_id).Trim();
                    }
                    else
                    {
                        Session["OperatorPhoto"] = "";
                    }
                    if (checkoperator == true)
                    {
                        return RedirectToAction("Index", "MachineEntry");
                    }
                    if (checkoperator12 == true)
                    {
                        return RedirectToAction("Index", "MachineEntryForQC");
                    }
                    Session["open_count_final"] = open_cnt + overdue_cnt + preq1 + purcount + open_cnt12 + open_cnt2 + open_cnt123;
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", new { msg = "failed" });
        }
    }
}