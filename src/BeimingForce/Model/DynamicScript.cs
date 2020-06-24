using System;
using System.Collections.Generic;
using System.Text;
using BeimingForce.Enum;

namespace BeimingForce.Model
{
    public class DynamicScript
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string FunctionName { get; set; }

        public DynamicScriptCompileTime CompileTime { get; set; }

        public DynamicScriptLanguageEnum Language { get; set; } = DynamicScriptLanguageEnum.CSharp;
    }
}