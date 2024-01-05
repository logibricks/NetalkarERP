using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class AssetGroupService : IAssetGroupService
    {
        private readonly ScifferContext _scifferContext;

        public AssetGroupService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_asset_group Add(ref_asset_group assetgroup)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                assetgroup.is_blocked = false;
                assetgroup.created_by = user;
                assetgroup.created_ts = DateTime.Now;
                _scifferContext.ref_asset_group.Add(assetgroup);
                _scifferContext.SaveChanges();
                assetgroup.asset_group_id = _scifferContext.ref_asset_group.Max(x => x.asset_group_id);
            }
            catch (Exception ex)
            {
                return assetgroup;
            }
            return assetgroup;

        }
        public List<ref_asset_group> GetAll()
        {
            return _scifferContext.ref_asset_group.ToList().ToList();
        }
        public ref_asset_group Update(ref_asset_group assetgroup)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                assetgroup.is_blocked = assetgroup.is_blocked;
                assetgroup.created_by = user;
                assetgroup.created_ts = DateTime.Now;
                assetgroup.modify_by = user;
                assetgroup.modify_ts = DateTime.Now;
                _scifferContext.Entry(assetgroup).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return assetgroup;
            }
            return assetgroup;
        }
    }
}

