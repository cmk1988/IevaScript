using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IevaScript_Framework
{
    public class CodePreInterpreter
    {
        public static bool FindCall(string code, out string outCode)
        {
            string c = "";
            var call = "";

            var index1 = code.IndexOf("#call_");
            var index2 = index1 + 6;
            if (index1 != -1)
            {
                while (code[index2] != ';')
                {
                    c += code[index2];
                    index2++;
                }
                var parameters = c.Split('=');
                if (parameters.Count() != 1 && parameters.Count() != 2)
                    throw new Exception("Syntax error: " + c + ";");
                var args = parameters[0].Split(':');
                if (args.Count() != 1 && args.Count() != 2)
                    throw new Exception("Syntax error(Parameters): " + c + ";");
                if (args.Count() == 2)
                    call += $"globalCache.Objects[scriptKeeper.GetScript(\"{args[0].Trim()}\").parameterId.ToString()]={args[1].Trim()};";
                call += $"scriptKeeper.Run(\"{args[0].Trim()}\");";
                if (parameters.Count() == 2)
                {
                    call += $"{parameters[1].Trim()}=Convert.ChangeType(" +
                        $"scriptKeeper.GetScript(\"{args[0].Trim()}\").result," +
                        $"scriptKeeper.GetScript(\"{args[0].Trim()}\").resultType);";
                }
            }
            outCode = c == "" ? code : code.Replace($"#call_{c};", call);
            return c != "";
        }

        public static string SetVariables(string code)
        {
            string c = "";

            return c;
        }

        public static string getTypeName(string code, out string outCode)
        {
            var c = "";

            var index1 = code.IndexOf("#result_");
            var index2 = index1 + 8;
            if (index1 != -1)
            {
                while (code[index2] != ';')
                {
                    c += code[index2];
                    index2++;
                }

                outCode = code.Replace($"#result_{c};", "");
            }
            else
                outCode = code;
            return c == "" ? "IevaScript_Framework.Void" : c;
        }
    }
}
