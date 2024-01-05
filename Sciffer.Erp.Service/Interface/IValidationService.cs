using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IValidationService:IDisposable
    {
        List<ref_validation> GetAll();
        ref_validation Get(int id);
        bool Add(ref_validation validation);
    }
}
