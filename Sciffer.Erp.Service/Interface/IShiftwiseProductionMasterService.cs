using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IShiftwiseProductionMasterService
    {
        List<mfg_shiftwise_production_master_vm> GetAll();
        mfg_shiftwise_production_master_vm Get(int id);
        string Add(mfg_shiftwise_production_master_vm shiftwise_production);

        List<shiftwiseProductionDetails> GetAllShiftwiseProduction(string entity, DateTime? posting_date, int? plant_id, int? shift_id, int? process_id, int? machine_id, string item_id);
        int GetQty(string entity, int? plant_id, int shift_id, int? process_id, int? machine_id, string item_id, int? operator_id);

        int CheckDuplicate(DateTime postingDate, int plantId, int shiftId);
        List<shiftwiseProductionDetails> GetDetails(int id);

        string Update(mfg_shiftwise_production_master_vm shiftwise_production, List<shiftwiseProductionDetails> newItem);

    }
}
