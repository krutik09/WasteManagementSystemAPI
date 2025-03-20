using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WasteManagementSystem.Data.Models;
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("WasteType")]
    public int WasteTypeId { get; set; }

    [ForeignKey("Status")]
    public int StatusId { get; set; }
    // Navigation properties
    public virtual User User { get; set; }
    public virtual WasteType WasteType { get; set; }
    public virtual Status Status { get; set; }
}

