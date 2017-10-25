using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public class GameOrderCalculation : GameOrderPossiblyNullCalculation
    {
        public GameOrderCalculation(
            GameOrderCandidate optimisedGameOrder, 
            PragmatisationLevel pragmatisationLevel, 
            string optimisationMessage) : 
            base(
                optimisedGameOrder, 
                pragmatisationLevel, 
                optimisationMessage)
        {
            Requires(optimisedGameOrder != null);
        }
    }
}