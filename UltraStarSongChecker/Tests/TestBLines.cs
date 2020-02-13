using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for B-lines in the song files.
    /// </summary>
    /// <remarks>
    /// Some UltraStar versions can't handle this.
    /// Be aware that you can't just delete the B lines, they have a meaning.
    /// </remarks>
    internal class TestBLines : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestBLines"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestBLines(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files containing lines starting with 'B' ...");
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
                if (line.Trim().StartsWith("B"))
                {
                    error = true;
                    break;
                }
            }
            if (error)
                logOutput.Add("    => B-line found: " + song.FileName);
            return error;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " B-lines in song files.");
        }
    }
}
