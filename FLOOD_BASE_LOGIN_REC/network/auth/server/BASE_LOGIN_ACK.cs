using bot.Logs;
using bot.modal;

namespace bot.network.auth.server
{
    class BASE_LOGIN_ACK : ReadBuffer
    {
        private uint _result;
        private string _login;
        private long _pId;

        public BASE_LOGIN_ACK(byte[] data)
        {
            makeme(data);
        }
        public override void read()
        {
            try
            {
                _result = nextUint();
                nextByte();
                _pId = nextLong();

                _login = nextString((int)nextByte());
            }
            catch
            {

            }

        }
        public override void run()
        {

            string msg = null;
            switch (_result)
            {
                case 0:
                    break;
                case 0x80000101:
                    msg = "Conta já esta online!";
                    break;
                case 0x80000118:
                    msg = "Senha invalida!";
                    break;
                case 0x80000107:
                    msg = "Voce esta banido!";
                    break;
                case 0x80000117:
                    msg = "Username invalido!";
                    break;
                case 0x80000126:
                case 0x80000121:
                    msg = "Regiao bloqueada";
                    break;
                case 0x80000119:
                case 0x80000102:
                    msg = "Deleted Account";
                    break;
                case 0x80000127:
                    msg = "Usuario ou senha incorretos";
                    break;
                case 0x80000133:
                    msg = "Hardware bloqueado";
                    break;
                default:
                    msg = "Error: [" + _result + "] ";
                    break;
            }
            if (_result != 0)
            {
                flooding.totalErros++;
                Printf.danger("Erro ao logar! pId: " + _pId + " Status: " + msg);
                return;
            }

            flooding.totalCreate++;
            Printf.sucess("[Status Login] Logado com Sucesso! PlayerID: " + _pId);
        }
    }
}
