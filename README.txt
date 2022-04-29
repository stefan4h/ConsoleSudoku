---- ConsoleSudoku ----

Building the .exe file:

1. Ensure the .NET SDK (>5.0.0) is installed
   If it is not installed it can be downloaded here: https://dotnet.microsoft.com/en-us/download/dotnet/5.0

2. Go to the root of the project folder (where the README.txt is located)

3. Run this command int the command line to build the executable
       dotnet publish -c Release -r win10-x64

4. The executable has now been built and can be found in the project folder under this path:
       bin\Release\net5.0\win10-x64\ConsoleSudoku.exe

---------------------------------------------------------------------------------------------------------------------
Info:
- The Option "Scoreboard" in the "Start Menu" will appear once a Sudoku is finished fast enough to receive a score
- A