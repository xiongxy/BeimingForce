using System.Collections.Generic;
using System.Reflection;
using BeimingForce.Core;
using BeimingForce.Model;
using Microsoft.CodeAnalysis;

namespace BeimingForce.Engine
{
    public interface IDynamicScript
    {
        /// <summary>
        /// 动态脚本引擎语言名称
        /// </summary>
        string Language { get; }

        /// <summary>
        /// 是否有语法或编译错误
        /// </summary>
        bool Compiled { get; }

        List<string> CompileErrorMessage { get; set; }

        /// <summary>
        /// 检测此代码是否存在编译错误
        /// </summary>
        /// <returns></returns>
        bool HasCompileError(string code, out string errorMessage, string[] nameSpaces = null);

        IDynamicScript BuildDynamicScript();

        /// <summary>
        /// 执行脚本中的函数
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="parameters">传递参数</param>
        /// <returns>执行结果</returns>
        T CallFunction<T>(params object[] parameters);

        void SetAssemblyLoadContext(BeimingAssemblyLoadContext context);
        
        void SetMetadataReferences(List<MetadataReference> metadataReferences);
    }
}