using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for invalid song files.
    /// </summary>
    /// <remarks>
    /// If this test found an invalid song, then either you have a corrupt UltraStar song file, or
    /// your text file is not a song file. In the latter case, I would recommend to give that file a different extension.
    /// </remarks>
    internal class TestInvalidSong : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestInvalidSong"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestInvalidSong(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for invalid song files (Artist, Title or MP3 missing) ...");
        }

        /// <summary>
        /// Internal method call to run the test.
        /// </summary>
        /// <param name="song">The song to check.</param>
        /// <param name="songFile">The parsed file as a string array.</param>
        /// <param name="bytes">The parsed file as a byte array.</param>
        /// <param name="songEntries">All song entries already found.</param>
        /// <returns><c>true</c> if an error was found; otherwise <c>false</c>.</returns>
        protected override bool onRun(SongEntry song, string[] songFile, byte[] bytes, List<SongEntry> songEntries)
        {
            if (song.Title == "" || song.Artist == "" || song.MP3 == "")
            {
                logOutput.Add("    => Invalid song found: " + song.FileName);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " invalid song files.");
        }
    }
}
