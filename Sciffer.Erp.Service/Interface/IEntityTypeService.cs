using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IEntityTypeService : IDisposable
    {
        List<REF_ENTITY_TYPE> GetAll();
        REF_ENTITY_TYPE Get(int id);
        bool Add(REF_ENTITY_TYPE entitytype);
        bool Update(REF_ENTITY_TYPE entitytype);
        bool Delete(int id);
    }
}
