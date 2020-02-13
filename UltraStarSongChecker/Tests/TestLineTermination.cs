using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for mixed line termination in the song files.
    /// </summary>
    /// <remarks>
    /// Some UltraStar versions can't handle a mix of Windows and Linux line termination.
    /// </remarks>
    internal class TestLineTermination : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestLineTermination"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestLineTermination(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files with mixed (Windows and Linux) line termination ...");
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
            byte prevByte = bytes[0];
            bool windowsLineTermination = false;
            bool linuxLineTermination = false;
            for (int i = 1; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x0A && prevByte == 0x0D) windowsLineTermination = true;
                if (bytes[i] == 0x0A && prevByte != 0x0D) linuxLineTermination = true;
                prevByte = bytes[i];
            }
            if (windowsLineTermination && linuxLineTermination)
            {
                logOutput.Add("    => Mixed line termination found: " + song.FileName);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + "  mixed line terminations in song files.");
        }
    }
}
