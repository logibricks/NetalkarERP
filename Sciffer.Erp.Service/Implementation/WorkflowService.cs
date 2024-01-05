using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Service.Implementation
{
    public class WorkflowService : IWorkflowService
    {
        private readonly ScifferContext _scifferContext;

        public WorkflowService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(workflowVM workflow)
        {
            try
            {
                ref_workflow work = new ref_workflow();
                work.document_type_id = workflow.document_type_id;
                work.category_id = workflow.category_id;
                work.has_value = workflow.has_value;
                work.value_from = workflow.value_from;
                work.value_to = workflow.value_to;
                work.no_of_approval = workflow.no_of_approval;
                work.is_active = true;

                List<ref_workflow_detail> detail = new List<ref_workflow_detail>();
                if (workflow.ref_workflow_detail != null)
                {
                    foreach (var items in workflow.ref_workflow_detail)
                    {
                        ref_workflow_detail d = new ref_workflow_detail();
                        d.approval_set_no = items.approval_set_no;
                        d.approval_set_name = items.approval_set_name;
                        detail.Add(d);
                    }
                }
                work.ref_workflow_detail = detail;
                List<ref_workflow_approval> app = new List<ref_workflow_approval>();
                if (workflow.ref_workflow_approval != null)
                {
                    foreach (var items in workflow.ref_workflow_approval)
                    {
                        ref_workflow_approval a = new ref_workflow_approval();
                        a.user_id = items.user_id;
                        app.Add(a);
                    }
                }
                work.ref_workflow_approval = app;
                _scifferContext.ref_workflow.Add(work);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }

        public workflowVM Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<workflowVM> getall()
        {
            throw new NotImplementedException();
        }

        public List<workflowVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(workflowVM workflow)
        {
            throw new NotImplementedException();
        }
    }
}
