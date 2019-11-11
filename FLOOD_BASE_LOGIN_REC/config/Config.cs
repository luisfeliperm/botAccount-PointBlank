using System;
using System.Net;

namespace bot.config
{
    class Config
    {
        public static string ipAddress, usuario, senha, userFileList;
        public static int pais, packets, sleep;
        public static ulong key;
        public static bool randMac;
        public static short[] clientVersion = new short[3];
        public static byte[]  mac = new byte[6];

        public static IPAddress localIp;

        public static void Load()
        {
            ConfigFile configFile = new ConfigFile("config.ini");
            ipAddress = configFile.readString("ip", "127.0.0.1");
            usuario = configFile.readString("usuario", "mikrotik");
            senha = configFile.readString("senha", "pocoyo");
            userFileList = configFile.readString("userFileList", "pocoyo");
            string x = configFile.readString("clientVersion", "1.15.42");

            clientVersion[0] = short.Parse(x.Split('.')[0]);
            clientVersion[1] = short.Parse(x.Split('.')[1]);
            clientVersion[2] = short.Parse(x.Split('.')[2]);



            pais = configFile.readUInt16("pais", 5);
            key = configFile.readUInt64("key", 0);
            randMac = configFile.readBoolean("randMac", false);

            localIp = IPAddress.Parse(configFile.readString("localIp", "192.168.0.1"));

            if (randMac)
            {
                Random r = new Random();
                mac[0] = Convert.ToByte(r.Next(0, 255));
                mac[1] = Convert.ToByte(r.Next(0, 255));
                mac[2] = Convert.ToByte(r.Next(0, 255));
                mac[3] = Convert.ToByte(r.Next(0, 255));
                mac[4] = Convert.ToByte(r.Next(0, 255));
                mac[5] = Convert.ToByte(r.Next(0, 255));
            }
            else
            {
                mac[0] = 0xba; mac[1] = 0xba; mac[2] = 0xca;
                mac[3] = 0xfa; mac[4] = 0xca; mac[5] = 0xda;
            }

            packets = configFile.readInt32("packets", 500);


            sleep = 1000 / configFile.readInt32("contasPerSecond", 2);

        }
    }
}