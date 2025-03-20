
using WasteManagementSystem.Business.DTO;

namespace WasteManagementSystem.Business.Validation;

public class WasteTypeDtoValidation:IValidateDto<WasteTypeDto>
{
    public List<string> Validate(WasteTypeDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("No waste type is given with request");
            return errors;
        }
        if (String.IsNullOrEmpty(dto.Name))
        {
            errors.Add("Waste type name cannot be empty or null");
        }
        return errors;
    }

}

