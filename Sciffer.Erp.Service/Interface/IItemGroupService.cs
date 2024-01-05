using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IItemGroupService: IDisposable
    {
        List<REF_ITEM_GROUP> GetAll();
        REF_ITEM_GROUP Get(int id);
        REF_ITEM_GROUP Create();
        REF_ITEM_GROUP Add(REF_ITEM_GROUP group);
        REF_ITEM_GROUP Update(REF_ITEM_GROUP group);
        bool Delete(int id);
    }
}
