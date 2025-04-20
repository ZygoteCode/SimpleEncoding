namespace SimpleEncoding
{
    public static class Base91
    {
        private static SimpleBase91 _SimpleBase91 = new SimpleBase91();

        public static string Encode(byte[] bytes)
        {
            return _SimpleBase91.Encode(bytes);
        }

        public static byte[] Decode(string str)
        {
            return _SimpleBase91.Decode(str);
        }
    }
}