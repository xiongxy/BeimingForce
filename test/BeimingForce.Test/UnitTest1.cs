using System;
using BeimingForce.Engine;
using BeimingForce.Model;
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

            //var vvv =
            //     Entry.RunDynamicScript("BeimingForce", code, Enum.DynamicScriptSequentialEnum.After, null);
        }
    }
}
