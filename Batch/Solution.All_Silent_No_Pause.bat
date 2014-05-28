@echo off

call Solution.Common.bat silent nopause
call Solution.Messaging.bat silent nopause
START cmd /c Solution.Services.Outings.bat silent nopause
START cmd /c Solution.Services.Ratings.bat silent nopause
START cmd /c Solution.Services.Users.bat silent nopause
START cmd /c Solution.Services.Venues.bat silent nopause
START cmd /c Solution.Services.Voting.bat silent nopause
START cmd /c Solution.Services.OutingsScheduler.bat silent nopause
START cmd /c Solution.Web.bat silent nopause