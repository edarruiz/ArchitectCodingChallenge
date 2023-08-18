using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ArchitectCodingChallenge.Infrastructure.Persistence.Abstractions;

/// <summary>
/// Represents the interface wrapper for file input-output operations, hiding the file system dependencies and make it 
/// possible to unit testing.
/// </summary>
/// <remarks>
/// This interface wraps some of the <see cref="Directory"/>, <see cref="Path"/> and <see cref="File"/> class methods.
/// </remarks>
public interface IFileSystem {
    /// <summary>
    /// Wraps the <see cref="Directory.CreateDirectory(string)"/> class method.
    /// <para>Creates all directories and subdirectories in the specified path with the specified permissions unless they already exist.</para>
    /// </summary>
    /// <param name="path">The directory to create.</param>
    /// <returns>An object that represents the directory at the specified path. This object is returned regardless of whether 
    /// a directory at the specified path already exists.</returns>
    DirectoryInfo CreateDirectory(string path);

    /// <summary>
    /// Wraps the <see cref="Directory.Exists(string?)"/> class method.
    /// <para>Determines whether the given path refers to an existing directory on disk.</para>
    /// </summary>
    /// <param name="path">The path to test.</param>
    /// <returns><c>true</c> if <c>path</c> refers to an existing directory; <c>false</c> if the directory does not exist 
    /// or an error occurs when trying to determine if the specified directory exists.</returns>
    bool DirectoryExists([NotNullWhen(true)] string? path);

    /// <summary>
    /// Wraps the <see cref="Path.GetDirectoryName(string?)"/> class method.
    /// <para>Returns the directory information for the specified path.</para>
    /// </summary>
    /// <param name="path">The path of a file or directory.</param>
    /// <returns>Directory information for <c>path</c>, or <c>null</c> if <c>path</c> denotes a root directory or is null. 
    /// Returns <see cref="string.Empty"/> if <c>path</c> does not contain directory information.</returns>
    string? GetDirectoryName(string? path);

    /// <summary>
    /// Wraps the <see cref="File.Exists(string?)"/> class method.
    /// <para>Determines whether the specified file exists.</para>
    /// </summary>
    /// <param name="path">The file to check.</param>
    /// <returns>Returns <c>true</c> if the caller has the required permissions and <c>path</c> contains the name of an existing file; 
    /// otherwise, <c>false</c>. This method also returns <c>false</c> if <c>path</c> is <c>null</c>, an invalid path, or a zero-length string. 
    /// If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method 
    /// returns <c>false</c> regardless of the existence of <c>path</c>.</returns>
    bool FileExists([NotNullWhen(true)] string? path);

    /// <summary>
    /// Wraps the <see cref="File.AppendAllText(string, string?, Encoding)"/> class method.
    /// <para>Appends the specified string to the file using the specified encoding, creating the file if it does not already exist.</para>
    /// </summary>
    /// <param name="path">The file to append the specified string to.</param>
    /// <param name="contents">The string to append to the file.</param>
    /// <param name="encoding">The character encoding to use.</param>
    void AppendAllText(string path, string? contents, Encoding encoding);

    /// <summary>
    /// Wraps the <see cref="File.OpenText(string)"/> class method.
    /// <para>Opens an existing UTF-8 encoded text file for reading.</para>
    /// </summary>
    /// <param name="path">The file to be opened for reading.</param>
    /// <returns>A <see cref="StreamReader"/> on the specified path.</returns>
    StreamReader OpenText(string path);

    /// <summary>
    /// Wraps the <see cref="File.WriteAllText(string, string?, Encoding)"/> class method.
    /// </summary>
    /// <remarks>
    /// Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. 
    /// If the target file already exists, it is overwritten.
    /// </remarks>
    /// <param name="path">The file to write to.</param>
    /// <param name="contents">The string to write to the file.</param>
    /// <param name="encoding">The encoding to apply to the string.</param>
    void WriteAllText(string path, string? contents, Encoding encoding);
}
