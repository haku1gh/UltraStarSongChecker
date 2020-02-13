using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for missing end makrs in song files.
    /// </summary>
    /// <remarks>
    /// Some UltraStar versions can't handle this. You just need to add an E at the end.
    /// But you should check that file if there is an E somewhere in the middle existing.
    /// </remarks>
    internal class TestMissingEndmark : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestMissingEndmark"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestMissingEndmark(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files where End mark 'E' is missing ...");
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
            bool error = true; // Default assume an error
            for(int i = songFile.Length - 1; i >= 0; i--)
            {
                if (songFile[i].Trim() == "") continue;
                else if (songFile[i].Trim().StartsWith("E")) error = false;
                break;
            }
            if (error)
                logOutput.Add("    => Missing end mark found: " + song.FileName);
            return error;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " missing end marks in song files.");
        }
    }
}
