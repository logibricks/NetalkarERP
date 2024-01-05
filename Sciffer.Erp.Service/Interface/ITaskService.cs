using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
   public interface ITaskService
    {
        string Add(ref_task_vm vm);
        List<ref_task_vm> GetAll();
        ref_task_vm Get(int id);
        ref_task_log Getattachment(int id);
    }
}
