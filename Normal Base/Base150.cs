using System;

namespace SimpleEncoding
{
    public class Base150
    {
        private const string Base150CharacterSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!#$%&()*+,-./:;<=>?@[]^_`{|}~";
        private const int Base = 150;

        public static string Encode(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            string result = "";
            ulong value = 0;
            int valueSize = 0;

            foreach (byte b in data)
            {
                value = (value << 8) | b;
                valueSize += 8;

                while (valueSize >= 5)
                {
                    char encodedChar = Base150CharacterSet[(int)(value >> (valueSize - 5))];
                    valueSize -= 5;
                    value &= ((1UL << valueSize) - 1);
                    result += encodedChar;
                }
            }

            if (valueSize > 0)
            {
                char encodedChar = Base150CharacterSet[(int)(value << (5 - valueSize))];
                result += encodedChar;
            }

            return result;
        }

        public static byte[] Decode(string encoded)
        {
            if (encoded == null)
                throw new ArgumentNullException("encoded");

            ulong value = 0;
            int valueSize = 0;
            var result = new System.Collections.Generic.List<byte>();

            foreach (char c in encoded)
            {
                int index = Base150CharacterSet.IndexOf(c);
                if (index == -1)
                    throw new ArgumentException("Invalid character in the encoded string: " + c);

                value = (value << 5) | (ulong)index;
                valueSize += 5;

                while (valueSize >= 8)
                {
                    byte decodedByte = (byte)(value >> (valueSize - 8));
                    valueSize -= 8;
                    value &= ((1UL << valueSize) - 1);
                    result.Add(decodedByte);
                }
            }

            return result.ToArray();
        }
    }
}