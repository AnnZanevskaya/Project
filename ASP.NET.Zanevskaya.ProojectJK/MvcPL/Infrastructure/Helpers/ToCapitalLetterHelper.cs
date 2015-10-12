using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Infrastructure.Helpers
{
    public static class ToCapitalLetterHelper
    {
        public static string ToCapitalLetter(this string text)
        {
            char[] charText = text.ToCharArray();
            charText[0] = char.ToUpper(charText[0]);

            for (int i = 0; i+1 < charText.Length; i++)
            {
                if (charText[i]=='.' && charText[i+1]==' ')
                {
                    charText[i + 2] = char.ToUpper(charText[i + 2]);
                }
            }
          return new string(charText);
        }

    }
}