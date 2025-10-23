namespace RealTimeChat.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
