using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_assests_depreciation_ledger_vm
    {
        public List<ref_asset_master_data_dep_parameter_vm> ref_asset_master_data_dep_parameter_vm { get; set; }
        public List<ref_asset_transaction_vm> ref_asset_transaction_vm { get; set; }
    }
}
