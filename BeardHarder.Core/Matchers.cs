using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeardHarder.Core
{
    public static class Matchers
    {
        public static Boolean MatchTVShow(String inEstablishedShow, String inGivenShow)
        {
            var givenShow = inGivenShow;
            var establishedShow = inEstablishedShow;

            if (establishedShow == null || givenShow == null)
                return false;

            foreach (var replacement in KnownMappings.TVShowNames)
                givenShow = givenShow.Replace(replacement.Key, replacement.Value);

            establishedShow = StripCharacters(establishedShow).ToLower();
            givenShow = StripCharacters(givenShow).ToLower();

            return establishedShow.Equals(givenShow);
        }

        private static String StripCharacters(String inString)
        {
            return Regex.Replace(inString, "[^a-zA-Z0-9]", "", RegexOptions.Compiled);
        }
    }
}
