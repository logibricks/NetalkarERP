using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
   public interface IDocumentTypeService:IDisposable
    {
        List<ref_document_type> GetAll();
        ref_document_type Get(int id);
        bool Add(ref_document_type Document);
        bool Update(ref_document_type Document);
        bool Delete(int id);
    }
}
