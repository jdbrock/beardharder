using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardHarder.Core
{
    public static class KnownMappings
    {
        public static readonly Dictionary<String, String> TVShowNames = new Dictionary<String, String>
        {
            { "Never Mind The Buzzcocks UK", "Never Mind The Buzzcocks" },
            { "Revolution 2012", "Revolution" },
            { "Man v Food", "Man v. Food" },
            { "Man v Food Nation", "Man v. Food Nation" },
            { "Aqua Teen Hunger Force", "Aqua TV Show Show" },
            { "Touch", "Touch (2012)" },
            { "Americas Next Top Model", "America's Next Top Model" },
            { "Star Trek TOS", "Star Trek" },
            { "Comedy Bang Bang", "Comedy Bang! Bang!" },
            { "The Tomorrow People", "The Tomorrow People (US)" },
            { "NTSF SD SUV", "NTSF:SD:SUV::" },
            { "Law and Order SVU", "Law & Order: Special Victims Unit" },
            { "Marvels Agents of S H I E L D", "Marvel's Agents of S.H.I.E.L.D." },
            { "Charlie Brookers Weekly Wipe", "Charlie Brooker's Weekly Wipe" }
        };
    }
}
