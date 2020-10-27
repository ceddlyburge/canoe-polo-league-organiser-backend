# CanoePoloLeagueOrganiser

[![Build status](https://ci.appveyor.com/api/projects/status/kicld3r0y4bpxa7g?svg=true)](https://ci.appveyor.com/project/ceddlyburge/canoe-polo-league-organiser-backend)

[![codecov](https://codecov.io/gh/ceddlyburge/canoe-polo-league-organiser-backend/branch/master/graph/badge.svg)](https://codecov.io/gh/ceddlyburge/canoe-polo-league-organiser-backend)

Designed to make it easier to organise UK Canoe Polo leagues. There are about 10 teams per league, and there are usually 5 days when the games are played. Each team will play in 4 of these 5 days.

## Organising tournament days

Given a list of matches, the program will optimising the game order, so that teams do not play back to back games if at all possible, and so that teams don't have to wait too long inbetween games.

It does this by optimising for the following ordered rules:

1. The maximum consecutive games a team plays should be minimised (eg one team playing 3 times in a row is less desirable than two teams playing twice in a row)
2. The total number of times team play consecutive matches should be minimised (eg the total number of times any team plays back to back games should be minimised)
3. The amount of games that teams don't play between their first and last games should be minimised (eg teams shouldn't play the first game and then have to wait until the last game)

Nuget package available at https://www.nuget.org/packages/CanoePoloLeagueOrganiser/
