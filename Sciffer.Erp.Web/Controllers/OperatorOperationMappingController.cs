using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class OperatorOperationMappingController : Controller
    {
        private readonly IOperatorOperationMappingService _mappingService;

        public OperatorOperationMappingController(IOperatorOperationMappingService MappingService)
        {
            _mappingService = MappingService;
        }

        [CustomAuthorizeAttribute("OOM")]
        // GET: OperatorOperationMapping
        public ActionResult Index()
        {
            ViewBag.operatorlist = new SelectList(_mappingService.GetOperatorList(), "user_id", "user_name");
            ViewBag.processlist = new SelectList(_mappingService.GetProcessList(), "process_id", "process_description");
            ViewBag.operatoroperationlist = _mappingService.GetOperatorOperationMapList();
            return View();
        }

        [CustomAuthorizeAttribute("OOM")]
        public ActionResult UpdateOperatorOperationMapping(int operator_id, string operation_id)
        {
            var result = _mappingService.UpdateOperatorOperationMapping(operator_id, operation_id);
            return Json(result);
        }

        public ActionResult GetMachineRole(int user_id)
        {
            var data = _mappingService.GetMachineRole(user_id);
            return Json(data, JsonRequestBehavior.AllowGet);
            
        }
    }
}