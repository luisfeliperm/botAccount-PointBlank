using bot.Logs;
using bot.network.auth.server;
using System;
using System.Threading;

namespace bot.network.auth
{
    class Read
    {
        

        public Read(byte[] buff)
        {
            UInt16 dataLenght = BitConverter.ToUInt16(buff, 0);
            UInt16 opcode = BitConverter.ToUInt16(buff, 2);
            Printf.danger("[Receive] - Opcode: "+opcode);

            if (!AuthClient.firstPacket && opcode != 2049)
            {
                Printf.blue("Primeiro pacote nao recebido");
                return;
            }

            ReadBuffer packet = null;
            switch (opcode)
            {
                case 2049:
                    AuthClient.firstPacket = true;
                    packet = new BASE_SERVER_LIST_ACK(buff);
                    break;
                case 2062:
                    packet = new SERVER_MESSAGE_DISCONNECT_PAK(buff);
                    break;
                case 2564:
                    packet = new BASE_LOGIN_ACK(buff);
                    break;
                default:
                    break;
            }
            if (packet != null)
                new Thread(packet.run).Start();

        }
    }
}
