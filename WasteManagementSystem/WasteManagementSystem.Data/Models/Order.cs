using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WasteManagementSystem.Data.Models;
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid OrderId { get; set; } = new Guid(Guid.NewGuid().ToString());
    public string OrderName { get; set; }
    public string OrderDescription { get; set; }
    public string OrderDate { get; set; } = DateTime.Now.ToShortDateString();
    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("WasteType")]
    public int WasteTypeId { get; set; }
    public int WasteAmount { get; set; }
    [ForeignKey("WasteUnit")]
    public int WasteUnitId { get; set; }
    [ForeignKey("Status")]
    public int StatusId { get; set; } = 1;

    [ForeignKey("Driver")]
    public int? DriverId { get; set; }
   
    // Navigation properties
    public virtual User User { get; set; }
    public virtual User Driver { get; set; }
    public virtual WasteType WasteType { get; set; }
    public virtual WasteUnit WasteUnit { get; set; }
    public virtual Status Status { get; set; }
}

