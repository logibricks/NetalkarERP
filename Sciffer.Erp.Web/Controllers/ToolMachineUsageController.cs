using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class ToolMachineUsageController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IToolMachineUsageService _ToolMachineUsage;
        private readonly IToolRenewTypeService _ToolRenewType;

        public ToolMachineUsageController(IGenericService generic, IToolMachineUsageService toolmachineusage, IToolRenewTypeService toolrenewtype)
        {
            _Generic = generic;
            _ToolMachineUsage = toolmachineusage;
            _ToolRenewType = toolrenewtype;
        }

        // GET: ToolMachineUsage
        [CustomAuthorizeAttribute("TMCUSG")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _ToolMachineUsage.GetAll();
            return View();
        }

        // GET: ToolMachineUsage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_tool_machine_usage_VM ref_tool_machine_usage_VM = _ToolMachineUsage.Get(id);
            if (ref_tool_machine_usage_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(ref_tool_machine_usage_VM);
        }

        // GET: ToolMachineUsage/Create
        [CustomAuthorizeAttribute("TMCUSG")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.tool_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.tool_renew_type_id = new SelectList(_ToolRenewType.GetAll(), "tool_renew_type_id", "tool_renew_type_name");
            return View();
        }

        public JsonResult GetItemDetails(int tool_id, int tool_renew_type_id, int machine_id)
        {
            var data = _ToolMachineUsage.GetItemDetails(tool_id, tool_renew_type_id, machine_id);
            return Json(data);
        }

        // POST: ToolMachineUsage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_tool_machine_usage_VM vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _ToolMachineUsage.Add(vm);
                if (issaved.Contains("Saved"))
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = issaved;
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }

            ViewBag.tool_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.tool_renew_type_id = new SelectList(_ToolRenewType.GetAll(), "tool_renew_type_id", "tool_renew_type_name");
            return View(vm);
        }

        // GET: ToolMachineUsage/Edit/5
        [CustomAuthorizeAttribute("TMCUSG")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_tool_machine_usage_VM ref_tool_machine_usage = _ToolMachineUsage.Get(id);
            if (ref_tool_machine_usage == null)
            {
                return HttpNotFound();
            }
            ViewBag.tool_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.tool_renew_type_id = new SelectList(_ToolRenewType.GetAll(), "tool_renew_type_id", "tool_renew_type_name");
            return View(ref_tool_machine_usage);
        }

        // POST: ToolMachineUsage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_tool_machine_usage_VM vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _ToolMachineUsage.Update(vm);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            ViewBag.tool_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.tool_renew_type_id = new SelectList(_ToolRenewType.GetAll(), "tool_renew_type_id", "tool_renew_type_name");
            return View(vm);
        }

        // GET: ToolMachineUsage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ref_tool_machine_usage = _ToolMachineUsage.Delete(id);
            return View(ref_tool_machine_usage);
        }

        // POST: ToolMachineUsage/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ref_tool_machine_usage ref_tool_machine_usage = db.ref_tool_machine_usage.Find(id);
        //    db.ref_tool_machine_usage.Remove(ref_tool_machine_usage);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
