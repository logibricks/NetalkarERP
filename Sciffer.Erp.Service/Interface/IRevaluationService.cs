using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IRevaluationService:IDisposable
    {
        List<inv_revaluation_vm> GetAll();       
        inv_revaluation_vm Get(int? id);
        string Add(inv_revaluation_vm revaluation);     
    }
}
