using bot.config;
using bot.network.auth.actions;
using System;

namespace bot.network.auth.client
{
    class BASE_LOGIN_REC : SendPacket
    {
        public BASE_LOGIN_REC()
        {
            
        }

        public override void write()
        {
            Random r = new Random();
            string USER = Config.usuario + floodAccount.seq1 +"_"+ r.Next(0, 20000);

            AuthClient.nextSeed();
            writeH(2561);
            writeH(AuthClient.Seed); // Seed


            writeC((byte)Config.clientVersion[0]);
            writeH((byte)Config.clientVersion[1]);
            writeH((byte)Config.clientVersion[2]);
            writeC((byte)Config.pais); // Pais
            writeC((byte)USER.Length);
            writeC((byte)Config.senha.Length);
            writeS(USER, USER.Length);
            writeS(Config.senha, Config.senha.Length);
            writeB(Config.mac);
            writeH(0); //0
            writeC(0);
            writeIP(Config.localIp);

            writeQ(Config.key); // key
            writeS(Config.userFileList, 32);
            writeS("0", 16);
            writeS("4a4f18a6befa9d27e1c33c47dc617775", 32); //d3x


            // Valores a mais q n sei oq é e só clonei da client v42
            /* 
             * 01 20 3C 01 01 02 75 0B FF 0F DE 10 00 00 65 
               0A 00 00 15 00 15 00 CC 03 64 01 00 10 0C 00 04 
               00 03 
               */

            byte[] resto = { 0x01, 0x20, 0x3C, 0x01, 0x01, 0x02, 0x75, 0x0B, 0xFF, 0x0F, 0xDE,
                0x10, 0x00, 0x00, 0x65, 0x0A, 0x00, 0x00, 0x15, 0x00, 0x15, 0x00, 0xCC, 0x03,
                0x64, 0x01, 0x00, 0x10, 0x0C, 0x00, 0x04, 0x00, 0x03};
            writeB(resto);

        }
    }
}
