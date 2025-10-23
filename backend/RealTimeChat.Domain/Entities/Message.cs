using RealTimeChat.Domain.Enums;

namespace RealTimeChat.Domain.Entities;

public class Message : BaseEntity
{
    public Guid UserId{ get; set; }
    public string Text { get; set; } = null!;
    public SentimentTypeEnum SentimentType { get; set; }
    public DateTime Created { get; set; }
    public User User { get; set; } = null!;
}
