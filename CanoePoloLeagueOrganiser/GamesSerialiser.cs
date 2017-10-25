using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanoePoloLeagueOrganiser
{
    public class GamesSerialiser
    {
        public string Serialise(IEnumerable<Game> games)
        {
            return JsonConvert.SerializeObject(
                games.Select(
                    g => new MutableGame
                    {
                        homeTeam = g.HomeTeam.Name,
                        awayTeam = g.AwayTeam.Name,
                        homeTeamPlayingConsecutively = g.HomeTeamPlayingConsecutively,
                        awayTeamPlayingConsecutively = g.AwayTeamPlayingConsecutively
                    }));
        }

        public List<Game> DeSerialise(string json)
        {
            return JsonConvert
                .DeserializeObject<List<MutableGame>>(json)
                .Select(
                    g => new Game(
                        homeTeam: new Team(g.homeTeam),
                        awayTeam: new Team(g.awayTeam),
                        homeTeamPlayingConsecutively: g.homeTeamPlayingConsecutively,
                        awayTeamPlayingConsecutively: g.awayTeamPlayingConsecutively)
                     )
                 .ToList();
        }
    }
}
