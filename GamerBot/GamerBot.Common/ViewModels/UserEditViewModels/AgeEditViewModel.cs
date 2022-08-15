using System.ComponentModel.DataAnnotations;

namespace GamerBot.Common.ViewModels.UserEditViewModels;

public class AgeEditViewModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public int Age { get; set; }
}