using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace CanoePoloLeagueOrganiser
{
    public class GamesSerialiser
    {
        public string Serialise(IEnumerable<Game> games) =>
            JsonConvert
                .SerializeObject(
                    CreateMutableGames(games));

        public List<Game> DeSerialise(string json) =>
            JsonConvert
                .DeserializeObject<List<MutableGame>>(json, ConvertPascalCaseToCamelCase())
                .Select(g => CreateGame(g))
                 .ToList();

        static IEnumerable<MutableGame> CreateMutableGames(IEnumerable<Game> games) =>
            games.Select(g => CreateMutableGame(g));

        static MutableGame CreateMutableGame(Game g) =>
            new MutableGame
            {
                HomeTeam = g.HomeTeam.Name,
                AwayTeam = g.AwayTeam.Name,
                HomeTeamPlayingConsecutively = g.HomeTeamPlayingConsecutively,
                AwayTeamPlayingConsecutively = g.AwayTeamPlayingConsecutively
            };

        static Game CreateGame(MutableGame g) =>
            new Game(
                homeTeam: new Team(g.HomeTeam),
                awayTeam: new Team(g.AwayTeam),
                homeTeamPlayingConsecutively: g.HomeTeamPlayingConsecutively,
                awayTeamPlayingConsecutively: g.AwayTeamPlayingConsecutively);

        JsonSerializerSettings ConvertPascalCaseToCamelCase() =>
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

    }
}
