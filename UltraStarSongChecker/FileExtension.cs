using System;

namespace UltraStarSongChecker
{
    /// <summary>
    /// Represents a class containing file extension infos.
    /// </summary>
    internal class FileExtension : IComparable<FileExtension>
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Gets or sets the occurrence.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="FileExtension"/>.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <param name="count">The initial occurrence of that extension.</param>
        public FileExtension(string extension, int count)
        {
            Extension = extension;
            Count = count;
        }

        // CompareTo operator is enough for Sort method in lists. //
        public int CompareTo(FileExtension other)
        {
            int ret = Count.CompareTo(other.Count);
            if (ret == 0)
                return Extension.CompareTo(other.Extension);
            else
                return -ret;
        }

        /// <summary>
        /// Returns a string representation of a file extension.
        /// </summary>
        public override string ToString()
        {
            return Extension + " [" + Count + "]";
        }
    }
}
