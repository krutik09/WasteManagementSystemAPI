namespace WasteManagementSystem.Business.DTO;

public class OrderRequestDto
{
    public string OrderName { get; set; }
    public string OrderDescription { get; set; }
    public string WasteTypeName { get; set; }
    public int WasteAmount { get; set; }
    public string WasteUnitName { get; set; }
}

