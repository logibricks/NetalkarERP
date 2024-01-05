using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public  interface IStateService: IDisposable
    {
        List<state_vm> GetStateList();
        List<REF_STATE> GetAll();
        REF_STATE Get(int id);   
        state_vm Add(state_vm state);
        state_vm Update(state_vm state);
        bool Delete(int id);
    }
}
