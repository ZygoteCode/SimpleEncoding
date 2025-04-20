using System;
using System.Text;

namespace SimpleEncoding
{
    public class Base64
    {
        private const string Base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        public static string Encode(byte[] data)
        {
            StringBuilder result = new StringBuilder((int)Math.Ceiling(data.Length * 8.0 / 6.0));
            
            int index = 0;
            int buffer = 0;
            int bufferLength = 0;

            while (index < data.Length || bufferLength > 0)
            {
                if (bufferLength < 6)
                {
                    if (index < data.Length)
                    {
                        buffer = (buffer << 8) | data[index];
                        bufferLength += 8;
                        index++;
                    }
                    else
                    {
                        buffer <<= (6 - bufferLength);
                        bufferLength = 6;
                    }
                }

                result.Append(Base64Chars[(buffer >> (bufferLength - 6)) & 0x3F]);
                bufferLength -= 6;
            }

            // Add padding characters if needed
            int paddingLength = result.Length % 4;
            if (paddingLength > 0)
            {
                for (int i = 0; i < 4 - paddingLength; i++)
                {
                    result.Append("=");
                }
            }

            return result.ToString();
        }

        public static byte[] Decode(string base64)
        {
            int buffer = 0;
            int bufferLength = 0;
            int paddingCount = 0;
            int resultLength = base64.Length * 6 / 8;
            byte[] result = new byte[resultLength];

            for (int i = 0, j = 0; i < base64.Length; i++)
            {
                char c = base64[i];
                if (c == '=')
                {
                    paddingCount++;
                    continue;
                }

                int value = Base64Chars.IndexOf(c);
                if (value < 0)
                    throw new ArgumentException("Invalid character in the Base64 string.");

                buffer = (buffer << 6) | value;
                bufferLength += 6;

                if (bufferLength >= 8)
                {
                    result[j++] = (byte)((buffer >> (bufferLength - 8)) & 0xFF);
                    bufferLength -= 8;
                }
            }

            if (paddingCount > 0)
            {
                // Remove padding bytes
                Array.Resize(ref result, resultLength - paddingCount);
            }

            return result;
        }
    }
}