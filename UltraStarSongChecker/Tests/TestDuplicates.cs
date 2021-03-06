﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a class to check for duplicate song files.
    /// </summary>
    /// <remarks>
    /// Some UltraStar versions might show duplicates. Please do a manual check of the two songs and delete
    /// if they are real duplicates.
    /// </remarks>
    internal class TestDuplicates : Test
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestDuplicates"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public TestDuplicates(string testName, bool enabled = true) : base(testName, enabled)
        {
            logOutput.Add("Checking for duplicate files ...");
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
            if (songEntries.Contains(song))
            {
                logOutput.Add("    => Duplicate song found: " + song.FileName);
                logOutput.Add("                             " + songEntries[songEntries.IndexOf(song, 0)].FileName);
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
            logOutput.Add("    Found " + ErrorCounter + " duplicate song files.");
        }
    }
}
