using System;
using System.Collections.Generic;
using System.Text;
using BeimingForce.Enum;

namespace BeimingForce.Model
{
    public class DynamicScriptBase
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 脚本语言
        /// </summary>
        public DynamicScriptLanguageEnum Language { get; set; }
        /// <summary>
        /// 脚本说明
        /// </summary>
        public string ScriptDescription { get; set; }
        /// <summary>
        /// 执行时序
        /// </summary>
        public DynamicScriptSequentialEnum DynamicScriptSequential { get; set; }
        /// <summary>
        /// 线程类型
        /// </summary>
        public DynamicScriptThreadTypeEnum ThreadType { get; set; }
    }
}
