namespace SimpleEncoding
{
    public static class Base1024
    {
        private static SimpleBase1024 _SimpleBase1024 = new SimpleBase1024();

        public static string Encode(byte[] bytes)
        {
            return _SimpleBase1024.Encode(bytes);
        }

        public static byte[] Decode(string str)
        {
            return _SimpleBase1024.Decode(str);
        }
    }
}