using Application.Common.Caching;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.SEORanking.Retrieve;
using NSubstitute;

namespace Application.Tests.SEORanking.Retrieve;

public class RetrieveSEORankingRequestHandlerTest
{
    [Test]
    public async Task RetrieveSEORankingRequestHandler_Handle_ShouldReturnRankingSuccessfully()
    {
        // Arrange
        var factoryMock = Substitute.For<ISearchEngineScrapeStrategyFactory>();
        var searchEngineScraperMock = Substitute.For<ISearchEngineScraper>();
        searchEngineScraperMock.GetSearchRankings(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns([1, 2, 3]);

        factoryMock.GetSearchEngineScrapeStrategies(Arg.Any<SearchEngine[]>()).Returns(
        [
            searchEngineScraperMock
        ]);

        var retrieveSEORankingRequestHandler = new RetrieveSEORankingRequestHandler(
            factoryMock,
            Substitute.For<ICacheService>());

        // Act
        var result = await retrieveSEORankingRequestHandler.Handle(
            new RetrieveSEORankingRequest
            {
                Keyword = "keyword",
                Url = "url",
                SearchEngines = [SearchEngine.Google]
            },
            default);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Results, Has.Length.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result.Results[0].Rankings, Is.EquivalentTo(new int[] { 1, 2, 3 }));
        });
        await searchEngineScraperMock.Received(1).GetSearchRankings(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task RetrieveSEORankingRequestHandler_Handle_ShouldReturnRankingFromCacheSuccessfully()
    {
        // Arrange
        var factoryMock = Substitute.For<ISearchEngineScrapeStrategyFactory>();
        var cacheServiceMock = Substitute.For<ICacheService>();
        cacheServiceMock.GetAsync<RetrieveSEORankingResponse.SEORankingResponse>(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(
            new RetrieveSEORankingResponse.SEORankingResponse
            {
                SearchEngine = "Google",
                Rankings = [1, 2, 3]
            });

        var searchEngineScraperMock = Substitute.For<ISearchEngineScraper>();
        factoryMock.GetSearchEngineScrapeStrategies(Arg.Any<SearchEngine[]>()).Returns(
        [
            searchEngineScraperMock
        ]);

        var retrieveSEORankingRequestHandler = new RetrieveSEORankingRequestHandler(
            factoryMock,
            cacheServiceMock
        );

        // Act
        var result = await retrieveSEORankingRequestHandler.Handle(
            new RetrieveSEORankingRequest
            {
                Keyword = "keyword",
                Url = "url",
                SearchEngines = [SearchEngine.Google]
            },
            default);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Results, Has.Length.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result.Results[0].SearchEngine, Is.EqualTo("Google"));
            Assert.That(result.Results[0].Rankings, Is.EquivalentTo(new int[] { 1, 2, 3 }));
        });
        await searchEngineScraperMock.DidNotReceive().GetSearchRankings(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await cacheServiceMock.DidNotReceive().SetAsync(Arg.Any<string>(), Arg.Any<RetrieveSEORankingResponse.SEORankingResponse>(), Arg.Any<TimeSpan>(), Arg.Any<CancellationToken>());
    }
}