using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Implementation;
using System.Linq;
using System.Web;
using System.IO;
using Excel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class BudgetController : Controller
    {
        private readonly BudgetMasterService _budgetService;
        private readonly IInstructionTypeService _instructionService;
        private readonly IFinancialYearService _financeService;
        private readonly IGeneralLedgerService _generalledgerService;
        private readonly IGenericService _Generic;
        string[] error = new string[30000];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";
        public BudgetController(IFinancialYearService FinancialYear, IInstructionTypeService Instruction, IGeneralLedgerService GeneralLedger,BudgetMasterService BudgetMaster, IGenericService gen)
        {
            _instructionService = Instruction;
            _budgetService = BudgetMaster;
            _Generic = gen;
            _financeService = FinancialYear;
            _generalledgerService = GeneralLedger;
        }
        public void ExportToExcel(string GridModel)
        {
            ExcelExport exp = new ExcelExport();
            var DataSource = _budgetService.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "Budget.xlsx", ExcelVersion.Excel2010, false, false, "flat-saffron");
        }
        public void ExportToWord(string GridModel)
        {
            WordExport exp = new WordExport();
            var DataSource = _budgetService.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "Budget.docx", false, false, "flat-saffron");
        }
        public void ExportToPdf(string GridModel)
        {
            PdfExport exp = new PdfExport();
            var DataSource = _budgetService.GetAll();
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "Budget.pdf", false, false, "flat-saffron");
        }
        private GridProperties ConvertGridObject(string gridProperty)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            GridProperties gridProp = new GridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    Type type = property.PropertyType;
                    string serialize = serializer.Serialize(ds.Value);
                    object value = serializer.Deserialize(serialize, type);
                    property.SetValue(gridProp, value, null);
                }
            }
            return gridProp;
        }
        // GET: Budget
        [CustomAuthorizeAttribute("BDGT")]
        public ActionResult Index()
        {
            var fylist = _financeService.GetAll();
            var genledg = _generalledgerService.GetAccountGeneral();
            ViewBag.DataSource = _budgetService.GetAll();
            var inst = _instructionService.GetAll();
            ViewBag.insttype = new SelectList(inst, "instruction_type_id", "instruction_name");
            ViewBag.financial_year_id = new SelectList(fylist, "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.Genledger = new SelectList(genledg, "gl_ledger_id", "gl_ledger_name");
            return View();
        }
        public ActionResult GetBudget()
        {
            var budget = _budgetService.GetAll();
            return Json(new { result = budget, count = budget.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InlineInsert(ref_budget_mastervm value)
        {

            var add = _Generic.CheckDuplicate(value.financial_year_id.ToString(), value.general_ledger_id.ToString(),"", "budget", value.budget_id);
            if (add == 0)
            {
                if (value.budget_id == 0)
                {
                    var data1 = _budgetService.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _budgetService.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {

            for (int m = 0; m < Request.Files.Count; m++)
            {
                HttpPostedFileBase file = Request.Files[m];
                if (file.ContentLength > 0)
                {

                    string extension = System.IO.Path.GetExtension(file.FileName);
                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Uploads"), file.FileName);
                    if (System.IO.File.Exists(path1))
                        System.IO.File.Delete(path1);

                    file.SaveAs(path1);
                    FileStream stream = System.IO.File.Open(path1, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReader;
                    if (extension == ".xls")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }

                    excelReader.IsFirstRowAsColumnNames = true;
                    System.Data.DataSet result = excelReader.AsDataSet();
                    int row = 0, col = 0;
                    string[] columnarray = new string[excelReader.FieldCount];

                    string uploadtype = Request.Params[0];
                    List<ref_budget_mastervm> ref_budget_mastervm = new List<ref_budget_mastervm>();
                    if (result.Tables.Count == 0)
                    {
                        errorList++;
                        error[error.Length - 1] = "File is Empty!";
                        errorMessage = "File is Empty!";
                    }
                    else
                    {
                        while (excelReader.Read())
                        {
                            if (row == 0)
                            {
                                for (int i = 0; i <= excelReader.FieldCount - 1; i++)
                                {
                                    columnarray[col] = excelReader.GetString(col);
                                    col = col + 1;
                                }
                            }
                            else
                            {
                                if (ValidateExcelColumns(columnarray) == true)
                                {
                                    string sr_no = excelReader.GetString(Array.IndexOf(columnarray, "Sr No"));
                                    if (sr_no == "")
                                    {
                                        errorList++;
                                        error[error.Length - 1] = "File is Empty!";
                                        errorMessage = "File is Empty!";
                                    }
                                    else
                                    {
                                        var financial_year = excelReader.GetString(Array.IndexOf(columnarray, "Financial Year"));
                                        if (ref_budget_mastervm.Count != 0)
                                        {
                                            var zz = ref_budget_mastervm.Where(z => z.financial_year == financial_year).FirstOrDefault();
                                            if (zz != null)
                                            {
                                                ref_budget_mastervm IBS = new ref_budget_mastervm();
                                                IBS.financial_year = excelReader.GetString(Array.IndexOf(columnarray, "Financial Year"));
                                                var financial_year_id = _Generic.GetFinancialYearID(IBS.financial_year);
                                                if (financial_year_id == 0)
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = IBS.financial_year;
                                                    errorMessage = IBS.financial_year + "not found!";
                                                }
                                                else
                                                {
                                                    IBS.financial_year_id = financial_year_id;
                                                }

                                                IBS.general_ledger_name = excelReader.GetString(Array.IndexOf(columnarray, "General Ledger Code"));
                                                var gl_ledger_id = _Generic.GetGLId(IBS.general_ledger_name);
                                                if (gl_ledger_id == 0)
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = IBS.general_ledger_name;
                                                    errorMessage = IBS.general_ledger_name + "not found!";
                                                }
                                                else
                                                {
                                                    IBS.general_ledger_id = gl_ledger_id;
                                                }

                                                IBS.amount = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Amount")));

                                                IBS.instruction_name = excelReader.GetString(Array.IndexOf(columnarray, "Instruction Type"));
                                                var instruction_type_id = _Generic.GetInstructionID(IBS.instruction_name);
                                                if (instruction_type_id == 0)
                                                {
                                                    errorList++;
                                                    error[error.Length - 1] = IBS.instruction_name;
                                                    errorMessage = IBS.instruction_name + "not found!";
                                                }
                                                else
                                                {
                                                    IBS.instruction_type_id = instruction_type_id;
                                                }

                                                var is_blocked = excelReader.GetString(Array.IndexOf(columnarray, "Blocked"));

                                                if (is_blocked == "Yes")
                                                {
                                                    IBS.is_blocked = true;
                                                }
                                                else
                                                {
                                                    IBS.is_blocked = false;
                                                }
                                                ref_budget_mastervm.Add(IBS);

                                            }
                                            else
                                            {
                                                errorList++;
                                                error[error.Length - 1] = financial_year;
                                                errorMessage = "Financial year is different!...";
                                            }
                                        }
                                        else
                                        {
                                            ref_budget_mastervm IBS = new ref_budget_mastervm();
                                            IBS.financial_year = excelReader.GetString(Array.IndexOf(columnarray, "Financial Year"));
                                            var financial_year_id = _Generic.GetFinancialYearID(IBS.financial_year);
                                            if (financial_year_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = IBS.financial_year;
                                                errorMessage = IBS.financial_year + " not found!";

                                            }
                                            else
                                            {
                                                IBS.financial_year_id = financial_year_id;
                                            }

                                            IBS.general_ledger_name = excelReader.GetString(Array.IndexOf(columnarray, "General Ledger Code"));
                                            var gl_ledger_id = _generalledgerService.GetID(IBS.general_ledger_name);
                                            if (gl_ledger_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = IBS.general_ledger_name;
                                                errorMessage = IBS.general_ledger_name + " not found!";
                                            }
                                            else
                                            {
                                                IBS.general_ledger_id = gl_ledger_id;
                                            }

                                            IBS.amount = Double.Parse(excelReader.GetString(Array.IndexOf(columnarray, "Amount")));

                                            IBS.instruction_name = excelReader.GetString(Array.IndexOf(columnarray, "Instruction Type"));
                                            var instruction_type_id = _Generic.GetInstructionID(IBS.instruction_name);
                                            if (instruction_type_id == 0)
                                            {
                                                errorList++;
                                                error[error.Length - 1] = IBS.instruction_name;
                                                errorMessage = IBS.instruction_name + " not found!";
                                            }
                                            else
                                            {
                                                IBS.instruction_type_id = instruction_type_id;
                                            }

                                            var is_blocked = excelReader.GetString(Array.IndexOf(columnarray, "Blocked"));

                                            if (is_blocked == "Yes")
                                            {
                                                IBS.is_blocked = true;
                                            }
                                            else
                                            {
                                                IBS.is_blocked = false;
                                            }
                                            ref_budget_mastervm.Add(IBS);
                                        }
                                    }
                                }
                                else
                                {
                                    errorList++;
                                    error[error.Length - 1] = "Check Header Name!";
                                    errorMessage = "Check Header Name!";
                                }

                            }
                            row = row + 1;
                        }
                    }
                    excelReader.Close();
                    
                    if (errorMessage == "")
                    {
                        var isSucess = _budgetService.AddExcel(ref_budget_mastervm);
                        if (isSucess)
                        {
                            errorMessage = "suceess";
                            return Json(errorMessage, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            errorMessage = "Failed";
                        }
                    }
                   
                }
                else
                {
                    errorList++;
                    error[error.Length - 1] = "Select File to Upload.";
                    errorMessage = "Select File to Upload.";
                }
            }
            //return Json(new { Status = Message, text = errorList, error = error, errorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
            return Json(errorList + " - " + errorMessage, JsonRequestBehavior.AllowGet);
        }
        public Boolean ValidateExcelColumns(string[] columnarray)
        {
            if (columnarray.Contains("Sr No") == false)
            {
                return false;
            }
            if (columnarray.Contains("Financial Year") == false)
            {
                return false;
            }
            if (columnarray.Contains("General Ledger Code") == false)
            {
                return false;
            }
            if (columnarray.Contains("Amount") == false)
            {
                return false;
            }
            if (columnarray.Contains("Instruction Type") == false)
            {
                return false;
            }
            if (columnarray.Contains("Blocked") == false)
            {
                return false;
            }
            return true;
        }
    }
}