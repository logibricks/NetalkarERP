using Sciffer.Erp.Domain.Model;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IQAService
    {
        string Add(pur_qa_VM vm);       
        List<pur_qa_VM> GetAll();
        pur_qa_VM Get(int? id);
        List<pur_qa_VM> GetSourceDocument();
    }
}
