using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISkillMatrixService
    {
        bool AddLevel(ref_skill_matrix_vm vm);

        bool OperatorLevel(List<ref_skill_matrix_vm> vm);

        List<ref_skill_matrix_vm> GetAll(string entity, int? id);
        bool MachineLevel(ref_skill_matrix_vm vm);
        List<ref_machine_master_VM> GetMachineListWithOperationAndUser1(int userId);

        List<ref_skill_matrix_vm> GetHistory(int userId, bool isActive);

        List<ref_user_management_VM> GetUser(int mappingId);

        List<ref_level_vm> GetLevels();

        ref_temp_operator_level_mapping Add_temp_operator_level_mapping(ref_temp_operator_level_mapping value);
        List<temporary_skill_matrix_access_vm> GetAll1(string entity, int? id);
        temporary_skill_matrix_access_vm GetByIdTemporarySkillMatrix(int id);
    }
}
