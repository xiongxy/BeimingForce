using System;
using System.Collections.Generic;
using System.Text;
using BeimingForce.Enum;

namespace BeimingForce.Engine
{
    public class CreateScriptEngine
    {
        private CreateScriptEngine()
        {
        }
        public static CreateScriptEngine Instance { get; } = new CreateScriptEngine();

        //public IDynamicScript CreateDynamicScript(string applicationName, DynamicScriptLanguageEnum language, string scriptText, string[] referenceNamespaces = null)
        //{
        //    IDynamicScript dynamicScript = null;
        //    switch (language)
        //    {
        //        case DynamicScriptLanguageEnum.CSharp:
        //            dynamicScript = new CSharpScriptEngine(applicationName, scriptText, referenceNamespaces);
        //            break;
        //    }
        //    return dynamicScript;
        //}
    }
}
