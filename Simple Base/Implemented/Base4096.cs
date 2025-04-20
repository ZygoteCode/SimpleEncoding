namespace SimpleEncoding
{
    public static class Base4096
    {
        private static SimpleBase4096 _SimpleBase4096 = new SimpleBase4096();

        public static string Encode(byte[] bytes)
        {
            return _SimpleBase4096.Encode(bytes);
        }

        public static byte[] Decode(string str)
        {
            return _SimpleBase4096.Decode(str);
        }
    }
}