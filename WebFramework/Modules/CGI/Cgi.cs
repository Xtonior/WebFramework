using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebFramework.Server;

namespace WebFramework.Modules.CGI
{
    /*  План:
     *  1. Читаем конфигурацию и путь до скрипта/exe
     *  2. Запускаем с параметрами из запроса
     *  3. Получаем STDOUT и записываем ответ
     *  4. Отсылаем или обрабатываем ответ
     */
    public class Cgi
    {
        private CgiConf cgiConf;

        private List<string> scripts;

        public Cgi()
        {
            cgiConf = new CgiConf();
        }

        public async Task Exec(HttpRequest request)
        {
            scripts = await cgiConf.GetScripts();
            if (scripts == null || scripts.Count == 0) return;

            string scriptPath = request.Path.Substring(1);

            if (!scripts.Contains(scriptPath))
            {
                return;
            }

            Process proc = new Process
            {
                StartInfo =
                {
                    FileName = "Scripts/" + scriptPath,
                    Arguments = "",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                }
            };

            proc.Start();

            string output = await proc.StandardOutput.ReadToEndAsync();

            Console.WriteLine(output);
        }
    }
}
