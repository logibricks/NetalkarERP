using Newtonsoft.Json;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.MovieScheduling.Web.Service;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript;
using Syncfusion.JavaScript.Models;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Syncfusion.Linq;
using System.Text.RegularExpressions;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class ShiftWiseDataController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IOperatorIncentiveService _opInc;

        public string plant_id { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public int start_date_shift_id { get; set; }
        public int end_date_shift_id { get; set; }

        public ShiftWiseDataController(IGenericService Generic, IOperatorIncentiveService opInc)
        {
            _Generic = Generic;
            _opInc = opInc;


        }

        // GET: ShiftWiseData
        [CustomAuthorizeAttribute("INCENTIVE")]
        public ActionResult Index()
        {
            ViewBag.plantlist = _Generic.GetPlantList();
            ViewBag.shiftlist = _Generic.GetShiftdesclist();
            return View();
        }

        //public ActionResult GetOperatorIncentiveSummary( int shift_id DateTime? from_date, DateTime? to_date, string plant_id)
        //{
        //    var data = _Generic.GetOperatorIncentiveSummaryDHB( from_date, to_date, plant_id);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}


        private DataTable GetGroupedBy(DataTable _dtSource, IList<string> _groupByColumnNames, IList<DataTableAggregateFunction> _fieldsForCalculation)
        {

            //Once the columns are added find the distinct rows and group it bu the numbet
            DataTable _dtReturn = _dtSource.DefaultView.ToTable(true, _groupByColumnNames.ToArray());

            //The column names in data table
            foreach (DataTableAggregateFunction _calculatedField in _fieldsForCalculation)
            {
                _dtReturn.Columns.Add(_calculatedField.OutPutColumnName, typeof(int));
            }

            //Gets the collection and send it back
            for (int i = 0; i < _dtReturn.Rows.Count; i = i + 1)
            {
                #region Gets the filter string
                string _filterString = string.Empty;
                for (int j = 0; j < _groupByColumnNames.Count; j = j + 1)
                {
                    if (j > 0)
                    {
                        _filterString += " AND ";
                    }
                    if (_dtReturn.Columns[_groupByColumnNames[j]].DataType == typeof(System.Int32))
                    {
                        _filterString += _groupByColumnNames[j] + " = " + _dtReturn.Rows[i][_groupByColumnNames[j]].ToString() + "";
                    }
                    else
                    {
                        _filterString += _groupByColumnNames[j] + " = '" + _dtReturn.Rows[i][_groupByColumnNames[j]].ToString() + "'";
                    }
                }
                #endregion

                #region Compute the aggregate command

                foreach (DataTableAggregateFunction _calculatedField in _fieldsForCalculation)
                {
                    var st = _calculatedField.enmFunction.ToString() + "(" + _calculatedField.ColumnName + ")";
                    //  var sum = _dtSource.AsEnumerable().Where(_filterString).Sum(x => Double.Parse( x.Field<string>("prod_qty")));
                    var s = _dtSource.Compute(st, _filterString);
                    _dtReturn.Rows[i][_calculatedField.OutPutColumnName] = _dtSource.Compute(st, _filterString);
                    //   _dtReturn.Rows[i][_calculatedField.OutPutColumnName] = _dtSource.Compute(_calculatedField.enmFunction.ToString() + "(" + _calculatedField.ColumnName + ")", _filterString);
                }

                #endregion
            }

            return _dtReturn;
        }

        public ActionResult GetAllViewIncentiveSummary(int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id)
        {
            to_date = to_date.AddDays(1);
            var detailandsum = _Generic.GetAllViewIncentiveSummary(start_date_shift_id, end_date_shift_id, from_date, to_date, plant_id);
            return Json(detailandsum, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ComputeIncentiveSummary(int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id)
        {
            to_date = to_date.AddDays(1);
            var detailandsum = _Generic.GetAllIncentiveDetailandSummary(start_date_shift_id, end_date_shift_id, from_date, to_date, plant_id);
            var val1 = detailandsum.Where(a => a.columnname == "Summary").ToList();
            return Json(val1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveStatus(string status, int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id)
        {
            to_date = to_date.AddDays(1);
            var records = _opInc.UpdateStatus(status, start_date_shift_id, end_date_shift_id, from_date, to_date, plant_id);
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIncenticeStatus(string entity, string plant_id, DateTime from_date, DateTime to_date)
        {
            var records = _Generic.GetIncenticeStatus(entity, plant_id, from_date, to_date);
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ComputeIncentive(int start_date_shift_id, int end_date_shift_id, DateTime from_date,
            DateTime to_date, string plant_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            to_date = to_date.AddDays(1);
            var res = _Generic.ComputeIncentive(start_date_shift_id, end_date_shift_id, from_date, to_date, plant_id);
            var dt1 = _Generic.ToDataTable(res);
            dt1.Columns.Add("production_qty", typeof(int));
            var dt2 = _Generic.ToDataTable(_Generic.GetMultiMachining());
            var benchmark = _Generic.GetInsentiveBenchmarkDetail("incentive_benchmark");
            var holiday_list = _Generic.GetHolidayList();
            string machine_id1 = "";
            string machine_id2 = "";
            string[] stringArray;
            List<ref_easy_hr_data> hrdata = new List<ref_easy_hr_data>();
            if (plant_id == "1")
            {
                hrdata = _Generic.GetEasyHRData().Where(x => x.shift_date >= from_date && x.shift_date <= to_date).ToList();
            }
            for (var i = 0; i <= dt1.Rows.Count - 1; i++)
            {
                DataRow row = dt1.Rows[i];
                var prod_qty = Convert.ToInt32(row["prod_qty"].ToString());
                row["production_qty"] = prod_qty;
                row["is_multi_machine"] = false;
                row["is_continued_shift"] = false;
                row["startrow"] = 0;
                row["endrow"] = 0;
            }

            //for continued shift
            for (var i = 0; i <= dt1.Rows.Count - 1; i++)
            {                
                DataRow dtr = dt1.Rows[i];
                var opname = dtr["operator_name"].ToString();
                var shiftdate = DateTime.Parse(dtr["date"].ToString());
                var nextdays = shiftdate.AddDays(1);
                var shiftname = int.Parse(dtr["shift_id"].ToString());
                var startrow = i;
                var endrow = 0;
                for (var j = i; j <= dt1.Rows.Count - 1; j++)
                {                   
                    DataRow dtr1 = dt1.Rows[j];
                    if (dtr1["operator_name"].ToString() == opname)
                    {
                        if (DateTime.Parse(dtr1["date"].ToString()) == shiftdate)
                        {
                            if (int.Parse(dtr1["shift_id"].ToString()) == shiftname)
                            {
                                endrow = j;
                                continue;

                            }
                            else
                            {
                                if (int.Parse(dtr1["shift_id"].ToString()) == shiftname + 1)
                                {
                                    endrow = j;
                                    continue;
                                }
                                else
                                {
                                    endrow = j - 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (DateTime.Parse(dtr1["date"].ToString()) == nextdays)
                            {
                                if (int.Parse(dtr1["shift_id"].ToString()) == shiftname - 2)
                                {
                                    endrow = j;
                                    continue;
                                }
                                else
                                {
                                    endrow = j - 1;
                                    break;
                                }
                            }
                            else
                            {
                                endrow = j - 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        endrow = j - 1;
                        break;
                    }
                }

                dtr["startrow"] = startrow;
                dtr["endrow"] = endrow;
                i = i + endrow - startrow;
                if (i == 84)
                {
                    var asdaf = "";
                }
            }

            //for updating shift id,shift code,shift date
            for (var i = 0; i <= dt1.Rows.Count - 1; i++)
            {
                DataRow dr = dt1.Rows[i];              
                var startrow = dr["startrow"].ToString();
                var endrow = dr["endrow"].ToString();
                var shift_id = dr["shift_id"].ToString();
                var shiftdate = DateTime.Parse(dr["date"].ToString());
                var shiftname = dr["shift_code"].ToString();
                var login_time = dr["login_time"].ToString();
                var logout_time = dr["logout_time"].ToString();
                var shift_hours = dr["shift_hours"].ToString();
                var is_multi_machine = dr["is_multi_machine"].ToString();
                var is_continued_shift = dr["is_continued_shift"].ToString();
                for (var j = i + 1; j <= int.Parse(endrow.ToString()); j++)
                {
                    DataRow dr1 = dt1.Rows[j];
                    var shift_date = DateTime.Parse(dr1["date"].ToString());
                    DayOfWeek dow = shift_date.DayOfWeek; //enum
                    string str = dow.ToString(); //string  
                    int numberOfholidays = holiday_list.Where(x => x.holiday_date == shift_date).ToList().Count;
                    var shiftid = int.Parse(dr1["shift_id"].ToString());
                    var flag = true;

                    if (plant_id == "1")
                    {
                        if (str == "Sunday" && shiftid == 2)
                        {
                            flag = false;
                        }
                        else if (str == "Sunday" && shiftid == 3)
                        {
                            flag = false;
                        }
                        else if (str == "Monday" && shiftid == 1)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        if (str == "Sunday" && shiftid == 5)
                        {
                            flag = false;
                        }
                        else if (str == "Sunday" && shiftid == 6)
                        {
                            flag = false;
                        }
                        else if (str == "Monday" && shiftid == 4)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }

                    if (numberOfholidays == 0)
                    {
                        if (flag == true)
                        {
                            if (shift_id != dr1["shift_id"].ToString())
                            {
                                dr1["is_continued_shift"] = true;
                            }

                            dr1["shift_id"] = shift_id;
                            dr1["shift_code"] = shiftname;
                            dr1["date"] = shiftdate;
                            dr1["login_time"] = login_time;
                            dr1["logout_time"] = logout_time;
                            dr1["shift_hours"] = shift_hours;
                            dr1["is_multi_machine"] = is_multi_machine;
                            dr1["is_continued_shift"] = is_continued_shift;
                            dr1["startrow"] = startrow;
                            dr1["endrow"] = endrow;
                            //  dr1["remarks"] = "Continued Shift";
                            i = j;
                        }
                    }
                }
            }

            //  dt1.Columns["prod_qty"].DataType = typeof(Int32);
            IList<string> _groupByColumnNames = new List<string>();
            _groupByColumnNames.Add("operator_id");
            _groupByColumnNames.Add("operator_name");
            _groupByColumnNames.Add("user_name");
            _groupByColumnNames.Add("date");
            _groupByColumnNames.Add("shift_id");
            _groupByColumnNames.Add("shift_code");
            _groupByColumnNames.Add("item_id");
            _groupByColumnNames.Add("item_code");
            _groupByColumnNames.Add("item_name");
            _groupByColumnNames.Add("machine_id");
            _groupByColumnNames.Add("machine_code");
            _groupByColumnNames.Add("machine_name");
            _groupByColumnNames.Add("process_id");
            _groupByColumnNames.Add("process_desc");
            _groupByColumnNames.Add("process_description");
            _groupByColumnNames.Add("plant_id");
            _groupByColumnNames.Add("plant_name");
            _groupByColumnNames.Add("incentive_applicability");
            _groupByColumnNames.Add("login_time");
            _groupByColumnNames.Add("logout_time");
            _groupByColumnNames.Add("shift_hours");
            _groupByColumnNames.Add("reporting_quantity");
            _groupByColumnNames.Add("incentive");
            _groupByColumnNames.Add("diff_qty");
            _groupByColumnNames.Add("amount");
            _groupByColumnNames.Add("is_multi_machine");
            _groupByColumnNames.Add("is_continued_shift");
            _groupByColumnNames.Add("startrow");
            _groupByColumnNames.Add("endrow");





            IList<DataTableAggregateFunction> _fieldsForCalculation = new List<DataTableAggregateFunction>();
            _fieldsForCalculation.Add(new DataTableAggregateFunction()
            { enmFunction = AggregateFunction.Sum, ColumnName = "production_qty", OutPutColumnName = "prod_qty" });

            DataTable dtGroupedBy = GetGroupedBy(dt1, _groupByColumnNames, _fieldsForCalculation);
            dtGroupedBy.Columns.Add("row_count", typeof(int));
            dtGroupedBy.Columns.Add("remarks", typeof(string));
            dtGroupedBy.Columns.Add("reportingquantity", typeof(int));
            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++)
            {
                if (i == 171)
                {
                    var abc = i;
                }

                DataRow dtr = dtGroupedBy.Rows[i];
                var opname = dtr["operator_name"].ToString();
                var shiftdate = DateTime.Parse(dtr["date"].ToString());
                var nextdays = shiftdate.AddDays(1);
                var shiftname = int.Parse(dtr["shift_id"].ToString());
                var startrow = i;
                var endrow = 0;
                for (var j = i; j <= dtGroupedBy.Rows.Count - 1; j++)
                {
                    DataRow dtr1 = dtGroupedBy.Rows[j];
                    if (dtr1["operator_name"].ToString() == opname)
                    {
                        if (DateTime.Parse(dtr1["date"].ToString()) == shiftdate)
                        {
                            if (int.Parse(dtr1["shift_id"].ToString()) == shiftname)
                            {
                                endrow = j;
                                continue;

                            }
                            else
                            {
                                if (int.Parse(dtr1["shift_id"].ToString()) == shiftname + 1)
                                {
                                    endrow = j;
                                    continue;
                                }
                                else
                                {
                                    endrow = j - 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (DateTime.Parse(dtr1["date"].ToString()) == nextdays)
                            {
                                if (int.Parse(dtr1["shift_id"].ToString()) == shiftname - 2)
                                {
                                    endrow = j;
                                    continue;
                                }
                                else
                                {
                                    endrow = j - 1;
                                    break;
                                }
                            }
                            else
                            {
                                endrow = j - 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        endrow = j - 1;
                        break;
                    }
                }

                dtr["startrow"] = startrow;
                dtr["endrow"] = endrow;
                i = i + endrow - startrow;
            }

            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++)
            {
                DataRow dr = dtGroupedBy.Rows[i];
                //var prod_qty = Convert.ToInt32(dr["prod_qty"].ToString());
                //dr["production_qty"] = prod_qty;
                var startrow = dr["startrow"].ToString();
                var endrow = dr["endrow"].ToString();
                var shift_id = dr["shift_id"].ToString();
                var shiftdate = DateTime.Parse(dr["date"].ToString());
                var shiftname = dr["shift_code"].ToString();
                var login_time = dr["login_time"].ToString();
                var logout_time = dr["logout_time"].ToString();
                var shift_hours = dr["shift_hours"].ToString();
                var is_multi_machine = dr["is_multi_machine"].ToString();
                var is_continued_shift = dr["is_continued_shift"].ToString();
                //   dr["remarks"] = "-";
                var operator_id = dr["operator_id"].ToString();
                if (operator_id == "295")
                {
                    var sdsd = "";
                }

                for (var j = i + 1; j <= int.Parse(endrow.ToString()); j++)
                {
                    DataRow dr1 = dtGroupedBy.Rows[j];
                    var shift_date = DateTime.Parse(dr1["date"].ToString());
                    DayOfWeek dow = shift_date.DayOfWeek; //enum
                    string str = dow.ToString(); //string  
                    int numberOfholidays = holiday_list.Where(x => x.holiday_date == shift_date).ToList().Count;
                    var shiftid = int.Parse(dr1["shift_id"].ToString());
                    //var flag = true;                   

                    //if (plant_id == "1")
                    //{
                    //    if (str == "Sunday" && shiftid == 2)
                    //    {
                    //        flag = false;
                    //    }
                    //    else if (str == "Sunday" && shiftid == 3)
                    //    {
                    //        flag = false;
                    //    }
                    //    else if (str == "Monday" && shiftid == 1)
                    //    {
                    //        flag = false;
                    //    }
                    //    else
                    //    {
                    //        flag = true;
                    //    }
                    //}
                    //else
                    //{
                    //    if (str == "Sunday" && shiftid == 5)
                    //    {
                    //        flag = false;
                    //    }
                    //    else if (str == "Sunday" && shiftid == 6)
                    //    {
                    //        flag = false;
                    //    }
                    //    else if (str == "Monday" && shiftid == 4)
                    //    {
                    //        flag = false;
                    //    }
                    //    else
                    //    {
                    //        flag = true;
                    //    }
                    //}
                    //if (numberOfholidays == 0)
                    //{
                    //    if (flag == true)
                    //    {
                    dr1["startrow"] = startrow;
                    dr1["endrow"] = endrow;
                    //  dr1["remarks"] = "Continued Shift";
                    i = j;

                }
            }


            // for multi machining

            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++) //for multi machining
            {
                DataRow row = dtGroupedBy.Rows[i];
                var operator_name = row["operator_name"].ToString();
                var operator_id = row["operator_id"].ToString();
                if(operator_id=="295")
                {
                    var a = "";
                }
                int numberOfRecords = dtGroupedBy.AsEnumerable().Where(x =>
                    x["operator_name"].ToString() == operator_name && x["date"].ToString() == row["date"].ToString() &&
                    x["shift_id"].ToString() == row["shift_id"].ToString()).ToList().Count;
                if (numberOfRecords > 1)
                {
                    var abc = dtGroupedBy.AsEnumerable().Where(x =>
                        x["operator_name"].ToString() == operator_name &&
                        x["date"].ToString() == row["date"].ToString() &&
                        x["shift_id"].ToString() == row["shift_id"].ToString()).Distinct().ToList();
                    var acc = abc.Where(x => x.ItemArray != null).ToList();
                    stringArray = new string[acc.Count()];
                    for (var a = 0; a <= acc.Count - 1; a++)
                    {
                        var ccc = abc[a];
                        var accc = ccc.ItemArray[9];
                        stringArray[a] = accc.ToString();
                    }

                    int numberOfmachines = stringArray.Distinct().Count();
                    if (numberOfmachines > 1)
                    {
                        for (var j = 0; j <= numberOfmachines - 2; j++)
                        {
                            DataRow row1 = dtGroupedBy.Rows[i + j];
                            machine_id1 = row1["machine_id"].ToString();
                            var machinegroup1 = dt2.AsEnumerable().Where(x => x["machine_id"].ToString() == machine_id1)
                                .ToList();

                            for (var k = j + 1; k <= numberOfmachines - 1; k++)
                            {
                                DataRow row2 = dtGroupedBy.Rows[i + k];
                                machine_id2 = row2["machine_id"].ToString();
                                if (machine_id1 != machine_id2)
                                {
                                    for (var m = 0; m <= machinegroup1.Count() - 1; m++)
                                    {
                                        var mm = machinegroup1[m];
                                        var mmm = mm.ItemArray[1];
                                        var machinegroup2 = dt2.AsEnumerable().Where(x =>
                                            x["machine_id"].ToString() == machine_id2 &&
                                            x["machine_group_id"].ToString() == mmm.ToString()).ToList();
                                        if (machinegroup2.Count() > 0)
                                        {
                                            for (var c = i; c <= i + numberOfRecords - 1; c++)
                                            {
                                                DataRow row3 = dtGroupedBy.Rows[c];

                                                row3["is_multi_machine"] = true;
                                            }
                                        }
                                    }
                                }


                            }
                        }
                    }

                    i = i + numberOfRecords - 1;
                }


            }

            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++)
            {
                DataRow row = dtGroupedBy.Rows[i];
                double reporting_quantity = 0;
                double incentive = 0;
                var operator_id = row["operator_id"].ToString();
                if (operator_id == "295")
                {
                    var sdsf = "";
                }

                var shift_id = row["shift_id"].ToString();
                var dtee = row["date"].ToString();
                var item_id = row["item_id"].ToString();
                var process_id = row["process_id"].ToString();
                var machine_id = row["machine_id"].ToString();
                int numberOfRecords = dtGroupedBy.AsEnumerable().Where(x =>
                    x["operator_id"].ToString() == operator_id && x["shift_id"].ToString() == shift_id &&
                    x["date"].ToString() == dtee).ToList().Count;
                var bm = benchmark.Where(x =>
                    x.item_id == int.Parse(item_id) && x.operation_id == int.Parse(process_id) &&
                    x.machine_id == int.Parse(row["machine_id"].ToString())).FirstOrDefault();
                reporting_quantity = bm == null ? 0 : bm.reporting_quantity;
                if (reporting_quantity > 0)
                {
                    var sfdf = 0;
                }
                if (plant_id == "1")
                {
                    if (shift_id == "1")
                    {
                        if(bm!=null)
                        {
                            bm.per_hr_qty = reporting_quantity / 7.50;
                        }
                       
                    }
                    else
                    {
                        if (bm != null)
                        {
                            bm.per_hr_qty = reporting_quantity / 8;
                        }                           
                    }
                    var em = hrdata.Where(x =>
                        x.shift_id.ToString() == shift_id && x.operator_id.ToString() == operator_id &&
                        x.shift_date.ToString() == dtee).FirstOrDefault();
                    var val = (em == null ? 0 : em.time_diff) * (bm == null ? 0 : bm.per_hr_qty) == 0 ? reporting_quantity : (em == null ? 0 : em.time_diff) * (bm == null ? 0 : bm.per_hr_qty);
                    //  val = 50;
                    reporting_quantity = reporting_quantity <= val ? reporting_quantity : val;
                }

                reporting_quantity = (int)Math.Floor(reporting_quantity);
                incentive = bm == null ? 0 : bm.incentive;
                row["remarks"] = bm == null ? "Incentive bench mark master missing" : "";
                row["reporting_quantity"] = reporting_quantity;
                row["reportingquantity"] = reporting_quantity;
                row["incentive"] = incentive;
                row["row_count"] = numberOfRecords;
            }

            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++)
            {
                DataRow row = dtGroupedBy.Rows[i];
                var shift_date = DateTime.Parse(row["date"].ToString());
                DayOfWeek dow = shift_date.DayOfWeek; //enum
                string str = dow.ToString(); //string                
                var is_multi_machine = row["is_multi_machine"].ToString();
                var operator_id = row["operator_id"].ToString();
                double reporting_quantity = double.Parse(row["reporting_quantity"].ToString());
                double incentive = double.Parse(row["incentive"].ToString());
                double prod_qty = double.Parse(row["prod_qty"].ToString() == "" ? "0" : row["prod_qty"].ToString());
                var incentive_applicability = row["incentive_applicability"].ToString().Trim();
                var startrow = int.Parse(row["startrow"].ToString());
                var endrow = int.Parse(row["endrow"].ToString());
                var shift_id = int.Parse(row["shift_id"].ToString());
                int numberOfRecords = int.Parse(row["row_count"].ToString());
                int numberOfholidays = holiday_list.Where(x => x.holiday_date == shift_date).ToList().Count;
                var flag = true;
                if(operator_id=="295")
                {
                    var sdsf = "";
                }

                if (incentive_applicability == "Yes")
                {
                    //for Holidays
                    if (numberOfholidays == 0)
                    {
                        //for sundays
                        if (str == "Sunday")
                        {
                            var vsadhgad = "";
                        }

                        if (plant_id == "1")
                        {
                            if (str == "Sunday" && shift_id == 2)
                            {
                                flag = false;
                            }
                            else if (str == "Sunday" && shift_id == 3)
                            {
                                flag = false;
                            }
                            else if (str == "Monday" && shift_id == 1)
                            {
                                flag = false;
                            }
                            else
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            if (str == "Sunday" && shift_id == 5)
                            {
                                flag = false;
                            }
                            else if (str == "Sunday" && shift_id == 6)
                            {
                                flag = false;
                            }
                            else if (str == "Monday" && shift_id == 4)
                            {
                                flag = false;
                            }
                            else
                            {
                                flag = true;
                            }
                        }

                        if (flag == true)
                        {
                            if (numberOfRecords == 1)
                            {
                                row["diff_qty"] = (prod_qty - reporting_quantity) < 0
                                    ? 0
                                    : (prod_qty - reporting_quantity);
                                var diff_qty = double.Parse(row["diff_qty"].ToString());
                                row["amount"] = diff_qty * incentive;
                            }
                            else if (is_multi_machine == "True" && numberOfRecords != 1)
                            {
                                var machine_id = 0;
                                string _filterString = string.Empty;
                                if (operator_id == "295")
                                {
                                    var sdsf = "";
                                }

                                machine_id = int.Parse(row["machine_id"].ToString());
                                _filterString = " operator_id" + " = '" + row["operator_id"].ToString() + "'" +
                                                " AND  shift_id" + " = '" + row["shift_id"].ToString() + "'" +
                                                " AND  date" + " = '" + row["date"].ToString() + "' AND machine_id" +
                                                " = '" + row["machine_id"].ToString() + "' ";
                                int count = Convert.ToInt32(dtGroupedBy.Compute("count([reportingquantity])",
                                    _filterString));
                                var id = 0;
                                if (count > 1)
                                {
                                    int minbenchqty =
                                        Convert.ToInt32(dtGroupedBy.Compute("min([reportingquantity])",
                                            _filterString));
                                    int bal_bench_qty = minbenchqty;
                                    for (var k = startrow; k <= endrow; k++)
                                    {
                                        DataRow row2 = dtGroupedBy.Rows[k];
                                        var machine_id3 = int.Parse(row2["machine_id"].ToString());
                                        var rqty = int.Parse(row2["reporting_quantity"].ToString());
                                        if (machine_id == machine_id3)
                                        {
                                            if (minbenchqty == rqty)
                                            {
                                                int production_qty = int.Parse(row2["prod_qty"].ToString());
                                                int reporting_qty = minbenchqty == 0 ? rqty :
                                                    (bal_bench_qty - production_qty) < 0 ? bal_bench_qty :
                                                    production_qty;
                                                row2["reporting_quantity"] = reporting_qty;
                                                bal_bench_qty = bal_bench_qty - reporting_qty;
                                                row2["diff_qty"] = (production_qty - reporting_qty) < 0
                                                    ? 0
                                                    : (production_qty - reporting_qty);
                                                incentive = double.Parse(row2["incentive"].ToString());
                                                var diff_qty = double.Parse(row2["diff_qty"].ToString());
                                                row2["amount"] = diff_qty * incentive;
                                                id = k;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            row["diff_qty"] = (prod_qty - reporting_quantity) < 0
                                                ? 0
                                                : (prod_qty - reporting_quantity);
                                            var diff_qty = double.Parse(row["diff_qty"].ToString());
                                            row["amount"] = diff_qty * incentive;
                                        }

                                        i = endrow;
                                    }

                                    for (var k = startrow; k <= endrow; k++)
                                    {
                                        DataRow row2 = dtGroupedBy.Rows[k];
                                        var machine_id3 = int.Parse(row2["machine_id"].ToString());
                                        var rqty = int.Parse(row2["reporting_quantity"].ToString());
                                        if (machine_id3 == machine_id)
                                        {
                                            if (id != k)
                                            {
                                                int production_qty = int.Parse(row2["prod_qty"].ToString());
                                                int reporting_qty = minbenchqty == 0 ? rqty :
                                                    (bal_bench_qty - production_qty) < 0 ? bal_bench_qty :
                                                    production_qty;
                                                row2["reporting_quantity"] = reporting_qty;
                                                bal_bench_qty = bal_bench_qty - reporting_qty;
                                                row2["diff_qty"] = (production_qty - reporting_qty) < 0
                                                    ? 0
                                                    : (production_qty - reporting_qty);
                                                incentive = double.Parse(row2["incentive"].ToString());
                                                var diff_qty = double.Parse(row2["diff_qty"].ToString());
                                                row2["amount"] = diff_qty * incentive;
                                                i = endrow;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    row["diff_qty"] = (prod_qty - reporting_quantity) < 0
                                        ? 0
                                        : (prod_qty - reporting_quantity);
                                    var diff_qty = double.Parse(row["diff_qty"].ToString());
                                    row["amount"] = diff_qty * incentive;
                                }

                            }
                            else if (is_multi_machine == "False" && numberOfRecords != 1)
                            {
                                string _filterString = string.Empty;
                                string _filterString1 = string.Empty;
                                var id = 0;
                                if (operator_id == "295")
                                {
                                    var sdsf = "";
                                }

                                _filterString = " operator_id" + " = '" + row["operator_id"].ToString() + "'" +
                                                " AND  shift_id" + " = '" + row["shift_id"].ToString() + "'" +
                                                " AND  date" + " = '" + row["date"].ToString() + "'";
                                int minbenchqty =
                                    Convert.ToInt32(dtGroupedBy.Compute("min([reportingquantity])", _filterString));
                                //  int minprodqty = Convert.ToInt32(dtGroupedBy.Compute("min([prod_qty])", _filterString));
                                int bal_bench_qty = minbenchqty;
                                _filterString1 = _filterString;
                                for (var k = startrow; k <= endrow; k++)
                                {
                                    DataRow row2 = dtGroupedBy.Rows[k];
                                    var rqty = int.Parse(row2["reporting_quantity"].ToString());
                                    if (minbenchqty == rqty)
                                    {
                                        int production_qty = int.Parse(row2["prod_qty"].ToString());
                                        int reporting_qty = minbenchqty == 0 ? rqty :
                                            (bal_bench_qty - production_qty) < 0 ? bal_bench_qty : production_qty;
                                        row2["reporting_quantity"] = reporting_qty;
                                        bal_bench_qty = bal_bench_qty - reporting_qty;
                                        row2["diff_qty"] = (production_qty - reporting_qty) < 0
                                            ? 0
                                            : (production_qty - reporting_qty);
                                        var diff_qty = double.Parse(row2["diff_qty"].ToString());
                                        incentive = double.Parse(row2["incentive"].ToString());
                                        row2["amount"] = diff_qty * incentive;
                                        id = k;
                                        break;
                                        // var dr = dtGroupedBy.Select(" operator_id" + " = '" + row2["operator_id"].ToString() + "'" + " AND  shift_id" + " = '" + row2["shift_id"].ToString() + "'" + " AND  date" + " = '" + row2["date"].ToString() + "' and reporting_quantity='" + minbenchqty.ToString() + "'");
                                    }

                                }

                                for (var j = startrow; j <= endrow; j++)
                                {
                                    if (startrow == 295)
                                    {
                                        var sdsd = 296;
                                    }

                                    if (j != id)
                                    {
                                        DataRow row1 = dtGroupedBy.Rows[j];
                                        incentive = double.Parse(row1["incentive"].ToString());
                                        int production_qty = int.Parse(row1["prod_qty"].ToString());
                                        int reporting_qty = minbenchqty == 0
                                            ?
                                            int.Parse(row1["reporting_quantity"].ToString())
                                            : (bal_bench_qty - production_qty) < 0
                                                ? bal_bench_qty
                                                : production_qty;
                                        row1["reporting_quantity"] = reporting_qty;
                                        bal_bench_qty = bal_bench_qty - reporting_qty;
                                        row1["diff_qty"] = (production_qty - reporting_qty) < 0
                                            ? 0
                                            : (production_qty - reporting_qty);
                                        var diff_qty = double.Parse(row1["diff_qty"].ToString());

                                        row1["amount"] = diff_qty * incentive;
                                    }
                                }

                                i = endrow;
                            }
                        }

                    }
                }

            }

            DataTable IDT = new DataTable();
            IDT.Columns.Add("date", typeof(DateTime));
            IDT.Columns.Add("shift_id", typeof(int));
            IDT.Columns.Add("process_id", typeof(int));
            IDT.Columns.Add("machine_id", typeof(int));
            IDT.Columns.Add("item_id", typeof(int));
            IDT.Columns.Add("operator_id", typeof(int));
            IDT.Columns.Add("plant_id", typeof(int));
            IDT.Columns.Add("reporting_quantity", typeof(double));
            IDT.Columns.Add("incentive", typeof(double));
            IDT.Columns.Add("prod_qty", typeof(int));
            IDT.Columns.Add("diff_qty", typeof(double));
            IDT.Columns.Add("incentive_applicability", typeof(string));
            IDT.Columns.Add("login_time", typeof(DateTime));
            IDT.Columns.Add("logout_time", typeof(DateTime));
            IDT.Columns.Add("amount", typeof(double));
            IDT.Columns.Add("remarks", typeof(string));
            foreach (DataRow dr in dtGroupedBy.Rows)
            {

                var date = DateTime.Parse(dr["date"].ToString());
                var shift_id = int.Parse(dr["shift_id"].ToString());
                var process_id = int.Parse(dr["process_id"].ToString());
                var machine_id = int.Parse(dr["machine_id"].ToString());
                var item_id = int.Parse(dr["item_id"].ToString());
                var operator_id = int.Parse(dr["operator_id"].ToString());
                var plant_id1 = int.Parse(dr["plant_id"].ToString());
                var reporting_quantity = double.Parse(dr["reporting_quantity"].ToString());
                var incentive = double.Parse(dr["incentive"].ToString());
                var prod_qty = dr["prod_qty"].ToString() == "" ? 0 : int.Parse(dr["prod_qty"].ToString());
                var diff_qty = dr["diff_qty"].ToString() == "" ? 0 : int.Parse(dr["diff_qty"].ToString());
                var incentive_applicability = dr["incentive_applicability"].ToString();
                var login_time = DateTime.Parse(dr["login_time"].ToString());
                var logout_time = DateTime.Parse(dr["logout_time"].ToString());
                var amount = double.Parse(dr["amount"].ToString());
                var remarks = dr["remarks"].ToString();
                IDT.Rows.Add(date, shift_id, process_id, machine_id
                    , item_id, operator_id, plant_id1, reporting_quantity
                    , incentive, prod_qty, diff_qty, incentive_applicability == "No" ? false : true,
                    login_time, logout_time, amount, remarks);


            }
            //  dtGroupedBy.AsEnumerable().CopyToDataTable(IDT, LoadOption.OverwriteChanges);

            var records =
                _opInc.UpdateRecords(start_date_shift_id, end_date_shift_id, from_date, to_date, plant_id, IDT);

            var detailandsum = _Generic.GetAllIncentiveDetailandSummary(start_date_shift_id, end_date_shift_id,
                from_date, to_date, plant_id);
            var val1 = detailandsum.Where(a => a.columnname == "Detail").ToList();
            //ServerSideSearch sss = new ServerSideSearch();
            //IEnumerable data = sss.ProcessDM(dm, val1);
            return Json(val1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ComputeIncentive1(int start_date_shift_id, int end_date_shift_id, DateTime from_date,
        DateTime to_date, string plant_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            to_date = to_date.AddDays(1);
            var res = _Generic.ComputeIncentive(start_date_shift_id, end_date_shift_id, from_date, to_date, plant_id);
            var dt1 = _Generic.ToDataTable(res);
            dt1.Columns.Add("production_qty", typeof(int));
            var dt2 = _Generic.ToDataTable(_Generic.GetMultiMachining());
            var benchmark = _Generic.GetInsentiveBenchmarkDetail("incentive_benchmark");
            var holiday_list = _Generic.GetHolidayList();
            string machine_id1 = "";
            string machine_id2 = "";
            string[] stringArray;
            List<ref_easy_hr_data> hrdata = new List<ref_easy_hr_data>();
            if (plant_id == "1")
            {
                hrdata = _Generic.GetEasyHRData().Where(x => x.shift_date >= from_date && x.shift_date <= to_date).ToList();
            }
            for (var i = 0; i <= dt1.Rows.Count - 1; i++)
            {
                DataRow row = dt1.Rows[i];
                var prod_qty = Convert.ToInt32(row["prod_qty"].ToString());
                row["production_qty"] = prod_qty;
                row["is_multi_machine"] = false;
                row["is_continued_shift"] = false;
                row["startrow"] = 0;
                row["endrow"] = 0;
            }

            //for continued shift
            for (var i = 0; i <= dt1.Rows.Count - 1; i++)
            {
                DataRow dtr = dt1.Rows[i];
                var opname = dtr["operator_name"].ToString();
                var shiftdate = DateTime.Parse(dtr["date"].ToString());
                var nextdays = shiftdate.AddDays(1);
                var shiftname = int.Parse(dtr["shift_id"].ToString());
                var startrow = i;
                var endrow = 0;
                for (var j = i; j <= dt1.Rows.Count - 1; j++)
                {
                    DataRow dtr1 = dt1.Rows[j];
                    if (dtr1["operator_name"].ToString() == opname)
                    {
                        if (DateTime.Parse(dtr1["date"].ToString()) == shiftdate)
                        {
                            if (int.Parse(dtr1["shift_id"].ToString()) == shiftname)
                            {
                                endrow = j;
                                continue;

                            }
                            else
                            {
                                if (int.Parse(dtr1["shift_id"].ToString()) == shiftname + 1)
                                {
                                    endrow = j;
                                    continue;
                                }
                                else
                                {
                                    endrow = j - 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (DateTime.Parse(dtr1["date"].ToString()) == nextdays)
                            {
                                if (int.Parse(dtr1["shift_id"].ToString()) == shiftname - 2)
                                {
                                    endrow = j;
                                    continue;
                                }
                                else
                                {
                                    endrow = j - 1;
                                    break;
                                }
                            }
                            else
                            {
                                endrow = j - 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        endrow = j - 1;
                        break;
                    }
                }

                dtr["startrow"] = startrow;
                dtr["endrow"] = endrow;
                i = i + endrow - startrow;
                if (i == 84)
                {
                    var asdaf = "";
                }
            }

            //for updating shift id,shift code,shift date
            for (var i = 0; i <= dt1.Rows.Count - 1; i++)
            {
                DataRow dr = dt1.Rows[i];
                var startrow = dr["startrow"].ToString();
                var endrow = dr["endrow"].ToString();
                var shift_id = dr["shift_id"].ToString();
                var shiftdate = DateTime.Parse(dr["date"].ToString());
                var shiftname = dr["shift_code"].ToString();
                var login_time = dr["login_time"].ToString();
                var logout_time = dr["logout_time"].ToString();
                var shift_hours = dr["shift_hours"].ToString();
                var is_multi_machine = dr["is_multi_machine"].ToString();
                var is_continued_shift = dr["is_continued_shift"].ToString();
                for (var j = i + 1; j <= int.Parse(endrow.ToString()); j++)
                {
                    DataRow dr1 = dt1.Rows[j];
                    var shift_date = DateTime.Parse(dr1["date"].ToString());
                    DayOfWeek dow = shift_date.DayOfWeek; //enum
                    string str = dow.ToString(); //string  
                    int numberOfholidays = holiday_list.Where(x => x.holiday_date == shift_date).ToList().Count;
                    var shiftid = int.Parse(dr1["shift_id"].ToString());
                    var flag = true;

                    if (plant_id == "1")
                    {
                        if (str == "Sunday" && shiftid == 2)
                        {
                            flag = false;
                        }
                        else if (str == "Sunday" && shiftid == 3)
                        {
                            flag = false;
                        }
                        else if (str == "Monday" && shiftid == 1)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        if (str == "Sunday" && shiftid == 5)
                        {
                            flag = false;
                        }
                        else if (str == "Sunday" && shiftid == 6)
                        {
                            flag = false;
                        }
                        else if (str == "Monday" && shiftid == 4)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }

                    if (numberOfholidays == 0)
                    {
                        if (flag == true)
                        {
                            if (shift_id != dr1["shift_id"].ToString())
                            {
                                dr1["is_continued_shift"] = true;
                            }

                            dr1["shift_id"] = shift_id;
                            dr1["shift_code"] = shiftname;
                            dr1["date"] = shiftdate;
                            dr1["login_time"] = login_time;
                            dr1["logout_time"] = logout_time;
                            dr1["shift_hours"] = shift_hours;
                            dr1["is_multi_machine"] = is_multi_machine;
                            dr1["is_continued_shift"] = is_continued_shift;
                            dr1["startrow"] = startrow;
                            dr1["endrow"] = endrow;
                            //  dr1["remarks"] = "Continued Shift";
                            i = j;
                        }
                    }
                }
            }

            //  dt1.Columns["prod_qty"].DataType = typeof(Int32);
            IList<string> _groupByColumnNames = new List<string>();
            _groupByColumnNames.Add("operator_id");
            _groupByColumnNames.Add("operator_name");
            _groupByColumnNames.Add("user_name");
            _groupByColumnNames.Add("date");
            _groupByColumnNames.Add("shift_id");
            _groupByColumnNames.Add("shift_code");
            _groupByColumnNames.Add("item_id");
            _groupByColumnNames.Add("item_code");
            _groupByColumnNames.Add("item_name");
            _groupByColumnNames.Add("machine_id");
            _groupByColumnNames.Add("machine_code");
            _groupByColumnNames.Add("machine_name");
            _groupByColumnNames.Add("process_id");
            _groupByColumnNames.Add("process_desc");
            _groupByColumnNames.Add("process_description");
            _groupByColumnNames.Add("plant_id");
            _groupByColumnNames.Add("plant_name");
            _groupByColumnNames.Add("incentive_applicability");
            _groupByColumnNames.Add("login_time");
            _groupByColumnNames.Add("logout_time");
            _groupByColumnNames.Add("shift_hours");
            _groupByColumnNames.Add("reporting_quantity");
            _groupByColumnNames.Add("incentive");
            _groupByColumnNames.Add("diff_qty");
            _groupByColumnNames.Add("amount");
            _groupByColumnNames.Add("is_multi_machine");
            _groupByColumnNames.Add("is_continued_shift");
            _groupByColumnNames.Add("startrow");
            _groupByColumnNames.Add("endrow");





            IList<DataTableAggregateFunction> _fieldsForCalculation = new List<DataTableAggregateFunction>();
            _fieldsForCalculation.Add(new DataTableAggregateFunction()
            { enmFunction = AggregateFunction.Sum, ColumnName = "production_qty", OutPutColumnName = "prod_qty" });

            DataTable dtGroupedBy = GetGroupedBy(dt1, _groupByColumnNames, _fieldsForCalculation);
            dtGroupedBy.Columns.Add("row_count", typeof(int));
            dtGroupedBy.Columns.Add("remarks", typeof(string));
            dtGroupedBy.Columns.Add("reportingquantity", typeof(int));
            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++)
            {
                if (i == 171)
                {
                    var abc = i;
                }

                DataRow dtr = dtGroupedBy.Rows[i];
                var opname = dtr["operator_name"].ToString();
                var shiftdate = DateTime.Parse(dtr["date"].ToString());
                var nextdays = shiftdate.AddDays(1);
                var shiftname = int.Parse(dtr["shift_id"].ToString());
                var startrow = i;
                var endrow = 0;
                for (var j = i; j <= dtGroupedBy.Rows.Count - 1; j++)
                {
                    DataRow dtr1 = dtGroupedBy.Rows[j];
                    if (dtr1["operator_name"].ToString() == opname)
                    {
                        if (DateTime.Parse(dtr1["date"].ToString()) == shiftdate)
                        {
                            if (int.Parse(dtr1["shift_id"].ToString()) == shiftname)
                            {
                                endrow = j;
                                continue;

                            }
                            else
                            {
                                if (int.Parse(dtr1["shift_id"].ToString()) == shiftname + 1)
                                {
                                    endrow = j;
                                    continue;
                                }
                                else
                                {
                                    endrow = j - 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (DateTime.Parse(dtr1["date"].ToString()) == nextdays)
                            {
                                if (int.Parse(dtr1["shift_id"].ToString()) == shiftname - 2)
                                {
                                    endrow = j;
                                    continue;
                                }
                                else
                                {
                                    endrow = j - 1;
                                    break;
                                }
                            }
                            else
                            {
                                endrow = j - 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        endrow = j - 1;
                        break;
                    }
                }

                dtr["startrow"] = startrow;
                dtr["endrow"] = endrow;
                i = i + endrow - startrow;
            }

            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++)
            {
                DataRow dr = dtGroupedBy.Rows[i];
                //var prod_qty = Convert.ToInt32(dr["prod_qty"].ToString());
                //dr["production_qty"] = prod_qty;
                var startrow = dr["startrow"].ToString();
                var endrow = dr["endrow"].ToString();
                var shift_id = dr["shift_id"].ToString();
                var shiftdate = DateTime.Parse(dr["date"].ToString());
                var shiftname = dr["shift_code"].ToString();
                var login_time = dr["login_time"].ToString();
                var logout_time = dr["logout_time"].ToString();
                var shift_hours = dr["shift_hours"].ToString();
                var is_multi_machine = dr["is_multi_machine"].ToString();
                var is_continued_shift = dr["is_continued_shift"].ToString();
                //   dr["remarks"] = "-";
                var operator_id = dr["operator_id"].ToString();
                if (operator_id == "295")
                {
                    var sdsd = "";
                }

                for (var j = i + 1; j <= int.Parse(endrow.ToString()); j++)
                {
                    DataRow dr1 = dtGroupedBy.Rows[j];
                    var shift_date = DateTime.Parse(dr1["date"].ToString());
                    DayOfWeek dow = shift_date.DayOfWeek; //enum
                    string str = dow.ToString(); //string  
                    int numberOfholidays = holiday_list.Where(x => x.holiday_date == shift_date).ToList().Count;
                    var shiftid = int.Parse(dr1["shift_id"].ToString());
                    //var flag = true;                   

                    //if (plant_id == "1")
                    //{
                    //    if (str == "Sunday" && shiftid == 2)
                    //    {
                    //        flag = false;
                    //    }
                    //    else if (str == "Sunday" && shiftid == 3)
                    //    {
                    //        flag = false;
                    //    }
                    //    else if (str == "Monday" && shiftid == 1)
                    //    {
                    //        flag = false;
                    //    }
                    //    else
                    //    {
                    //        flag = true;
                    //    }
                    //}
                    //else
                    //{
                    //    if (str == "Sunday" && shiftid == 5)
                    //    {
                    //        flag = false;
                    //    }
                    //    else if (str == "Sunday" && shiftid == 6)
                    //    {
                    //        flag = false;
                    //    }
                    //    else if (str == "Monday" && shiftid == 4)
                    //    {
                    //        flag = false;
                    //    }
                    //    else
                    //    {
                    //        flag = true;
                    //    }
                    //}
                    //if (numberOfholidays == 0)
                    //{
                    //    if (flag == true)
                    //    {
                    dr1["startrow"] = startrow;
                    dr1["endrow"] = endrow;
                    //  dr1["remarks"] = "Continued Shift";
                    i = j;

                }
            }


            // for multi machining

            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++) //for multi machining
            {
                DataRow row = dtGroupedBy.Rows[i];
                var operator_name = row["operator_name"].ToString();
                var operator_id = row["operator_id"].ToString();
                if (operator_id == "295")
                {
                    var a = "";
                }
                int numberOfRecords = dtGroupedBy.AsEnumerable().Where(x =>
                    x["operator_name"].ToString() == operator_name && x["date"].ToString() == row["date"].ToString() &&
                    x["shift_id"].ToString() == row["shift_id"].ToString()).ToList().Count;
                if (numberOfRecords > 1)
                {
                    var abc = dtGroupedBy.AsEnumerable().Where(x =>
                        x["operator_name"].ToString() == operator_name &&
                        x["date"].ToString() == row["date"].ToString() &&
                        x["shift_id"].ToString() == row["shift_id"].ToString()).Distinct().ToList();
                    var acc = abc.Where(x => x.ItemArray != null).ToList();
                    stringArray = new string[acc.Count()];
                    for (var a = 0; a <= acc.Count - 1; a++)
                    {
                        var ccc = abc[a];
                        var accc = ccc.ItemArray[9];
                        stringArray[a] = accc.ToString();
                    }

                    int numberOfmachines = stringArray.Distinct().Count();
                    if (numberOfmachines > 1)
                    {
                        for (var j = 0; j <= numberOfmachines - 2; j++)
                        {
                            DataRow row1 = dtGroupedBy.Rows[i + j];
                            machine_id1 = row1["machine_id"].ToString();
                            var machinegroup1 = dt2.AsEnumerable().Where(x => x["machine_id"].ToString() == machine_id1)
                                .ToList();

                            for (var k = j + 1; k <= numberOfmachines - 1; k++)
                            {
                                DataRow row2 = dtGroupedBy.Rows[i + k];
                                machine_id2 = row2["machine_id"].ToString();
                                if (machine_id1 != machine_id2)
                                {
                                    for (var m = 0; m <= machinegroup1.Count() - 1; m++)
                                    {
                                        var mm = machinegroup1[m];
                                        var mmm = mm.ItemArray[1];
                                        var machinegroup2 = dt2.AsEnumerable().Where(x =>
                                            x["machine_id"].ToString() == machine_id2 &&
                                            x["machine_group_id"].ToString() == mmm.ToString()).ToList();
                                        if (machinegroup2.Count() > 0)
                                        {
                                            for (var c = i; c <= i + numberOfRecords - 1; c++)
                                            {
                                                DataRow row3 = dtGroupedBy.Rows[c];

                                                row3["is_multi_machine"] = true;
                                            }
                                        }
                                    }
                                }


                            }
                        }
                    }

                    i = i + numberOfRecords - 1;
                }


            }

            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++)
            {
                DataRow row = dtGroupedBy.Rows[i];
                double reporting_quantity = 0;
                double incentive = 0;
                var operator_id = row["operator_id"].ToString();
                if (operator_id == "295")
                {
                    var sdsf = "";
                }

                var shift_id = row["shift_id"].ToString();
                var dtee = row["date"].ToString();
                var item_id = row["item_id"].ToString();
                var process_id = row["process_id"].ToString();
                var machine_id = row["machine_id"].ToString();
                int numberOfRecords = dtGroupedBy.AsEnumerable().Where(x =>
                    x["operator_id"].ToString() == operator_id && x["shift_id"].ToString() == shift_id &&
                    x["date"].ToString() == dtee).ToList().Count;
                var bm = benchmark.Where(x =>
                    x.item_id == int.Parse(item_id) && x.operation_id == int.Parse(process_id) &&
                    x.machine_id == int.Parse(row["machine_id"].ToString())).FirstOrDefault();
                reporting_quantity = bm == null ? 0 : bm.reporting_quantity;
                if (reporting_quantity > 0)
                {
                    var sfdf = 0;
                }
                if (plant_id == "1")
                {
                    if (shift_id == "1")
                    {
                        if (bm != null)
                        {
                            bm.per_hr_qty = reporting_quantity / 7.50;
                        }

                    }
                    else
                    {
                        if (bm != null)
                        {
                            bm.per_hr_qty = reporting_quantity / 8;
                        }
                    }
                    var em = hrdata.Where(x =>
                        x.shift_id.ToString() == shift_id && x.operator_id.ToString() == operator_id &&
                        x.shift_date.ToString() == dtee).FirstOrDefault();
                    var val = (em == null ? 0 : em.time_diff) * (bm == null ? 0 : bm.per_hr_qty) == 0 ? reporting_quantity : (em == null ? 0 : em.time_diff) * (bm == null ? 0 : bm.per_hr_qty);
                    //  val = 50;
                    reporting_quantity = reporting_quantity <= val ? reporting_quantity : val;
                }

                reporting_quantity = (int)Math.Floor(reporting_quantity);
                incentive = bm == null ? 0 : bm.incentive;
                row["remarks"] = bm == null ? "Incentive bench mark master missing" : "";
                row["reporting_quantity"] = reporting_quantity;
                row["reportingquantity"] = reporting_quantity;
                row["incentive"] = incentive;
                row["row_count"] = numberOfRecords;
            }

            for (var i = 0; i <= dtGroupedBy.Rows.Count - 1; i++)
            {
                DataRow row = dtGroupedBy.Rows[i];
                var shift_date = DateTime.Parse(row["date"].ToString());
                DayOfWeek dow = shift_date.DayOfWeek; //enum
                string str = dow.ToString(); //string                
                var is_multi_machine = row["is_multi_machine"].ToString();
                var operator_id = row["operator_id"].ToString();
                double reporting_quantity = double.Parse(row["reporting_quantity"].ToString());
                double incentive = double.Parse(row["incentive"].ToString());
                double prod_qty = double.Parse(row["prod_qty"].ToString() == "" ? "0" : row["prod_qty"].ToString());
                var incentive_applicability = row["incentive_applicability"].ToString().Trim();
                var startrow = int.Parse(row["startrow"].ToString());
                var endrow = int.Parse(row["endrow"].ToString());
                var shift_id = int.Parse(row["shift_id"].ToString());
                int numberOfRecords = int.Parse(row["row_count"].ToString());
                int numberOfholidays = holiday_list.Where(x => x.holiday_date == shift_date).ToList().Count;
                var flag = true;
                if (operator_id == "295")
                {
                    var sdsf = "";
                }

                if (incentive_applicability == "Yes")
                {
                    //for Holidays
                    if (numberOfholidays == 0)
                    {
                        //for sundays
                        if (str == "Sunday")
                        {
                            var vsadhgad = "";
                        }

                        if (plant_id == "1")
                        {
                            if (str == "Sunday" && shift_id == 2)
                            {
                                flag = false;
                            }
                            else if (str == "Sunday" && shift_id == 3)
                            {
                                flag = false;
                            }
                            else if (str == "Monday" && shift_id == 1)
                            {
                                flag = false;
                            }
                            else
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            if (str == "Sunday" && shift_id == 5)
                            {
                                flag = false;
                            }
                            else if (str == "Sunday" && shift_id == 6)
                            {
                                flag = false;
                            }
                            else if (str == "Monday" && shift_id == 4)
                            {
                                flag = false;
                            }
                            else
                            {
                                flag = true;
                            }
                        }

                        if (flag == true)
                        {
                            if (numberOfRecords == 1)
                            {
                                row["diff_qty"] = (prod_qty - reporting_quantity) < 0
                                    ? 0
                                    : (prod_qty - reporting_quantity);
                                var diff_qty = double.Parse(row["diff_qty"].ToString());
                                row["amount"] = diff_qty * incentive;
                            }
                            else if (is_multi_machine == "True" && numberOfRecords != 1)
                            {
                                var machine_id = 0;
                                string _filterString = string.Empty;
                                if (operator_id == "295")
                                {
                                    var sdsf = "";
                                }

                                machine_id = int.Parse(row["machine_id"].ToString());
                                _filterString = " operator_id" + " = '" + row["operator_id"].ToString() + "'" +
                                                " AND  shift_id" + " = '" + row["shift_id"].ToString() + "'" +
                                                " AND  date" + " = '" + row["date"].ToString() + "' AND machine_id" +
                                                " = '" + row["machine_id"].ToString() + "' ";
                                int count = Convert.ToInt32(dtGroupedBy.Compute("count([reportingquantity])",
                                    _filterString));
                                var id = 0;
                                if (count > 1)
                                {
                                    int minbenchqty =
                                        Convert.ToInt32(dtGroupedBy.Compute("min([reportingquantity])",
                                            _filterString));
                                    int bal_bench_qty = minbenchqty;
                                    for (var k = startrow; k <= endrow; k++)
                                    {
                                        DataRow row2 = dtGroupedBy.Rows[k];
                                        var machine_id3 = int.Parse(row2["machine_id"].ToString());
                                        var rqty = int.Parse(row2["reporting_quantity"].ToString());
                                        if (machine_id == machine_id3)
                                        {
                                            if (minbenchqty == rqty)
                                            {
                                                int production_qty = int.Parse(row2["prod_qty"].ToString());
                                                int reporting_qty = minbenchqty == 0 ? rqty :
                                                    (bal_bench_qty - production_qty) < 0 ? bal_bench_qty :
                                                    production_qty;
                                                row2["reporting_quantity"] = reporting_qty;
                                                bal_bench_qty = bal_bench_qty - reporting_qty;
                                                row2["diff_qty"] = (production_qty - reporting_qty) < 0
                                                    ? 0
                                                    : (production_qty - reporting_qty);
                                                incentive = double.Parse(row2["incentive"].ToString());
                                                var diff_qty = double.Parse(row2["diff_qty"].ToString());
                                                row2["amount"] = diff_qty * incentive;
                                                id = k;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            row["diff_qty"] = (prod_qty - reporting_quantity) < 0
                                                ? 0
                                                : (prod_qty - reporting_quantity);
                                            var diff_qty = double.Parse(row["diff_qty"].ToString());
                                            row["amount"] = diff_qty * incentive;
                                        }

                                        i = endrow;
                                    }

                                    for (var k = startrow; k <= endrow; k++)
                                    {
                                        DataRow row2 = dtGroupedBy.Rows[k];
                                        var machine_id3 = int.Parse(row2["machine_id"].ToString());
                                        var rqty = int.Parse(row2["reporting_quantity"].ToString());
                                        if (machine_id3 == machine_id)
                                        {
                                            if (id != k)
                                            {
                                                int production_qty = int.Parse(row2["prod_qty"].ToString());
                                                int reporting_qty = minbenchqty == 0 ? rqty :
                                                    (bal_bench_qty - production_qty) < 0 ? bal_bench_qty :
                                                    production_qty;
                                                row2["reporting_quantity"] = reporting_qty;
                                                bal_bench_qty = bal_bench_qty - reporting_qty;
                                                row2["diff_qty"] = (production_qty - reporting_qty) < 0
                                                    ? 0
                                                    : (production_qty - reporting_qty);
                                                incentive = double.Parse(row2["incentive"].ToString());
                                                var diff_qty = double.Parse(row2["diff_qty"].ToString());
                                                row2["amount"] = diff_qty * incentive;
                                                i = endrow;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    row["diff_qty"] = (prod_qty - reporting_quantity) < 0
                                        ? 0
                                        : (prod_qty - reporting_quantity);
                                    var diff_qty = double.Parse(row["diff_qty"].ToString());
                                    row["amount"] = diff_qty * incentive;
                                }

                            }
                            else if (is_multi_machine == "False" && numberOfRecords != 1)
                            {
                                string _filterString = string.Empty;
                                string _filterString1 = string.Empty;
                                var id = 0;
                                if (operator_id == "295")
                                {
                                    var sdsf = "";
                                }

                                _filterString = " operator_id" + " = '" + row["operator_id"].ToString() + "'" +
                                                " AND  shift_id" + " = '" + row["shift_id"].ToString() + "'" +
                                                " AND  date" + " = '" + row["date"].ToString() + "'";
                                int minbenchqty =
                                    Convert.ToInt32(dtGroupedBy.Compute("min([reportingquantity])", _filterString));
                                //  int minprodqty = Convert.ToInt32(dtGroupedBy.Compute("min([prod_qty])", _filterString));
                                int bal_bench_qty = minbenchqty;
                                _filterString1 = _filterString;
                                for (var k = startrow; k <= endrow; k++)
                                {
                                    DataRow row2 = dtGroupedBy.Rows[k];
                                    var rqty = int.Parse(row2["reporting_quantity"].ToString());
                                    if (minbenchqty == rqty)
                                    {
                                        int production_qty = int.Parse(row2["prod_qty"].ToString());
                                        int reporting_qty = minbenchqty == 0 ? rqty :
                                            (bal_bench_qty - production_qty) < 0 ? bal_bench_qty : production_qty;
                                        row2["reporting_quantity"] = reporting_qty;
                                        bal_bench_qty = bal_bench_qty - reporting_qty;
                                        row2["diff_qty"] = (production_qty - reporting_qty) < 0
                                            ? 0
                                            : (production_qty - reporting_qty);
                                        var diff_qty = double.Parse(row2["diff_qty"].ToString());
                                        incentive = double.Parse(row2["incentive"].ToString());
                                        row2["amount"] = diff_qty * incentive;
                                        id = k;
                                        break;
                                        // var dr = dtGroupedBy.Select(" operator_id" + " = '" + row2["operator_id"].ToString() + "'" + " AND  shift_id" + " = '" + row2["shift_id"].ToString() + "'" + " AND  date" + " = '" + row2["date"].ToString() + "' and reporting_quantity='" + minbenchqty.ToString() + "'");
                                    }

                                }

                                for (var j = startrow; j <= endrow; j++)
                                {
                                    if (startrow == 295)
                                    {
                                        var sdsd = 296;
                                    }

                                    if (j != id)
                                    {
                                        DataRow row1 = dtGroupedBy.Rows[j];
                                        incentive = double.Parse(row1["incentive"].ToString());
                                        int production_qty = int.Parse(row1["prod_qty"].ToString());
                                        int reporting_qty = minbenchqty == 0
                                            ?
                                            int.Parse(row1["reporting_quantity"].ToString())
                                            : (bal_bench_qty - production_qty) < 0
                                                ? bal_bench_qty
                                                : production_qty;
                                        row1["reporting_quantity"] = reporting_qty;
                                        bal_bench_qty = bal_bench_qty - reporting_qty;
                                        row1["diff_qty"] = (production_qty - reporting_qty) < 0
                                            ? 0
                                            : (production_qty - reporting_qty);
                                        var diff_qty = double.Parse(row1["diff_qty"].ToString());

                                        row1["amount"] = diff_qty * incentive;
                                    }
                                }

                                i = endrow;
                            }
                        }

                    }
                }

            }

            DataTable IDT = new DataTable();
            IDT.Columns.Add("date", typeof(DateTime));
            IDT.Columns.Add("shift_id", typeof(int));
            IDT.Columns.Add("process_id", typeof(int));
            IDT.Columns.Add("machine_id", typeof(int));
            IDT.Columns.Add("item_id", typeof(int));
            IDT.Columns.Add("operator_id", typeof(int));
            IDT.Columns.Add("plant_id", typeof(int));
            IDT.Columns.Add("reporting_quantity", typeof(double));
            IDT.Columns.Add("incentive", typeof(double));
            IDT.Columns.Add("prod_qty", typeof(int));
            IDT.Columns.Add("diff_qty", typeof(double));
            IDT.Columns.Add("incentive_applicability", typeof(string));
            IDT.Columns.Add("login_time", typeof(DateTime));
            IDT.Columns.Add("logout_time", typeof(DateTime));
            IDT.Columns.Add("amount", typeof(double));
            IDT.Columns.Add("remarks", typeof(string));
            foreach (DataRow dr in dtGroupedBy.Rows)
            {

                var date = DateTime.Parse(dr["date"].ToString());
                var shift_id = int.Parse(dr["shift_id"].ToString());
                var process_id = int.Parse(dr["process_id"].ToString());
                var machine_id = int.Parse(dr["machine_id"].ToString());
                var item_id = int.Parse(dr["item_id"].ToString());
                var operator_id = int.Parse(dr["operator_id"].ToString());
                var plant_id1 = int.Parse(dr["plant_id"].ToString());
                var reporting_quantity = double.Parse(dr["reporting_quantity"].ToString());
                var incentive = double.Parse(dr["incentive"].ToString());
                var prod_qty = dr["prod_qty"].ToString() == "" ? 0 : int.Parse(dr["prod_qty"].ToString());
                var diff_qty = dr["diff_qty"].ToString() == "" ? 0 : int.Parse(dr["diff_qty"].ToString());
                var incentive_applicability = dr["incentive_applicability"].ToString();
                var login_time = DateTime.Parse(dr["login_time"].ToString());
                var logout_time = DateTime.Parse(dr["logout_time"].ToString());
                var amount = double.Parse(dr["amount"].ToString());
                var remarks = dr["remarks"].ToString();
                IDT.Rows.Add(date, shift_id, process_id, machine_id
                    , item_id, operator_id, plant_id1, reporting_quantity
                    , incentive, prod_qty, diff_qty, incentive_applicability == "No" ? false : true,
                    login_time, logout_time, amount, remarks);


            }
            //  dtGroupedBy.AsEnumerable().CopyToDataTable(IDT, LoadOption.OverwriteChanges);

            var records =
                _opInc.UpdateRecords(start_date_shift_id, end_date_shift_id, from_date, to_date, plant_id, IDT);

            var detailandsum = _Generic.GetAllIncentiveDetailandSummary(start_date_shift_id, end_date_shift_id,
                from_date, to_date, plant_id);
            var val1 = detailandsum.Where(a => a.columnname == "Detail").ToList();
            //ServerSideSearch sss = new ServerSideSearch();
            //IEnumerable data = sss.ProcessDM(dm, val1);
            return Json(val1, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetIncentiveReport_Partial(int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.start_date_shift_id = start_date_shift_id;
            ViewBag.end_date_shift_id = end_date_shift_id;
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.plant_id = plant_id;

            return PartialView("Partial_IncetiveDetail", ViewBag);
        }

        public void ExportToExcel(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            var DataSource = GetAllData(ctrlname);
            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
        }



        public object GetAllData(string controller)
        {
            object datasource = null;
            var det = _Generic.GetAllIncentiveDetailandSummary(start_date_shift_id, end_date_shift_id, from_date, to_date, plant_id);
            datasource = det.Where(a => a.columnname == "Detail").ToList();
            return datasource;
        }


        private GridProperties ConvertGridObject(string gridProperty, string ctrlname)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            GridProperties gridProp = new GridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (ds.Key == "plant_id")
                {
                    if (ds.Value != "")
                    {
                        plant_id = ds.Value.ToString();
                    }
                    continue;

                }

                if (ds.Key == "from_date")
                {
                    if (ds.Value != "")
                    {
                        from_date = DateTime.Parse(ds.Value.ToString());
                    }
                    continue;
                }
                if (ds.Key == "to_date")
                {
                    if (ds.Value != "")
                    {
                        to_date = DateTime.Parse(ds.Value.ToString());
                    }
                    continue;
                }

                if (ds.Key == "start_date_shift_id")
                {
                    if (ds.Value != "")
                    {
                        start_date_shift_id = int.Parse(ds.Value.ToString());
                    }
                    continue;
                }

                if (ds.Key == "end_date_shift_id")
                {
                    if (ds.Value != "")
                    {
                        end_date_shift_id = int.Parse(ds.Value.ToString());
                    }
                    continue;
                }
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

        public ActionResult PullEasyHRData(int plant_id, List<Report_incentive_vm> tableArr)
        {
            try
            {
                string URL = System.Configuration.ConfigurationManager.AppSettings["Url"].ToString();

                foreach (var d in tableArr)
                {
                    DateTime dte = (DateTime)d.date;
                    var shift_code = d.shift_code;
                    HttpWebRequest request =
                        (HttpWebRequest)WebRequest.Create((URL + "?date=" + dte + "&shift_code=" + shift_code).Trim());
                    request.Method = "GET";
                    request.KeepAlive = true;
                    request.AllowAutoRedirect = false;
                    request.Accept = "*/*";
                    request.ContentType = "application/json";
                    request.Headers.Add("X-API-KEY", "0a9861ddc356cc162dd138afd0e2db1a4bf063b8");
                    WebResponse response = request.GetResponse();
                    string result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    JObject details = JObject.Parse(result);
                    JArray jarray = JArray.Parse(details["result"].ToString());
                    var dt1 = (DataTable)JsonConvert.DeserializeObject(jarray.ToString(), (typeof(DataTable)));
                    dt1.Columns.Add("shift_id", typeof(int));
                    foreach (DataRow row in dt1.Rows)
                    {
                        //need to set value to NewColumn column
                        row["shift_id"] = d.shift_id; // or set it to some other value

                        // DataRow[] selected = dt1.Select("[Shift Code] =" + d.shift_code);
                    }
                    var is_saved = _opInc.UploadEasyHrData(dt1);

                }
                return Json("Saved", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

        }

    }
}