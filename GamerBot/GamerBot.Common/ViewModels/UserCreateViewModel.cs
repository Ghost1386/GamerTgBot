using System.ComponentModel.DataAnnotations;

namespace GamerBot.Common.ViewModels;

public class UserCreateViewModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int Age { get; set; }
    
    [Required]
    public string SteamUrl { get; set; }
    
    [Required]
    public string Game { get; set; }
    
    [Required]
    public string Rank { get; set; }
    
    [Required]
    public long ChatId { get; set; }
}