using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IevaScript_Framework
{
    public class Config
    {
        public string[] references { get; set; }
        public string[] usings { get; set; }

        internal string usingsToString()
        {
            string str = "";
            foreach (var s in usings)
            {
                str += $"using {s};\n";
            }
            return str;
        }
    }
}
