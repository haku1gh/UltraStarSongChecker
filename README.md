# UltraStarSongChecker
A command line tool to check all TXT song files for correctness.
The tool is written in C# (.NET 4.5) and requires the .NET Framework to be installed.

This tool currently contains 17 tests, were 12 are enabled by default.
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

TBD
