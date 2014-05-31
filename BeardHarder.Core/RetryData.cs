using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardHarder.Core
{
    public class RetryData
    {
        public static RetryData Instance { get; private set; }

        public Dictionary<String, Int32> RetryCounter { get; set; }

        private RetryData()
        {
            RetryCounter = new Dictionary<String, Int32>();
        }

        public static void Load()
        {
            if (Instance != null)
                return;

            var databaseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BeardHarder");
            Directory.CreateDirectory(databaseDirectory);

            var databasePath = Path.Combine(databaseDirectory, "retry.json");

            if (File.Exists(databasePath))
            {
                using (var inputStream = new StreamReader(databasePath))
                {
                    var serializer = JsonSerializer.Create();
                    Instance = serializer.Deserialize<RetryData>(new JsonTextReader(inputStream));
                }
            }
            else
                Instance = new RetryData();
        }

        public static void Increment(String inKey)
        {
            var key = inKey.ToLower();
            var instance = Instance;

            if (instance.RetryCounter.ContainsKey(key))
                instance.RetryCounter[key] = instance.RetryCounter[key] + 1;
            else
                instance.RetryCounter[key] = 1;
        }

        public static Boolean ExceedsLimit(Int32 inLimit, String inKey)
        {
            var key = inKey.ToLower();
            var instance = Instance;

            if (!instance.RetryCounter.ContainsKey(key))
                return false;

            return instance.RetryCounter[key] > inLimit;
        }

        public static void Save()
        {
            var databaseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BeardHarder");
            Directory.CreateDirectory(databaseDirectory);

            var databasePath = Path.Combine(databaseDirectory, "retry.json");

            using (var outputStream = File.Create(databasePath))
            {
                var writer = new StreamWriter(outputStream);
                JsonSerializer.Create().Serialize(writer, Instance);

                writer.Flush();
                outputStream.Flush();
            }
        }
    }
}
