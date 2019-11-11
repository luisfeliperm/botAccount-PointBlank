using System;
using System.IO;

namespace bot.Logs
{
    public class SaveLog
    {
        private static object Sync = new object();

        public static void fatal(string txt)
        {
            writeLog(txt, "logs/fatal/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }
        public static void error(string txt)
        {
            writeLog(txt, "logs/error/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }
        public static void warning(string txt)
        {
            writeLog(txt, "logs/warning/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }
        public static void info(string txt)
        {
            writeLog(txt, "logs/info/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }

        
        private static void writeLog(string txt, string file)
        {
            try
            {
                lock (Sync)
                {
                    txt = "[" + DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "] " + txt;
                    using (StreamWriter streamWriter = new StreamWriter(file, true))
                    {
                        streamWriter.WriteLine(txt);
                        streamWriter.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Printf.b_danger(ex.ToString());
            }
        }
        private static void requireFolder(string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch (Exception ex)
            {
                Printf.b_danger(ex.ToString());
            }
        }
        public static void checkDirectorys()
        {
            requireFolder("logs/fatal");
            requireFolder("logs/error");
            requireFolder("logs/warning");
            requireFolder("logs/info");
        }
    }
}
