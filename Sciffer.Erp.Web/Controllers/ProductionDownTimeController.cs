using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class ProductionDownTimeController : Controller
    {
        private readonly IProductionDownTimeService _proddt;
        private readonly IGenericService _Generic;
        private readonly ILoginService _login;

        public ProductionDownTimeController(IProductionDownTimeService proddt, IGenericService Generic, ILoginService login)
        {
            _proddt = proddt;
            _Generic = Generic;
            _login = login;
        }

        // GET: ProductionDownTime
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
            ViewBag.DataSource = _proddt.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult CreateExcel(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var msg = _proddt.AddExcel(file);
                TempData["doc_num"] = msg;
                if (msg.Contains("saved"))
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            int user_id = int.Parse(Session["User_Id"].ToString());
            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            if (checkoperator == true)
            {
                ViewBag.role = "Operator";
            }
            else
            {
                ViewBag.role = "Not Operator";
            }

            ViewBag.prodplan_details = _Generic.GetProdPlanDetails();
            ViewBag.proddt_details = _Generic.GetProdDownTimeDetails();
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");            
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }

        public ActionResult Save(List<prod_downtime_vm> DepParaArr)
        {
          
                var isValid = _proddt.Add(DepParaArr);
                if (isValid.Contains("Saved"))
                {
                    TempData["doc_num"] = isValid;
                    return Json(isValid, JsonRequestBehavior.AllowGet);
                }

            ViewBag.proddt_details = _Generic.GetProdDownTimeDetails();
            ViewBag.prodplan_details = _Generic.GetProdPlanDetails();
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }



     

        public ActionResult Edit(DateTime? prod_date)
        {
         

            List<prod_downtime_vm> vm = _proddt.Get((DateTime)prod_date);
            if (vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.operator_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.details = vm;
            return View(vm);
        }
    }
}