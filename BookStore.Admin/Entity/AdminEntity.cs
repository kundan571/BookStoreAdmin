using System.ComponentModel.DataAnnotations;

namespace BookStore.Admin.Entity;

public class AdminEntity
{
    [Key]
    public int AdminId { get; set; }
    [Required]
    public string AdminName { get; set; }
    [Required]
    public string AdminEmail { get; set; }
    [Required]
    public string AdminPassword { get; set; }
}
