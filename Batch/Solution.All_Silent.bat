@echo off

call Solution.Common.bat silent
call Solution.Messaging.bat silent
START cmd /c Solution.Services.Outings.bat silent
START cmd /c Solution.Services.Ratings.bat silent
START cmd /c Solution.Services.Users.bat silent
START cmd /c Solution.Services.Venues.bat silent
START cmd /c Solution.Services.Voting.bat silent
START cmd /c Solution.Services.OutingsScheduler.bat silent
START cmd /c Solution.Web.bat silent