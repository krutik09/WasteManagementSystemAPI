﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WasteManagementSystem.Data.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int PhoneNumber {  get; set; }
    [ForeignKey("UserType")]    
    public int UserTypeId { get; set; }

    // Navigation properties
    public virtual UserType UserType { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}

