using System.Diagnostics.CodeAnalysis;
using System.Text;
using ArchitectCodingChallenge.Infrastructure.Persistence.Abstractions;

namespace ArchitectCodingChallenge.Infrastructure.Persistence.Services;

/// <summary>
/// Represents the wrapper for file input-output operations, hiding the file system dependencies and make it 
/// possible to unit testing.
/// </summary>
/// <remarks>
/// This interface wraps some of the <see cref="Directory"/>, <see cref="Path"/> and <see cref="File"/> class methods.
/// </remarks>
public sealed class FileSystem : IFileSystem {
    #region IFileSystem
    /// <inheritdoc/>
    public void AppendAllText(string path, string? contents, Encoding encoding) => File.AppendAllText(path, contents, encoding);

    /// <inheritdoc/>
    public DirectoryInfo CreateDirectory(string path) => Directory.CreateDirectory(path);

    /// <inheritdoc/>
    public bool DirectoryExists([NotNullWhen(true)] string? path) => Directory.Exists(path);

    /// <inheritdoc/>
    public bool FileExists([NotNullWhen(true)] string? path) => File.Exists(path);

    /// <inheritdoc/>
    public string? GetDirectoryName(string? path) => Path.GetDirectoryName(path);

    /// <inheritdoc/>
    public StreamReader OpenText(string path) => File.OpenText(path);
    #endregion
}
