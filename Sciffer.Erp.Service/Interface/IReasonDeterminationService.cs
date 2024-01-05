using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;


namespace Sciffer.Erp.Service.Interface
{
    public interface IReasonDeterminationService:IDisposable
    {
        List<reasonvm> GetReasonListByCode(string code);
        List<reasonvm> GetReasonList(int id);
        List<REF_REASON_DETERMINATION> GetAll();
        REF_REASON_DETERMINATION Get(int id);
        reasonvm Add(reasonvm reason);
        reasonvm Update(reasonvm reason);
        bool Delete(int id);
        List<REF_REASON_DETERMINATION> GetReasonByCode(string code);
    }
}
