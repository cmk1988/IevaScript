using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IevaScript_Framework
{
    public class CSharpCompiler
    {
        IIevaScript scriptKeeper;

        Config config;
        
        DataCache cache;

        public CSharpCompiler(Config config, DataCache cache, IIevaScript scriptKeeper)
        {
            this.config = config;
            this.scriptKeeper = scriptKeeper;
            this.cache = cache;
        }

        public ScriptObject Compile(string code)
        {

            CompilerParameters parameters;
            CSharpCodeProvider codeProvider;
            ICodeCompiler icc;
            parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.ReferencedAssemblies.Add("IevaScript_Framework.dll");
            parameters.ReferencedAssemblies.AddRange(config.references);
            codeProvider = new CSharpCodeProvider();
            icc = codeProvider.CreateCompiler();
            var type = CodePreInterpreter.getTypeName(code, out code);
            while (CodePreInterpreter.FindCall(code, out code)) ;
            var guid = Guid.NewGuid();

            string source =
            "using IevaScript_Framework;" +
            config.usingsToString() +
            "namespace LambdaNamespace" +
            "{" +
                "public class LambdaClass" +
                "{" +
                    "delegate void MethodDelegate(DataCache globalCache, IIevaScript scriptKeeper);" +
                    "public void EntryPoint(DataCache globalCache, IIevaScript scriptKeeper)" +
                    "{" +
                        $"var parameter = globalCache.Objects.FirstOrDefault(x => x.Key == \"{guid}\").Value;" +
                        code +
                    "}" +
                "}" +
            "}";

            if (type != "IevaScript_Framework.Void")
                source = source.Replace("void", type);
            cache.Objects.Add(guid.ToString(), new object());
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, source);
            if (results.Errors.HasErrors)
            {
                string text = "Compile error: ";
                foreach (CompilerError ce in results.Errors)
                {
                    text += "\r\n" + ce.ToString();
                }
                throw new Exception(text);
            }
            if (!results.Errors.HasErrors && !results.Errors.HasWarnings)
            {
                Assembly ass = results.CompiledAssembly;
                var result = new ScriptObject
                {
                    script = ass.CreateInstance("LambdaNamespace.LambdaClass"),
                    resultType = Type.GetType(type),
                    parameterId = guid
                };
                return result;
            }
            throw new Exception();
        }

        public void Invoke(ScriptObject obj)
        {
            if (obj.resultType != typeof(IevaScript_Framework.Void))
                obj.result = obj.script.GetType().InvokeMember("EntryPoint",
                        BindingFlags.InvokeMethod, null, obj.script, new object[] { cache, scriptKeeper });
            else
                obj.script.GetType().InvokeMember("EntryPoint",
                        BindingFlags.InvokeMethod, null, obj.script, new object[] { cache, scriptKeeper });
        }
    }
}
