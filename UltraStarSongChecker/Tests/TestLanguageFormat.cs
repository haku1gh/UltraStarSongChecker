using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check the format of the language tag in the song files.
    /// </summary>
    /// <remarks>
    /// While this is not really an issue in UltraStar, some like to use PascalCase formatting.
    /// Unfortunately many txt files contain the language in upper case.
    /// </remarks>
    internal class TestLanguageFormat : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestLanguageFormat"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestLanguageFormat(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files where language is not in PascalCase format ...");
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
            // Basic check
            if (song.Language == "") return false;   // Nothing written in file
            // Test for PascalCase
            if (song.Language.All(char.IsUpper) || song.Language.All(char.IsLower) || song.Language[0] != song.Language.ToUpper()[0])
            {
                logOutput.Add("    => Invalid language format found: " + song.FileName);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " problematic language formats in song files.");
        }
    }
}
