

using WasteManagementSystem.Business.DTO;

namespace WasteManagementSystem.Business.Validation;
public class UserTypeDtoValidation : IValidateDto<UserTypeDto>
{
    public List<string> Validate(UserTypeDto dto)
    {
        var errors = new List<string>();
        if(dto == null)
        {
            errors.Add("No user type is given with request");
            return errors;
        }
        if (String.IsNullOrEmpty(dto.Name)) {
            errors.Add("User type name cannot be empty or null");
        }
        return errors;
    }
}

