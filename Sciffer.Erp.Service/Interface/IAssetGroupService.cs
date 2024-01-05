using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
   public interface IAssetGroupService
    {
        List<ref_asset_group> GetAll();
        ref_asset_group Add(ref_asset_group assetgroup);
        ref_asset_group Update(ref_asset_group assetgroup);
    }
}
