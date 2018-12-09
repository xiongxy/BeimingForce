using System;
using System.Collections.Generic;
using System.Text;

namespace BeimingForce.ToolsKit
{
    public class Const
    {
        public const string Using = "using";
        public const string Class = "class";
        public const string Methods = "methods";
        public const string AssemblyScriptKey = "GeneratedAssembly_{0}_{1}_{2}";
        public const string MethodTypeName = "BeimingForce.Currency";

        public const string RefMscorlib = "mscorlib.dll";
        public const string RefSystem = "System.dll";
        public const string RefSystemCore = "System.Core.dll";
        public const string RefMicrosoftCSharp = "Microsoft.CSharp.dll";
        public const string RefCollections = "System.Collections.dll";
        public const string RefSystemRuntime = "System.Runtime.dll";
        public const string RefSystemConsole = "System.Console.dll";

        public const string CSharpTemplateNameSpacesPlaceholder = "[$NameSpaces]";
        public const string CSharpTemplateMainFunctionPlaceholder = "[$MainFunction]";
        public const string CSharpTemplate = @"
using System;
[$NameSpaces]
namespace BeimingForce
{
    public class Currency
    {
        public object Main(){
            [$MainFunction]
        }
    }
}
";
    }
}
