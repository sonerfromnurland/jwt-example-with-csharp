using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [NotMapped]
    public string Token { get; set; }
}