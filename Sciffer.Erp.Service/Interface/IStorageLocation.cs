using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IStorageLocation : IDisposable
    {
        List<storage_vm> getstoragelist();
        List<REF_STORAGE_LOCATION> GetAll();
        REF_STORAGE_LOCATION Get(int id);
        storage_vm Add(storage_vm storage);
        storage_vm Update(storage_vm storage);
        bool Delete(int id);
        int GetID(string st);
        List<storage_vm> getstoragelistUsingDocumentId(int docId);
    }
}
