using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class SPCController : Controller
    {

        private readonly ISPCService _SPCService;
        private readonly IGenericService _Generic;
        public SPCController(ISPCService SPCService ,IGenericService gen)
        {
            _SPCService = SPCService;
            _Generic = gen;
        }

        // GET: SPC
        public ActionResult Index()
        {
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.process_list = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            return View();
        }

        //Return machine list by process
        public ActionResult GetMachineListByProcess(int process_id)
        {
            var data = new SelectList(_SPCService.GetMachineListByProcess(process_id), "machine_id", "machine_name");
            return Json(data);
        }
        
        public ActionResult Get_Operator_Name_List(int item_id,int machine_id)
        {
            var data = new SelectList(_SPCService.GetOperatorNameList(item_id, machine_id), "mfg_op_oc_id", "mfg_op_name");
            return Json(data);
        }
        
        public ActionResult Get_QCOperator_Name_List(int item_id, int machine_id)
        {
            var data = new SelectList(_SPCService.GetQCOperatorNameList(item_id, machine_id), "mfg_op_qc_id", "mfg_qc_name");
            return Json(data);
        }
        
        public ActionResult GetSPCChartReport(DateTime fromDate, int item_id, int machine_id, TimeSpan start_time, TimeSpan end_time, int is_operator_parameter, int parameter_id)//(DateTime fromDate, int item_id, int machine_id, TimeSpan start_time, TimeSpan end_time, int is_operator_parameter, int parameter_id)
        {
            var data = _SPCService.GetSPCChartReport(fromDate, item_id, machine_id, start_time, end_time, is_operator_parameter, parameter_id);// (fromDate,item_id,machine_id,start_time,end_time,is_operator_parameter,parameter_id);
            return Json(data);
            //var data = new SelectList(_SPCService.GetQCOperatorNameList(item_id, machine_id), "mfg_op_qc_id", "mfg_qc_name");
            //return Json(data);
        }


        public ActionResult GetSPCChartReportCalculation(DateTime fromDate, int item_id, int machine_id, TimeSpan start_time, TimeSpan end_time, int is_operator_parameter, int parameter_id)
        {
            var data = _SPCService.GetSPCChartReportCalculation(fromDate, item_id, machine_id, start_time, end_time, is_operator_parameter, parameter_id);
            return Json(data);
            //var data = new SelectList(_SPCService.GetQCOperatorNameList(item_id, machine_id), "mfg_op_qc_id", "mfg_qc_name");
            //return Json(data);
        }

        //public ActionResult GetSPCChartReportQuality(DateTime fromDate, int item_id, int machine_id, TimeSpan start_time, TimeSpan end_time, int quality_parameter_id)
        //{
        //    var data = new SelectList(_SPCService.GetQCOperatorNameList(item_id, machine_id), "mfg_op_qc_id", "mfg_qc_name");
        //    return Json(data);
        //}



    }

}