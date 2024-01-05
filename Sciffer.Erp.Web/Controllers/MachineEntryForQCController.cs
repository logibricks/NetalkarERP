using Sciffer.Erp.Service.Interface;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class MachineEntryForQCController : Controller
    {
        private readonly IMachineEntryService _machineEntry;
        private readonly IMachineEntryForQCService _machineEntryForQC;

        // GET: MachineEntryForQC
        public MachineEntryForQCController(IMachineEntryService MachineEntry, IMachineEntryForQCService MachineEntryForQC)
        {
            _machineEntry = MachineEntry;
            _machineEntryForQC = MachineEntryForQC;
        }

        [CustomAuthorizeAttribute("ME-IN-QC")]
        public ActionResult Index()
        {
            ViewBag.nc_status_list = new SelectList(_machineEntryForQC.GetNcStatus(), "nc_status_id", "nc_status_desc");
            ViewBag.supervisor_list = new SelectList(_machineEntry.GetSupervisorList(), "user_id", "user_name");

            return View();
        }

        //get under qc records
        public ActionResult RealtimeUpdate(int status_id = 0)
        {
            ViewBag.UnderQc = _machineEntryForQC.GetMachinetaskUnderQC(status_id);
            ViewBag.AllStatus = _machineEntry.GetAllStatus();
            return PartialView("PV_UnderQc", ViewBag);
        }

        //get qc approved records
        public ActionResult RealtimeUpdateApproved(int status_id = 0)
        {
            var QcApproved = _machineEntryForQC.GetMachinetaskUnderQC(status_id);
            return Json(QcApproved);
        }

        //get rejected records
        public ActionResult RealtimeUpdateRejected(int status_id = 0)
        {

            var QcRejected = _machineEntryForQC.GetMachinetaskUnderQC(status_id);
            return Json(QcRejected);
        }

        //update item status
        public ActionResult UpdateItemStatus(int status, int machine_task_qc_qc_id, int machine_id, string[] parametervalue, int[] parameterid)
        {
            var res = _machineEntryForQC.CheckDuplicateTagNumber(machine_id, machine_task_qc_qc_id);
            if (res == "Approved")
            {
                return Json(new { text = "true" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (parametervalue != null && parameterid != null)
                {
                    var result = _machineEntryForQC.UpdateItemStatus(status, machine_task_qc_qc_id, machine_id, parametervalue, parameterid);
                    ViewBag.AllStatus = _machineEntry.GetAllStatus();
                    ViewBag.UnderQc = _machineEntryForQC.GetMachinetaskUnderQC(4);
                    return PartialView("PV_UnderQc", ViewBag);
                }
                return Json(new { text = "false" }, JsonRequestBehavior.AllowGet);
            }
        }

        //get blocked machines
        public ActionResult RealtimeMachineStatus(int status_id = 0)
        {
            ViewBag.MachineStatus = _machineEntryForQC.GetMahineStatus();
            return PartialView("PV_MachineStatus", ViewBag);
        }

        //get quality paramters
        public ActionResult GetAllParameters(int item_id, int machine_id)
        {
            var frequency = _machineEntryForQC.GetAllParameters(item_id, machine_id);
            return Json(frequency);
        }

        //remove machine block
        public ActionResult InlineUpdate(int machine_id, bool is_machine_blocked)
        {
            var QCCount = _machineEntry.GetMachineTaskUnderQc(machine_id, 4);
            if (QCCount.Count != 0)
            {
                return Json(new { text = "true" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = _machineEntryForQC.UpdateMachineStatus(machine_id, is_machine_blocked);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        //save rejection detail and non-conforming status
        public ActionResult SaveRejectionDetail(int machine_task_qc_qc_id, int nc_status_id, string root_cause, string nc_details, string action_plan, string remarks, string[] why_why_analysis, string nc_tag_number)
        {
            var result = _machineEntryForQC.SaveRejectionDetail(machine_task_qc_qc_id, nc_status_id, root_cause, nc_details, action_plan, remarks, why_why_analysis, nc_tag_number);
            return Json(result);
        }

        //get track on rejection workflow
        public ActionResult GetNonConformingTrack(int machine_task_qc_qc_id)
        {
            var data = _machineEntryForQC.GetNonConformingTrack(machine_task_qc_qc_id);
            return Json(data);
        }

    }
}