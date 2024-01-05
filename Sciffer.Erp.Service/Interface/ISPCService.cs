using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sciffer.Erp.Domain.ViewModel;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface  ISPCService
    {
        List<ref_machine_master_VM> GetMachineListByProcess(int process_id);
        List<SPC_Chart_VM> GetOperatorNameList(int item_id, int machine_id);
        List<SPC_Chart_VM> GetQCOperatorNameList(int item_id, int machine_id);
        List<SPC_Chart_VM> GetSPCChartReport(DateTime fromDate, int item_id, int machine_id, TimeSpan start_time, TimeSpan end_time, int is_operator_parameter, int parameter_id);
        List<spc_chart_calculation> GetSPCChartReportCalculation(DateTime fromDate, int item_id, int machine_id, TimeSpan start_time, TimeSpan end_time, int is_operator_parameter, int parameter_id);
    }
}
