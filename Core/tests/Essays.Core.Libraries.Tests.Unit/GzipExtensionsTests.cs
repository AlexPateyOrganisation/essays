using Essays.Core.Libraries.Extensions;
using Shouldly;

namespace Essays.Core.Libraries.Tests.Unit;

public class GzipExtensionsTests
{
    [Fact]
    public void CompressWithGzip_ShouldReturnCompressedBytes_WhenPassedNonEmptyUncompressedString()
    {
        //Arrange
        const string uncompressedText = "test";

        //Act
        var result = uncompressedText.CompressWithGzip();

        //Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.DecompressWithGzip().ShouldBe(uncompressedText);
    }

    [Fact]
    public void CompressWithGzip_ShouldThrowException_WhenPassedEmptyString()
    {
        //Arrange
        var uncompressedText = string.Empty;

        //Act
        Should.Throw<ArgumentException>(() =>
            uncompressedText.CompressWithGzip()).Message.ShouldBe("Text cannot be null or whitespace.");
    }

    [Fact]
    public void CompressWithGzip_ShouldThrowException_WhenPassedWhitespaceString()
    {
        //Arrange
        const string uncompressedText = "   ";

        //Act
        Should.Throw<ArgumentException>(() =>
            uncompressedText.CompressWithGzip()).Message.ShouldBe("Text cannot be null or whitespace.");
    }

    [Fact]
    public void DecompressWithGzip_ShouldReturnUncompressedString_WhenPassedCompressedBytes()
    {
        //Arrange
        byte[] compressedBytes = [31, 139, 8, 0, 0, 0, 0, 0, 0, 10, 43, 73, 45, 46, 1, 0, 12, 126, 127, 216, 4, 0, 0, 0];

        //Act
        var result = compressedBytes.DecompressWithGzip();

        //Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.ShouldBe("test");
    }

    [Fact]
    public void CompressWithGzip_ShouldThrowException_WhenPassedEmptyBytesArray()
    {
        //Arrange
        byte[] compressedBytes = [];

        //Act
        Should.Throw<ArgumentException>(() =>
            compressedBytes.DecompressWithGzip()).Message.ShouldBe("Compressed bytes cannot be null or empty.");
    }

    [Fact]
    public void CompressWithGzip_ShouldThrowException_WhenPassedNull()
    {
        //Arrange
        byte[] compressedBytes = null!;

        //Act
        Should.Throw<ArgumentException>(() =>
            compressedBytes.DecompressWithGzip()).Message.ShouldBe("Compressed bytes cannot be null or empty.");
    }
}