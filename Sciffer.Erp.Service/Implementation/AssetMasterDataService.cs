using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class AssetMasterDataService : BaseService, IAssetMasterDataService
    {
        private readonly ScifferContext _scifferContext;

        public AssetMasterDataService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(ref_asset_master_data_vm Assetdata, List<ref_asset_master_data_dep_parameter_vm> DepParaArr)
        {
            try
            {

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("asset_master_dep_parameter_id", typeof(int));
                dt1.Columns.Add("dep_area_id", typeof(int));
                dt1.Columns.Add("dep_start_date", typeof(string));
                dt1.Columns.Add("dep_end_date", typeof(string));
                dt1.Columns.Add("dep_type_frquency_id", typeof(int));
                dt1.Columns.Add("useful_life_months", typeof(int));
                dt1.Columns.Add("remaining_life_months", typeof(int));
                dt1.Columns.Add("asset_class_dep_id", typeof(int));


                if (DepParaArr != null)
                {
                    foreach (var d in DepParaArr)
                    {
                        dt1.Rows.Add(d.asset_master_dep_parameter_id, d.dep_area_id, d.dep_start_date, d.dep_end_date, d.dep_type_frquency_id, d.useful_life_months, d.remaining_life_months, d.asset_class_dep_id);
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_asset_master_dep_parameter_detail";
                t1.Value = dt1;



                DateTime dte = new DateTime(0001, 01, 01);
                DateTime dte1 = new DateTime(1990, 01, 01);
                var created_by1 = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var asset_master_data_id = new SqlParameter("@asset_master_data_id", Assetdata.asset_master_data_id == null ? 0 : Assetdata.asset_master_data_id);
                var asset_master_data_code = new SqlParameter("@asset_master_data_code", Assetdata.asset_master_data_code == null ? "" : Assetdata.asset_master_data_code);
                var machine_id = new SqlParameter("@machine_id", Assetdata.machine_id == null ? 0 : Assetdata.machine_id);
                var asset_master_data_desc = new SqlParameter("@asset_master_data_desc", Assetdata.asset_master_data_desc == null ? "" : Assetdata.asset_master_data_desc);
                var asset_class_id = new SqlParameter("@asset_class_id", Assetdata.asset_class_id == null ? 0 : Assetdata.asset_class_id);
                var asset_group_id = new SqlParameter("@asset_group_id", Assetdata.asset_group_id == null ? 0 : Assetdata.asset_group_id);
                var status_id = new SqlParameter("@status_id", Assetdata.status_id == null ? 0 : Assetdata.status_id);
                var plant_id = new SqlParameter("@plant_id", Assetdata.plant_id == null ? 0 : Assetdata.plant_id);
                var purchase_order = new SqlParameter("@purchase_order", Assetdata.purchase_order == null ? "" : Assetdata.purchase_order);
                var manufacturer = new SqlParameter("@manufacturer", Assetdata.manufacturer == null ? "" : Assetdata.manufacturer);
                var manufacturer_part_no = new SqlParameter("@manufacturer_part_no", Assetdata.manufacturer_part_no == null ? "" : Assetdata.manufacturer_part_no);
                var manufacturing_country_id = new SqlParameter("@manufacturing_country_id", Assetdata.manufacturing_country_id == null ? 0 : Assetdata.manufacturing_country_id);
                var priority_id = new SqlParameter("@priority_id", Assetdata.priority_id == null ? 0 : Assetdata.priority_id);
                var business_unit_id = new SqlParameter("@business_unit_id", Assetdata.business_unit_id == null ? 0 : Assetdata.business_unit_id);
                var asset_tag_no = new SqlParameter("@asset_tag_no", Assetdata.asset_tag_no == null ? "" : Assetdata.asset_tag_no);
                var purchasing_vendor_id = new SqlParameter("@purchasing_vendor_id", Assetdata.purchasing_vendor_id == null ? 0 : Assetdata.purchasing_vendor_id);
                var model_no = new SqlParameter("@model_no", Assetdata.model_no == null ? "" : Assetdata.model_no);
                var manufacturing_serial_number = new SqlParameter("@manufacturing_serial_number", Assetdata.manufacturing_serial_number == null ? "" : Assetdata.manufacturing_serial_number);
                var manufacturing_date = new SqlParameter("@manufacturing_date", Assetdata.manufacturing_date == null ? dte1 : Assetdata.manufacturing_date == dte ? dte1 : Assetdata.manufacturing_date);
                var cost_center_id = new SqlParameter("@cost_center_id", Assetdata.cost_center_id == null ? 0 : Assetdata.cost_center_id);
                var capitalization_date = new SqlParameter("@capitalization_date", Assetdata.capitalization_date == null ? dte1 : Assetdata.capitalization_date == dte ? dte1 : Assetdata.capitalization_date);
                var created_by = new SqlParameter("@created_by", created_by1);
                var is_active = new SqlParameter("@is_active", Assetdata.is_active == true ? true : false);



                var val = _scifferContext.Database.SqlQuery<string>("exec save_asset_master_data @asset_master_data_id ,@asset_master_data_code ,@machine_id ,@asset_master_data_desc ,@asset_class_id ,@asset_group_id ,@status_id,@plant_id ,@purchase_order ,@manufacturer ,@manufacturer_part_no ,@manufacturing_country_id ,@priority_id ,@business_unit_id ,@asset_tag_no ,@purchasing_vendor_id ,@model_no ,@manufacturing_serial_number ,@manufacturing_date ,@cost_center_id ,@capitalization_date ,@created_by ,@is_active,@t1 ", asset_master_data_id, asset_master_data_code, machine_id, asset_master_data_desc, asset_class_id, asset_group_id, status_id, plant_id, purchase_order, manufacturer, manufacturer_part_no, manufacturing_country_id, priority_id, business_unit_id, asset_tag_no, purchasing_vendor_id, model_no, manufacturing_serial_number, manufacturing_date, cost_center_id, capitalization_date, created_by, is_active, t1).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    var sp = val.Split('~')[1];
                    return val;
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.InnerException.Message;
            }
        }


        public List<ref_asset_master_data_vm> GetAll()
        {
            var query = (from order in _scifferContext.ref_asset_master_data.Where(x => x.is_active == true).OrderByDescending(x => x.asset_master_data_id)
                         join machine in _scifferContext.ref_machine on order.machine_id equals machine.machine_id into mach1
                         from mach2 in mach1.DefaultIfEmpty()
                         join assetclass in _scifferContext.ref_asset_class on order.asset_class_id equals assetclass.asset_class_id into assetclass1
                         from assetclass2 in assetclass1.DefaultIfEmpty()
                         join assetgroup in _scifferContext.ref_asset_group on order.asset_group_id equals assetgroup.asset_group_id into assetgroup1
                         from assetgroup2 in assetgroup1.DefaultIfEmpty()
                         join status in _scifferContext.ref_status on order.status_id equals status.status_id into status1
                         from status2 in status1.DefaultIfEmpty()
                         select new ref_asset_master_data_vm()
                         {
                             asset_master_data_id = order.asset_master_data_id,
                             asset_master_data_code = order.asset_master_data_code,
                             machine_name = mach2 == null ? "" : mach2.machine_name,
                             asset_master_data_desc = order.asset_master_data_desc,
                             asset_class_name = assetclass2 == null ? "" : assetclass2.asset_class_code + "/" + assetclass2.asset_class_des,
                             asset_group_name = assetgroup2 == null ? "" : assetgroup2.asset_group_code + "/" + assetgroup2.asset_group_des,
                             status_name = status2 == null ? "" : status2.status_name

                         }).OrderByDescending(x => x.asset_master_data_id).ToList();
            return query;
        }

        public ref_asset_master_data_vm Get(int id)
        {
            ref_asset_master_data po = _scifferContext.ref_asset_master_data.FirstOrDefault(c => c.asset_master_data_id == id && c.is_active == true);
            Mapper.CreateMap<ref_asset_master_data, ref_asset_master_data_vm>();
            ref_asset_master_data_vm mmv = Mapper.Map<ref_asset_master_data, ref_asset_master_data_vm>(po);

            mmv.dep_area_id1 = mmv.dep_area_id2 = _scifferContext.ref_dep_area.OrderByDescending(x => x.dep_area_id).FirstOrDefault(x => x.is_blocked == false).dep_area_id;

            mmv.ref_asset_master_data_dep_parameter_vm = (from detail in _scifferContext.ref_asset_class_depreciation
                                                          join depm in _scifferContext.ref_asset_master_data_dep_parameter on detail.asset_class_dep_id equals depm.asset_class_dep_id into dep12
                                                          from dep22 in dep12.DefaultIfEmpty()
                                                          join ar in _scifferContext.ref_dep_area on detail.dep_area_id equals ar.dep_area_id into ar1
                                                          from ar2 in ar1.DefaultIfEmpty()
                                                          join ty in _scifferContext.ref_dep_type on detail.dep_type_id equals ty.dep_type_id into ty1
                                                          from ty2 in ty1.DefaultIfEmpty()

                                                          select new ref_asset_master_data_dep_parameter_vm
                                                          {
                                                              asset_master_data_id = dep22.asset_master_data_id,
                                                              asset_master_dep_parameter_id = dep22.asset_master_dep_parameter_id,
                                                              dep_area_id = detail.dep_area_id,
                                                              dep_area_code = ar2.dep_area_code + "/" + ar2.dep_area_description,
                                                              dep_type_code = ty2.dep_type_code + "/" + ty2.dep_type_description,
                                                              dep_type_frquency = detail.dep_type_frquency_id == 1 ? "MONTHLY" : detail.dep_type_frquency_id == 2 ? "ANNUALLY" : "",
                                                              useful_life_months = detail.useful_life_months,
                                                              asset_class_dep_id = detail.asset_class_dep_id,
                                                              dep_start_date = dep22.dep_start_date.ToString(),
                                                              dep_end_date = dep22.dep_end_date.ToString(),
                                                          }
                                                         ).Where(a => a.asset_master_data_id == id).ToList();

            mmv.ref_asset_transaction_vm = (from detail in _scifferContext.ref_asset_transaction.Where(x => x.asset_id == id && x.dep_area_id == mmv.dep_area_id1)
                                            select new ref_asset_transaction_vm
                                            {
                                                transaction_code = detail.transaction_code,
                                                posting_date = detail.posting_date,
                                                value = detail.value,
                                                cum_value = detail.cum_value
                                            }).ToList();

            var asset_initial_data = _scifferContext.ref_asset_initial_data.FirstOrDefault(x => x.asset_class_id == id);

            if (asset_initial_data != null)
            {
                mmv.op_historical_cost = asset_initial_data.historical_cost;
                mmv.op_accumulated_dep = asset_initial_data.acc_depriciation;
                mmv.op_net_value = asset_initial_data.net_value;
            }

            var asset_transaction = _scifferContext.ref_asset_transaction.FirstOrDefault(x => x.asset_id == id);

            if (asset_transaction != null)
            {
                mmv.historical_cost = Convert.ToDecimal(asset_transaction.value);
                mmv.accumulated_dep = 0;
                mmv.net_value = Convert.ToDecimal(asset_transaction.cum_value);
            }

            return mmv;
        }


        public List<ref_asset_master_data_dep_parameter_vm> GetDep()
        {

            var mv = (from detail in _scifferContext.ref_asset_class_depreciation

                      join ar in _scifferContext.ref_dep_area on detail.dep_area_id equals ar.dep_area_id into ar1
                      from ar2 in ar1.DefaultIfEmpty()
                      join ty in _scifferContext.ref_dep_type on detail.dep_type_id equals ty.dep_type_id into ty1
                      from ty2 in ty1.DefaultIfEmpty()
                      select new ref_asset_master_data_dep_parameter_vm()
                      {
                          asset_class_id = detail.asset_class_id,
                          dep_area_id = detail.dep_area_id,
                          dep_area_code = ar2.dep_area_code + "/" + ar2.dep_area_description,
                          dep_type_code = ty2.dep_type_code + "/" + ty2.dep_type_description,
                          dep_type_frquency = detail.dep_type_frquency_id == 1 ? "MONTHLY" : detail.dep_type_frquency_id == 2 ? "ANNUALLY" : "",
                          useful_life_months = detail.useful_life_months,
                          asset_class_dep_id = detail.asset_class_dep_id,
                          dep_start_date = "",
                          dep_end_date = "",
                      }).ToList();


            return mv;

        }

        public List<ref_asset_master_data_dep_parameter_vm> GetDepArea()
        {

            var mv = _scifferContext.ref_dep_area.Where(x => x.is_blocked == false).Select(x => new ref_asset_master_data_dep_parameter_vm
            {
                dep_area_id = x.dep_area_id,
                dep_area_code = x.dep_area_description
            }).ToList();

            return mv;

        }

        public List<ref_asset_transaction_vm> GetDepDetails(int dep_area_id, int asset_master_data_id, string name)
        {

            if (name == "Depreciation")
            {
                var mv = (from detail in _scifferContext.ref_asset_transaction.Where(a => a.asset_id == asset_master_data_id && a.dep_area_id == dep_area_id && a.transaction_code == "DEPRUN")
                          join ar in _scifferContext.ref_dep_posting_period on detail.dep_area_posting_period_id equals ar.dep_area_posting_period_id into ar1
                          from ar2 in ar1.DefaultIfEmpty()
                          join ty in _scifferContext.REF_FINANCIAL_YEAR on ar2.financial_year_id equals ty.FINANCIAL_YEAR_ID into ty1
                          from ty2 in ty1.DefaultIfEmpty()
                          select new ref_asset_transaction_vm()
                          {
                              asset_ledger_id = detail.asset_ledger_id,
                              financial_year_id = ar2.financial_year_id,
                              financial_year_name = ty2.FINANCIAL_YEAR_NAME,
                              posting_period_code = ar2.posting_periods_code,
                              dep_area_posting_period_id = detail.dep_area_posting_period_id,
                              planned_value = detail.value,
                              posted_value = 0,
                              cum_value = detail.cum_value,
                              transaction_code = detail.transaction_code
                          }).ToList();
                return mv;
            }

            else if (name == "AssetLedger")
            {
                var mv = (from detail in _scifferContext.ref_asset_transaction.Where(a => a.asset_id == asset_master_data_id && a.dep_area_id == dep_area_id)
                          join ar in _scifferContext.ref_dep_posting_period on detail.dep_area_posting_period_id equals ar.dep_area_posting_period_id into ar1
                          from ar2 in ar1.DefaultIfEmpty()
                          join ty in _scifferContext.REF_FINANCIAL_YEAR on ar2.financial_year_id equals ty.FINANCIAL_YEAR_ID into ty1
                          from ty2 in ty1.DefaultIfEmpty()
                          select new ref_asset_transaction_vm()
                          {
                              asset_ledger_id = detail.asset_ledger_id,
                              financial_year_id = ar2.financial_year_id,
                              financial_year_name = ty2.FINANCIAL_YEAR_NAME,
                              posting_period_code = ar2.posting_periods_code,
                              dep_area_posting_period_id = detail.dep_area_posting_period_id,
                              planned_value = detail.value,
                              posted_value = 0,
                              cum_value = detail.cum_value,
                              transaction_code = detail.transaction_code
                          }).ToList();
                return mv;
            }

            else
            {
                var mv = (from detail in _scifferContext.ref_asset_current_data.Where(a => a.is_active == true && a.asset_id == asset_master_data_id && a.dep_area_id == dep_area_id)

                          select new ref_asset_transaction_vm()
                          {
                              asset_current_data_id = detail.asset_current_data_id,
                              historical_cost = detail.historical_cost,
                              acc_depreciation = detail.acc_depreciation,
                              net_value = detail.net_value

                          }).ToList();
                return mv;
            }

        }
        public ref_assests_depreciation_ledger_vm GetDepreciationAndLedgerDetails(int dep_area_id, int asset_master_data_id, string name)
        {
            ref_assests_depreciation_ledger_vm vm = new ref_assests_depreciation_ledger_vm();

            if (name == "Depreciation")
            {
                vm.ref_asset_master_data_dep_parameter_vm = (from detail in _scifferContext.ref_asset_class_depreciation
                                                             join depm in _scifferContext.ref_asset_master_data_dep_parameter on detail.asset_class_dep_id equals depm.asset_class_dep_id into dep12
                                                             from dep22 in dep12.DefaultIfEmpty()
                                                             join ar in _scifferContext.ref_dep_area on detail.dep_area_id equals ar.dep_area_id into ar1
                                                             from ar2 in ar1.DefaultIfEmpty()
                                                             join ty in _scifferContext.ref_dep_type on detail.dep_type_id equals ty.dep_type_id into ty1
                                                             from ty2 in ty1.DefaultIfEmpty()

                                                             select new ref_asset_master_data_dep_parameter_vm
                                                             {
                                                                 asset_master_data_id = dep22.asset_master_data_id,
                                                                 asset_master_dep_parameter_id = dep22.asset_master_dep_parameter_id,
                                                                 dep_area_id = detail.dep_area_id,
                                                                 dep_area_code = ar2.dep_area_code + "/" + ar2.dep_area_description,
                                                                 dep_type_code = ty2.dep_type_code + "/" + ty2.dep_type_description,
                                                                 dep_type_frquency = detail.dep_type_frquency_id == 1 ? "MONTHLY" : detail.dep_type_frquency_id == 2 ? "ANNUALLY" : "",
                                                                 useful_life_months = detail.useful_life_months,
                                                                 asset_class_dep_id = detail.asset_class_dep_id,
                                                                 dep_start_date = dep22.dep_start_date.ToString(),
                                                                 dep_end_date = dep22.dep_end_date.ToString(),
                                                             }
                                                         ).Where(a => a.asset_master_data_id == asset_master_data_id && dep_area_id == dep_area_id).ToList();
                return vm;
            }

            else if (name == "AssetLedger")
            {
                vm.ref_asset_transaction_vm = (from detail in _scifferContext.ref_asset_transaction.Where(x => x.asset_id == asset_master_data_id && x.dep_area_id == dep_area_id)
                                               select new ref_asset_transaction_vm
                                               {
                                                   transaction_code = detail.transaction_code,
                                                   posting_date = detail.posting_date,
                                                   value = detail.value,
                                                   cum_value = detail.cum_value
                                               }).ToList();
                return vm;
            }

            return vm;
        }

        public string AddExcel(List<ref_asset_master_data_excel_vm> Assetdata, bool is_based_on_machine_code)
        {

            try
            {

                DataTable dt1 = new DataTable();

                dt1.Columns.Add("row_no", typeof(string));
                dt1.Columns.Add("is_based_on_machine_code", typeof(string));
                dt1.Columns.Add("asset_code", typeof(string));
                dt1.Columns.Add("machine_code", typeof(string));  //Available at without also
                dt1.Columns.Add("asset_class", typeof(string));   //Available at without also
                dt1.Columns.Add("asset_group", typeof(string));   //Available at without also
                dt1.Columns.Add("status", typeof(string));        //Available at without also
                dt1.Columns.Add("cost_center", typeof(string));   // Available at without also
                dt1.Columns.Add("capitalisation_date", typeof(DateTime)); //Available at without also

                //Without Columns Start

                dt1.Columns.Add("description", typeof(string)); //Available at without also
                dt1.Columns.Add("plant", typeof(string)); //Available at without also
                dt1.Columns.Add("purchase_order", typeof(string)); //Available at without also
                dt1.Columns.Add("manufacturer", typeof(string)); //Available at without also
                dt1.Columns.Add("manufacturer_part_number", typeof(string)); //Available at without also
                dt1.Columns.Add("manufacturing_country", typeof(string)); //Available at without also
                dt1.Columns.Add("priority", typeof(string)); //Available at without also
                dt1.Columns.Add("business_unit", typeof(string)); //Available at without also
                dt1.Columns.Add("asset_tag_no", typeof(string)); //Available at without also
                dt1.Columns.Add("purchasing_vendor_code", typeof(string)); //Available at without also
                dt1.Columns.Add("model_number", typeof(string)); //Available at without also
                dt1.Columns.Add("manufacturer_serial_number", typeof(string)); //Available at without also
                dt1.Columns.Add("manufacturing_date", typeof(DateTime)); //Available at without also


                if (Assetdata != null)
                {
                    int row_no = 0;
                    foreach (var d in Assetdata)
                    {
                        dt1.Rows.Add(
                                      d.row_no = row_no + 1
                                    , d.is_based_on_machine_code
                                    , d.asset_code
                                    , d.machine_code
                                    , d.asset_class
                                    , d.asset_group
                                    , d.status
                                    , d.cost_center
                                    , d.capitalisation_date

                                   //Without
                                   , d.description
                                   , d.plant
                                   , d.purchase_order
                                   , d.manufacturer
                                   , d.manufacturer_part_number
                                   , d.manufacturing_country
                                   , d.priority
                                   , d.business_unit
                                   , d.asset_tag_no
                                   , d.purchasing_vendor_code
                                   , d.model_number
                                   , d.manufacturer_serial_number
                                   , d.manufacturing_date
                                  );
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_asset_master_excel";
                t1.Value = dt1;

                var created_by1 = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                var created_by = new SqlParameter("@created_by", created_by1);

                var is_based_on_machine_code_val = new SqlParameter("@is_based_on_machine_code", is_based_on_machine_code);


                var val = _scifferContext.Database.SqlQuery<string>("exec save_asset_master_excel @created_by,@is_based_on_machine_code,@t1", created_by, is_based_on_machine_code_val, t1).FirstOrDefault();

                return val;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //#region Header
            //SqlParameter[] param = new SqlParameter[23];
            //param[0] = new SqlParameter("@fin_credit_debit_note_id", GetFormatedInteger(debit_note_header.fin_credit_debit_note_id));
            //param[1] = new SqlParameter("@is_debit_note", GetFormatedbool(debit_note_header.is_debit_note));
            //param[2] = new SqlParameter("@plant_id", GetFormatedInteger(debit_note_header.plant_id));
            //param[3] = new SqlParameter("@document_numbering_detail_id", GetFormatedInteger(debit_note_header.document_numbering_detail_id));
            //param[4] = new SqlParameter("@document_no", GetFormatedString(debit_note_header.document_no));
            //param[5] = new SqlParameter("@posting_date", GetFormatedDate(debit_note_header.posting_date_string));
            //param[6] = new SqlParameter("@entity_type_id", GetFormatedInteger(debit_note_header.entity_type_id));
            //param[7] = new SqlParameter("@entity_id", GetFormatedInteger(debit_note_header.entity_id));
            //param[8] = new SqlParameter("@source_id", GetFormatedInteger(debit_note_header.source_id));
            //param[9] = new SqlParameter("@customer_vendor_location_id", GetFormatedInteger(debit_note_header.customer_vendor_location_id));
            //param[10] = new SqlParameter("@currency_id", GetFormatedInteger(debit_note_header.currency_id));
            //param[11] = new SqlParameter("@base_on", GetFormatedbool(debit_note_header.base_on));
            //param[12] = new SqlParameter("@business_unit_id", GetFormatedInteger(debit_note_header.business_unit_id));
            //#endregion
            //#region Information
            //param[13] = new SqlParameter("@internal_remarks", GetFormatedString(debit_note_header.internal_remarks));
            //param[14] = new SqlParameter("@remarks_on_document", GetFormatedString(debit_note_header.remarks_on_document));
            //param[15] = new SqlParameter("@attachement", GetFormatedString(debit_note_header.attachement));
            //#endregion
            //#region TimeStamp And Other
            //int created_by = int.Parse(HttpContext.Current.Session["user_id"].ToString());
            //param[16] = new SqlParameter("@created_by", created_by);
            //int company_id = int.Parse(HttpContext.Current.Session["comp"].ToString());
            //param[17] = new SqlParameter("@company_id", company_id);
            //#endregion
            //#region tax_values
            //param[18] = new SqlParameter("@net_amount", GetFormateddecimal(debit_note_header.net_amount));
            //param[19] = new SqlParameter("@round_off", GetFormateddecimal(debit_note_header.round_off));
            //param[20] = new SqlParameter("@gross_amount", GetFormateddecimal(debit_note_header.gross_amount));
            //#endregion
            //#region detail Tab
            //DataTable dt = new DataTable();
            //if (details != null)
            //{

            //    string json = Newtonsoft.Json.JsonConvert.SerializeObject(details);
            //    DataTable pDt = JsonConvert.DeserializeObject<DataTable>(json);
            //    string[] selectedColumns = new[] { "fin_credit_debit_note_detail_id", "gl_code", "item_codes", "user_desc", "hsn_sac_type","hsn_codes",
            //            "qty", "rate", "amount", "gst_code" };
            //    dt = new DataView(pDt).ToTable(false, selectedColumns);
            //}

            //param[21] = new SqlParameter("@t1", SqlDbType.Structured);
            //param[21].TypeName = "dbo.temp_fin_credit_debit_note_detail";
            //param[21].Value = dt;
            //#endregion
            //#region Select Sorce Document

            //DataTable dt1 = new DataTable();

            //    if (Assetdata != null)
            //    {
            //        string json = Newtonsoft.Json.JsonConvert.SerializeObject(sourceDocument);
            //        DataTable pDt1 = JsonConvert.DeserializeObject<DataTable>(json);
            //        string[] selectedColumns = new[] { "fin_credit_debit_note_id", "source_document_id", "document_type_code" };
            //        dt1 = new DataView(pDt1).ToTable(false, selectedColumns);
            //    }
            //    else
            //    {
            //        //Dt1 create new instance hence when source is direct
            //        dt1.Columns.Add("fin_credit_debit_note_id", typeof(int));
            //        dt1.Columns.Add("source_document_id", typeof(int));
            //        dt1.Columns.Add("document_type_code", typeof(string));
            //    }

            //param[22] = new SqlParameter("@t2", SqlDbType.Structured);
            //param[22].TypeName = "dbo.temp_fin_credit_debit_note_source_document";
            //param[22].Value = dt1;

            //#endregion
            ////var con = ConfigurationManager.ConnectionStrings["LogibricksContext"].ConnectionString;
            //var val = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "save_credit_debit_note", param).ToString();
        }
    }
}
