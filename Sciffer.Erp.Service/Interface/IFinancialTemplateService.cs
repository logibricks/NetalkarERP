using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
   public interface IFinancialTemplateService : IDisposable
    {
        ref_fin_template_vm GetTreeVeiwList(int id);
        bool Add(ref_fin_template_vm template);
        List<ref_fin_template_vm> Get_Parent_Ledger(int id);
        bool Update(ref_fin_template_detail ledger);
        List<exportdata1> GetExport();
        List<ref_fin_template_vm> getall();
        List<ref_fin_template_detail> GetGroupName();
        bool AddTemplateGLMapping(ref_fin_template_gl_mapping_vm vm);
        List<ref_fin_template_gl_mapping_vm> GetGroupByTemplate(int id);
    }
    
}
