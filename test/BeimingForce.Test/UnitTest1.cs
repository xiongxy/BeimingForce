using System;
using BeimingForce.Engine;
using BeimingForce.Model;
using Template;
using Xunit;

namespace BeimingForce.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            var code = @"

                using System;
                using System.Collections;
                using System.Collections.Generic;
             
                namespace CSharpScriptEngine
                {
                    public class GeneratedExecutor
                    {
                        public string add(string a,string b,string z){
												   string c = a + b + z;
                                                   Console.Write(c);
												   return c;}
                    }
                }
";
            //初始化脚本引擎
            AbstractEngine scriptEngine = new  CSharpScriptEngine();
            var code1 = CurrencyTemplate.Template.Replace("$Namespace", "BeimingForce").Replace("$MainFunction", code);
            scriptEngine.Run(code);
        }
    }
}
