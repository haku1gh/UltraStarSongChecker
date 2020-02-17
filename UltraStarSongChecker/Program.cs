using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UltraStarSongChecker.Tests;

namespace UltraStarSongChecker
{
    /// <summary>
    /// Prepresents the main program.
    /// </summary>
    class Program
    {
        // Variables
        static string configFileName = "";
        static List<string> directories = new List<string>();
        static Dictionary<string, Test> tests = new Dictionary<string, Test>();
        static bool statistics = true;

        /// <summary>
        /// MAIN - Entry Point.
        /// </summary>
        /// <param name="args">A list of all application arguments.</param>
        static void Main(string[] args)
        {
            // Create test objects
            addTests();

            // Parse arguments
            parseApplicationArguments(args);
            tests["invalidsong"].Enabled = true;    // Enable this test again, if it was accidentally disabled

            print("Ultrastar Song Checker 1.5");
            print("==========================");

            // Either no arguments are passed, or no directories been given. So nothing to do.
            if (args.Length == 0 || (configFileName == "" && directories.Count == 0))
            {
                print("Nothing to do. Try \"UltraStarSongChecker --help\", if you need help.");
                Environment.Exit(0);
            }

            // Retrieve list of directories to search
            IEnumerable<string> listOfDirectories = null;
            if (configFileName != "")
            {
                print("Analyzing Config File ...");
                List<string> configDirectories = getSongFoldersFromConfigFile(configFileName);
                listOfDirectories = configDirectories.Union(directories);   // without duplicates
                print("    Finished.");
                print("");
                // Exit program if no directories had been found
                if (directories.Count == 0 && configDirectories.Count == 0)
                {
                    print("No directories found in config file. Nothing to do.");
                    Environment.Exit(0);
                }
            }
            else
                listOfDirectories = directories;

            // Search for all text files and run tests
            print("Analyzing Song Directories ...");
            List<SongEntry> songEntries = analyzeSongDirectories(listOfDirectories);
            print("    Finished.");
            print("");

            // Print output of the tests
            foreach (Test t in tests.Values) t.Print();

            // Print statistics
            if (statistics) printStatistics(songEntries);
        }

        /// <summary>
        /// Parse all application arguments.
        /// </summary>
        /// <param name="args">An array of all arguments.</param>
        private static void parseApplicationArguments(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.ToLower() == "--help" || arg.ToLower() == "-h")
                {
                    showHelp();
                    Environment.Exit(0);
                    return; // Not really required. Application already ended.
                }

                if (arg.ToLower().StartsWith("--config="))
                {
                    FileInfo fi = new FileInfo(arg.Substring(9));
                    if (fi.Exists) configFileName = fi.FullName;
                }
                if (arg.ToLower().StartsWith("--dir="))
                {
                    DirectoryInfo di = new DirectoryInfo(arg.Substring(6));
                    if (di.Exists) directories.Add(di.FullName);
                }

