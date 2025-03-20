namespace WasteManagementSystem.Business.Validation;
public interface IValidateDto<T>
{
    List<string> Validate(T dto);
}

