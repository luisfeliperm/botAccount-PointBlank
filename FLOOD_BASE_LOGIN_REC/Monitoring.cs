using System;
using System.Threading.Tasks;
using System.Diagnostics;
using bot.modal;

namespace bot
{
    public static class Monitoring
    {
        public static int TestSlot = 1;
        public static async void updateRAM()
        {
            try
            {
                PerformanceCounter cpuCounter;
                cpuCounter = new PerformanceCounter("Process", "% Processor Time", "FLOOD_BASE_LOGIN_REC", true);


                while (true)
                {
                    double pct = cpuCounter.NextValue();
                    Console.Title = "Bot Account Flood - PB Private [Send: " + flooding.totalSend + "; Create: "+ flooding.totalCreate +"; Erros: " + flooding.totalErros + " || CPU: " + pct.ToString("n") + "% RAM: " + (GC.GetTotalMemory(true) / 1024) + " KB]";
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


    }
}