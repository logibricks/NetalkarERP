using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sciffer.Erp.Web.Controllers
{
    public class ProcessMachineMappingController : Controller
    {
        private readonly IProcessMachineMappingService _processMachine;
        private readonly IGenericService _generic;
        public ProcessMachineMappingController(IProcessMachineMappingService ProcessMachine, IGenericService Generic)
        {
            _processMachine = ProcessMachine;
            _generic = Generic;
        }

        [CustomAuthorizeAttribute("PRO_MACHI_MAP")]
        // GET: ProcessMachineMapping 
        public ActionResult Index()
        {
            ViewBag.processlist = new SelectList(_processMachine.GetAllProcess(), "process_id", "process_code");
            ViewBag.machinelist = new SelectList(_generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.GetProcessDetails = _processMachine.GetProcessDetails();
            return View();
        }
        
        public ActionResult GetAllMachinesForProcess(int processid)
        {
            var lstitem = _processMachine.GetAllMachinesForProcess(processid);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstitem);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeAttribute("PRO_MACHI_MAP")]
        public ActionResult UpdateMachines(int process_id, string machine_ids)
        {
            var result = _processMachine.UpdateProcessMapping(process_id, machine_ids);
            return Json(result);
        }
    }
}