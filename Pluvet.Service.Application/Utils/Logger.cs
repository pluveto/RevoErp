using System;
using System.Text;
using System.Threading;

namespace Pluvet.Service.Application.Utils
{
    public class Logger
    {
        public static string LogFileName { get; set; } = string.Empty;
        private static bool Lock = false;
        private static void Print(string type, string text, bool writeFile = true)
        {
            var appDir = PathExt.GetAppDir("", false);
            var appDirUnix = PathExt.GetAppDir("", true);
            var content = string.Format("[{0}] {1}: {2}\n", DateTime.Now.ToString("HH:mm:ss.fff"), type,
                text
                .Replace("\n", "\n" + Indent(17 + type.Length, " "))
                .Replace(appDir, "<AppDir>")
                .Replace(appDirUnix, "<AppDir>"));
            Console.Write(content);
            if (!writeFile) return;
            var logfile = LogFileName == string.Empty ? appDir + "/app.log" : LogFileName;
            while (Logger.Lock)
            {
                Thread.Sleep(10);
            }
            if (!System.IO.File.Exists(logfile))
            {
                System.IO.File.Create(logfile);
            }
            Logger.Lock = true;
            System.IO.File.AppendAllText(logfile, content);
            Logger.Lock = false;
        }

        public static void Debug(string text)
        {
            var type = "Debug";
            Print(type, text);
        }
        public static void Info(string text)
        {
            var type = "Info";
            Print(type, text);
        }
        public static void BoxInfo(string text)
        {
            var type = "Info";
            var appDir = PathExt.GetAppDir("", false);
            var appDirUnix = PathExt.GetAppDir("", true);
            Console.WriteLine("[{0}] {1}: {2}", DateTime.Now.ToString("HH:mm:ss.fff"), type,
                "\n" + Indent(17 + type.Length, "-") + "----------------------------------------------\n" +
                text
                .Replace("\n", "\n" + Indent(17 + type.Length, " "))
                .Replace(appDir, "<AppDir>")
                .Replace(appDirUnix, "<AppDir>") +
                "\n" + Indent(17 + type.Length, "-") + "----------------------------------------------\n");
        }
        public static void Error(string text)
        {
            var type = "Error";
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!");
            Print(type, text);
        }
        public static string Indent(int count, string str = "*")
        {
            var sb = new StringBuilder();
            while (count-- > 0)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }

    }

}
