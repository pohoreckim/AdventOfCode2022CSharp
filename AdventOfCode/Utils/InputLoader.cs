namespace Utils
{
    public static class InputLoader
    {
        public static string LoadInput(string path ="../../../input.txt")
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}