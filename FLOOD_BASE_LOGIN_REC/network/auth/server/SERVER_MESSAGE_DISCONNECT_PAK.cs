using bot.Logs;
using System;

namespace bot.network.auth.server
{
    class SERVER_MESSAGE_DISCONNECT_PAK : ReadBuffer
    {
        private uint _erro;
        private bool useHack;

        public SERVER_MESSAGE_DISCONNECT_PAK(byte[] data)
        {
            makeme(data);
        }
        public override void read()
        {
            try
            {
                nextUint();
                _erro = nextUint();
                useHack = Convert.ToBoolean(nextByte());
            }
            catch
            {

            }
        }
        public override void run()
        {
            Printf.danger("Voce foi desconectado! Codigo: "+ _erro + " Hack: " + useHack);
        }
    }
}
