using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{

    public class ProcessSequenceController : Controller
    {   
        private readonly IProcessSequence _processSequence;
       
        public ProcessSequenceController(IProcessSequence ProcessSequence)
        {
            _processSequence = ProcessSequence;
        }
        // GET: ProcessSequence
        public ActionResult Index()
        {
            ViewBag.ProcessSequence = _processSequence.GetAll();
            return View();
        }

        public ActionResult GetMachinebyProcess(int process_id)
        {
            var data = _processSequence.GetMachinebyProcess(process_id);
            SelectList d = new SelectList(data, "Machine_id", "Machine_name");
            return Json(d);
        }

        public ActionResult Create()
        {
            ViewBag.Process = _processSequence.GetProcessCode();
            ViewBag.Machine = _processSequence.GetMachineCode();
            ViewBag.Item = _processSequence.GetItemCode();
            return View();
        }

        [HttpPost]
        public ActionResult CreateProcessSequence(process_sequence_vm vm)
        {
            var result = _processSequence.SaveProcessSequence(vm);
            if (result == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Process = _processSequence.GetProcessCode();
            ViewBag.Machine = _processSequence.GetMachineCode();
            ViewBag.Item = _processSequence.GetItemCode();

            ViewBag.result = _processSequence.Get(id);
            return View();
        }

        [HttpPost]
        public ActionResult UpdateProcessSequence(process_sequence_vm vm)
        {
            var result = _processSequence.UpdateProcessSequence(vm);
            if (result == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Process = _processSequence.GetProcessCode();
                ViewBag.Machine = _processSequence.GetMachineCode();
                ViewBag.Item = _processSequence.GetItemCode();
                return RedirectToAction("Edit", new { id = vm.process_sequence_id });
            }

        }
    }
}