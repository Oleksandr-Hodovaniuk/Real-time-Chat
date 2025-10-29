using RealTimeChat.Domain.Enums;

namespace RealTimeChat.Application.Interfaces;

public interface ITextAnalyticsService
{
    Task<SentimentTypeEnum> AnalyzeSentimentAsync(string text);
}
