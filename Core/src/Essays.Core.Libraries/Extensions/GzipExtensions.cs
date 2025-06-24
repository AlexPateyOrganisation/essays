using System.IO.Compression;
using System.Text;

namespace Essays.Core.Libraries.Extensions;

public static class GzipExtensions
{
    public static byte[] CompressWithGzip(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text cannot be null or whitespace.");
        }

        var bytes = Encoding.UTF8.GetBytes(text);
        using var output = new MemoryStream();
        using (var gzip = new GZipStream(output, CompressionLevel.Optimal))
        {
            gzip.Write(bytes, 0, bytes.Length);
        }

        return output.ToArray();
    }

    public static string DecompressWithGzip(this byte[] compressedBytes)
    {
        if (compressedBytes is null || compressedBytes.Length is 0)
        {
            throw new ArgumentException("Compressed bytes cannot be null or empty.");
        }

        using var input = new MemoryStream(compressedBytes);
        using var gzip = new GZipStream(input, CompressionMode.Decompress);
        using var output = new MemoryStream();
        gzip.CopyTo(output);
        return Encoding.UTF8.GetString(output.ToArray());
    }
}