using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IProdOrderIssueService
    {
        string Add(ProdIssueVM vm);
        bool Update(ProdIssueVM vm);
        bool Delete(int id);
        List<ProdIssueVM> GetAll();
        ProdIssueVM Get(int? id);
        List<ProOrderDetailVM> GetNonTagProductionOrderIssue(int id);
        List<ProOrderDetailVM> GetTagProductionOrderIssue(int id);
        ProdIssueVM GetDetails(int? id);
        bom_details getClumpsumBatchQuantity(string sloc_id, string plant_id, string item_id, string bucket_id, string entity_id, int? reason_id);
        bom_details GetItemForProdGoodsIssue(string sloc_id, string plant_id, string item_id, string bucket_id , int? reason_id);
    }
}
