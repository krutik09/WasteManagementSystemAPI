﻿using System.ComponentModel.DataAnnotations.Schema;
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
    public string OrderDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    public string LastUpdatedDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    [ForeignKey("OrderUpdatedBy")]
    public int LastUpdatedByUserId { get; set; }
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
}

