using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IJobWorkRejectionService
    {
        List<jobwork_rejection_VM> GetAll();
        jobwork_rejection_VM Get(int id);
        string Add(jobwork_rejection_VM invoice);
        bool Delete(int id);
        List<jobwork_rejection_VM> jobworkrejection(int id);
        List<jobwork_rejection_detail_VM> jobworkrejectiondetail(int id);
        List<jobwork_rejection_VM> GetJobWorkRejectionDetail(string entity, int id);
        List<jobwork_rejection_item_detail_VM> jobworkrejectionitemdetail(int id);
        string GetItemCategoryByItem(int item_id);
        int GetState(int id);
        int GetdeliveryState(int id);


    }
}
