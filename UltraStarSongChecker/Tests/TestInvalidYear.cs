using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for invalid year tags in the song files.
    /// </summary>
    /// <remarks>
    /// If the year tag is present, it should only contain positive integers.
    /// </remarks>
    internal class TestInvalidYear : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestInvalidYear"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestInvalidYear(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files with invalid year tag (not a positive integer) ...");
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
                if (line.ToUpper().StartsWith("#YEAR:"))
                {
                    string year = line.Substring(6).Trim();
                    if (year == "") break;

                    char[] numbers = "0123456789".ToCharArray();
                    foreach (char c in year)
                        if (!numbers.Contains(c)) error = true;
                    if (year.Length > 4) error = true;
                    if (!error)
                    {
                        int iYear = Convert.ToInt32(year);
                        if (iYear < 0 || iYear > 2100) error = true;
                    }
                }
            }
            if (error)
                logOutput.Add("    => Invalid year tag found: " + song.FileName);
            return error;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " invalid year tags in song files.");
        }
    }
}
