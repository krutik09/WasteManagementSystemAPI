namespace WasteManagementSystem.Business.DTO;
public class OrderDto
{
    public Guid OrderId { get; set; }
    public string OrderName { get; set; }
    public string OrderDate { get; set; }
    public string OrderDescription { get; set; }
    public string WasteTypeName { get; set; }
    public int WasteAmount { get; set; }
    public string WasteUnitName { get; set; }
    public string UserName { get; set; }
    public string? DriverName { get; set; }
    public string StatusName { get; set; }

}

