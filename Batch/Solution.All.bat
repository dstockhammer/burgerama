@echo off

call Solution.Common.bat
call Solution.Messaging.bat
START cmd /c Solution.Services.Outings.bat
START cmd /c Solution.Services.Ratings.bat
START cmd /c Solution.Services.Users.bat
START cmd /c Solution.Services.Venues.bat
START cmd /c Solution.Services.Voting.bat
START cmd /c Solution.Services.OutingsScheduler.bat
START cmd /c Solution.Web.bat