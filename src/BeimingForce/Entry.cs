using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BeimingForce.Engine;
using BeimingForce.Enum;
using BeimingForce.Model;

namespace BeimingForce
{
    public class Entry
    {
        /// <summary>
        /// 执行动态脚本
        /// </summary>
        /// <param name="scriptName">脚本名称</param>
        /// <param name="scriptText">脚本内容</param>
        /// <param name="sequential">动态脚本序列</param>
        /// <param name="paramters">入参</param>
        /// <returns></returns>
        public static object RunDynamicScript<T>(string scriptName, string scriptText, DynamicScriptSequentialEnum sequential, object[] paramters)
        {
            DynamicScript dynamicScript = new DynamicScript()
            {
                FunctionName = "add",
                ApplicationName = "BeimingTest",
                CompileTime = new DynamicScriptCompileTime()
                {
                    ScriptText = scriptText
                },
                RunTime = new DynamicScriptRunTime()
                {
                    Language = DynamicScriptLanguageEnum.CSharp,
                    ScriptDescription = "说明",
                    ThreadType = DynamicScriptThreadTypeEnum.Sync,

                }
            };
            return RunningDynamicScript<T>(new List<DynamicScript>() { dynamicScript }, paramters);
        }

        /// <summary>
        /// 执行多个脚本
        /// </summary>
        /// <param name="scriptKey"></param>
        /// <param name="dynamicScripts"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static object RunningDynamicScript<T>(List<DynamicScript> dynamicScripts, params object[] parameters)
        {
            object scriptResult = null;
            foreach (var script in dynamicScripts)
            {
                var runResult = RunningDynamicScript<T>(script, parameters, ref scriptResult);
            }
            return scriptResult;
        }
        /// <summary>
        /// 执行单个脚本
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parameters"></param>
        /// <param name="scriptResult"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        private static bool RunningDynamicScript<T>(DynamicScript script, object[] parameters, ref object scriptResult)
        {
            var watch = new Stopwatch();
            watch.Restart();
            var dynamicScript = CreateScriptEngine.Instance.CreateDynamicScript(script);
            if (!dynamicScript.Compiled) return false;
            scriptResult = dynamicScript.CallFunction<T>(script.FunctionName, parameters);
            watch.Stop();
            return true;
        }
    }
}
