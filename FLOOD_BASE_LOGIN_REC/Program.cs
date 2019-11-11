using bot.config;
using bot.Logs;
using bot.network.auth;
using System;
using System.Threading;

namespace bot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Bot Account - PointBlank Private";
            Printf.blue("[Licença de uso]", false);
            Printf.blue("[+] Esta é uma versão PUBLICA!!!", false);
            Printf.blue("[+] https://github.com/luisfeliperm", false);
            Printf.info("\n\n\n Iniciando bot...", false);
            Config.Load();
            //Thread.Sleep(5000);
            Console.Clear();



            Printf.danger("=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=", false);
            Printf.danger("\tBOT FOR POINT BLANK PRIVATE v.1.0.0 ", false);
            Printf.danger("\t\t{Flooding Accounts}", false);
            Printf.danger("\t\t-Creator Uchihaker-", false);
            Printf.danger("=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=", false);

            Printf.info("[Informações carregadas]");
            Printf.info("[+]Alvo:\t" + Config.ipAddress + ":39190");
            Printf.info("[+]usuario:\t" + Config.usuario+"{rand}");
            Printf.info("[+]senha:\t" + Config.senha);
            Printf.info("[+]UseFileL.:\t" + Config.userFileList);
            Printf.info("[+]ClientV:\t" + Config.clientVersion[0] + "." + Config.clientVersion[1] + "." + Config.clientVersion[2]);
            Printf.info("[+]pais:\t" + Config.pais);
            Printf.info("[+]key:\t" + Config.key);
            Printf.info("[+]localIp:\t" + Config.localIp);
            Printf.info("[+]Sleep:\t" + Config.sleep);
            Printf.info("[+]Packets:\t" + Config.packets);

            Printf.white("\r\n Precione qualquer tecla para iniciar o ataque...");
            Console.ReadKey();


            AuthClient.Start();
            Monitoring.updateRAM();

            Console.ReadKey();
        }
    }
}
