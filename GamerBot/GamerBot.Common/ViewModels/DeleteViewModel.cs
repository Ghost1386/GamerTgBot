using System.ComponentModel.DataAnnotations;

namespace GamerBot.Common.ViewModels;

public class DeleteViewModel
{
    [Required]
    public string Email { get; set; }
}