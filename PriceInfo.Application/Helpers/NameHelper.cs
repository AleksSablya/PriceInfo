using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PriceInfo.Application.Helpers
{
    public static class NameHelper
    {
        private const string _nonLetterChars = "[^a-zA-Z]";

        public static string RemoveAllNonLetterChars(string str) => Regex.Replace(str, _nonLetterChars, "");
    }
}
