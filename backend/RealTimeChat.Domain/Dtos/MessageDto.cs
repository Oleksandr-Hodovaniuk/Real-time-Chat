using RealTimeChat.Domain.Enums;

namespace RealTimeChat.Domain.Dtos;

public class MessageDto
{
    public string Text { get; set; } = null!;
    public string SentimentType { get; set; } = null!;
    public string Created { get; set; } = null!;
}
