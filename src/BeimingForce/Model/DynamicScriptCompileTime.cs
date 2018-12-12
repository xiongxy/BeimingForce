using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BeimingForce.Model
{
    public class DynamicScriptCompileTime
    {
        /// <summary>
        /// 脚本内容
        /// </summary>
        public string ScriptText { get; set; }
        /// <summary>
        /// 脚本引用的命名空间
        /// </summary>
        public string[] ScriptReferenceNamespace { get; set; }
        /// <summary>
        /// 脚本引用的程序集
        /// </summary>
        public Assembly[] ScriptReferenceAssemblies { get; set; }
        /// <summary>
        /// 脚本引用的程序集
        /// </summary>
        public string[] ScriptReferenceAssemblyNames { get; set; }
    }
}
