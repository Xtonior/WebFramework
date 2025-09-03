using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebFramework.Modules.CGI
{
    struct ConfScript
    {
        public string Path { get; set; } = "";

        public ConfScript(string path)
        {
            Path = path;
        }
    }

    public class CgiConf
    {
        string config = "Config/cgi_config.json";

        public CgiConf()
        {
            if (!File.Exists(config))
            {
                _ = GenerateConfig();
            }
        }

        public async Task<List<string>> GetScripts()
        {
            List<string> scripts = new List<string>();

            var cf = await ParseConfig();
            if (cf == null) return scripts;

            for (int i = 0; i < cf.Count; i++)
            {
                scripts.Add(cf[i].Path);
            }

            return scripts;
        }

        private async Task<List<ConfScript>?> ParseConfig()
        {
            if (!File.Exists(config))
            {
                await GenerateConfig();
                return null;
            }

            List<ConfScript>? scripts;

            using var fs = new FileStream(config, FileMode.Open, FileAccess.Read);
            if (fs.Length == 0) return null;

            scripts = await JsonSerializer.DeserializeAsync<List<ConfScript>>(fs);

            return scripts;
        }

        private async Task GenerateConfig()
        {
            using (FileStream fs = new FileStream(config, FileMode.OpenOrCreate))
            {
                ConfScript script = new ConfScript("ctest.exe");

                List<ConfScript> scripts = new List<ConfScript>();
                scripts.Add(script);

                await JsonSerializer.SerializeAsync<List<ConfScript>>(fs, scripts);
                Console.WriteLine("Data has been saved to file");
            }
        }
    }
}
