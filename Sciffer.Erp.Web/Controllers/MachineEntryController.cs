using Newtonsoft.Json;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class MachineEntryController : Controller
    {
        private readonly IMachineEntryService _machineEntry;
        private readonly IGenericService _Generic;
        private readonly IShiftService _Shift;
        private readonly IOperatorScreenUpgradationService _Upgradation;
        private readonly IShiftwiseProductionMasterService _shiftwiseproduction;
        private readonly IPlanMaintenanceOrderService _planMOService;
        private readonly IParameterListService _param;

        public MachineEntryController(IMachineEntryService MachineEntry, IGenericService generic, IShiftService shift, IOperatorScreenUpgradationService Upgradation, IShiftwiseProductionMasterService shiftwiseproduction, IPlanMaintenanceOrderService planMOService, IParameterListService param)
        {
            _machineEntry = MachineEntry;
            _Generic = generic;
            _Shift = shift;
            _Upgradation = Upgradation;
            _shiftwiseproduction = shiftwiseproduction;
            _planMOService = planMOService;
            _param = param;
        }

        [CustomAuthorizeAttribute("MACHINE_ENTRY")]
        // GET: MachineEntry
        public ActionResult Index(int machine_id = 0, int item_id = 0, int shift_id = 0, int process_id = 0)
        {
            int user_id = int.Parse(Session["User_Id"].ToString());


            ViewBag.Machine_id = machine_id;
            ViewBag.Item_Id = item_id;
            ViewBag.SHIFT_ID = shift_id;
            ViewBag.PROCESS_ID = process_id;
            int plant_id = _machineEntry.GetPlantId(shift_id);
            ViewBag.plant_id = plant_id;

            if (Session["User_Id"] == null)
            {
                ViewBag.OperatorName = "Employee not found";
                ViewBag.OperatorPhoto = "";
            }
            else
            {
                ViewBag.OperatorName = _machineEntry.GetOperatorName(user_id);
                ViewBag.Operatorid = user_id;
                var is_logged_in = _machineEntry.SaveOperatorShift(machine_id, shift_id);
                if (is_logged_in == true)
                {
                    _machineEntry.UpdateShiftCount(machine_id, item_id);
                }
                if (_machineEntry.GetOperatorPhoto(user_id) != null)
                {
                    ViewBag.OperatorPhoto = _machineEntry.GetOperatorPhoto(user_id).Trim();
                }
                else
                {
                    ViewBag.OperatorPhoto = "";
                }
            }
            if (machine_id != 0)
            {
                ViewBag.machine_name = _machineEntry.GetMachineName(machine_id);
            }
            if (machine_id != 0 && user_id != 0)
            {
                ViewBag.level_status = _machineEntry.get_machine_entry_mac_level_mapping("mac_opr_mapping", machine_id, user_id, shift_id);

            }

            if (machine_id != 0 && shift_id != 0)
            {
                //ViewBag.target_qty = _machineEntry.GetTargetQtyCount(machine_id,0,shift_id);
                ViewBag.target_qty = _shiftwiseproduction.GetQty("GetTargetQty", plant_id, shift_id, process_id, machine_id, item_id.ToString(), user_id);

            }

            ViewBag.isAvailablePlanMaintenance = _planMOService.CheckPlanMaintenanceOrder((int)machine_id);
            ViewBag.file_list = _Upgradation.GetFileByItemMachineId(machine_id, item_id);
            ViewBag.shift_list = new SelectList(_Shift.GetShiftList(), "shift_id", "shift_desc");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.process_list = new SelectList(_machineEntry.GetProcessListByOperator(user_id), "process_id", "process_description");
            ViewBag.supervisor_list = new SelectList(_machineEntry.GetSupervisorList(), "user_id", "user_name");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            ViewBag.employeeList_attended_by = new SelectList(_Generic.GetEmployeeList(user_id), "employee_id", "employee_code");
            return View();
        }

        //Return machine list by process
        public ActionResult GetMachineListByProcess(int process_id)
        {
            var data = new SelectList(_machineEntry.GetMachineListByProcess(process_id), "machine_id", "machine_name");
            return Json(data);
        }

        public ActionResult GetMachineStatus(int machine_id = 0)
        {
            if (machine_id == 0)
            {
                return Json("false");
            }
            var machinestatus = _machineEntry.CheckMachineBlocked(machine_id);
            return Json(machinestatus);
        }

        public ActionResult RealtimeUpdate(int machine_id = 0, int item_id = 0, int status = 1, string searchtag = "")
        {
            var datasource = _machineEntry.GetMachineTask(machine_id, item_id, status, searchtag);
            ViewBag.datasource = datasource;
            ViewBag.machine_id = machine_id;
            return PartialView("PV_MachineAssigned", ViewBag);
        }

        public ActionResult RealtimeUpdateStatusOk(int machine_id = 0, int item_id = 0, int status = 0)
        {
            var Completed = _machineEntry.GetMachineTask(machine_id, item_id, status, "");
            return Json(Completed);
        }

        public ActionResult RealtimeUpdateStatusNotOk(int machine_id = 0, int item_id = 0, int status = 0)
        {
            var NotCompleted = _machineEntry.GetMachineTask(machine_id, item_id, status, "");
            return Json(NotCompleted);
        }

        public ActionResult RealtimeUpdateStatusUnderQc(int machine_id = 0, int status = 0)
        {
            var UnderQC = _machineEntry.GetMachineTaskUnderQc(machine_id, status);
            return Json(UnderQC);
        }

        public ActionResult GetLastTagUsed(int machine_task_id)
        {
            var previous_tag = _machineEntry.GetLastTagUsed(machine_task_id);
            return Json(previous_tag);
        }

        //update item status
        public ActionResult UpdateItemStatus(int status, int machine_task_id, int in_item_id, int machine_id, string[] parametervalue, int[] parameterid, string heatcode, string runcode, int process_id, string tag_no_two = "", int item_id = 0, int supervisor_id = 0, string supervisor_remarks = "")
        {
            var MachineCount = _machineEntry.GetMachineStatusOkay(machine_task_id);
            if (MachineCount == "Okay")
            {
                return Json(new { text = "true" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (parametervalue != null && parameterid != null)
                {
                    var result = _machineEntry.UpdateItemStatus(status, machine_task_id, in_item_id, machine_id, parametervalue, parameterid, heatcode, runcode, process_id, tag_no_two, supervisor_id, supervisor_remarks);
                    ViewBag.Messege = result;
                    ViewBag.datasource = _machineEntry.GetMachineTask(machine_id, item_id, 1, "");
                    return PartialView("PV_MachineAssigned", ViewBag);
                }
                return Json(new { text = "false" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllParameters(int item_id, int machine_id)
        {
            var frequency = _machineEntry.GetAllParameters(item_id, machine_id);
            return Json(frequency);
        }

        public ActionResult GetProductionCount(string machine_id, int item_id, int status)
        {
            var count = _machineEntry.GetProductionCount(machine_id, item_id, status);
            return Json(count);
        }
        public ActionResult GetProductionCount1(string machine_id, int item_id, int status)
        {
            var count = _machineEntry.GetProductionCount1(machine_id, item_id, status);
            return Json(count);
        }
        public ActionResult GetTargetQtyCount(int machine_id, int item_id, int shift_id)
        {
            var count = _machineEntry.GetTargetQtyCount(machine_id, item_id, shift_id);
            return Json(count);
        }

        //Get all shift OK count
        public ActionResult GetProductionShiftOkCountOfOperator(int machine_id, int item_id)
        {
            var shiftoneokcount = _machineEntry.GetOperatorProductionCountPerShift("GetFirstShiftCount", machine_id, item_id);
            var shifttwookcount = _machineEntry.GetOperatorProductionCountPerShift("GetSecondShiftCount", machine_id, item_id);
            var shiftthreeokcount = _machineEntry.GetOperatorProductionCountPerShift("GetThirdShiftCount", machine_id, item_id);

            return Json(new { shiftoneokcount = shiftoneokcount, shifttwookcount = shifttwookcount, shiftthreeokcount = shiftthreeokcount });
        }

        //Get all shift NOT OK count
        public ActionResult GetProductionShiftNotOkCountOfOperator(int machine_id, int item_id)
        {
            var shiftonenotokcount = _machineEntry.GetOperatorProductionCountPerShift("GetFirstShiftNOKCount", machine_id, item_id);
            var shifttwonotokcount = _machineEntry.GetOperatorProductionCountPerShift("GetSecondShiftNOKCount", machine_id, item_id);
            var shiftthreenotokcount = _machineEntry.GetOperatorProductionCountPerShift("GetThirdShiftNOKCount", machine_id, item_id);

            return Json(new { shiftonenotokcount = shiftonenotokcount, shifttwonotokcount = shifttwonotokcount, shiftthreenotokcount = shiftthreenotokcount });
        }
        public ActionResult StatusOkCountCurr(int machine_id = 0, int item_id = 0, int status = 0)
        {
            var data = _machineEntry.GetMachineTask(machine_id, item_id, status, "");
            var count = new List<mfg_machine_task_VM>();

            var current_day = DateTime.Now.Date;
            foreach (var item in data)
            {
                if (((DateTime)(item.task_time)).Date == current_day)
                {
                    count.Add(item);
                }
            }
            return Json(count.Count);
        }

        public ActionResult StatusOkCountPrev(int machine_id = 0, int item_id = 0, int status = 0)
        {
            var data = _machineEntry.GetMachineTask(machine_id, item_id, status, "");
            var count = new List<mfg_machine_task_VM>();

            var prev_day = DateTime.Now.AddDays(-1).Date;
            foreach (var item in data)
            {
                if (((DateTime)(item.task_time)).Date == prev_day)
                {
                    count.Add(item);
                }
            }
            return Json(count.Count);
        }


        public ActionResult StatusNotOkCountCurr(int machine_id = 0, int item_id = 0, int status = 0)
        {
            var data = _machineEntry.GetMachineTask(machine_id, item_id, status, "");
            var count = new List<mfg_machine_task_VM>();

            var current_day = DateTime.Now.Date;
            foreach (var item in data)
            {
                if (((DateTime)(item.task_time)).Date == current_day)
                {
                    count.Add(item);
                }
            }
            return Json(count.Count);
        }

        public ActionResult StatusNotOkCountPrev(int machine_id = 0, int item_id = 0, int status = 0)
        {
            var data = _machineEntry.GetMachineTask(machine_id, item_id, status, "");
            var count = new List<mfg_machine_task_VM>();

            var prev_day = DateTime.Now.Date;
            foreach (var item in data)
            {
                if (((DateTime)(item.task_time)).Date == prev_day)
                {
                    count.Add(item);
                }
            }
            return Json(count.Count);
        }

        public ActionResult GetTargetQty(int machine_id = 0, int item_id = 0, int shift_id = 0, int process_id = 0)
        {
            int user_id = int.Parse(Session["User_Id"].ToString());
            int plant_id = _machineEntry.GetPlantId(shift_id);
            var target_qty = _shiftwiseproduction.GetQty("GetTargetQty", plant_id, shift_id, process_id, machine_id, item_id.ToString(), user_id);
            return Json(target_qty);
        }
        public ActionResult GetAllParametersAssign(int machine_id)
        {
            var frequency = _machineEntry.GetAllParametersAssign(machine_id);
            return Json(frequency);
        }

        public ActionResult GetCycleTime(int item_id, int process_id, int machine_id)
        {
            var cycleTime = _machineEntry.GetCycleTime(item_id, process_id, machine_id);
            return Json(cycleTime);
        }

        public ActionResult GetPlanMaintenanceOrderDetail(int machine_id)
        {
            try
            {
                var plan_maintenance_order = _planMOService.Getplanmaintenanceorderformachineentry(machine_id);
                string s = string.Empty;
                s = JsonConvert.SerializeObject(plan_maintenance_order,
                               new JsonSerializerSettings
                               {
                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                               });

                var json = Json(s, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}