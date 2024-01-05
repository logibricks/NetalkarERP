using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IAssetClassService
    {
        string Add(ref_asset_class_vm fin_map, List<ref_asset_class_gl_vm> fin_detail, List<ref_asset_class_depreciation_vm> dip_detail);
        List<ref_asset_class_vm> getall();
        ref_asset_class_vm Get(int id);
    }
}
