using Essays.Core.Libraries.Extensions;
using Essays.Core.Models.Models;
using Essays.Writer.Application.Repositories.Interfaces;
using Essays.Writer.Application.Services;
using Essays.Writer.Application.Services.Interfaces;
using NSubstitute;
using Shouldly;

namespace Essays.Writer.Application.Tests.Unit;

public class EssayWriterServiceTests
{
    private readonly IEssayCacheService _essayCacheService;
    private readonly IEssayWriterRepository _essayWriterRepository;
    private readonly EssayWriterService _sut;

    public EssayWriterServiceTests()
    {
        _essayCacheService = Substitute.For<IEssayCacheService>();
        _essayWriterRepository = Substitute.For<IEssayWriterRepository>();
        _sut = new EssayWriterService(_essayCacheService, _essayWriterRepository);
    }

    [Fact]
    public async Task CreateEssay_ShouldReturnTrue_WhenEssayIsCreated()
    {
        //Arrange
        Essay essay = new()
        {
            Id = new Guid("f76a1529-1725-469d-8d27-8fb0f0e61c40"),
            Title = "Test Title",
            CompressedBody =
                "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement."
                    .CompressWithGzip(),
            Author = "Test Author",
            CreatedWhen = new DateTime(2025, 7, 1)
        };

        _essayWriterRepository.CreateEssay(essay)
            .Returns(true);

        //Act
        var result = await _sut.CreateEssay(essay);

        //Assert
        result.ShouldBe(true);
    }

    [Fact]
    public async Task CreateEssay_ShouldReturnFalse_WhenEssayIsNotCreated()
    {
        //Arrange
        Essay essay = new()
        {
            Id = new Guid("f76a1529-1725-469d-8d27-8fb0f0e61c40"),
            Title = "Test Title",
            CompressedBody =
                "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement."
                    .CompressWithGzip(),
            Author = "Test Author",
            CreatedWhen = new DateTime(2025, 7, 1)
        };

        _essayWriterRepository.CreateEssay(essay)
            .Returns(false);

        //Act
        var result = await _sut.CreateEssay(essay);

        //Assert
        result.ShouldBe(false);
    }

    [Fact]
    public async Task UpdateEssay_ShouldReturnUpdatedEssayAndUpdateCache_WhenEssayIsUpdated()
    {
        //Arrange
        Essay essay = new()
        {
            Id = new Guid("f76a1529-1725-469d-8d27-8fb0f0e61c40"),
            Title = "Test Title",
            CompressedBody =
                "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement."
                    .CompressWithGzip(),
            Author = "Test Author",
            CreatedWhen = new DateTime(2025, 7, 1)
        };

        Essay updatedEssay = new()
        {
            Id = new Guid("f76a1529-1725-469d-8d27-8fb0f0e61c40"),
            Title = "Test Title",
            CompressedBody =
                "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement."
                    .CompressWithGzip(),
            Author = "Test Author",
            CreatedWhen = new DateTime(2025, 7, 1)
        };

        _essayWriterRepository.UpdateEssay(essay)
            .Returns(updatedEssay);

        //Act
        var result = await _sut.UpdateEssay(essay);

        //Assert
        result.ShouldBeEquivalentTo(updatedEssay);
        await _essayCacheService.Received().DeleteEssay(essay.Id);
    }

    [Fact]
    public async Task UpdateEssay_ShouldReturnNullAndNotUpdateCache_WhenEssayIsNotUpdated()
    {
        //Arrange
        Essay essay = new()
        {
            Id = new Guid("f76a1529-1725-469d-8d27-8fb0f0e61c40"),
            Title = "Test Title",
            CompressedBody =
                "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement."
                    .CompressWithGzip(),
            Author = "Test Author",
            CreatedWhen = new DateTime(2025, 7, 1)
        };

        _essayWriterRepository.UpdateEssay(essay)
            .Returns(null as Essay);

        //Act
        var result = await _sut.UpdateEssay(essay);

        //Assert
        result.ShouldBeNull();
        await _essayCacheService.DidNotReceive().DeleteEssay(essay.Id);
    }

    [Fact]
    public async Task DeleteEssay_ShouldReturnTrueAndUpdateCache_WhenEssayIsDeleted()
    {
        //Arrange
        var essayId = Guid.NewGuid();

        _essayWriterRepository.DeleteEssay(essayId)
            .Returns(true);

        //Act
        var result = await _sut.DeleteEssay(essayId);

        //Assert
        result.ShouldBe(true);
        await _essayCacheService.Received().DeleteEssay(essayId);
    }

    [Fact]
    public async Task DeleteEssay_ShouldReturnFalseAndNotUpdateCache_WhenEssayIsNotDeleted()
    {
        //Arrange
        var essayId = Guid.NewGuid();

        _essayWriterRepository.DeleteEssay(essayId)
            .Returns(false);

        //Act
        var result = await _sut.DeleteEssay(essayId);

        //Assert
        result.ShouldBe(false);
        await _essayCacheService.DidNotReceive().DeleteEssay(essayId);
    }
}