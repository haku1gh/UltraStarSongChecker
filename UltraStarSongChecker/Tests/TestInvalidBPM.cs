using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for invalid BPM tags in the song files.
    /// </summary>
    /// <remarks>
    /// If the BPM tag is present, it should only contain positive double.
    /// </remarks>
    internal class TestInvalidBPM : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestInvalidBPM"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestInvalidBPM(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files with invalid BPM tag (not a positive double) ...");
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
                if (line.ToUpper().StartsWith("#BPM:"))
                {
                    string bpm = line.Substring(5).Trim();
                    if (bpm == "") break;

                    char[] numbers = "0123456789.,".ToCharArray();
                    foreach (char c in bpm)
                        if (!numbers.Contains(c)) error = true;
                    if (!error)
                    {
                        bpm = bpm.Replace(',', '.');
                        double iBpm = Convert.ToDouble(bpm, CultureInfo.CreateSpecificCulture("en-US"));
                        if (iBpm < 0) error = true;
                    }
                }
            }
            if (error)
                logOutput.Add("    => Invalid BPM tag found: " + song.FileName);
            return error;
        }

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected override void postprocessing()
        {
            logOutput.Add("    Found " + ErrorCounter + " invalid BPM tags in song files.");
        }
    }
}
