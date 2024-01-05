
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IGoodsIssueService : IDisposable
    {
        List<GOODS_ISSUE_VM> GetAll();
        List<GOODS_ISSUE_VM> getall();
        GOODS_ISSUE_VM Get(int id);
        string Add(GOODS_ISSUE_VM goodsIssue);
        bool Update(GOODS_ISSUE_VM goodsIssue);
        bool Delete(int id);
        List<MRNDetailListVM> GetNonTagMRNProductForGoodsIssue(int id);
        List<MRNDetailListVM> GetTagMRNProductForGoodsIssue(int id);
        List<MRNDetailListVM> GetTagItemsForGoodsIssue(int sloc_id, int plant_id, int item_id);
        List<MRNDetailListVM> GetNonTagItemsForGoodsIssue(int sloc_id, int plant_id, int item_id);
        GOODS_ISSUE_VM GetDetails(int id);
        string AddExcel(List<GOODS_ISSUE_VM> header, List<goods_issue_detail_VM> detail, List<goods_issue_batch_VM> batch);
        string GetChilItem(string entity, int item_id);
    }
}
