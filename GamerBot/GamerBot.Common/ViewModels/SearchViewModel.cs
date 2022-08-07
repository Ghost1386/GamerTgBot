using System.ComponentModel.DataAnnotations;

namespace GamerBot.Common.ViewModels;

public class SearchViewModel
{
    [Required]
    public string Game { get; set; }
    
    [Required]
    public string Rank { get; set; }
}