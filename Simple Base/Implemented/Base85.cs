namespace SimpleEncoding
{
    public static class Base85
    {
        private static SimpleBase85 _SimpleBase85 = new SimpleBase85();

        public static string Encode(byte[] bytes)
        {
            return _SimpleBase85.Encode(bytes);
        }

        public static byte[] Decode(string str)
        {
            return _SimpleBase85.Decode(str);
        }
    }
}