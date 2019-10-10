using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IevaScript_Framework
{
    public class ScriptObject
    {
        public Type resultType { get; set; }
        public object script { get; set; }
        public object result { get; set; }
        public Guid parameterId { get; set; }
    }
}
