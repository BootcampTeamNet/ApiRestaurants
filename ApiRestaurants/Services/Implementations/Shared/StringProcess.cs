using Services.Interfaces;
using System.Text.RegularExpressions;

namespace Services.Implementations.Shared
{
    public class StringProcess: IStringProcess
    {
        public string removeSpecialCharacter(string input)
        {
            return Regex.Replace(input, @"[^0-9a-zA-Z\._]", string.Empty);
        }
    }
}
