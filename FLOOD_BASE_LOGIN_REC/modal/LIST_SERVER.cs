using System.Collections.Generic;
using System.Net;

namespace bot.modal
{
    class LIST_SERVER
    {
        public class GameServerModel
        {
            public int _id, _state, _type, _LastCount, _maxPlayers;
            public IPAddress _ip;
            public ushort _port;

            public IPEndPoint Connection;

            /*
            public GameServerModel()
            {
                //Printf.sucess("[Load Server] " + ip.ToString() + ":" + port);
            }
            */
        }



        public static List<GameServerModel> _servers = new List<GameServerModel>();
        public static GameServerModel getServer(int id)
        {
            lock (_servers)
            {
                for (int i = 0; i < _servers.Count; i++)
                {
                    GameServerModel server = _servers[i];
                    if (server._id == id)
                        return server;
                }
                return null;
            }
        }
    }
}
