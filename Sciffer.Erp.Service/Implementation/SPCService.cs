using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Implementation
{
    public class SPCService : ISPCService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public SPCService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        //Get machine list by operator mapped with operator and machine
        public List<ref_machine_master_VM> GetMachineListByProcess(int process_id)
        {
            var list = new List<ref_machine_master_VM>();
            try
            {
                list = (from mmpm in _scifferContext.map_mfg_process_machine
                        join rm in _scifferContext.ref_machine on mmpm.machine_id equals rm.machine_id
                        where mmpm.process_id == process_id
                        select new ref_machine_master_VM
                        {
                            machine_id = rm.machine_id,
                            machine_name = rm.machine_name,
                        }).ToList();

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
            }
            return list;
        }


        //Get Operator list
        public List<SPC_Chart_VM> GetOperatorNameList(int item_id, int machine_id)
        {
            var list = new List<SPC_Chart_VM>();
            try
            {
                list = (from op in _scifferContext.mfg_op_qc_parameter_list
                        join op_oc in _scifferContext.mfg_op_qc_parameter on op.mfg_op_qc_parameter_id equals op_oc.mfg_op_qc_parameter_id
                        join ri in _scifferContext.REF_ITEM on op_oc.item_id equals ri.ITEM_ID
                        join rm in _scifferContext.ref_machine on op_oc.machine_id equals rm.machine_id
                        where op_oc.item_id == item_id && op_oc.machine_id == machine_id && op.is_numeric == true
                        && op.is_active == true && op_oc.is_active == true
                        select new SPC_Chart_VM
                        {
                            mfg_op_oc_id = op.mfg_op_qc_parameter_list_id,
                            mfg_op_name = op.parameter_name,
                        }).ToList();

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
            }
            return list;
        }


        //Get QC Operator list
        public List<SPC_Chart_VM> GetQCOperatorNameList(int item_id, int machine_id)
        {
            var list = new List<SPC_Chart_VM>();
            try
            {
                list = (from qc in _scifferContext.mfg_qc_qc_parameter_list
                        join qc_qc in _scifferContext.mfg_op_qc_parameter on qc.mfg_qc_qc_parameter_id equals qc_qc.mfg_op_qc_parameter_id
                        join ri in _scifferContext.REF_ITEM on qc_qc.item_id equals ri.ITEM_ID
                        join rm in _scifferContext.ref_machine on qc_qc.machine_id equals rm.machine_id
                        where qc_qc.item_id == item_id && qc_qc.machine_id == machine_id
                        && qc.is_active == true && qc_qc.is_active == true
                        select new SPC_Chart_VM
                        {
                            mfg_op_qc_id = qc.mfg_qc_qc_parameter_list_id,
                            mfg_qc_name = qc.parameter_name,
                        }).ToList();

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
            }
            return list;
        }

        public List<SPC_Chart_VM> GetSPCChartReport(DateTime fromDate, int item_id, int machine_id, TimeSpan start_time, TimeSpan end_time, int is_operator_parameter, int parameter_id)
        {
            var entity = "";
            if (is_operator_parameter == 1)
            {
                entity = "OperatorParameter";
            }
            else
            {
                entity = "QualityParameter";
            }
            var Entity = new SqlParameter("@entity", entity);
            var From_Date = new SqlParameter("@fromDate", fromDate);
            var Item_id = new SqlParameter("@item_id", item_id);
            var Machine_id = new SqlParameter("@machine_id", machine_id);
            var Start_time = new SqlParameter("@start_time", start_time);
            var End_time = new SqlParameter("@end_time", end_time);
            var Is_operator_parameter = new SqlParameter("@is_operator_parameter", is_operator_parameter);
            var Parameter_id = new SqlParameter("@parameter_id", parameter_id);

            var val = _scifferContext.Database.SqlQuery<SPC_Chart_VM>(
             "exec GetSPCChatReport @entity,@fromDate,@item_id,@machine_id,@start_time,@end_time,@parameter_id",
             Entity, From_Date, Item_id, Machine_id, Start_time, End_time, Parameter_id).ToList();

            return val;
        }

        public List<spc_chart_calculation> GetSPCChartReportCalculation(DateTime fromDate, int item_id, int machine_id, TimeSpan start_time, TimeSpan end_time, int is_operator_parameter, int parameter_id)
        {
            var entity = "";
            if (is_operator_parameter == 1)
            {
                entity = "OperatorParameter";
            }
            else
            {
                entity = "QualityParameter";
            }
            var Entity = new SqlParameter("@entity", entity);
            var From_Date = new SqlParameter("@fromDate", fromDate);
            var Item_id = new SqlParameter("@item_id", item_id);
            var Machine_id = new SqlParameter("@machine_id", machine_id);
            var Start_time = new SqlParameter("@start_time", start_time);
            var End_time = new SqlParameter("@end_time", end_time);
            var Is_operator_parameter = new SqlParameter("@is_operator_parameter", is_operator_parameter);
            var Parameter_id = new SqlParameter("@parameter_id", parameter_id);

            var val = _scifferContext.Database.SqlQuery<SPC_Chart_VM>(
             "exec GetSPCChatReport @entity,@fromDate,@item_id,@machine_id,@start_time,@end_time,@parameter_id",
             Entity, From_Date, Item_id, Machine_id, Start_time, End_time, Parameter_id).ToList();

            List<spc_chart_calculation> scc = new List<spc_chart_calculation>();

            var flag = 1;
            foreach (var xx in val)
            {
                double result;
                //to check xx.parameter_value is numeric
                if (double.TryParse(xx.parameter_value, out result))
                {
                    xx.parameter_value_converted = result;
                }
                else
                {
                    xx.parameter_value_converted = 0;
                }
            }
            if (flag == 1)
            {
                spc_chart_calculation spc = new spc_chart_calculation();
                spc.text = "Max";
                spc.value = val.Max(x => Convert.ToDouble(x.parameter_value_converted)).ToString();
                scc.Add(spc);

                spc_chart_calculation spc1 = new spc_chart_calculation();
                spc1.text = "Min";
                spc1.value = val.Min(x => Convert.ToDouble(x.parameter_value_converted)).ToString();
                scc.Add(spc1);


                spc_chart_calculation spc2 = new spc_chart_calculation();
                spc2.text = "Avrage";
                spc2.value = val.Average(x => Convert.ToDouble(x.parameter_value_converted)).ToString();
                scc.Add(spc2);

                spc_chart_calculation spc3 = new spc_chart_calculation();
                spc3.text = "Start Range";
                spc3.value = _scifferContext.mfg_op_qc_parameter_list.Where(x => x.mfg_op_qc_parameter_list_id == parameter_id).FirstOrDefault().std_range_start;
                scc.Add(spc3);

                spc_chart_calculation spc4 = new spc_chart_calculation();
                spc4.text = "End Range";
                spc4.value = _scifferContext.mfg_op_qc_parameter_list.Where(x => x.mfg_op_qc_parameter_list_id == parameter_id).FirstOrDefault().std_range_end;
                scc.Add(spc4);

                spc_chart_calculation spc5 = new spc_chart_calculation();
                spc5.text = "Avrage";
                spc5.value = _scifferContext.mfg_op_qc_parameter_list.Where(x => x.mfg_op_qc_parameter_list_id == parameter_id).FirstOrDefault().std_range_end;
                scc.Add(spc5);

                spc_chart_calculation spc6 = new spc_chart_calculation();
                spc6.text = "Range";
                double max = val.Max(x => Convert.ToDouble(x.parameter_value_converted));
                double min = val.Min(x => Convert.ToDouble(x.parameter_value_converted));
                double range = max - min;
                spc6.value = range.ToString();
                scc.Add(spc6);

                spc_chart_calculation spc7 = new spc_chart_calculation();
                spc7.text = "Tolerance";
                string std_end_range = _scifferContext.mfg_op_qc_parameter_list.Where(x => x.mfg_op_qc_parameter_list_id == parameter_id).FirstOrDefault().std_range_end; ;
                string std_start_range = _scifferContext.mfg_op_qc_parameter_list.Where(x => x.mfg_op_qc_parameter_list_id == parameter_id).FirstOrDefault().std_range_start; ;
                spc7.value = (Convert.ToDouble(std_end_range) - Convert.ToDouble(std_start_range)).ToString();
                scc.Add(spc7);

                spc_chart_calculation spc8 = new spc_chart_calculation();
                spc8.text = "Sigma";

                double sum = val.Sum(x => Convert.ToDouble(x.parameter_value_converted));
                double mean = sum / val.Count;

                double[] subtracted_mean_sqr = new double[val.Count];
                int i = 0;
                foreach (var item in val)
                {
                    subtracted_mean_sqr[i] = ((mean - Convert.ToDouble(item.parameter_value_converted)) * (mean - Convert.ToDouble(item.parameter_value_converted)));
                    ++i;
                }
                double sum_of_subtracted_mean_sqr = 0;
                for (int j = 0; j < val.Count; ++j)
                    sum_of_subtracted_mean_sqr += subtracted_mean_sqr[j];
                double std_dev = Math.Sqrt(sum_of_subtracted_mean_sqr / val.Count);
                spc8.value = std_dev.ToString();

                scc.Add(spc8);


                spc_chart_calculation spc9 = new spc_chart_calculation();
                spc9.text = "Cp";
                spc9.value = (Convert.ToDouble(spc7.value) / Convert.ToDouble(spc8.value) * 6).ToString();
                scc.Add(spc9);


                spc_chart_calculation spc10 = new spc_chart_calculation();
                spc10.text = "Cp";
                spc10.value = val.Min(x=>(Convert.ToDouble(spc4.value) - Convert.ToDouble(spc3.value) / 6 * Convert.ToDouble(spc8.value)) - (Convert.ToDouble(spc4.value) - Convert.ToDouble(spc3.value) / 3 * Convert.ToDouble(spc8.value))).ToString();
                scc.Add(spc10);


                //double sum_of_subtracted_mean_sqr = 0;
                //for (int j = 0; j < val.Count; ++j)
                //    sum_of_subtracted_mean_sqr += subtracted_mean_sqr[j];
                //double std_dev = Math.Sqrt(sum_of_subtracted_mean_sqr / val.Count);


                //scc.max = val.Max(x => Convert.ToDouble(x.parameter_value));
                //scc.min = val.Min(x => Convert.ToDouble(x.parameter_value));
                //scc.avg = val.Average(x => Convert.ToDouble(x.parameter_value));
                //scc.std_start_range = _scifferContext.mfg_op_qc_parameter_list.Where(x => x.mfg_op_qc_parameter_list_id == parameter_id).FirstOrDefault().std_range_start;
                //scc.std_end_range = _scifferContext.mfg_op_qc_parameter_list.Where(x => x.mfg_op_qc_parameter_list_id == parameter_id).FirstOrDefault().std_range_end;
                //scc.range = scc.max - scc.min;              
                //scc.tolerance = Convert.ToDouble(scc.std_end_range) - Convert.ToDouble(scc.std_start_range);
                //double sum = val.Sum(x => Convert.ToDouble(x.parameter_value));
                //double mean = sum / val.Count;

                //double[] subtracted_mean_sqr = new double[val.Count];
                //int i = 0;
                //foreach(var item in val)
                //{
                //    subtracted_mean_sqr[i] = ((mean - Convert.ToDouble(item.parameter_value)) * (mean - Convert.ToDouble(item.parameter_value)));
                //    ++i;
                //}

                //double sum_of_subtracted_mean_sqr = 0;
                //for (int j = 0; j < val.Count; ++j)
                //    sum_of_subtracted_mean_sqr += subtracted_mean_sqr[j];
                //double std_dev = Math.Sqrt(sum_of_subtracted_mean_sqr / val.Count);
                //scc.sigma = std_dev;
                //scc.cp = scc.tolerance / (6 * scc.sigma);
                //scc.cpk = val.Min(x => (Convert.ToDouble(x.std_range_end) - Convert.ToDouble(x.std_range_start) / 6 * scc.sigma) * (Convert.ToDouble(x.std_range_end) - Convert.ToDouble(x.std_range_start) / 3 * scc.sigma));

            }
          
            return scc;

        }

    }

}
