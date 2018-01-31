using static System.Diagnostics.Contracts.Contract;

namespace CanoePoloLeagueOrganiser
{
    public class GameOrderCalculation : GameOrderPossiblyNullCalculation
    {
        public GameOrderCalculation(GameOrderPossiblyNullCalculation gameOrderPossiblyNullCalculation) : 
            base(
                gameOrderPossiblyNullCalculation.OptimisedGameOrder,
                gameOrderPossiblyNullCalculation.PragmatisationLevel,
                gameOrderPossiblyNullCalculation.OptimisationMessage)
        {
            Requires(gameOrderPossiblyNullCalculation.OptimisedGameOrder != null);
        }
    }
}