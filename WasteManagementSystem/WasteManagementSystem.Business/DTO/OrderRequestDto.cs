namespace WasteManagementSystem.Business.DTO;

public class OrderRequestDto
{
    public string OrderName { get; set; }
    public string OrderDescription { get; set; }
    public int WasteTypeId { get; set; }
    public int WasteAmount { get; set; }
    public int WasteUnitId { get; set; }
}

