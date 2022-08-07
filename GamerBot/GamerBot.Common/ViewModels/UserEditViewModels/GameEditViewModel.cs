using System.ComponentModel.DataAnnotations;

namespace GamerBot.Common.ViewModels.UserEditViewModels;

public class GameEditViewModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Game { get; set; }
}