namespace WasteManagementSystem.Business.DTO;
public class OrderDto
{
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public string OrderName { get; set; }
    public string OrderDate { get; set; }
    public string OrderDescription { get; set; }
    public int WasteTypeId { get; set; }
    public string WasteTypeName { get; set; }
    public int WasteAmount { get; set; }
    public int WasteUnitId { get; set; }
    public string WasteUnitName { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int? DriverId { get; set; }
    public string? DriverName { get; set; }
    public int StatusId { get; set; }
    public string StatusName { get; set; }
    public int LastUpdatedByUserId { get; set; }
    public string LastUpdatedByUserName { get; set; }
    public string LastUpdatedDate { get; set; }
}

