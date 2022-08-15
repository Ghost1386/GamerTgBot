using System.ComponentModel.DataAnnotations;

namespace GamerBot.Common.ViewModels.UserEditViewModels;

public class RankEditViewModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Rank { get; set; }
}