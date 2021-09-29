using System;
using System.IO;
using System.Timers;

namespace VFCreateOldFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] config = GetConfig();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "VFCreateOldFile";


            //Делаем таймер для авто-обновления
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = Convert.ToInt32(config[2]) * 1000;
            aTimer.Enabled = true;

            Console.ReadKey();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string[] config = GetConfig();
            var pathNew = config[0];
            var pathOld = config[1];

            // Проверяем есть ли фпайл
            if (File.Exists(pathNew))
            {
                try
                {
                    File.Copy(pathNew, pathOld, true);
                    Console.WriteLine(DateTime.Now + " Скипт отработал");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static string[] GetConfig()
        {
            string[] configTmp = new string[4];
            using (StreamReader sr = new StreamReader("config.ini", System.Text.Encoding.Default))
            {
                int i = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    configTmp[i] = line;
                    i++;
                }
            }

            string[] config = new string[4];

            String[] tmp = configTmp[1].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            String[] tmp1 = configTmp[2].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            String[] tmp2 = configTmp[3].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

            config[0] = tmp[1]; // pathNew
            config[1] = tmp1[1]; // pathOld
            config[2] = tmp2[1]; // timeUpdate

            return config;
        }
    }
}
