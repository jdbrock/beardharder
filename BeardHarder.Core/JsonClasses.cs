using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardHarder.Core
{
    public class StageLog
    {
        public string name { get; set; }
        public List<string> actions { get; set; }
    }

    public class Slot
    {
        //public string action_line { get; set; }
        //public string show_details { get; set; }
        //public string script_log { get; set; }
        //public object meta { get; set; }
        //public string fail_message { get; set; }
        //public bool loaded { get; set; }
        //public Int64 id { get; set; }
        //public string size { get; set; }
        public string category { get; set; }
        //public string pp { get; set; }
        //public Int64 retry { get; set; }
        //public Int64 completeness { get; set; }
        //public string script { get; set; }
        //public string nzb_name { get; set; }
        //public Int64 download_time { get; set; }
        //public string storage { get; set; }
        public string status { get; set; }
        //public string script_line { get; set; }
        //public Int64 completed { get; set; }
        public string nzo_id { get; set; }
        //public object downloaded { get; set; }
        //public string report { get; set; }
        //public string path { get; set; }
        //public Int64 postproc_time { get; set; }
        public string name { get; set; }
        //public string url { get; set; }
        //public object bytes { get; set; }
        //public string url_info { get; set; }
        //public List<StageLog> stage_log { get; set; }
    }

    public class History
    {
        //public string uniconfig { get; set; }
        //public string cache_size { get; set; }
        //public string active_lang { get; set; }
        //public bool paused { get; set; }
        //public string session { get; set; }
        //public bool restart_req { get; set; }
        //public bool power_options { get; set; }
        public List<Slot> slots { get; set; }
        //public string speed { get; set; }
        //public string helpuri { get; set; }
        //public string size { get; set; }
        //public string uptime { get; set; }
        //public string total_size { get; set; }
        //public string month_size { get; set; }
        //public string my_home { get; set; }
        //public bool have_quota { get; set; }
        //public string week_size { get; set; }
        //public string version { get; set; }
        //public string new_rel_url { get; set; }
        //public string my_lcldata { get; set; }
        //public string color_scheme { get; set; }
        //public string diskspacetotal1 { get; set; }
        //public bool nt { get; set; }
        //public string status { get; set; }
        //public string last_warning { get; set; }
        //public string have_warnings { get; set; }
        //public string cache_art { get; set; }
        //public string sizeleft { get; set; }
        //public object finishaction { get; set; }
        //public bool paused_all { get; set; }
        //public string quota { get; set; }
        //public string newzbin_url { get; set; }
        //public string new_release { get; set; }
        //public string pause_Int64 { get; set; }
        //public string mbleft { get; set; }
        //public string diskspace1 { get; set; }
        //public bool pp_pause_event { get; set; }
        //public bool darwin { get; set; }
        //public string timeleft { get; set; }
        //public string mb { get; set; }
        //public Int64 noofslots { get; set; }
        //public string day_size { get; set; }
        //public string eta { get; set; }
        //public string diskspacetotal2 { get; set; }
        //public string nzb_quota { get; set; }
        //public string loadavg { get; set; }
        //public string cache_max { get; set; }
        //public string kbpersec { get; set; }
        //public string speedlimit { get; set; }
        //public string webdir { get; set; }
        //public string left_quota { get; set; }
        //public string diskspace2 { get; set; }
    }

    public class HistoryRequest
    {
        public History history { get; set; }
    }

    ////////////////////////////////////////////////////////////////////////////////

    public class ShowsRequest
    {
        public Dictionary<String, Show> data { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }

    public class Show
    {
        public Int64 air_by_date { get; set; }
        public Cache cache { get; set; }
        public string language { get; set; }
        public string network { get; set; }
        public string next_ep_airdate { get; set; }
        public Int64 paused { get; set; }
        public string quality { get; set; }
        public string show_name { get; set; }
        public string status { get; set; }
        public string tvrage_id { get; set; }
        public string tvrage_name { get; set; }
    }

    public class Cache
    {
        public Int64 banner { get; set; }
        public Int64 poster { get; set; }
    }

    ///////////////////////////////////////////////////////////////////////////////

    public class EpisodeSetStatusRequest
    {
        public List<EpisodeSetStatusData> data { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }

    public class EpisodeSetStatusData
    {
        public Int64 episode { get; set; }
        public string message { get; set; }
        public string result { get; set; }
        public Int64 season { get; set; }
        public string status { get; set; }
    }

    //////////////////////////////////////////////////////////////////////////////

    public class EpisodesRequest
    {
        public Dictionary<String, AirByDateEpisode> data { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }

    public class AirByDateEpisode
    {
        public string airdate { get; set; }
        public string name { get; set; }
        public string quality { get; set; }
        public string status { get; set; }
    }

    /////////////////////////////////////////////////////////////////////////////

    public class Episode
    {
        public string airdate { get; set; }
        public string description { get; set; }
        public Int64 file_size { get; set; }
        public string file_size_human { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public string quality { get; set; }
        public string release_name { get; set; }
        public string status { get; set; }
    }

    public class EpisodeRequest
    {
        public Episode data { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
