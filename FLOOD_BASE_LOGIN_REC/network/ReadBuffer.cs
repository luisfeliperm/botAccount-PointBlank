using System;
using System.Text;

namespace bot.network
{
    public abstract class ReadBuffer
    {
        private byte[] _buffer;
        private int _offset = 4;

        protected internal void makeme(byte[] buffer)
        {
            _buffer = buffer;
            read();
        }

        protected internal string readA()
        {
            string num = BitConverter.ToString(_buffer, 0);
            return num;
        }

        protected internal byte nextByte()
        {
            byte num = _buffer[_offset];
            _offset++;
            return num;
        }

        protected internal byte[] nextByte(int Length)
        {
            byte[] result = new byte[Length];
            Array.Copy(_buffer, _offset, result, 0, Length);
            _offset += Length;
            return result;
        }
        protected internal ushort nextUshort()
        {
            ushort num = BitConverter.ToUInt16(_buffer, _offset);
            _offset += 2;
            return num;
        }
        protected internal short nextShort()
        {
            short num = BitConverter.ToInt16(_buffer, _offset);
            _offset += 2;
            return num;
        }
        protected internal uint nextUint()
        {
            uint num = BitConverter.ToUInt32(_buffer, _offset);
            _offset += 4;
            return num;
        }
        protected internal int nextInt()
        {
            int num = BitConverter.ToInt32(_buffer, _offset);
            _offset += 4;
            return num;
        }

        protected internal double nextDouble()
        {
            double num = BitConverter.ToDouble(_buffer, _offset);
            _offset += 8;
            return num;
        }
        protected internal long nextLong()
        {
            long num = BitConverter.ToInt64(_buffer, _offset);
            _offset += 8;
            return num;
        }
        protected internal ulong nextUlong()
        {
            ulong num = BitConverter.ToUInt64(_buffer, _offset);
            _offset += 8;
            return num;
        }
        protected internal string nextString(int Length)
        {
            string str = "";
            try
            {
                str = Encoding.GetEncoding(1252).GetString(_buffer, _offset, Length);
                int length = str.IndexOf(char.MinValue);
                if (length != -1)
                    str = str.Substring(0, length);
                _offset += Length;
            }
            catch{}
            return str;
        }
       

        public abstract void read();
        public abstract void run();
    }
    
}
