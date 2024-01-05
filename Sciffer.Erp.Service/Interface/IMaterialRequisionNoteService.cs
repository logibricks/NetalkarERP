using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface IMaterialRequisionNoteService:IDisposable
    {
        List<material_requision_note_vm> GetPendigApprovedList();
        List<material_requision_note_vm> GetAll();
        material_requision_note_vm Get(int id);
        string Add(material_requision_note_vm material);
        string Update(material_requision_note_vm material);
        bool Delete(int id);
        List<material_requision_note_vm> GetMRnList();
        bool ChangeApprovedStatus(material_requision_note_vm vm);
        double GetMaterialRate(int plant_id,int item_id);

        string materialRequisionNoteupdatestatusseen();

    }
}
