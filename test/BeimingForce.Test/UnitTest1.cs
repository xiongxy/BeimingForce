using System;
using System.Diagnostics;
using BeimingForce.Container;
using BeimingForce.Core.Runner;
using BeimingForce.Engine;
using BeimingForce.Model;
using Xunit;
using Xunit.Abstractions;

namespace BeimingForce.Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        const string Code = @"
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

        [Fact]
        public void TestByContainer()
        {
            var container = ContainerFactory.CreateContainer();
            var runner = container.RegisterScript(new DynamicScriptDefinition()
            {
                Script = new DynamicScript()
                {
                    FunctionName = "add",
                    CompileTime = new DynamicScriptCompileTime()
                    {
                        ScriptText = Code,
                    }
                }
            }).Pretreatment();

            var dynamicScriptResult = runner.RunDynamicScript<string>("今天天气", "真的", "还可以");
            _testOutputHelper.WriteLine(dynamicScriptResult.ToString());
        }

        [Fact]
        public void TestByPerformance()
        {
            var container = ContainerFactory.CreateContainer();

            var runner = container.RegisterScript(new DynamicScriptDefinition()
            {
                Script = new DynamicScript()
                {
                    FunctionName = "add",
                    CompileTime = new DynamicScriptCompileTime()
                    {
                        ScriptText = Code,
                    }
                }
            }).Pretreatment();

            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 10000; i++)
            {
                container.GetRunner("add").RunDynamicScript<string>("今天天气", "真的", "还可以");
            }

            var stopwatchElapsedMilliseconds = (decimal) stopwatch.ElapsedMilliseconds / 10000;
            _testOutputHelper.WriteLine($"单条执行时间为:{stopwatchElapsedMilliseconds}");
        }

        [Fact]
        public void TestCompileErrorMessage()
        {
            var container = ContainerFactory.NewContainer();

            var runner = container.RegisterScript(new DynamicScriptDefinition()
            {
                Script = new DynamicScript()
                {
                    FunctionName = "add",
                    CompileTime = new DynamicScriptCompileTime()
                    {
                        ScriptText = @"
                using System;
                using System.Collections;
                using System.Collections.Generic;
             
                namespace BeimingForce
                {
                    public class Currency
                    {
                        public string add(string a,string b,string z){
												   string c = a + b + z +-`` 123;
                                                   Console.Write(c);
												   return c;}
                    }
                }
",
                    }
                }
            });
            runner.Pretreatment();
            runner.GetCompileErrorMessage().ForEach(x => _testOutputHelper.WriteLine(x));
        }
    }
}