using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for song files without a cover.
    /// </summary>
    /// <remarks>
    /// All UltraStar versions should be able handle this and usually just show the default cover.
    /// </remarks>
    internal class TestNoCover : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestNoCover"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestNoCover(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files where no cover is mentioned in the txt file ...");
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
            if (song.Cover == "")
            {
                logOutput.Add("    => No cover found: " + song.FileName);
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
            logOutput.Add("    Found " + ErrorCounter + " no mentioned cover(s) in song files.");
        }
    }
}
