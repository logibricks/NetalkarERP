using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IModeOfTransportService:IDisposable
    {
        List<ref_mode_of_transport> GetAll();
        ref_mode_of_transport Get(int id);
        ref_mode_of_transport Add(ref_mode_of_transport trasport);
        ref_mode_of_transport Update(ref_mode_of_transport trasport);
        bool Delete(int id);
    }
}
