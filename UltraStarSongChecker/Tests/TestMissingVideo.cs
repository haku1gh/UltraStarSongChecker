using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for missing video song files.
    /// </summary>
    /// <remarks>
    /// All UltraStar versions should be able handle this and usually just show a white background.
    /// </remarks>
    internal class TestMissingVideo : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestMissingVideo"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestMissingVideo(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for files where the video is in the txt file, but not existing ...");
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
            // Basic check
            if (song.Video == "") return false;   // Nothing written in file
            // Get path of the file
            string path = song.FileName.Substring(0, song.FileName.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            // Get full qualified file name
            string fullName = path + song.Video;
            // Check if file exists
            FileInfo fi = new FileInfo(fullName);
            if(!fi.Exists)
            {
                logOutput.Add("    => Missing video found: " + song.FileName);
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
            logOutput.Add("    Found " + ErrorCounter + " missing video(s) in song files.");
        }
    }
}
