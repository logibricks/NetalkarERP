using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IAssetMasterDataService
    {
        string Add(ref_asset_master_data_vm Assetdata, List<ref_asset_master_data_dep_parameter_vm> DepParaArr);
        string AddExcel(List<ref_asset_master_data_excel_vm> Assetdata, bool is_based_on_machine_code);
        List<ref_asset_master_data_vm> GetAll();
        ref_asset_master_data_vm Get(int id);
        List<ref_asset_master_data_dep_parameter_vm> GetDep();
        List<ref_asset_transaction_vm> GetDepDetails(int dep_area_id, int asset_master_data_id, string name);

        List<ref_asset_master_data_dep_parameter_vm> GetDepArea();
        ref_assests_depreciation_ledger_vm GetDepreciationAndLedgerDetails(int dep_area_id, int asset_master_data_id, string name);
    }
}
