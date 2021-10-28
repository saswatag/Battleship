Battleship

Build on .Net 5

The test project BattleshipStateTrackerTests uses:
- XUnit
- FluentAssertions

Build:
---------
Used the IDE VS-2019 Preview for development. If using Visual studio, please checkout the main branch and build. 
It should build successfully. On successful build, execute all the tests.

Docker:
A docker file 'Dockerfile' is included.

# Docker build command to build the image till the final layer/target. Execute the following from src\BattleshipStateTracker
The docker build will fail if any of the tests fail.

> docker build -f BattleshipStateTracker/Dockerfile -t battleship .

# Create and run the container. No entry point here.
> docker run -it --rm --entrypoint "bash" battleship

# Docker build command to build the image till the intermediate layer/target 'testrunner'. Execute the following from src\BattleshipStateTracker

> docker build -f BattleshipStateTracker/Dockerfile --target testrunner -t battleshiptests .
> docker run -it --rm --entrypoint "bash" --name tests-1 battleshiptests

Once the container is up, execute the following to run all the tests
> dotnet test "BattleshipStateTrackerTests.csproj"

Tests:
The tests for the overall game are in BattleshipGameTests.cs
The other tests file have tests for the collaborators.

Code:
The latest updated code in in branch 'main'




