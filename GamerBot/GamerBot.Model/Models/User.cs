namespace GamerBot.Model.Models;

public class User
{
    public int Id { get; set; }
    
    public string Email { get; set; }
    
    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public string SteamUrl { get; set; }
    
    public string Game { get; set; }

    public string Rank { get; set; }
    
    public long ChatId { get; set; }
}