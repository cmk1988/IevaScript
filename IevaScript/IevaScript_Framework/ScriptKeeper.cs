using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IevaScript_Framework
{
    public interface IIevaScript
    {
        void Run(string key);
        ScriptObject GetScript(string key);
    }

    public class IevaScript : IIevaScript
    {
        DataCache globalCache;
        Dictionary<string, ScriptObject> Scripts;

        CSharpCompiler scriptCompiler;

        public IevaScript(Config config)
        {
            globalCache = new DataCache();
            scriptCompiler = new CSharpCompiler(config, globalCache, this);
            Scripts = new Dictionary<string, ScriptObject>();
        }

        public void Compile(string filePath, string scriptName = null)
        {
            var code = File.ReadAllText(filePath);
            var key = scriptName ?? Path.GetFileNameWithoutExtension(filePath);
            Scripts.Add(key, scriptCompiler.Compile(code));
        }

        public ScriptObject GetScript(string key)
        {
            return Scripts.FirstOrDefault(x => x.Key == key).Value;
        }

        public void Run(string key)
        {
            var script = Scripts.FirstOrDefault(x => x.Key == key).Value;
            if (script != null)
                scriptCompiler.Invoke(script);
        }
    }
}
