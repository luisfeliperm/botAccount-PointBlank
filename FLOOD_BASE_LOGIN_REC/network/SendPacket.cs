using bot.Logs;
using bot.network.auth;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace bot.network
{
    public abstract class SendPacket
    {
        public MemoryStream mstream = new MemoryStream();

        public byte[] GetEncrypted()
        {
            byte[] buffer = new byte[] { 0 };
            Sender packetFinal = new Sender();

            try
            {
                write();

                byte[] dataNormal = mstream.ToArray();
                ushort LenghtDecrypt = (ushort)(dataNormal.Length - 2 | 0x8000);
                byte[] dataEnc = Utils.Encrypt(dataNormal, AuthClient.Shift);

                packetFinal.WriteH(LenghtDecrypt);
                packetFinal.WriteB(dataEnc);

                buffer = packetFinal.stream.ToArray();
            }
            catch (Exception ex)
            {
                Printf.b_danger("[SendPacket.GetBytes] Erro fatal!");
                return new byte[0];
            }
            finally
            {
                packetFinal.Close();
            }
            return buffer;
        }



        protected internal void writeIP(string address)
        {
            writeB(IPAddress.Parse(address).GetAddressBytes());
        }
        protected internal void writeIP(IPAddress address)
        {
            writeB(address.GetAddressBytes());
        }
        protected internal void writeB(byte[] value)
        {
            mstream.Write(value, 0, value.Length);
        }
        protected internal void writeB(byte[] value, int offset, int length)
        {
            mstream.Write(value, offset, length);
        }
        protected internal void writeD(bool value)
        {
            writeB(new byte[] { Convert.ToByte(value), 0, 0, 0 });
        }
        protected internal void writeD(uint valor)
        {
            writeB(BitConverter.GetBytes(valor));
        }
        protected internal void writeD(int value)
        {
            writeB(BitConverter.GetBytes(value));
        }
        protected internal void writeH(ushort valor)
        {
            writeB(BitConverter.GetBytes(valor));
        }
        protected internal void writeH(short val)
        {
            writeB(BitConverter.GetBytes(val));
        }
        protected internal void writeC(byte value)
        {
            mstream.WriteByte(value);
        }
        /// <summary>
        /// True = 1; False = 0.
        /// </summary>
        /// <param name="value"></param>
        protected internal void writeC(bool value)
        {
            mstream.WriteByte(Convert.ToByte(value));
        }
        protected internal void writeT(float value)
        {
            writeB(BitConverter.GetBytes(value));
        }
        protected internal void writeF(double value)
        {
            writeB(BitConverter.GetBytes(value));
        }
        protected internal void writeQ(ulong valor)
        {
            writeB(BitConverter.GetBytes(valor));
        }
        protected internal void writeQ(long valor)
        {
            writeB(BitConverter.GetBytes(valor));
        }
        protected internal void writeS(string value)
        {
            if (value != null)
                writeB(Encoding.Unicode.GetBytes(value));
        }
        protected internal void writeS(string name, int count)
        {
            if (name == null)
                return;
            writeB(Encoding.GetEncoding(1252).GetBytes(name));
            writeB(new byte[count - name.Length]);
        }
        public abstract void write();
    }





    public class Sender
    {
        public MemoryStream stream;
        public Sender()
        {
            stream = new MemoryStream();
        }
        public void WriteC(byte v) { stream.WriteByte(v); }
        public void WriteB(byte[] v) { stream.Write(v, 0, v.Length); }
        public void WriteD(int v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteH(short v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteH(ushort v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteF(double v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteQ(long v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteQ(ulong v) { WriteB(BitConverter.GetBytes(v)); }
        public void WriteS(string name, int count)
        {
            if (name == null)
                name = "";
            WriteB(Encoding.GetEncoding(1251).GetBytes(name));
            WriteB(new byte[count - name.Length]);
        }
        public void Close()
        {
            try
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
                stream = null;
            }
            catch (Exception e)
            {
                Printf.b_danger(e.ToString());
            }
        }
    }
}
