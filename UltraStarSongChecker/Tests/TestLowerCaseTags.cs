using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for lower-case tags in the song files.
    /// </summary>
    /// <remarks>
    /// Unclear if UltraStar has a problem with it, but these tags should be in upper case.
    /// </remarks>
    internal class TestLowerCaseTags : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestLowerCaseTags"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestLowerCaseTags(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files with tags in lower-case characters ...");
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
            bool error = false; // Default assume NO error
            foreach (string line in songFile)
            {
                if (line.StartsWith("#") && line.Contains(':'))
                {
                    string tagName = line.Substring(1, line.IndexOf(':') - 1);
                    if (tagName.Any(char.IsLower))
                        error = true;
                }
            }
            if (error)
                logOutput.Add("    => Lower-case characters found: " + song.FileName);
            return error;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " tags with lower-case characters in song files.");
        }
    }
}
