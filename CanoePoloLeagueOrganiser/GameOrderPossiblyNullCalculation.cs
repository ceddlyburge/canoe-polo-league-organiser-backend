namespace CanoePoloLeagueOrganiser
{
    public class GameOrderPossiblyNullCalculation
    {
        public GameOrderCandidate OptimisedGameOrder { get; }
        public PragmatisationLevel PragmatisationLevel { get; }
        public string OptimisationMessage { get; }

        public GameOrderPossiblyNullCalculation(
            GameOrderCandidate optimisedGameOrder, 
            PragmatisationLevel pragmatisationLevel, 
            string optimisationMessage)
        {
            OptimisationMessage = optimisationMessage;
            PragmatisationLevel = pragmatisationLevel;
            OptimisedGameOrder = optimisedGameOrder;
        }
    }
}