using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IProductionService
    {
        List<mfg_prod_order_VM> GetAll();
        string Add(mfg_prod_order_VM ref_plan_maintenance_VM);
        bool Update(mfg_prod_order_VM ref_plan_maintenance_VM);
        bool Delete(int id);
        mfg_prod_order_VM Get(int id);
        List<ref_mfg_bom> GetAllBom();
        List<mfg_prod_order_VM> GetBOMGridData(int bom_id, double itemquantity);
        List<mfg_process_seq_alt> Getprocess_seq(int item);
        List<ref_mfg_bom> GetBOMForItem(int out_item_id);
        List<mfg_prod_order_VM> GetActivePOListForIssue();
        List<mfg_prod_order_VM> GetActivePOListForReceipt();
        List<string> GetProcessSequence(int id);
        List<string> GetProcessSequenceByProcessSequenceId(int process_sequence_id);
        List<ref_machine_master_VM> GetMachineList();
        string UpdateProcessSequence(int id, string process_sequence);
        List<mfg_prod_order_VM> GetProductionOrderList(int item_id, string entity_id);
        string UpdateProcessSequenceById(int process_sequence_id, string process_sequence);

        //---------------------------Operation Sequence------------------------//
        List<GetOperationSequenceWithMachine> GetOperationsByOperationSequenceId(int process_seq_alt_id);
        List<ref_machine> GetMappedMachinesByProcessId(int process_id);

        //---------------------------Update Sequence---------------------------//
        List<mfg_prod_order_VM> GetTagNumbers(string machine, int prod_order_id);
        string UpdateTagNumbers(string source, string destination, int prod_order_id);
    }
}
