using System.Reflection;
using BeimingForce.Model;

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


        /// <summary>
        /// 执行动态脚本
        /// </summary>
        /// <param name="code">表达式或脚本</param>
        /// <returns>执行结果</returns>
        dynamic Execute(string code);

        /// <summary>
        /// 执行动态脚本
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="code">表达式或脚本</param>
        /// <returns>执行结果</returns>
        T Execute<T>(string code);


        /// <summary>
        /// 检测此代码是否存在编译错误
        /// </summary>
        /// <returns></returns>
        bool HasCompileError(string code, out string errorMessage, string[] nameSpaces = null);


        /// <summary>
        /// 执行脚本中的函数
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="functionName">函数名称</param>
        /// <param name="parameters">传递参数</param>
        /// <returns>执行结果</returns>
        T CallFunction<T>(string functionName, params object[] parameters);

    }
}
