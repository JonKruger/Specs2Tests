using System;

namespace Specs2Tests
{
    public static class StringExtensions
    {
        public static string[] SplitIntoLines(this string s)
        {
            return s.Split(new[] {"\r\n"}, StringSplitOptions.None);
        }
    }
}