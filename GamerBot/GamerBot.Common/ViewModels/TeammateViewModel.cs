using System.ComponentModel.DataAnnotations;

namespace GamerBot.Common.ViewModels;

public class TeammateViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int Age { get; set; }
    
    [Required]
    public string SteamUrl { get; set; }

    [Required]
    public string Rank { get; set; }
}