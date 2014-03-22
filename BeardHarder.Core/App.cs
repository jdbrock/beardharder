using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardHarder.Core
{
    public class App
    {
        private const String SABNZBD_URI = "http://192.168.1.50:8080";
        private const String SICKBEARD_URI = "http://192.168.1.80:8081";

        private const String SABNZBD_APIKEY = "7f4ad651733f37bc1887a0df332d73ae";
        private const String SICKBEARD_APIKEY = "f84b98045c153a9597aee7056587c837";

        private const Int32 SABNZBD_HISTORY_LIMIT = 50;

        public void Go()
        {
            var sabnzbdClient = new RestClient(SABNZBD_URI);
            sabnzbdClient.AddDefaultParameter("apikey", SABNZBD_APIKEY);
            sabnzbdClient.AddDefaultParameter("output", "json");

            var sickbeardClient = new RestClient(SICKBEARD_URI);

            // Get SABnzbd history.
            var historyRequest = new RestRequest("api", Method.GET);
            historyRequest.AddParameter("mode", "history");
            historyRequest.AddParameter("start", 0);
            historyRequest.AddParameter("limit", SABNZBD_HISTORY_LIMIT);

            var history = sabnzbdClient.Execute<HistoryRequest>(historyRequest);

            // Filter for failed items.
            var failedRaw = history.Data.history.slots
                .Where(X => X.status == "Failed" && X.category == "tv");

            var failed = new List<TVEpisode>();

            // Parse failed items.
            foreach (var slot in failedRaw)
            {
                var episode = TVEpisode.FromFileName(slot.name);

                if (episode == null)
                    continue;

                failed.Add(episode);
            }

            // Get SickBeard shows.
            var showsRequest = new RestRequest("api/" + SICKBEARD_APIKEY + "/?cmd=shows");
            var showsRaw = sickbeardClient.Execute<ShowsRequest>(showsRequest);
            var shows = showsRaw.Data.data;

            // Find failed items in SickBeard.
            foreach (var failedItem in failed)
            {
                // Try to match show.
                var showId = shows.Where(X => X.Value.show_name.Equals(failedItem.ShowName, StringComparison.OrdinalIgnoreCase)).Select(X => X.Key).FirstOrDefault();

                if (showId == null)
                {
                    Console.WriteLine("Couldn't find show in SickBeard: " + failedItem.ShowName + ", skipping...");
                    continue;
                }



            }
        }
    }
}
