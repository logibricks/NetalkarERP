using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IDocumentNumbringService:IDisposable
    {
        List<document_numbring> GetDocumentNumbering();
        List<ref_document_numbring> GetAll();
        ref_document_numbring Get(int id);
        document_numbring Add(document_numbring doc);
        document_numbring Update(document_numbring doc);
        bool Delete(int id);
        bool checksetdefault(int id, int financial_year_id);
    }
}
