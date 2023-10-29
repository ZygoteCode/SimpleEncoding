using System.Text;
using System;

namespace SimpleEncoding
{
    public class Base2
    {
        public static string Encode(byte[] data)
        {
            StringBuilder result = new StringBuilder(data.Length * 8);

            foreach (byte b in data)
            {
                for (int i = 7; i >= 0; i--)
                {
                    result.Append((b >> i) & 1);
                }
            }

            return result.ToString();
        }

        public static byte[] Decode(string base2)
        {
            int inputLength = base2.Length;
            int outputLength = inputLength / 8;
            if (inputLength % 8 != 0)
            {
                throw new ArgumentException("Input length is not a multiple of 8.");
            }

            byte[] result = new byte[outputLength];

            for (int i = 0; i < outputLength; i++)
            {
                byte byteValue = 0;

                for (int j = 0; j < 8; j++)
                {
                    byteValue = (byte)((byteValue << 1) | (base2[i * 8 + j] == '1' ? 1 : 0));
                }

                result[i] = byteValue;
            }

            return result;
        }
    }
}