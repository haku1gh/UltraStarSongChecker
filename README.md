# UltraStarSongChecker
A command line tool to check all TXT song files for correctness.
The tool is written in C# (.NET 4.5) and requires the .NET Framework to be installed.

This tool currently contains 19 tests, were 12 are enabled by default.
It is working and tested in Windows and Linux.

# Help
The programm offers various options. The full list of options can be retrieved with

    UltraStarSongChecker --help

# Usage
## Examples Windows
The following line searches the UltraStar config file to find all song directories. All default tests are preformed.

    UltraStarSongChecker --config="<path>\config.ini"

Instead of searching the config file, it is also possible to directly address a songs folder.

    UltraStarSongChecker --dir="<path>\songs"

.. or even add multiple songs folders

    UltraStarSongChecker --dir="<path>\songs" --dir="<path>\songs2"

With the --enable or --disable options you can activate or deactivate the tests as you like.

    UltraStarSongChecker --config="<path>\config.ini" --enable-all

And everything can be combined of course.

    UltraStarSongChecker --config="<path>\config.ini" --dir="<path>\additional_songs" --enable-all --disable-missing-language

## Examples Linux
The above mentioned examples all work also in Linux. They just need to be slightly modified.

    $ mono UltraStarSongChecker.exe --help
    $ mono UltraStarSongChecker.exe --config='<path>/config.ini'
    $ mono UltraStarSongChecker.exe --dir='<path>/songs'
    $ mono UltraStarSongChecker.exe --dir='<path>/songs' --dir='<path>/songs2'
    $ mono UltraStarSongChecker.exe --config='<path>/config.ini' --enable-all
    $ mono UltraStarSongChecker.exe --config='<path>/config.ini' --dir='<path>/additional_songs' --enable-all --disable-missing-language

# Tests / Checks
The following table shows all avaiable tests.

| Name             | Default  | Description                                                                     |
| ---------------- |:--------:| ------------------------------------------------------------------------------- |
| invalidsong      | enabled  | Check for invalid song files (Artist, Title or MP3 missing)                     |
| duplicatefiles   | enabled  | Check for duplicate song files.                                                 |
| missing-audio    | enabled  | Check for files with an existing MP3 tag, but the audio file does not exists.   |
| missing-video    | enabled  | Check for files with an existing VIDEO tag, but the video file does not exists. |
| missing-cover    | enabled  | Check for files with an existing COVER tag, but the cover file does not exists. |
| missing-endmark  | enabled  | Check for files where the end mark 'E' at the end is missing.                   |
| newlinefirstnote | enabled  | Check for files where the first note is a newline '-'.                          |
| blines           | enabled  | Check for files containing one or more lines starting with 'B'.                 |
| emptylines       | enabled  | Check for files containing more than one empty line.                            |
| invalid-year     | enabled  | Check for files with an existing but invalid YEAR tag (not a positive integer). |
| invalid-bpm      | enabled  | Check for files with an existing but invalid BPM tag (not a positive double).   |
| invalid-gap      | enabled  | Check for files with an existing but invalid GAP tag (not a double).            |
| linetermination  | disabled | Check for files with mixed (Windows and Linux) line termination.                |
| lowercasetags    | disabled | Check for files containing tags in lower-case characters.                       |
| languageformat   | disabled | Check for files where LANGUAGE is not in PascalCase format.                     |
| no-language      | disabled | Check for files with a missing or empty LANGUAGE tag.                           |
| no-year          | disabled | Check for files with a missing or empty YEAR tag.                               |
| no-cover         | disabled | Check for files with a missing or empty COVER tag.                              |
| no-video         | disabled | Check for files with a missing or empty VIDEO tag.                              |
