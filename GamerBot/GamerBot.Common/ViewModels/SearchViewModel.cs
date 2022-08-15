using System.ComponentModel.DataAnnotations;

namespace GamerBot.Common.ViewModels;

public class SearchViewModel
{
    [Required]
    public long ChatId { get; set; }
    
    [Required]
    public string Game { get; set; }
    
    [Required]
    public string Rank { get; set; }
}