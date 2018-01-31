using System;

namespace CanoePoloLeagueOrganiser
{
    public class PlayListException : Exception
    {
        public PlayListException()
        {
        }

        public PlayListException(string message) : base(message)
        {
        }

        public PlayListException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}