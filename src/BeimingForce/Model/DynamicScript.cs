using System;
using System.Collections.Generic;
using System.Text;

namespace BeimingForce.Model
{
    public class DynamicScript:DynamicScriptBase
    {
        /// <summary>
        /// 脚本内容
        /// </summary>
        public string ScriptText { get; set; }

        /// <summary>
        /// 脚本引用的命名空间
        /// </summary>
        public string[] ScriptReferenceNamespace { get; set; }
    }
}
