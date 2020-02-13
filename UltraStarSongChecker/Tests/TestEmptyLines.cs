using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for empty lines in the song files.
    /// </summary>
    /// <remarks>
    /// Some UltraStar versions can't handle this, don't ask why.
    /// This test checks if there is a maximum of 1 empty line (usually after the end mark).
    /// </remarks>
    internal class TestEmptyLines : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestEmptyLines"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestEmptyLines(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files with more than one empty line ...");
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
            int count = 0;
            foreach (string line in songFile)
            {
                if (line.Trim() == "")
                    count++;
            }
            if (count >= 1)
            {
                logOutput.Add("    => Multiple empty lines found: " + song.FileName);
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
            logOutput.Add("    Found " + ErrorCounter + " files with multiple empty lines in song files.");
        }
    }
}