                if (arg.ToLower().StartsWith("--disable-"))
                {
                    string testName = arg.Substring(10).ToLower();
                    if (testName == "all")
                    {
                        foreach (Test t in tests.Values) t.Enabled = false;
                    }
                    else if (testName == "statistics")
                    {
                        statistics = false;
                    }
                    else if (tests.ContainsKey(testName))
                        tests[testName].Enabled = false;
                }
                if (arg.ToLower().StartsWith("--enable-"))
                {
                    string testName = arg.Substring(9).ToLower();
                    if (testName == "all")
                    {
                        foreach (Test t in tests.Values) t.Enabled = true;
                    }
                    else if (testName == "statistics")
                    {
                        statistics = true;
                    }
                    else if (tests.ContainsKey(testName)) tests[testName].Enabled = true;
                }
            }
        }

        /// <summary>
        /// Add all test objects.
        /// </summary>
        private static void addTests()
        {
            // Create test objects
            tests.Add("invalidsong", new TestInvalidSong("invalidsong", true));
            tests.Add("duplicatefiles", new TestDuplicates("duplicatefiles", true));
            tests.Add("missing-audio", new TestMissingAudio("missing-audio", true));
            tests.Add("missing-video", new TestMissingVideo("missing-video", true));
            tests.Add("missing-cover", new TestMissingCover("missing-cover", true));
            tests.Add("missing-endmark", new TestMissingEndmark("missing-endmark", true));
            tests.Add("newlinefirstnote", new TestNewLineFirstNote("newlinefirstnote", true));
            tests.Add("blines", new TestBLines("blines", true));
            tests.Add("emptylines", new TestEmptyLines("emptylines", true));
            tests.Add("invalid-year", new TestInvalidYear("invalid-year", true));
            tests.Add("invalid-bpm", new TestInvalidBPM("invalid-bpm", true));
            tests.Add("invalid-gap", new TestInvalidGAP("invalid-gap", true));

            tests.Add("linetermination", new TestLineTermination("linetermination", false));
            tests.Add("lowercasetags", new TestLowerCaseTags("lowercasetags", false));
            tests.Add("languageformat", new TestLanguageFormat("languageformat", false));
            tests.Add("no-language", new TestNoLanguage("no-language", false));
            tests.Add("no-year", new TestNoYear("no-year", false));
            tests.Add("no-cover", new TestNoCover("no-cover", false));
            tests.Add("no-video", new TestNoVideo("no-video", false));

            tests.Add("wrong-tag-langauge", new WrongTagLangauge("wrong-tag-langauge", false));
        }

        /// <summary>
        /// Shows the help text for this program.
        /// </summary>
        private static void showHelp()
        {
            print("Usage: UltraStarSongChecker [options]");
            print("Options: [defaults in brackets after descriptions]");
            print("");
            print("Help options:");
            print("  --help                       print this message");
            print("");
            print("Standard options:");
            print("  --config=FILE                Search in config FILE for song dirs []");
            print("  --dir=DIR                    Search in additional song DIR (can be added multiple times) []");
            print("");
            print("Configuration options:");
            print("  --disable-statistics         disable ouput of statistics [no]");
            print("  --disable-all                disable all checks");
            print("  --enable-all                 enable all checks");
            print("");
            print("  --disable-duplicatefiles     do not check for duplicated files [no]");
            print("  --disable-missing-audio      do not check for mentioned, but missing audio files [no]");
            print("  --disable-missing-video      do not check for mentioned, but missing video files [no]");
            print("  --disable-missing-cover      do not check for mentioned, but missing cover files [no]");
            print("  --disable-missing-endmark    do not check for missing end marks [no]");
            print("  --disable-newlinefirstnote   do not check for newline characters as first note [no]");
            print("  --disable-blines             do not check for lines starting with 'B' [no]");
            print("  --disable-emptylines         do not check for multiple empty lines [no]");
            print("  --disable-invalid-year       do not check for invalid year [no]");
            print("  --disable-invalid-bpm        do not check for invalid BPM [no]");
            print("  --disable-invalid-gap        do not check for invalid GAP [no]");
            print("");
            print("  --enable-linetermination     do check for mixed (Windows and Linux) line termination [no]");
            print("  --enable-lowercasetags       do check for tags with lower-case characters [no]");
            print("  --enable-languageformat      do check for languages not in PascalCase format [no]");
            print("  --enable-no-language         do check for missing language tag [no]");
            print("  --enable-no-year             do check for missing year tag [no]");
            print("  --enable-no-cover            do check for not mentioned cover files [no]");
            print("  --enable-no-video            do check for not mentioned video files [no]");
            print("");
            print("  --enable-wrong-tag-langauge  do check for wrong tag LANGAUGE [no]");
            print("");
        }

        /// <summary>
        /// Prints text on the console.
        /// </summary>
        /// <param name="text">The text to print.</param>
        /// <param name="lineTermination">Indicator whether a new-line character shall be added.</param>
        private static void print(string text, bool lineTermination = true)
        {
            if (lineTermination)
                Console.WriteLine(text);
            else
                Console.Write(text);
        }

        /// <summary>
        /// Reads all lines from a text file.
        /// </summary>
        /// <param name="fileName">The file to read from.</param>
        /// <param name="encoding">The intial encoding to be used. This might change while reading. If you're unsure use <see cref="Encoding.Default"/>.</param>
        /// <returns>An array with all lines from the text file.</returns>
        private static string[] readAllLinesFromFile(string fileName, Encoding encoding)
        {
            // The following lines solve a lot of problems with different encoding, but not all!
            using (StreamReader reader = new StreamReader(fileName, encoding, true))
            {
                reader.Peek(); // DO NOT DELETE. You need this!
                encoding = reader.CurrentEncoding;
            }
            return File.ReadAllLines(fileName, encoding);
        }

        /// <summary>
        /// Gets all song folders from the provided config file.
        /// </summary>
        /// <param name="configFileName">The relative or absolute path of the config file.</param>
        /// <returns>
        /// A list of all found song directories.
        /// </returns>
        private static List<string> getSongFoldersFromConfigFile(string configFileName)
        {
            // Create return object
            List<string> songDirectories = new List<string>();
            // Check whether the config file exists
            FileInfo fi = new FileInfo(configFileName);
            if (!fi.Exists) return songDirectories;
            // First look for the songs folder where the config resides
            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName + Path.DirectorySeparatorChar + "songs");
            if (di.Exists) songDirectories.Add(di.FullName);
            // Second parse through the config file and add all other possible song locations
            string[] configFile = readAllLinesFromFile(configFileName, Encoding.UTF8);
            foreach (string line in configFile)
            {
                if (line.StartsWith("SongDir"))
                {
                    int index = line.IndexOf('=');
                    if (index == -1) continue;  // -1 means that "=" char hasn't been found
                    try
                    {
                        di = new DirectoryInfo(line.Substring(index + 1));
                        if (di.Exists) songDirectories.Add(di.FullName);
                    }
                    catch { }
                }
            }
            // Return
            return songDirectories;
        }

        /// <summary>
        /// Analyze all song directories.
        /// </summary>
        private static List<SongEntry> analyzeSongDirectories(IEnumerable<string> songDirectories)
        {
            // Create return object
            List<SongEntry> songEntries = new List<SongEntry>();
            // Get list of all text files
            foreach (string songDirectory in songDirectories)
            {
                print("    ... searching in '" + songDirectory + "' ...");
                IEnumerable<string> files = Directory.EnumerateFiles(songDirectory, "*.txt", SearchOption.AllDirectories);
                // Parse through all song files
                foreach (string file in files)
                {
                    // Read file (code does not look good, but is working :-)
                    string[] songFileA = null, songFileB = null, songFileC = null;
                    try
                    {
                        songFileA = readAllLinesFromFile(file, Encoding.Default);
                        songFileB = readAllLinesFromFile(file, Encoding.UTF8);
                        songFileC = readAllLinesFromFile(file, Encoding.GetEncoding(1252));
                    }
                    catch (PathTooLongException)
                    {
                        print("    => File found, exceeding the maximum path length in your operating system. Try shorten the path/file name.");
                        print("       " + file);
                        continue;
                    }
                    byte[] bytes = File.ReadAllBytes(file);
                    SongEntry songA = createSongEntry(file, songFileA);
                    SongEntry songB = createSongEntry(file, songFileB);
                    SongEntry songC = createSongEntry(file, songFileC);

                    // Choose best encoding (code does not look good, but is working :-)
                    string[] songFile;
                    SongEntry song;
                    int res = chooseSongEncoding(songA, songB, songC);
                    if (res == 0)
                    {
                        songFile = songFileA;
                        song = songA;
                    }
                    else if(res == 1)
                    {
                        songFile = songFileB;
                        song = songB;
                    }
                    else
                    {
                        songFile = songFileC;
                        song = songC;
                    }

                    // The first test is mandatory (check for invalid song). It doesn't make sense to do further tests when the whole file is corrupt.
                    if (tests["invalidsong"].Run(song, songFile, bytes, songEntries))
                    {
                        continue;
                    }
                    // Run all other tests
                    foreach (Test t in tests.Values)
                    {
                        if (t.GetType() == typeof(TestInvalidSong)) continue;
                        else t.Run(song, songFile, bytes, songEntries);
                    }

                    // Add song to entry list
                    songEntries.Add(song);
                }
            }
            // Return
            return songEntries;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="songA">Song entry with a particular encoding.</param>
        /// <param name="songB">Song entry with a different encoding.</param>
        /// <returns><c>0</c> to use songA; <c>1</c> to use songB; <c>2</c> to use songC.</returns>
        private static int chooseSongEncoding(SongEntry songA, SongEntry songB, SongEntry songC)
        {
            // Basic check
            if (songB.MP3 == "" && songC.MP3 == "") return 0;
            if (songA.MP3 == "" && songC.MP3 == "") return 1;
            if (songA.MP3 == "" && songB.MP3 == "") return 2;
            // Get path of the file
            string pathA = songA.FileName.Substring(0, songA.FileName.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            string pathB = songB.FileName.Substring(0, songB.FileName.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            string pathC = songC.FileName.Substring(0, songC.FileName.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            // Get full qualified file name
            string fullName_songA = pathA + songA.MP3;
            string fullName_songB = pathB + songB.MP3;
            string fullName_songC = pathC + songC.MP3;
            FileInfo fiA = new FileInfo(fullName_songA);
            FileInfo fiB = new FileInfo(fullName_songB);
            FileInfo fiC = new FileInfo(fullName_songC);
            if (fiA.Exists)
                return 0;
            else if (fiB.Exists)
                return 1;
            else if (fiC.Exists)
                return 2;
            else
                return 0;
        }

        /// <summary>
        /// Creates a new song entry.
        /// </summary>
        /// <param name="file">The full qualified file name of the song.</param>
        /// <param name="songFile">The parsed file.</param>
        /// <returns>A new song entry.</returns>
        private static SongEntry createSongEntry(string file, string[] songFile)
        {
            string artist = "", title = "", language = "", genre = "", creator = "", mp3 = "", video = "", cover = "";
            foreach (string line in songFile)
            {
                if (line.ToUpper().StartsWith("#ARTIST:")) artist = line.Substring(8).Trim();
                if (line.ToUpper().StartsWith("#TITLE:")) title = line.Substring(7).Trim();
                if (line.ToUpper().StartsWith("#LANGUAGE:")) language = line.Substring(10).Trim();
                if (line.ToUpper().StartsWith("#GENRE:")) genre = line.Substring(7).Trim();
                if (line.ToUpper().StartsWith("#CREATOR:")) creator = line.Substring(9).Trim();
                if (line.ToUpper().StartsWith("#MP3:")) mp3 = line.Substring(5).Trim();
                if (line.ToUpper().StartsWith("#VIDEO:")) video = line.Substring(7).Trim();
                if (line.ToUpper().StartsWith("#COVER:")) cover = line.Substring(7).Trim();
            }
            return new SongEntry(file, artist, title, language, genre, creator, mp3, video, cover);
        }

        /// <summary>
        /// Print the statistics.
        /// </summary>
        /// <param name="songEntries">A list of song entries.</param>
        private static void printStatistics(List<SongEntry> songEntries)
        {
            // Print count of song entries
            print("");
            print("Statistics:");
            print("    Found " + songEntries.Count + " song files.");

            // Collect information about extensions
            Dictionary<string, FileExtension> mp3Extensions = new Dictionary<string, FileExtension>();
            Dictionary<string, FileExtension> videoExtensions = new Dictionary<string, FileExtension>();
            Dictionary<string, FileExtension> coverExtensions = new Dictionary<string, FileExtension>();
            foreach (SongEntry songEntry in songEntries)
            {
                addExtension(songEntry.MP3, mp3Extensions);
                addExtension(songEntry.Video, videoExtensions);
                addExtension(songEntry.Cover, coverExtensions);
            }
            List<FileExtension> listMp3Extensions = mp3Extensions.Values.ToList();
            listMp3Extensions.Sort();
            List<FileExtension> listVideoExtensions = videoExtensions.Values.ToList();
            listVideoExtensions.Sort();
            List<FileExtension> listCoverExtensions = coverExtensions.Values.ToList();
            listCoverExtensions.Sort();

            // Print information about extensions
            print("    Audio File Types:");
            foreach (FileExtension fe in listMp3Extensions)
                print("        " + fe.Extension.PadLeft(5) + " ... " + fe.Count);
            print("    Video File Types:");
            foreach (FileExtension fe in listVideoExtensions)
                print("        " + fe.Extension.PadLeft(5) + " ... " + fe.Count);
            print("    Cover File Types:");
            foreach (FileExtension fe in listCoverExtensions)
                print("        " + fe.Extension.PadLeft(5) + " ... " + fe.Count);
        }

        /// <summary>
        /// Adds an extension to a dictionary of extensions.
        /// </summary>
        /// <param name="fileName">The file name from where the extension shall be extracted.</param>
        /// <param name="fileExtensions">The dictionary to be modified.</param>
        private static void addExtension(string fileName, Dictionary<string, FileExtension> fileExtensions)
        {
            // Get the extension
            string extension = "";
            try
            {
                extension = Path.GetExtension(fileName).ToLower();
            }
            catch { }
            // Add to dictionary
            if (fileExtensions.ContainsKey(extension))
                fileExtensions[extension].Count++;
            else
            {
                fileExtensions.Add(extension, new FileExtension(extension, 1));
            }
        }
    }

}
