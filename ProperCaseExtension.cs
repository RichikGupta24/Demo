﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public static class ProperCaseExtension
    {
        /// <summary>
        /// This method converts Camel Case to Proper Case
        /// </summary>
        /// <param name="the_string"></param>
        /// <returns></returns>
        public static string ToProperCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Start with the first character.
            string result = the_string.Substring(0, 1).ToUpper();

            // Add the remaining characters.
            for (int i = 1; i < the_string.Length; i++)
            {
                if (char.IsUpper(the_string[i])) result += " ";
                result += the_string[i];
            }

            return result;
        }
    }
}
