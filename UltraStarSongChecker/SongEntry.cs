using System;
using System.Collections.Generic;

namespace UltraStarSongChecker
{
    /// <summary>
    /// Represents a song entry from a txt file.
    /// </summary>
    internal class SongEntry : IEquatable<SongEntry>
    {
        /// <summary>
        /// Gets the full qualified file name of the song.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets the name of the artist.
        /// </summary>
        public string Artist { get; }

        /// <summary>
        /// Gets the title of the song.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the language of the song.
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// Gets the genre of the song.
        /// </summary>
        public string Genre { get; }

        /// <summary>
        /// Gets the creator of the song file.
        /// </summary>
        public string Creator { get; }

        /// <summary>
        /// Gets the MP3 file name to the song.
        /// </summary>
        public string MP3 { get; }

        /// <summary>
        /// Gets the video file name to the song.
        /// </summary>
        public string Video { get; }

        /// <summary>
        /// Gets the cover file name to the song.
        /// </summary>
        public string Cover { get; }

        /// <summary>
        /// Gets the background file name of the song.
        /// </summary>
        public string Background { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="SongEntry"/>.
        /// </summary>
        /// <param name="fileName">The full qualified file name of the song.</param>
        /// <param name="artist">The name of the artist.</param>
        /// <param name="title">The title of the song.</param>
        /// <param name="language">The language of the song.</param>
        /// <param name="genre">The genre of the song.</param>
        /// <param name="creator">The creator of the song file.</param>
        /// <param name="mp3">The MP3 file name.</param>
        /// <param name="video">The video file name.</param>
        /// <param name="cover">The cover file name.</param>
        /// <param name="background">The background file name.</param>
        public SongEntry(string fileName, string artist, string title, string language, string genre, string creator, string mp3, string video, string cover, string background)
        {
            this.FileName = fileName;
            this.Artist = artist;
            this.Title = title;
            this.Language = language;
            this.Genre = genre;
            this.Creator = creator;
            this.MP3 = mp3;
            this.Video = video;
            this.Cover = cover;
            this.Background = background;
        }

        // Equals operator is enough for Contains method in lists. //
/*        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SongEntry objAsSongEntry = obj as SongEntry;
            if (objAsSongEntry == null) return false;
            else return Equals(objAsSongEntry);
        }*/
        public bool Equals(SongEntry other)
        {
            if (other == null) return false;
            return (this.Artist.ToUpper().Equals(other.Artist.ToUpper()) && this.Title.ToUpper().Equals(other.Title.ToUpper()));
        }
/*        public override int GetHashCode()   // Auto generated code
        {
            var hashCode = -823500969;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Artist);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            return hashCode;
        }
*/
        /// <summary>
        /// Returns a string representation of a song entry.
        /// </summary>
        public override string ToString()
        {
            return Artist + " - " + Title + " [" + FileName + "]";
        }
    }
}
