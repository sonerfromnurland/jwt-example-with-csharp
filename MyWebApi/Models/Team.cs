using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Models;

public class Team
{
    public DateTime? CreatedTime { get; set; }
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}