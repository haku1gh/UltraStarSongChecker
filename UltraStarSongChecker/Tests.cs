using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraStarSongChecker
{
    internal static class Tests2
    {
        /// <summary>
        /// Check for invalid song files.
        /// </summary>
        /// <param name="file">The full qualified file name of the song.</param>
        /// <param name="songFile">The parsed file.</param>
        /// <param name="logOutput">The log where error messages shall be written to.</param>
        /// <returns><c>true</c> if an error was found; otherwise <c>false</c>.</returns>
        public static bool CheckInvalidSong(string file, string[] songFile, List<string> logOutput)
        {
            string artist = "", title = "", mp3 = "";
            foreach (string line in songFile)
            {
                if (line.ToUpper().StartsWith("#ARTIST:")) artist = line.Substring(8).Trim();
                if (line.ToUpper().StartsWith("#TITLE:")) title = line.Substring(7).Trim();
                if (line.ToUpper().StartsWith("#MP3:")) mp3 = line.Substring(5).Trim();
            }
            if (title == "" || artist == "" || mp3 == "")
            {
                logOutput.Add("  => Invalid song found: " + file);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Check for invalid song files.
        /// </summary>
        /// <param name="song">The song to check.</param>
        /// <param name="logOutput">The log where error messages shall be written to.</param>
        /// <returns><c>true</c> if an error was found; otherwise <c>false</c>.</returns>
        public static bool CheckInvalidSong(SongEntry song, List<string> logOutput)
        {
            if (song.Title == "" || song.Artist == "" || song.MP3 == "")
            {
                logOutput.Add("  => Invalid song found: " + song.FileName);
                return true;
            }
            else
                return false;
        }
    }
}
