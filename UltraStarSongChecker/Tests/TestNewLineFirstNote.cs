using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for new 'sing'-line as the first note.
    /// </summary>
    /// <remarks>
    /// Some UltraStar versions can't handle this. You just need to delete that line.
    /// </remarks>
    internal class TestNewLineFirstNote : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestNewLineFirstNote"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestNewLineFirstNote(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files where the first note is a newline '-' ...");
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
            for(int i = 0; i < songFile.Length; i++)
            {
                if (songFile[i].StartsWith("#")) continue;
                else if (songFile[i].Trim().StartsWith("-")) error = true;
                break;
            }
            if (error)
                logOutput.Add("    => Newline as first note found: " + song.FileName);
            return error;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " newlines as first notes in song files.");
        }
    }
}
