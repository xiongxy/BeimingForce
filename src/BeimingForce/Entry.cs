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
        //public object RunDynamicScript(string scriptName, string scriptText, DynamicScriptSequentialEnum sequential, object[] paramters)
        //{
        //    DynamicScript dynamicScript = new DynamicScript()
        //    {
        //        Language = DynamicScriptLanguageEnum.CSharp,
        //        ScriptText = scriptText,
        //        ScriptDescription = "说明",
        //        ThreadType = DynamicScriptThreadTypeEnum.Sync,
        //        FunctionName = "add"
        //    };
        //}

        /// <summary>
        /// 执行多个脚本
        /// </summary>
        /// <param name="scriptKey"></param>
        /// <param name="dynamicScripts"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal object RunningDynamicScript(string scriptKey, List<DynamicScript> dynamicScripts, params object[] parameters)
        {
            object scriptResult = null;
            foreach (var script in dynamicScripts)
            {
                var runResult = RunningDynamicScript(script, parameters, ref scriptResult, script.FunctionName);
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
        private bool RunningDynamicScript(DynamicScript script, object[] parameters, ref object scriptResult, string functionName = null)
        {
            //var watch = new Stopwatch();
            //watch.Restart();
            //var dynamicScript = CreateScriptEngine.Instance.CreateDynamicScript(script.ApplicationName, script.Language,
            //    script.ScriptText, script.ScriptReferenceNamespace);
            //if (!dynamicScript.Compiled) return false;
            //scriptResult = dynamicScript.CallFunction<object>(functionName, parameters);
            //watch.Stop();
            //dynamicScript.Dispose();
            return true;
        }
    }
}
