using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IWorkflowService:IDisposable
    {
        List<workflowVM> GetAll();
        List<workflowVM> getall();
        workflowVM Get(int id);
        bool Add(workflowVM workflow);
        bool Update(workflowVM workflow);
        bool Delete(int id);
    }
}
