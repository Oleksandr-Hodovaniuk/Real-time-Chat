namespace RealTimeChat.Application.Dtos;

public class MessageDto
{
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string SentimentType { get; set; } = null!;
    public string Created { get; set; } = null!;
}
