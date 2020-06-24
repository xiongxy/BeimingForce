using BeimingForce.Container;
using BeimingForce.Core;
using BeimingForce.Enum;
using BeimingForce.Model;

namespace BeimingForce.Engine.ScriptEngine
{
    public static class ScriptEngineFactory
    {
        public static IDynamicScript CreateDynamicScript(
            DynamicScriptDefinition definition)
        {
            IDynamicScript dynamicScript = null;
            switch (definition.Script.Language)
            {
                case DynamicScriptLanguageEnum.CSharp:
                    dynamicScript = new CSharpScriptEngine(definition);
                    break;
            }
            return dynamicScript;
        }
    }
}