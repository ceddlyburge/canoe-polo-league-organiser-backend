using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace CanoePoloLeagueOrganiser
{
    public class GamesSerialiser
    {
        public string Serialise(IEnumerable<AnalysedGame> games) =>
            JsonConvert
                .SerializeObject(
                    CreateMutableGames(games), ConvertPascalCaseToCamelCase());

        public List<AnalysedGame> DeSerialise(string json) =>
            JsonConvert
                .DeserializeObject<List<MutableGame>>(json, ConvertPascalCaseToCamelCase())
                .Select(g => CreateGame(g))
                .ToList();

        static IEnumerable<MutableGame> CreateMutableGames(IEnumerable<AnalysedGame> games) =>
            games.Select(g => CreateMutableGame(g));

        static MutableGame CreateMutableGame(AnalysedGame g) =>
            new MutableGame
            {
                HomeTeam = g.HomeTeam.Name,
                AwayTeam = g.AwayTeam.Name,
                HomeTeamPlayingConsecutively = g.HomeTeamPlayingConsecutively,
                AwayTeamPlayingConsecutively = g.AwayTeamPlayingConsecutively
            };

        static AnalysedGame CreateGame(MutableGame g) =>
            new AnalysedGame(
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
