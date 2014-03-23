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
            Match m = null;

            foreach (var pattern in RegexPatterns.EpisodeRegex)
            {
                m = new Regex(pattern, RegexOptions.IgnoreCase).Match(inFileName);

                if (m.Success)
                    break;
            }

            if (m == null || !m.Success)
            {
                Console.WriteLine("FAILED: Failed to parse " + inFileName + ", skipping...");
                return null;
            }

            var e = new TVEpisode
            {
                ShowName = m.Groups["series_name"].Value.Replace(".", " "),
                NamedByDate = m.Groups["air_year"].Success,
                SabnzbdId = inSabnzbdId
            };

            if (e.NamedByDate)
            {
                try
                {
                    e.SeasonNumber = Int32.Parse(m.Groups["air_year"].Value);
                    e.NamedDate = String.Join(".", m.Groups["air_year"].Value, m.Groups["air_month"].Value, m.Groups["air_day"].Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTION: " + e.Description + ", " + ex.ToString());
                    return null;
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(m.Groups["ep_num"].Value))
                {
                    Console.WriteLine("FAILED: This is a full season pack, we don't know how to handle these at present. Skipping " + e.Description + "...");
                    return null;
                }

                try
                {
                    e.SeasonNumber = Int32.Parse(m.Groups["season_num"].Value);
                    e.EpisodeNumber = Int32.Parse(m.Groups["ep_num"].Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTION: " + e.Description + ", " + ex.ToString());
                    return null;
                }
            }

            var x = e;

            return e;
        }

    }
}
