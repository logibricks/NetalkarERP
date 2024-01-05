using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IGSTTdsCodeService : IDisposable
    {
        List<ref_gst_tds_code_vm> GetAll();
        ref_gst_tds_code_vm Get(int id);
        string Add(ref_gst_tds_code_vm TDSCode);       
    }
}
