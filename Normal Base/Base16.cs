using System.Text;
using System;

namespace SimpleEncoding
{
    public class Base16
    {
        private const string HexChars = "0123456789ABCDEF";

        public static string Encode(byte[] data)
        {
            StringBuilder result = new StringBuilder(data.Length * 2);

            foreach (byte b in data)
            {
                result.Append(HexChars[b >> 4]);
                result.Append(HexChars[b & 0x0F]);
            }

            return result.ToString();
        }

        public static byte[] Decode(string base16)
        {
            if (base16.Length % 2 != 0)
            {
                throw new ArgumentException("Input must have an even number of characters.");
            }

            int length = base16.Length / 2;
            byte[] result = new byte[length];

            for (int i = 0; i < length; i++)
            {
                int highNibble = HexChars.IndexOf(base16[i * 2]);
                int lowNibble = HexChars.IndexOf(base16[i * 2 + 1]);

                if (highNibble == -1 || lowNibble == -1)
                {
                    throw new ArgumentException("Invalid character in the Base16 string.");
                }

                result[i] = (byte)((highNibble << 4) | lowNibble);
            }

            return result;
        }
    }
}