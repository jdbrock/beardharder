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
        private const String SABNZBD_URI = "http://192.168.1.50:8080/api";
        private const String SICKBEARD_URI = "http://192.168.1.80:8081/api/" + SICKBEARD_APIKEY + "/";

        private const String SABNZBD_APIKEY = "7f4ad651733f37bc1887a0df332d73ae";
        private const String SICKBEARD_APIKEY = "f84b98045c153a9597aee7056587c837";

        private const Int32 SABNZBD_HISTORY_LIMIT = 1000;

        public void RetryFailedEpisodes()
        {
            var sabnzbdClient = new RestClient(SABNZBD_URI);
            sabnzbdClient.AddDefaultParameter("apikey", SABNZBD_APIKEY);
            sabnzbdClient.AddDefaultParameter("output", "json");

            var sickbeardClient = new RestClient(SICKBEARD_URI);

            // Get SABnzbd history.
            var historyRequest = new RestRequest();
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
                var episode = TVEpisode.FromFileName(slot.name, slot.nzo_id);

                if (episode == null)
                    continue;

                failed.Add(episode);
            }

            // Get SickBeard shows.
            var showsRequest = new RestRequest();
            showsRequest.AddParameter("cmd", "shows");

            var showsRaw = sickbeardClient.Execute<ShowsRequest>(showsRequest);
            var shows = showsRaw.Data.data;

            // Find failed items in SickBeard.
            foreach (var failedItem in failed)
            {
                if (!failedItem.NamedByDate && (failedItem.SeasonNumber == 0 || failedItem.EpisodeNumber == 0))
                {
                    Console.WriteLine("FAILED: Season and episode number must be non-zero for " + failedItem.Description + ", skipping...");
                    continue;
                }

                if (String.IsNullOrWhiteSpace(failedItem.SabnzbdId) || !failedItem.SabnzbdId.StartsWith("SABnzbd"))
                {
                    Console.WriteLine("FAILED: Unrecognized SABnzbd ID for " + failedItem.Description + ", skipping...");
                    continue;
                }

                // Try to match show.
                var showId = shows.Where(X => Matchers.MatchTVShow(X.Value.show_name, failedItem.ShowName)).Select(X => X.Key).FirstOrDefault();

                if (showId == null)
                {
                    Console.WriteLine("FAILED: Couldn't find show in SickBeard for " + failedItem.Description + ", skipping...");
                    continue;
                }

                Int32 seasonNumber = 0;
                Int32 episodeNumber = 0;

                if (failedItem.NamedByDate)
                {
                    var episodesRequest = new RestRequest();
                    episodesRequest.AddParameter("cmd", "show.seasons");
                    episodesRequest.AddParameter("tvdbid", showId);
                    //episodesRequest.AddParameter("season", failedItem.SeasonNumber);

                    var episodes = sickbeardClient.Execute<EpisodesRequest>(episodesRequest);

                    if (!episodes.Data.result.Equals("success", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("FAILED: Encountered non-success code getting episodes for " + failedItem.Description + ", skipping...");
                        continue;
                    }

                    AirByDateEpisode episode = null;

                    foreach (var season in episodes.Data.data)
                    {
                        foreach (var ep in season.Value)
                            if (ep.Value.airdate == failedItem.NamedDate.Replace(".", "-"))
                            {
                                episode = ep.Value;
                                seasonNumber = Int32.Parse(season.Key);
                                episodeNumber = Int32.Parse(ep.Key);
                                break;
                            }

                        if (episode != null)
                            break;
                    }

                    if (episode == null)
                    {
                        Console.WriteLine("FAILED: Couldn't find episode number for " + failedItem.Description + ", skipping...");
                        continue;
                    }
                }
                else
                {
                    seasonNumber = failedItem.SeasonNumber;
                    episodeNumber = failedItem.EpisodeNumber;
                }

                var getEpisodeRequest = new RestRequest();
                getEpisodeRequest.AddParameter("cmd", "episode");
                getEpisodeRequest.AddParameter("tvdbid", showId);
                getEpisodeRequest.AddParameter("season", seasonNumber);
                getEpisodeRequest.AddParameter("episode", episodeNumber);

                var getEpisodeResult = sickbeardClient.Execute<EpisodeRequest>(getEpisodeRequest);

                if (!getEpisodeResult.Data.result.Equals("success", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("FAILED: Encountered non-success code getting episode status for " + failedItem.Description + ", skipping...");
                    continue;
                }

                if (String.IsNullOrWhiteSpace(getEpisodeResult.Data.data.location))
                {
                    var setEpisodeStatusRequest = new RestRequest();
                    setEpisodeStatusRequest.AddParameter("cmd", "episode.setstatus");
                    setEpisodeStatusRequest.AddParameter("tvdbid", showId);
                    setEpisodeStatusRequest.AddParameter("season", seasonNumber);
                    setEpisodeStatusRequest.AddParameter("episode", episodeNumber);
                    setEpisodeStatusRequest.AddParameter("status", "wanted");

                    var setStatusResult = sickbeardClient.Execute<EpisodeSetStatusRequest>(setEpisodeStatusRequest).Data;

                    if (!setStatusResult.result.Equals("success", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("FAILED: Encountered non-success code setting episode status for " + failedItem.Description + ", skipping...");
                        continue;
                    }

                    Console.WriteLine("SUCCESS: Retried " + failedItem.Description);
                }
                else
                {
                    Console.WriteLine("We already have this episode downloaded, removing from SAB history. Details: " + failedItem.Description);
                }

                var deleteHistoryRequest = new RestRequest();
                deleteHistoryRequest.AddParameter("mode", "history");
                deleteHistoryRequest.AddParameter("name", "delete");
                deleteHistoryRequest.AddParameter("value", failedItem.SabnzbdId);
                deleteHistoryRequest.AddParameter("failed_only", 1);
                deleteHistoryRequest.AddParameter("del_files", 0);

                sabnzbdClient.Execute(deleteHistoryRequest);

                Console.WriteLine("SUCCESS: Removed old SAB history entry for " + failedItem.Description);
            }
        }

        public void RestartSabnzbd()
        {
            var sabnzbdClient = new RestClient(SABNZBD_URI);
            sabnzbdClient.AddDefaultParameter("apikey", SABNZBD_APIKEY);
            sabnzbdClient.AddDefaultParameter("output", "json");

            var restartRequest = new RestRequest();
            restartRequest.AddParameter("cmd", "restart");

            sabnzbdClient.ExecuteAsync(restartRequest, X => { });
        }
    }
}
