using Microsoft.EntityFrameworkCore;
using WasteManagementSystem.Data.Models;

namespace WasteManagementSystem.Data.Context;
public partial class WmsContext: DbContext
{
    public WmsContext(DbContextOptions<WmsContext> options):base(options)
    {
        
    }
    public virtual DbSet<UserType> UserTypes { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }
    public virtual DbSet<WasteType> WasteTypes { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<WasteUnit> WasteUnits { get; set; }
    
}

