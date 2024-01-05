using CrystalDecisions.CrystalReports.Engine;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class SkillMatrixController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly ISkillMatrixService _skillmatrix;

        public SkillMatrixController(IGenericService Generic, ISkillMatrixService skillmatrix)
        {
            _Generic = Generic;
            _skillmatrix = skillmatrix;
        }

        [CustomAuthorizeAttribute("SKMATX")]
        // GET: SkillMatrix
        public ActionResult Index()
        {
            ViewBag.operator_list = new SelectList(_Generic.GetOperatorList(), "user_id", "user_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.level_list = new SelectList(_Generic.GetLevelList(), "level_id", "level_code");
            ViewBag.DataSource = _skillmatrix.GetAll("operator_level_index", 0);
            ViewBag.machine_level_index = _skillmatrix.GetAll("machine_level_index", 0);
            ViewBag.level_index = _skillmatrix.GetAll("level_index", 0);
            return View();
        }
        public JsonResult GetData(string entity, int? id)
        {
            ViewBag.datasource = _skillmatrix.GetAll(entity, id);
            var data = _skillmatrix.GetAll(entity, id);
            return Json(new { result = data, count = data.Count }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorizeAttribute("SKMATX")]
        public ActionResult SaveLevel(ref_skill_matrix_vm value)
        {
            var data = 0;
            var data1 = true;
            //ViewBag.datasource = _employee.GetAll();
            data = _Generic.CheckDuplicate(value.level_code, "", "", "LevelMaster", value.level_id);

            bool duplicate = false;
            if (data > 0)
            {

                duplicate = false;
                return Json(duplicate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //if (value.level_id == 0)
                //{
                data1 = _skillmatrix.AddLevel(value);
                //}
                //else
                //{
                //    data1 = _skillmatrix.Update(value);
                //}

                // duplicate = true;
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }

        [CustomAuthorizeAttribute("SKMATX")]
        public ActionResult SaveOperatorLevel(List<ref_skill_matrix_vm> value)
        {
            var data1 = false;
            Random rnd = new Random();
            int mappingId = rnd.Next();
            if (value.Count > 0)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    var data = 0;
                    if (value[i].operator_level_mapping_id == 0)
                    {
                        value[i].MappingId = mappingId;
                        //data1 = true;
                        //ViewBag.datasource = _employee.GetAll();

                    }
                    data = _Generic.CheckDuplicate(value[i].machine_id.ToString(), value[i].MappingId.ToString(), value[i].supervisor_id.ToString(), "OperatorLevelMapping", value[i].operator_level_mapping_id);

                    if (data > 0)
                    {
                        bool duplicate = false;
                        return Json(duplicate, JsonRequestBehavior.AllowGet);
                    }
                }

                data1 = _skillmatrix.OperatorLevel(value);
            }
            return Json(data1, JsonRequestBehavior.AllowGet);


        }

        [CustomAuthorizeAttribute("SKMATX")]
        public ActionResult SaveMachineLevel(ref_skill_matrix_vm value)
        {
            var data = 0;
            var data1 = true;
            //ViewBag.datasource = _employee.GetAll();
            data = _Generic.CheckDuplicate(value.machine_id2.ToString(), value.level_id2.ToString(), "", "MachineLevelMapping", value.machine_level_mapping_id);


            bool duplicate = false;
            if (data > 0)
            {
                duplicate = false;
                return Json(duplicate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //if (value.operator_level_mapping_id == 0)
                //{
                data1 = _skillmatrix.MachineLevel(value);
                //}
                //else
                //{
                //    data1 = _skillmatrix.Update(value);
                //}

                // duplicate = true;
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetMachineListWithOperationAndUser1(int id)
        {
            var vm = _skillmatrix.GetMachineListWithOperationAndUser1(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public CrystalReportPdfResult Pdf(int userId)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var userData = _skillmatrix.GetUser(userId);
                var isHistory = _skillmatrix.GetHistory(userId, false);
                string AssemblyPath = Path.Combine(Server.MapPath("~"), userData[0].employeephoto);
                var isNotHistory = _skillmatrix.GetHistory(userId, true);
                var footerData = _skillmatrix.GetLevels();
                DataSet ds = new DataSet("SkillMatrix");
                var dt1 = _Generic.ToDataTable(isHistory);
                var dt2 = _Generic.ToDataTable(isNotHistory);
                var dt3 = _Generic.ToDataTable(userData);
                var dt4 = _Generic.ToDataTable(footerData);
                dt1.TableName = "isHistory";
                dt2.TableName = "isCurrentHistory";
                dt3.TableName = "Header";
                dt4.TableName = "footerdata";
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                ds.Tables.Add(dt3);
                ds.Tables.Add(dt4);

                if (userData[0].employeephoto != null && userData[0].employeephoto != "")
                {
                    var webClient = new WebClient();
                    byte[] imageBytes = webClient.DownloadData(AssemblyPath.Replace("~", ""));
                    ds.Tables[2].Columns.Add("photo", typeof(byte[]));
                    dt3.Rows[0]["photo"] = imageBytes;
                }

                rd.Load(Path.Combine(Server.MapPath("~/Reports/SkillMatrixReport.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "SkillMatrixReport.rpt");
                return new CrystalReportPdfResult(reportPath, rd);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                rd.Close();
                rd.Clone();
                rd.Dispose();
                rd = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }
    }
}