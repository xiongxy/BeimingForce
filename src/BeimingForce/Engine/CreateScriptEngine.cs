using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BeimingForce.Enum;
using BeimingForce.Model;

namespace BeimingForce.Engine
{
    public class CreateScriptEngine
    {
        private CreateScriptEngine()
        {
        }
        public static CreateScriptEngine Instance { get; } = new CreateScriptEngine();

        public IDynamicScript CreateDynamicScript(DynamicScript script)
        {
            IDynamicScript dynamicScript = null;
            switch (script.RunTime.Language)
            {
                case DynamicScriptLanguageEnum.CSharp:
                    dynamicScript = new CSharpScriptEngine(script.ApplicationName)
                        .LoadNameSpaces(script.CompileTime.ScriptReferenceNamespace)
                        .LoadAssembly(script.CompileTime.ScriptReferenceAssemblies)
                        .LoadAssembly(script.CompileTime.ScriptReferenceAssemblyNames)
                        .InitMetadataReference()
                        .BuildDynamicScript(script.CompileTime);
                    break;
            }
            return dynamicScript;
        }
    }
}
