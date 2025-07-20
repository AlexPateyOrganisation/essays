using Essays.Core.Libraries.Extensions;
using Essays.Core.Models.Models;
using Essays.Retriever.Application.Repositories.Interfaces;
using Essays.Retriever.Application.Services;
using Essays.Retriever.Application.Services.Interfaces;
using NSubstitute;
using Shouldly;

namespace Essays.Retriever.Application.Tests.Unit;

public class EssayRetrieverServiceTests
{
    private readonly IEssayCacheService _essayCacheService;
    private readonly IEssayRetrieverRepository _essayRetrieverRepository;
    private readonly EssayRetrieverService _sut;

    public EssayRetrieverServiceTests()
    {
        _essayCacheService = Substitute.For<IEssayCacheService>();
        _essayRetrieverRepository = Substitute.For<IEssayRetrieverRepository>();

        _sut = new EssayRetrieverService(_essayCacheService, _essayRetrieverRepository);
    }

    [Fact]
    public async Task GetEssay_ShouldReturnCachedEssay_WhenCachedEssayExists()
    {
        //Arrange
        var essayId = Guid.NewGuid();

        Essay essay = new()
        {
            Id = essayId,
            Title = "Test Title",
            CompressedBody =
                "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement."
                    .CompressWithGzip(),
            Author = "Test Author",
            CreatedWhen = new DateTime(2025, 7, 1)
        };
        
        _essayCacheService.GetEssay(essayId)
            .Returns(essay);

        //Act
        var result = await _sut.GetEssay(essayId);

        //Assert
        result.ShouldBeEquivalentTo(result);
        await _essayRetrieverRepository.DidNotReceive().GetEssay(essayId);
    }

    [Fact]
    public async Task GetEssay_ShouldReturnEssayFromDatabase_WhenCachedEssayDoesNotExist()
    {
        //Arrange
        var essayId = Guid.NewGuid();

        Essay essay = new()
        {
            Id = essayId,
            Title = "Test Title",
            CompressedBody =
                "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement."
                    .CompressWithGzip(),
            Author = "Test Author",
            CreatedWhen = new DateTime(2025, 7, 1)
        };

        _essayCacheService.GetEssay(essayId)
            .Returns(null as Essay);

        _essayRetrieverRepository.GetEssay(essayId)
            .Returns(essay);

        //Act
        var result = await _sut.GetEssay(essayId);

        //Assert
        result.ShouldBeEquivalentTo(result);
        await _essayRetrieverRepository.Received().GetEssay(essayId);
    }

    [Fact]
    public async Task GetEssay_ShouldReturnNull_WhenEssayDoesNotExist()
    {
        //Arrange
        var essayId = Guid.NewGuid();

        _essayCacheService.GetEssay(essayId)
            .Returns(null as Essay);

        _essayRetrieverRepository.GetEssay(essayId)
            .Returns(null as Essay);

        //Act
        var result = await _sut.GetEssay(essayId);

        //Assert
        result.ShouldBeNull();
    }

}