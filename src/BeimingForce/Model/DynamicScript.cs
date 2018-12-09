using System;
using System.Collections.Generic;
using System.Text;

namespace BeimingForce.Model
{
    public class DynamicScript
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string FunctionName { get; set; }
        public DynamicScriptCompileTime CompileTime { get; set; }
        public DynamicScriptRunTime RunTime { get; set; }
    }
}
