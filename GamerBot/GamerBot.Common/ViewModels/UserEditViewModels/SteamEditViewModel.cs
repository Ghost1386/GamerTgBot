using System.ComponentModel.DataAnnotations;

namespace GamerBot.Common.ViewModels.UserEditViewModels;

public class SteamEditViewModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string SteamUrl { get; set; }
}