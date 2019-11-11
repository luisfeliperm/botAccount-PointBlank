using bot.Logs;
using bot.network.auth.actions;
using bot.network.auth.client;
using System;
using System.Net;
using static bot.modal.LIST_SERVER;

namespace bot.network.auth.server
{
    class BASE_SERVER_LIST_ACK : ReadBuffer
    {
        private uint sessionId;
        private int countServer;
        private short cryptoKey;
        private ushort seed;
        private IPAddress ip;

        public BASE_SERVER_LIST_ACK(byte[] data)
        {
            makeme(data);
        }
        public override void read()
        {
            try
            {
                sessionId = nextUint();
                ip = new IPAddress(nextByte(4));
                cryptoKey = nextShort();
                seed = nextUshort();
                nextByte(11);
                countServer = nextInt();

                for (int i = 0; i < countServer; i++)
                {
                    GameServerModel server = new GameServerModel()
                    {
                        _id = i,
                        _state = nextInt(),
                        _ip = new IPAddress(nextByte(4)),
                        _port = nextUshort(),
                        _type = nextByte(),
                        _maxPlayers = nextUshort(),
                        _LastCount = nextInt()
                        
                    };
                    _servers.Add(server);
                }
            }
            catch (Exception e)
            {
                Printf.b_danger("[BASE_SERVER_LIST_ACK.READ] \r\n" + e);
            }

        }
        public override void run()
        {

            AuthClient.SessionId = sessionId;
            AuthClient.Seed = seed;
            AuthClient.Shift = (int)(sessionId % 7 + 1); ;


            Printf.warnDark("[INFORMAÇÕES RECEBIDAS]");
            Printf.sucess("[] SessionID: " + sessionId);
            Printf.sucess("[] CryptoKey: " + cryptoKey);
            Printf.sucess("[] Shift: " + AuthClient.Shift);
            Printf.sucess("[] Seed: " + seed);
            Printf.sucess("[] Server Count: " + countServer);

            int totalPlayers = 0;
            if(_servers.Count > 1)
            {
                for (int i = 0; i < _servers.Count; i++)
                {
                    totalPlayers += _servers[i]._LastCount;
                }
            }
            else
            {
                totalPlayers = _servers[0]._LastCount;
            }

            Printf.sucess("[] Players Online: " + totalPlayers);
            Printf.sucess("------------------------------------");

            // Envia LOGIN
            floodAccount.Start();

            // AuthClient.Send(new BASE_LOGIN_REC());
             



        }
    }
}
