using Azure;
using Azure.AI.TextAnalytics;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Domain.Enums;

namespace RealTimeChat.Infrastructure.Services;

internal class TextAnalyticsService : ITextAnalyticsService
{
    private readonly TextAnalyticsConnection _analyticsConnection;
    private readonly TextAnalyticsClient _client;
    public TextAnalyticsService(TextAnalyticsConnection analyticsConnection)
    {
        _analyticsConnection = analyticsConnection;

        _client = new TextAnalyticsClient(
            new Uri(_analyticsConnection.Endpoint),
            new AzureKeyCredential(_analyticsConnection.ApiKey)
        );
    }

    public async Task<SentimentTypeEnum> AnalyzeSentimentAsync(string text)
    {
        var response = await _client.AnalyzeSentimentAsync(text, "en");

        return response.Value.Sentiment switch
        {
            
            TextSentiment.Neutral => SentimentTypeEnum.Neutral,
            TextSentiment.Positive => SentimentTypeEnum.Positive,
            TextSentiment.Negative => SentimentTypeEnum.Negative,
            TextSentiment.Mixed => SentimentTypeEnum.Mixed,
            _ => SentimentTypeEnum.Neutral
        };
    }
}
