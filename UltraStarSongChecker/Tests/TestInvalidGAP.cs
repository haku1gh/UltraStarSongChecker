using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for invalid GAP tags in the song files.
    /// </summary>
    /// <remarks>
    /// If the GAP tag is present, it should only contain positive integers.
    /// As some song files also contain negative GAP, we are just checking for double.
    /// </remarks>
    internal class TestInvalidGAP : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestInvalidGAP"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestInvalidGAP(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files with invalid GAP tag (not a double) ...");
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
                if (line.ToUpper().StartsWith("#GAP:"))
                {
                    string gap = line.Substring(5).Trim();
                    if (gap == "") break;

                    char[] numbers = "-0123456789.,".ToCharArray();
                    foreach (char c in gap)
                        if (!numbers.Contains(c)) error = true;
                }
            }
            if (error)
                logOutput.Add("    => Invalid GAP tag found: " + song.FileName);
            return error;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " invalid GAP tags in song files.");
        }
    }
}
