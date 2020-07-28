using System.Collections.Generic;
using BeimingForce.Model;

namespace BeimingForce.Core.Runner
{
    public interface IRunner
    {
        /// <summary>
        /// 预处理
        /// </summary>
        /// <returns>IRunner</returns>
        IRunner Pretreatment();

        List<string> GetCompileErrorMessage();
        
        DynamicScriptResult<T> RunDynamicScript<T>(params object[] args);
    }
}