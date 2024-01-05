using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class QualityControlParameterController : Controller
    {
        private readonly IQualityControlParameterService _qualityControl;
        private readonly IItemService _item_service;

        public QualityControlParameterController(IQualityControlParameterService QualityControl, IItemService Item_service)
        {
            _qualityControl = QualityControl;
            _item_service = Item_service;
        }
        // GET: QualityControlParameter
        [CustomAuthorizeAttribute("QCP")]
        public ActionResult Index()
        {
            ViewBag.QualityController = _qualityControl.GetAll();
            return View();
        }

        [CustomAuthorizeAttribute("QCP")]
        public ActionResult Create()
        {
            ViewBag.Item = _qualityControl.GetItemCode();
            ViewBag.Machine = _qualityControl.GetMachineCode();
            return View();
        }

        [HttpPost]
        public ActionResult SaveQualityParameter(quality_parameter_vm vm)
        {
            var result = _qualityControl.SaveQCParameter(vm);
            if(result == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
            
        }

        [CustomAuthorizeAttribute("QCP")]
        public ActionResult Edit(int? id)
        {
            ViewBag.Item = _qualityControl.GetItemCode();
            ViewBag.Machine = _qualityControl.GetMachineCode();
            ViewBag.result = _qualityControl.Get(id);
            return View();
        }

        [HttpPost]
        public ActionResult UpdateQualityParameter(quality_parameter_vm vm)
        {
            var result = _qualityControl.UpdateQCParameter(vm);
            if (result == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Item = _qualityControl.GetItemCode();
                ViewBag.Machine = _qualityControl.GetMachineCode();
                return RedirectToAction("Edit", new { id = vm.mfg_qc_id });
            }
            
        }

    }
}