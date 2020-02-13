using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for missing language tags in the song files.
    /// </summary>
    /// <remarks>
    /// </remarks>
    internal class TestMissingLanguageTag : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestMissingLanguageTag"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestMissingLanguageTag(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files with an empty or missing language tag ...");
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
            if (song.Language == "")
            {
                logOutput.Add("    => Empty or missing language tag found: " + song.FileName);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " empty or missing language tags in song files.");
        }
    }
}
