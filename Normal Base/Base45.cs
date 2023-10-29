using System.Collections.Generic;

namespace SimpleEncoding
{
    public class Base45
    {
        private static char[] _Encoding = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ', '$', '%', '*', '+', '-', '.', '/', ':' };
        private static Dictionary<char, byte> _Decoding = new Dictionary<char, byte>(45);

        static Base45()
        {
            byte i = 0;

            while ((int)i < _Encoding.Length)
            {
                _Decoding.Add(_Encoding[(int)i], i);
                i += 1;
            }
        }

        public static string Encode(byte[] buffer)
        {
            bool flag = buffer == null;
            bool flag4 = flag;

            if (flag4)
            {
                return null;
            }

            int wholeChunkCount = buffer.Length / 2;
            char[] result = new char[wholeChunkCount * 3 + ((buffer.Length % 2 == 1) ? 2 : 0)];
            bool flag2 = result.Length == 0;
            bool flag5 = flag2;
            string result2;

            if (flag5)
            {
                result2 = string.Empty;
            }

            else
            {
                int resultIndex = 0;
                int wholeChunkLength = wholeChunkCount * 2;
                int i = 0;

                while (i < wholeChunkLength)
                {
                    int value = (int)buffer[i++] * 256 + (int)buffer[i++];

                    result[resultIndex++] = _Encoding[value % 45];
                    result[resultIndex++] = _Encoding[value / 45 % 45];
                    result[resultIndex++] = _Encoding[value / 2025 % 45];
                }

                bool flag3 = buffer.Length % 2 == 0;
                bool flag6 = flag3;

                if (flag6)
                {
                    result2 = new string(result);
                }
                else
                {
                    char[] array = result;
                    array[array.Length - 2] = _Encoding[(int)(buffer[buffer.Length - 1] % 45)];
                    char[] array2 = result;
                    array2[array2.Length - 1] = ((buffer[buffer.Length - 1] < 45) ? _Encoding[0] : _Encoding[(int)(buffer[buffer.Length - 1] / 45 % 45)]);
                    result2 = new string(result);
                }
            }

            return result2;
        }

        public static byte[] Decode(string value)
        {
            bool flag = value == null;
            bool flag6 = flag;

            if (flag6)
            {
                return null;
            }

            bool flag2 = value.Length == 0;
            bool flag7 = flag2;
            byte[] result2;

            if (flag7)
            {
                return null;
            }

            int remainderSize = value.Length % 3;
            bool flag3 = remainderSize == 1;
            bool flag8 = flag3;

            if (flag8)
            {
                return null;
            }

            byte[] buffer = new byte[value.Length];

            for (int i = 0; i < value.Length; i++)
            {
                byte decoded;
                bool flag4 = _Decoding.TryGetValue(value[i], out decoded);
                bool flag9 = !flag4;

                if (flag9)
                {
                    return null;
                }

                buffer[i] = decoded;
            }

            int wholeChunkCount = buffer.Length / 3;
            byte[] result3 = new byte[wholeChunkCount * 2 + ((remainderSize == 2) ? 1 : 0)];
            int resultIndex = 0;
            int wholeChunkLength = wholeChunkCount * 3;
            int j = 0;

            while (j < wholeChunkLength)
            {
                int val = (int)(buffer[j++] + 45 * buffer[j++]) + 2025 * (int)buffer[j++];

                result3[resultIndex++] = (byte)(val / 256);
                result3[resultIndex++] = (byte)(val % 256);
            }

            bool flag5 = remainderSize == 0;
            bool flag10 = flag5;

            if (flag10)
            {
                return result3;
            }

            byte[] array = result3;
            int num = array.Length - 1;
            byte[] array2 = buffer;
            byte b = array2[array2.Length - 2];
            byte b2 = 45;
            byte[] array3 = buffer;
            array[num] = (byte)(b + b2 * array3[array3.Length - 1]);
            result2 = result3;

            return result2;
        }
    }
}