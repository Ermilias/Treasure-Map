using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap
{
    class Utils
    {
        public string[] splitString(string source)
        {
            string[] stringSeparators = new string[] { "-" };
            string[] result;

            result = source.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }

        public int countMvt(string myString)
        {
            int i = 0;
            foreach (char c in myString)
                i++;
            return i;
        }

        public string adjustNameLenght(string name)
        {
            string newName;
            if (name.Length <= 4)
                newName = name;
            else
                newName = name.Substring(0, 4);
            return newName;
        }
    }
}
