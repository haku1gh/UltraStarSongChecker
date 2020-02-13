using System;
using System.Collections.Generic;

namespace UltraStarSongChecker.Tests
{
    /// <summary>
    /// Represents a generic test class.
    /// </summary>
    internal abstract class Test
    {
        /// <summary>
        /// Gets the name of the test.
        /// </summary>
        public string TestName { get; }

        /// <summary>
        /// Gets or sets an indicator whether the test is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets an error counter for this test.
        /// </summary>
        public int ErrorCounter { get; protected set; }

        /// <summary>
        /// Object for logging.
        /// </summary>
        protected List<string> logOutput;

        /// <summary>
        /// Initializes a new instance of <see cref="Test"/>.
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="enabled">Indicator whether the test is enabled.</param>
        public Test(string testName, bool enabled = true)
        {
            TestName = testName;
            Enabled = enabled;
            ErrorCounter = 0;
            logOutput = new List<string>();
        }

        /// <summary>
        /// Runs the test.
        /// </summary>
        /// <param name="song">The song to check.</param>
        /// <param name="songFile">The parsed file as a string array.</param>
        /// <param name="bytes">The parsed file as a byte array.</param>
        /// <param name="songEntries">All song entries already found.</param>
        /// <returns><c>true</c> if an error was found; otherwise <c>false</c>.</returns>
        public bool Run(SongEntry song, string[] songFile, byte[] bytes, List<SongEntry> songEntries)
        {
            if (Enabled)
            {
                bool result = onRun(song, songFile, bytes, songEntries);
                if (result) ErrorCounter++;
                return result;
            }
            else return false;
        }

        /// <summary>
        /// Internal method call to run the test.
        /// </summary>
        /// <param name="song">The song to check.</param>
        /// <param name="songFile">The parsed file as a string array.</param>
        /// <param name="bytes">The parsed file as a byte array.</param>
        /// <param name="songEntries">All song entries already found.</param>
        /// <returns><c>true</c> if an error was found; otherwise <c>false</c>.</returns>
        protected abstract bool onRun(SongEntry song, string[] songFile, byte[] bytes, List<SongEntry> songEntries);

        /// <summary>
        /// Perform post processing before print.
        /// </summary>
        protected abstract void postprocessing();

        /// <summary>
        /// Prints the results of the test.
        /// </summary>
        public virtual void Print()
        {
            if (Enabled)
            {
                postprocessing();
                foreach (string line in logOutput)
                    Console.WriteLine(line);
            }
        }
    }
}
