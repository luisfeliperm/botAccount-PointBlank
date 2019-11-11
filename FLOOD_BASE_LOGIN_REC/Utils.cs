namespace bot
{
    class Utils
    {
        public static byte[] Encrypt(byte[] buffer, int shift)
        {
            int length = buffer.Length;
            byte first = buffer[0];
            byte current;
            for (int i = 0; i < length; i++)
            {
                if (i >= (length - 1))
                    current = first;
                else
                    current = buffer[i + 1];
                buffer[i] = (byte)(current >> (8 - shift) | (buffer[i] << shift));
            }
            return buffer;
        }

    }
}
