
namespace VinsUncoderLibrary.Extentions
{
    public static class StringExtention
    {
        public static string SubstringUpToTheSpace(this string line)
        {
            string substringOfLineUpToTheSpace = "";
            foreach(char symbol in line)
            {
                if(symbol != ' ')
                {
                    substringOfLineUpToTheSpace += symbol;
                }
                else
                {
                    break;
                }
            }
            return substringOfLineUpToTheSpace;
        }
    }
}
