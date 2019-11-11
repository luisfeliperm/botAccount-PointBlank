using bot.config;
using bot.Logs;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bot.network.auth
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 256;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    class AuthClient
    {
        private static ManualResetEvent connectDone =
        new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        private static String response = String.Empty;

        public static Socket _client;
        public DateTime ConnectDate;
        public static uint SessionId;
        public static ushort Seed;
        public static int Shift;
        public static bool firstPacket = false;

        public static void nextSeed()
        {
            Seed = (ushort)(Seed * 214013 + 2531011 >> 16 & 0x7FFF);
        }
        public static void Start()
        {
            IPAddress addr = IPAddress.Parse(Config.ipAddress);
            IPEndPoint remoteEP = new IPEndPoint(addr, 39190);

            _client = new Socket(addr.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            _client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), _client);

            connectDone.WaitOne();

            Task.Run(() => Receive());
        }

        private static void SendPacket(byte[] buffer)
        {
            //Send();
            sendDone.WaitOne();
        }

        public static bool isSocketConnected()
        {
            return !((_client.Poll(1000, SelectMode.SelectRead) && (_client.Available == 0)) || !_client.Connected);
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive()
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = _client;
                _client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                _client.Close(0);
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                
                int bytesCount = state.workSocket.EndReceive(ar);
                if (bytesCount < 1) return;

                new Read(state.buffer);
                new Thread(Receive).Start();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static bool Send(SendPacket packet)
        {
            if (!isSocketConnected())
            {
                Printf.danger("Falha ao enviar, desconectado");
                return false;
            }

            byte[] data = packet.GetEncrypted();
            
            _client.BeginSend(data, 0, data.Length, 0,
            new AsyncCallback(SendCallback), _client);
            return true;
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                if (client != null && client.Connected)
                    client.EndSend(ar);

                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
