using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CanoePoloLeagueOrganiser
{
    public class Team
    {
        public string Name { get; }

        public Team(string name) =>
            Name = name;

        public override bool Equals(object  obj)
        {
            if (obj is Team)
                return (obj as Team).Name.Equals(Name, StringComparison.CurrentCultureIgnoreCase);

            return false;
        }

        public override int GetHashCode() =>
            Name.GetHashCode();
    }
}
