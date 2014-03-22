using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeardHarder.Core
{
    public class TVEpisode
    {
        public String ShowName { get; private set; }
        public Int32 SeasonNumber { get; private set; }
        public Int32 EpisodeNumber { get; private set; }

        public Boolean NamedByDate { get; private set; }
        public String NamedDate { get; private set; }

        private TVEpisode() { }

        public static TVEpisode FromFileName(String inFileName)
        {
            Match m = null;

            foreach (var pattern in RegexPatterns.EpisodeRegex)
            {
                m = new Regex(pattern, RegexOptions.IgnoreCase).Match(inFileName);

                if (m.Success)
                    break;
            }

            if (m == null || !m.Success)
            {
                Console.WriteLine("Failed to parse " + inFileName + ", skipping...");
                return null;
            }

            var e = new TVEpisode
            {
                ShowName = m.Groups["series_name"].Value.Replace(".", " "),
                NamedByDate = m.Groups["air_year"].Success
            };

            if (e.NamedByDate)
                e.NamedDate = String.Join(".", m.Groups["air_year"].Value, m.Groups["air_month"].Value, m.Groups["air_day"].Value);
            else
            {
                e.SeasonNumber = Int32.Parse(m.Groups["season_num"].Value);
                e.EpisodeNumber = Int32.Parse(m.Groups["ep_num"].Value);
            }

            var x = e;

            return e;
        }
    }
}
