using System;
using System.Collections.Generic;
using BeimingForce;
using BeimingForce.Enum;
using BeimingForce.Model;

namespace Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var code = @"

                using System;
                using System.Collections;
                using System.Collections.Generic;
             
                namespace BeimingForce
                {
                    public class Currency
                    {
                        public string add(string a,string b,string z){
												   string c = a + b + z;
                                                   Console.Write(c);
												   return c;}
                    }
                }
";
            //var vvv =
            //    Entry.RunDynamicScript<string>("BeimingForce", code, DynamicScriptSequentialEnum.After, new object[] { "熊霄宇", "是天才", "噢" });

            DynamicScript dynamicScript = new DynamicScript()
            {
                FunctionName = "add",
                ApplicationName = "BeimingTest",
                CompileTime = new DynamicScriptCompileTime()
                {
                    ScriptText = code,
                    ScriptReferenceAssemblyNames = new[] {"Dapper"}
                },
                RunTime = new DynamicScriptRunTime()
                {
                    Language = DynamicScriptLanguageEnum.CSharp,
                    ScriptDescription = "说明",
                    ThreadType = DynamicScriptThreadTypeEnum.Sync,

                }
            };
            var vv = Entry.RunDynamicScript<string>(new List<DynamicScript>() {dynamicScript},
                new object[] {"熊霄宇", "是天才", "噢"});

        }
    }
}

