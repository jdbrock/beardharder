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
        public String Description
        {
            get
            {
                if (!NamedByDate)
                    return String.Format("{0} - S{1:00}E{2:00}", ShowName, SeasonNumber, EpisodeNumber);

                return String.Format("{0} - {1}", ShowName, NamedDate);
            }
        }

        public String ShowName { get; private set; }
        public Int32 SeasonNumber { get; private set; }
        public Int32 EpisodeNumber { get; private set; }

        public Boolean NamedByDate { get; private set; }
        public String NamedDate { get; private set; }

        public String SabnzbdId { get; private set; }

        private TVEpisode() { }

        public static TVEpisode FromFileName(String inFileName, String inSabnzbdId)
        {
            Match match = null;

            if (inFileName.StartsWith("http"))
                return null;

            foreach (var pattern in RegexPatterns.EpisodeRegexes)
            {
                match = new Regex(pattern, RegexOptions.IgnoreCase).Match(inFileName);

                if (match.Success)
                    break;
            }

            if (match == null || !match.Success)
            {
                Console.WriteLine("FAILED: Failed to parse " + inFileName + ", skipping...");
                return null;
            }

            var episode = new TVEpisode
            {
                ShowName = match.Groups["series_name"].Value.Replace(".", " "),
                NamedByDate = match.Groups["air_year"].Success,
                SabnzbdId = inSabnzbdId
            };

            if (episode.NamedByDate)
            {
                try
                {
                    episode.SeasonNumber = Int32.Parse(match.Groups["air_year"].Value);
                    episode.NamedDate = String.Join(".", match.Groups["air_year"].Value, match.Groups["air_month"].Value, match.Groups["air_day"].Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTION: " + episode.Description + ", " + ex.ToString());
                    return null;
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(match.Groups["ep_num"].Value))
                {
                    Console.WriteLine("FAILED: This is a full season pack, we don't know how to handle these at present. Skipping " + episode.Description + "...");
                    return null;
                }

                try
                {
                    episode.SeasonNumber = Int32.Parse(match.Groups["season_num"].Value);
                    episode.EpisodeNumber = Int32.Parse(match.Groups["ep_num"].Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTION: " + episode.Description + ", " + ex.ToString());
                    return null;
                }
            }

            return episode;
        }

    }
}
