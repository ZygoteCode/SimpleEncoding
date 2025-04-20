namespace SimpleEncoding
{
    public static class Base128
    {
        private static SimpleBase128 _SimpleBase128 = new SimpleBase128();

        public static string Encode(byte[] bytes)
        {
            return _SimpleBase128.Encode(bytes);
        }

        public static byte[] Decode(string str)
        {
            return _SimpleBase128.Decode(str);
        }
    }
}