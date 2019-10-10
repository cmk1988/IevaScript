using IevaScript_Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IevaScript_Executer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config
            {
                references = new string[]
                {
                    "System.dll",
                    "System.Core.dll",
                },
                usings = new string[]
                {
                    "System",
                    "System.Linq",
                    "System.Collections.Generic"
                }
            };

            var ievaScript = new IevaScript(config);

            ievaScript.Compile("Script_1.isc");
            ievaScript.Compile("Script_2.isc");
            ievaScript.Compile("Script_3.isc");

            ievaScript.Run("Script_1");
        }
    }
}
